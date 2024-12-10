using PanelWay_Backend.API.Payload.Requests.Subscriptions;
using PanelWay_Backend.API.Payload.Responses.Subscriptions;

namespace PanelWay_Backend.API.Services.Interfaces;

public interface ISubscriptionService
{
    Task<SubscriptionResponse?> GetSubscriptionById(Guid id);
    Task<ICollection<SubscriptionResponse>> GetSubscriptionList();
    Task<SubscriptionResponse> CreateNewSubscription(CreateSubscriptionRequest request);
    Task<SubscriptionResponse> UpdateSubscription(UpdateSubscriptionRequest request);
}