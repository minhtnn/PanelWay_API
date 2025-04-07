using PanelWay_Backend.API.Payload.Requests.UserSubscriptions;
using PanelWay_Backend.API.Payload.Responses.UserSubscriptions;
using PanelWay_Backend.Domain.Paginate;

namespace PanelWay_Backend.API.Services.Interfaces;

public interface IUserSubscriptionService
{
    Task<UserSubscriptionResponse> GetUserSubscriptionById(Guid id);
    Task<ICollection<UserSubscriptionResponse>> GetUserSubscriptionByAccountId(Guid id, string status);
    Task<IPaginate<UserSubscriptionResponse>> GetUserSubscriptionBySubscriptionId(int size, int page,Guid id, string status);
    Task<UserSubscriptionResponse?> CreateNewUserSubscription(CreateUserSubscriptionRequest request);
    Task<UserSubscriptionResponse> UpdateUserSubscription(UpdateUserSubscriptionRequest request);
    Task<int> GetPurchasingVolume(string status, DateTime? startDate, DateTime? endDate);
}