using AutoMapper;
using PanelWay_Backend.API.Payload.Requests.UserSubscriptions;
using PanelWay_Backend.API.Payload.Responses.UserSubscriptions;
using PanelWay_Backend.Domain.Entities;

namespace PanelWay_Backend.API.Mappers;

public class UserSubscriptionMapper : Profile
{
    public UserSubscriptionMapper()
    {
        CreateMap<CreateUserSubscriptionRequest, UserSubscription>();
        CreateMap<UpdateUserSubscriptionRequest, UserSubscription>();
        CreateMap<UserSubscription, UserSubscriptionResponse>();
    }
}