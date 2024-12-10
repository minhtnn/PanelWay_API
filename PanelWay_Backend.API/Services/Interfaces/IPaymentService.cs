using PanelWay_Backend.API.Payload.Requests.Payments;
using PanelWay_Backend.API.Payload.Responses.Payments;

namespace PanelWay_Backend.API.Services.Interfaces;

public interface IPaymentService
{
    Task<PaymentResponse?> GetPaymentById(Guid id);
    Task<PaymentResponse> CreateNewPayment(CreatePaymentRequest request);
}