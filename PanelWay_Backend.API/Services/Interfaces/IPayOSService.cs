using Net.payOS.Types;
using PanelWay_Backend.API.Payload.Requests.PayOS;

namespace PanelWay_Backend.API.Services.Interfaces;

public interface IPayOSService
{
    Task<CreatePaymentResult?> CreateCheckoutLink(CreatePayOSRequest request);
    Task<PaymentLinkInformation?> GetPaymentLinkInformation(long orderId);
    Task<PaymentLinkInformation?> CancelPaymentLink(long orderId);
}