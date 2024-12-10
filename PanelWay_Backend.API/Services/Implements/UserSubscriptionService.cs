using AutoMapper;
using PanelWay_Backend.API.Payload.Requests.UserSubscriptions;
using PanelWay_Backend.API.Payload.Responses.UserSubscriptions;
using PanelWay_Backend.API.Services.Interfaces;
using PanelWay_Backend.Domain.Entities;
using PanelWay_Backend.Repository.Interfaces;

namespace PanelWay_Backend.API.Services.Implements;

public class UserSubscriptionService : BaseService<UserSubscriptionService>, IUserSubscriptionService
{
    public UserSubscriptionService(IUnitOfWork<PanelWayDbContext> unitOfWork, ILogger<UserSubscriptionService> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(unitOfWork, logger, mapper, httpContextAccessor)
    {
    }

    public async Task<UserSubscriptionResponse> GetUserSubscriptionById(Guid id)
    {
        var response = await _unitOfWork.GetRepository<UserSubscription>().SingleOrDefaultAsync
        (
            predicate: x => x.Id.Equals(id)
        );
        return (response != null) ? _mapper.Map<UserSubscriptionResponse>(response) : null;
    }

    public async Task<ICollection<UserSubscriptionResponse>> GetUserSubscriptionByAccountId(Guid id)
    {
        var response = await _unitOfWork.GetRepository<UserSubscription>().GetListAsync
        (
            predicate: x => x.AccountId.Equals(id)
        );
        return (response != null) ? _mapper.Map<ICollection<UserSubscriptionResponse>>(response) : null;
    }

    public Task<UserSubscriptionResponse> CreateNewUserSubscription(CreateUserSubscriptionRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<UserSubscriptionResponse> UpdateUserSubscription(UpdateUserSubscriptionRequest request)
    {
        throw new NotImplementedException();
    }
}