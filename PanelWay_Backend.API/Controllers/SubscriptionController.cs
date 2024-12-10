using Microsoft.AspNetCore.Mvc;
using PanelWay_Backend.API.Constants;
using PanelWay_Backend.API.Payload.Responses.Subscriptions;
using PanelWay_Backend.API.Services.Interfaces;

namespace PanelWay_Backend.API.Controllers;

public class SubscriptionController : BaseController<SubscriptionController>
{
    private readonly ISubscriptionService _subscriptionService;
    public SubscriptionController(ILogger<SubscriptionController> logger, ISubscriptionService subscriptionService) : base(logger)
    {
        _subscriptionService = subscriptionService;
    }
    
    [HttpGet(ApiEndpointConstant.Subscription.SubscriptionApiEndpoint)]
    [ProducesResponseType(typeof(ICollection<SubscriptionResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetSubscriptions()
    {
        var responses = await _subscriptionService.GetSubscriptionList();
        return (responses != null) ? Ok(responses) : NotFound(new {Message = MessageConstant.PanelWaySystem.SystemError});
    }
    
    [HttpGet(ApiEndpointConstant.Subscription.FindSubscriptionByIdApiEndpoint)]
    [ProducesResponseType(typeof(SubscriptionResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetSubscriptionById(Guid id)
    {
        var responses = await _subscriptionService.GetSubscriptionById(id);
        return (responses != null) ? Ok(responses) : NotFound(new {Message = MessageConstant.Subscription.NotFindSubscription});
    }
}