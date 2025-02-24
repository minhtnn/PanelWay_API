using Microsoft.AspNetCore.Mvc;
using PanelWay_Backend.API.Constants;
using PanelWay_Backend.API.Enums;
using PanelWay_Backend.API.Payload.Requests.UserSubscriptions;
using PanelWay_Backend.API.Payload.Responses.UserSubscriptions;
using PanelWay_Backend.API.Services.Interfaces;
using PanelWay_Backend.API.Validators;

namespace PanelWay_Backend.API.Controllers;

public class UserSubscriptionController : BaseController<UserSubscriptionController>
{
    private readonly IUserSubscriptionService _userSubscriptionService;
    public UserSubscriptionController(ILogger<UserSubscriptionController> logger, IUserSubscriptionService userSubscriptionService) : base(logger)
    {
        _userSubscriptionService = userSubscriptionService;
    }
    
    [CustomAuthorize(RoleEnum.Admin, RoleEnum.Manager,RoleEnum.AdvertisingClient, RoleEnum.SpaceProvider)]
    [HttpGet(ApiEndpointConstant.UserSubscription.FindUserSubscriptionByIdApiEndpoint)]
    [ProducesResponseType(typeof(UserSubscriptionResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetUserSubscriptionById(Guid id)
    {
        var responses = await _userSubscriptionService.GetUserSubscriptionById(id);
        return (responses != null) ? Ok(responses) : NotFound(new {Message = MessageConstant.Transaction.NotFindTransaction});
    }
    
    [CustomAuthorize(RoleEnum.Admin, RoleEnum.Manager, RoleEnum.AdvertisingClient, RoleEnum.SpaceProvider)]
    [HttpGet(ApiEndpointConstant.UserSubscription.FindUserSubscriptionByAccountIdApiEndpoint)]
    [ProducesResponseType(typeof(UserSubscriptionResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetUserSubscriptionByAccountId(Guid id)
    {
        var responses = await _userSubscriptionService.GetUserSubscriptionByAccountId(id);
        return (responses != null) ? Ok(responses) : NotFound(new {Message = MessageConstant.Transaction.NotFindTransaction});
    }
    
    [CustomAuthorize(RoleEnum.AdvertisingClient, RoleEnum.SpaceProvider)]
    [HttpPost(ApiEndpointConstant.UserSubscription.UserSubscriptionApiEndpoint)]
    [ProducesResponseType(typeof(UserSubscriptionResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateNewUserSubcription(CreateUserSubscriptionRequest request)
    {
        var responses = await _userSubscriptionService.CreateNewUserSubscription(request);
        return (responses != null) ? Ok(responses) : NotFound(new {Message = MessageConstant.UserSubscription.CreateUserSubscriptionFail});
    }
    
    [CustomAuthorize(RoleEnum.Admin, RoleEnum.Manager)]
    [HttpGet(ApiEndpointConstant.UserSubscription.PurchasingVolumeApiEndpoint)]
    [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetPurchasingVolume(string status, DateTime startDate, DateTime endDate)
    {
        var responses = await _userSubscriptionService.GetPurchasingVolume(status, startDate, endDate);
        return (responses != null) ? Ok(responses) : NotFound(new {Message = MessageConstant.UserSubscription.CreateUserSubscriptionFail});
    }
}