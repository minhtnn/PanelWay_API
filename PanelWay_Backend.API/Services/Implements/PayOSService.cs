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
    private readonly int MAX_EXIST_QR = 180;
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
            expiredAt:DateTimeOffset.UtcNow.ToUnixTimeSeconds() + MAX_EXIST_QR 
        );
        var paymentTypeId = await _unitOfWork.GetRepository<PaymentType>().SingleOrDefaultAsync(
                selector: x => x.Id,
                predicate: x => x.Code.Equals("CKNH")
            );
        
        var response = await payOS.createPaymentLink(paymentLinkRequest);
        var newPayment = new Payment
        {
            Id = Guid.NewGuid(),
            PaymentTypeId = paymentTypeId,
            PayOsOrderCode = response.orderCode,
            CreatedAt = DateTime.UtcNow,
            Status = response.status,
            Details = response.description
        };
        await _unitOfWork.GetRepository<Payment>().InsertAsync(newPayment);
        var newTransaction = new Transaction
        {
            Id = Guid.NewGuid(),
            SubscriptionId = Guid.Parse(request.SubcriptionId),
            AccountId = Guid.Parse(request.AccountId),
            PaymentId = newPayment.Id,
            Status = newPayment.Status,
            Amount = request.Amount,
        };
        await _unitOfWork.GetRepository<Transaction>().InsertAsync(newTransaction);
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
                    var existingTransaction = await _unitOfWork.GetRepository<Transaction>().SingleOrDefaultAsync(
                        predicate: x => x.PaymentId == payment.Id
                    );
                    existingTransaction.Status = payment.Status;
                    existingTransaction.TransactionDate =
                        DateTime.Parse(transactions[0].transactionDateTime).ToUniversalTime();
                    _unitOfWork.GetRepository<Transaction>().UpdateAsync(existingTransaction);
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