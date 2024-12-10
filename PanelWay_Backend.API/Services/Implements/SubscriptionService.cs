using AutoMapper;
using PanelWay_Backend.API.Payload.Requests.Subscriptions;
using PanelWay_Backend.API.Payload.Responses.Subscriptions;
using PanelWay_Backend.API.Services.Interfaces;
using PanelWay_Backend.Domain.Entities;
using PanelWay_Backend.Repository.Interfaces;

namespace PanelWay_Backend.API.Services.Implements;

public class SubscriptionService : BaseService<SubscriptionService>, ISubscriptionService
{
    public SubscriptionService(IUnitOfWork<PanelWayDbContext> unitOfWork, ILogger<SubscriptionService> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(unitOfWork, logger, mapper, httpContextAccessor)
    {
    }

    public async Task<SubscriptionResponse?> GetSubscriptionById(Guid id)
    {
        var response = await _unitOfWork.GetRepository<Subscription>().SingleOrDefaultAsync
            (
                predicate: x => x.Id.Equals(id)
                );
        return (response != null) ? _mapper.Map<SubscriptionResponse>(response) : null;
    }

    public async Task<ICollection<SubscriptionResponse>> GetSubscriptionList()
    {
        var responses = await _unitOfWork.GetRepository<Subscription>().GetListAsync();
        return _mapper.Map<ICollection<SubscriptionResponse>>(responses);
    }

    public Task<SubscriptionResponse> CreateNewSubscription(CreateSubscriptionRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<SubscriptionResponse> UpdateSubscription(UpdateSubscriptionRequest request)
    {
        throw new NotImplementedException();
    }
}