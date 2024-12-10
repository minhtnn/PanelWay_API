using Microsoft.AspNetCore.Mvc;
using PanelWay_Backend.API.Constants;

namespace PanelWay_Backend.API.Controllers;
[Route(ApiEndpointConstant.ApiEndpoint)]
[ApiController]
public class BaseController<T> : ControllerBase where T : BaseController<T>
{
    protected ILogger<T> _logger;

    public BaseController(ILogger<T> logger)
    {
        _logger = logger;
    }
}