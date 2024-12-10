using AutoMapper;
using PanelWay_Backend.API.Payload.Requests.Payments;
using PanelWay_Backend.API.Payload.Responses.Payments;
using PanelWay_Backend.Domain.Entities;

namespace PanelWay_Backend.API.Mappers;

public class PaymentMapper : Profile
{
    public PaymentMapper()
    {
        CreateMap<CreatePaymentRequest, Payment>();
        CreateMap<Payment, PaymentResponse>();
    }
}