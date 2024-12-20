using Microsoft.AspNetCore.Mvc;
using PanelWay_Backend.API.Constants;
using PanelWay_Backend.API.Payload.Requests.UserSubscriptions;
using PanelWay_Backend.API.Payload.Responses.UserSubscriptions;
using PanelWay_Backend.API.Services.Interfaces;

namespace PanelWay_Backend.API.Controllers;

public class UserSubscriptionController : BaseController<UserSubscriptionController>
{
    private readonly IUserSubscriptionService _userSubscriptionService;
    public UserSubscriptionController(ILogger<UserSubscriptionController> logger, IUserSubscriptionService userSubscriptionService) : base(logger)
    {
        _userSubscriptionService = userSubscriptionService;
    }
    [HttpGet(ApiEndpointConstant.UserSubscription.FindUserSubscriptionByIdApiEndpoint)]
    [ProducesResponseType(typeof(UserSubscriptionResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetUserSubscriptionById(Guid id)
    {
        var responses = await _userSubscriptionService.GetUserSubscriptionById(id);
        return (responses != null) ? Ok(responses) : NotFound(new {Message = MessageConstant.Transaction.NotFindTransaction});
    }
    [HttpGet(ApiEndpointConstant.UserSubscription.FindUserSubscriptionByAccountIdApiEndpoint)]
    [ProducesResponseType(typeof(UserSubscriptionResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetUserSubscriptionByAccountId(Guid id)
    {
        var responses = await _userSubscriptionService.GetUserSubscriptionByAccountId(id);
        return (responses != null) ? Ok(responses) : NotFound(new {Message = MessageConstant.Transaction.NotFindTransaction});
    }
    
    [HttpPost(ApiEndpointConstant.UserSubscription.UserSubscriptionApiEndpoint)]
    [ProducesResponseType(typeof(UserSubscriptionResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateNewUserSubcription(CreateUserSubscriptionRequest request)
    {
        var responses = await _userSubscriptionService.CreateNewUserSubscription(request);
        return (responses != null) ? Ok(responses) : NotFound(new {Message = MessageConstant.UserSubscription.CreateUserSubscriptionFail});
    }
}