using AutoMapper;
using PanelWay_Backend.API.Payload.Requests.Subscriptions;
using PanelWay_Backend.API.Payload.Responses.Subscriptions;
using PanelWay_Backend.Domain.Entities;

namespace PanelWay_Backend.API.Mappers;

public class SubscriptionMapper : Profile
{
    public SubscriptionMapper()
    {
        CreateMap<CreateSubscriptionRequest, Subscription>();
        CreateMap<UpdateSubscriptionRequest, Subscription>();
        CreateMap<Subscription, SubscriptionResponse>();
    }
}