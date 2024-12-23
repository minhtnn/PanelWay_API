using AutoMapper;
using Net.payOS;
using Net.payOS.Types;
using PanelWay_Backend.API.Configurations;
using PanelWay_Backend.API.Payload.Requests.PayOS;
using PanelWay_Backend.API.Services.Interfaces;
using PanelWay_Backend.Domain.Entities;
using PanelWay_Backend.Repository.Interfaces;

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
    public async Task<CreatePaymentResult> CreateCheckoutLink(CreatePayOSRequest request)
    {
        var payOS = GetPayOS();
        var paymentLinkRequest = new PaymentData
        (
            orderCode: request.OrderCode,
            amount: request.Amount,
            description: request.Description,
            items: request.Items,
            returnUrl: request.ReturnUrl,
            cancelUrl: request.CancelUrl
        );
        var response = await payOS.createPaymentLink(paymentLinkRequest);
        return response;
    }

    public async Task<PaymentLinkInformation> GetPaymentLinkInformation(long orderId)
    {
        var payOS = GetPayOS();
        var response = await payOS.getPaymentLinkInformation(orderId);
        return response;
    }

    public async Task<PaymentLinkInformation> CancelPaymentLink(long orderId)
    {
        var payOS = GetPayOS();
        var response = await payOS.cancelPaymentLink(orderId);
        return response;
    }
}