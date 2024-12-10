using AutoMapper;
using PanelWay_Backend.API.Payload.Requests.Payments;
using PanelWay_Backend.API.Payload.Requests.PaymentTypes;
using PanelWay_Backend.API.Payload.Responses.PaymentTypes;
using PanelWay_Backend.Domain.Entities;

namespace PanelWay_Backend.API.Mappers;

public class PaymentTypeMapper : Profile
{
    public PaymentTypeMapper()
    {
        CreateMap<CreatePaymentRequest, PaymentType>();
        CreateMap<UpdatePaymentTypeRequest, PaymentType>();
        CreateMap<PaymentType, PaymentTypeResponse>();
    }
}