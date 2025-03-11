using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PanelWay_Backend.API.Constants;
using PanelWay_Backend.API.Enums;
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

    public async Task<ICollection<UserSubscriptionResponse>> GetUserSubscriptionByAccountId(Guid id, string status)
    {
        var response = await _unitOfWork.GetRepository<UserSubscription>().GetListAsync
        (
            predicate: x => x.AccountId.Equals(id) && x.Status.Equals(status)
        );
        return (response != null) ? _mapper.Map<ICollection<UserSubscriptionResponse>>(response) : null;
    }

    public async Task<UserSubscriptionResponse?> CreateNewUserSubscription(CreateUserSubscriptionRequest request)
    {
        //Check if account exists
        var account = await _unitOfWork.GetRepository<Account>().SingleOrDefaultAsync
        (
            selector: x => x.Id,
            predicate: x => x.Id.Equals(request.AccountId)
                );
        if (account.Equals(Guid.Empty) || account == null) throw new BadHttpRequestException(MessageConstant.Account.NotFindAccount);
        //Check if subscription exists
        var subcription = await _unitOfWork.GetRepository<Subscription>().SingleOrDefaultAsync
            (
                predicate: x => x.Id.Equals(request.SubscriptionId)
            );
        if (subcription == null) throw new BadHttpRequestException(MessageConstant.Subscription.NotFindSubscription);
        //Check if user registers the subscription before
        var userSubcription = await _unitOfWork.GetRepository<UserSubscription>().SingleOrDefaultAsync
            (
                predicate: x => x.AccountId.Equals(request.AccountId) &&
                                x.SubscriptionId.Equals(request.SubscriptionId) &&
                                !x.Status.Equals(nameof(UserSubcriptionStatusEnum.Inactive)) &&
                                x.EndDate >= (DateTime.UtcNow)
                );
        if (userSubcription != null) throw new BadHttpRequestException(MessageConstant.UserSubscription.ExistUserSubscription);
        
        //Check if user registers the lower subscription
        var lowerUserSubcription = await _unitOfWork.GetRepository<UserSubscription>().SingleOrDefaultAsync
        (
            predicate: x => x.AccountId.Equals(request.AccountId) &&
                            x.Subscription.Priority < subcription.Priority &&
                            !x.Status.Equals(nameof(UserSubcriptionStatusEnum.Inactive)) &&
                            x.EndDate >= (DateTime.UtcNow)
        );
        if (lowerUserSubcription != null )throw new BadHttpRequestException(MessageConstant.UserSubscription.RegisterUserSubscriptionFail);
        request.GetNewPrimaryKey(Guid.NewGuid());
        request.SetEndDate(subcription.Duration);
        var newUserSubcription = _mapper.Map<UserSubscription>(request);
        await _unitOfWork.GetRepository<UserSubscription>().InsertAsync(newUserSubcription!);
        var prevSubcription = await _unitOfWork.GetRepository<UserSubscription>().SingleOrDefaultAsync(
                predicate: x => x.AccountId.Equals(request.AccountId)
            );
        if (prevSubcription != null)
        {
            prevSubcription.Status = nameof(UserSubcriptionStatusEnum.Inactive);
            _unitOfWork.GetRepository<UserSubscription>().UpdateAsync(prevSubcription);
        }
        var check = (await _unitOfWork.CommitAsync()) > 0;
        return check ? _mapper.Map<UserSubscriptionResponse>(newUserSubcription) : null;
    }

    public Task<UserSubscriptionResponse> UpdateUserSubscription(UpdateUserSubscriptionRequest request)
    {
        throw new NotImplementedException();
    }

    public async Task<int> GetPurchasingVolume(string status, DateTime? startDate, DateTime? endDate)
    {
        var response = await _unitOfWork.GetRepository<UserSubscription>().CountAsync(
            predicate: x =>
                (string.IsNullOrEmpty(status) || x.Status.Equals(status)) &&
                (!startDate.HasValue || x.StartDate > startDate.Value) &&
                (!endDate.HasValue || x.StartDate <= endDate.Value)
        );
        return response;
    }


    private double? CalculatePackagePriceIfUpgrade(DateTime startDate, DateTime endDate, Subscription oldSubscription, Subscription newSubscription)
    {
        DateTime now = DateTime.UtcNow;
        //Check cannot downgrade subcription
        if (oldSubscription.Priority >= newSubscription.Priority) throw new BadHttpRequestException(MessageConstant.UserSubscription.RegisterUserSubscriptionFail);
        //Check if the old subscription is expired
        if ((startDate <= now) && (now <= endDate))
        {
            TimeSpan difference = endDate - now;
            double totalDays = difference.Days;
            double? packageCostRemain = oldSubscription.Price * totalDays / 30;
            return newSubscription.Price - packageCostRemain;
        }
        else
        {
            return newSubscription.Price;
        }
    }
}