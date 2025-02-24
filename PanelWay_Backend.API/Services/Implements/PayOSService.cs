using AutoMapper;
using Net.payOS;
using Net.payOS.Types;
using PanelWay_Backend.API.Configurations;
using PanelWay_Backend.API.Enums;
using PanelWay_Backend.API.Payload.Requests.PayOS;
using PanelWay_Backend.API.Services.Interfaces;
using PanelWay_Backend.Domain.Entities;
using PanelWay_Backend.Repository.Interfaces;
using Transaction = PanelWay_Backend.Domain.Entities.Transaction;

namespace PanelWay_Backend.API.Services.Implements;

public class PayOSService : BaseService<PayOSService>, IPayOSService
{
    public PayOSService(IUnitOfWork<PanelWayDbContext> unitOfWork, ILogger<PayOSService> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(unitOfWork, logger, mapper, httpContextAccessor)
    {
    }
    
    private static PayOsConfig GetPayOs(IConfiguration configuration)
    {
        var clientId = configuration.GetValue<string>("PayOS:ClientId");
        var apiKey = configuration.GetValue<string>("PayOS:ApiKey");
        var checksumKey = configuration.GetValue<string>("PayOS:ChecksumKey");
        return new PayOsConfig()
        {
            ClientID = clientId,
            ApiKey = apiKey,
            ChecksumKey = checksumKey
        };
    }

    private static PayOS GetPayOS()
    {
        IConfiguration configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true).Build();
        var payOsConfig = GetPayOs(configuration);
        return new PayOS(payOsConfig.ClientID!, payOsConfig.ApiKey!, payOsConfig.ChecksumKey!);
    }
    public async Task<CreatePaymentResult?> CreateCheckoutLink(CreatePayOSRequest request)
    {
        var payOS = GetPayOS();
        var paymentLinkRequest = new PaymentData
        (
            orderCode: request.OrderCode,
            amount: request.Amount,
            description: request.Description,
            items: request.Items,
            returnUrl: request.ReturnUrl,
            cancelUrl: request.CancelUrl,
            expiredAt:DateTimeOffset.UtcNow.ToUnixTimeSeconds() + 60 
        );
        var paymentTypeId = await _unitOfWork.GetRepository<PaymentType>().SingleOrDefaultAsync(
                selector: x => x.Id,
                predicate: x => x.Code.Equals("CKNH")
            );
        
        var response = await payOS.createPaymentLink(paymentLinkRequest);
        await _unitOfWork.GetRepository<Payment>().InsertAsync(new Payment
        {
            Id = Guid.NewGuid(),
            PaymentTypeId = paymentTypeId,
            PayOsOrderCode = response.orderCode,
            CreatedAt = DateTime.UtcNow,
            Status = response.status,
            Details = response.description
        });
        var isCreateSuccess = await _unitOfWork.CommitAsync() > 0;
        return isCreateSuccess? response : null;
    }

    public async Task<PaymentLinkInformation> GetPaymentLinkInformation(long orderId)
    {
        try
        {
            var payOS = GetPayOS();
            var response = await payOS.getPaymentLinkInformation(orderId);
            if (!response.status.Equals(nameof(PayOSStatusEnum.PENDING)))
            {
                var payment = await _unitOfWork.GetRepository<Payment>().SingleOrDefaultAsync(
                    predicate: x => x.PayOsOrderCode.ToString().Equals(orderId.ToString())
                );
                if (payment!= null && !payment.Status.Equals(nameof(PayOSStatusEnum.PAID)))
                {
                    payment.Status = response.status;
                    _unitOfWork.GetRepository<Payment>().UpdateAsync(payment);
                    var transactions = response.transactions;
                    foreach (var item in transactions)
                    {
                        var newTransaction = new Transaction
                        {
                            Id = Guid.NewGuid(),
                            SubscriptionId = Guid.Parse(item.reference),
                            PaymentId = payment.Id,
                            Status = payment.Status,
                            Amount = item.amount,
                            TransactionDate = (response.status == nameof(PayOSStatusEnum.PAID))
                                ? DateTime.Parse(response.createdAt)
                                : DateTime.Parse(response.canceledAt),

                        };
                        await _unitOfWork.GetRepository<Transaction>().InsertAsync(newTransaction);
                    }
                    var isUpdateSuccess = await _unitOfWork.CommitAsync();
                }
            }
            return response;
        }
        catch (Exception e)
        {
            throw new BadHttpRequestException(e.Message);
        }
        
    }

    public async Task<PaymentLinkInformation> CancelPaymentLink(long orderId)
    {
        var payOS = GetPayOS();
        var response = await payOS.cancelPaymentLink(orderId);
        if (response.status.Equals(nameof(PayOSStatusEnum.CANCELLED)))
        {
            var payment = await _unitOfWork.GetRepository<Payment>().SingleOrDefaultAsync(
                predicate: x => x.PayOsOrderCode.Equals(response.orderCode)
            );
            if (!payment.Status.Equals(nameof(PayOSStatusEnum.CANCELLED)) && !payment.Status.Equals(nameof(PayOSStatusEnum.PAID)))
            {
                payment.Status = response.status;
                _unitOfWork.GetRepository<Payment>().UpdateAsync(payment);
                var isUpdateSuccess = await _unitOfWork.CommitAsync() > 0;
            }
        }
        return response;
    }
}