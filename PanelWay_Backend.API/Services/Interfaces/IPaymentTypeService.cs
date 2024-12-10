using PanelWay_Backend.API.Payload.Requests.PaymentTypes;
using PanelWay_Backend.API.Payload.Responses.PaymentTypes;

namespace PanelWay_Backend.API.Services.Interfaces;

public interface IPaymentTypeService
{
    Task<ICollection<PaymentTypeResponse>> GetPaymentTypeList();
    Task<PaymentTypeResponse?> UpdatePaymentType(UpdatePaymentTypeRequest request);
}