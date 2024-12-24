using Microsoft.AspNetCore.Mvc;
using PanelWay_Backend.API.Constants;
using PanelWay_Backend.API.Payload;
using PanelWay_Backend.API.Payload.Requests.Authentication;
using PanelWay_Backend.API.Services;
using PanelWay_Backend.API.Services.Interfaces;

namespace PanelWay_Backend.API.Controllers;

public class AuthenticationController : BaseController<AuthenticationController>
{
    private readonly IAuthenticationService _authenticationService;
    public AuthenticationController(ILogger<AuthenticationController> logger, IAuthenticationService authenticationService) : base(logger)
    {
        _authenticationService = authenticationService;
    }
    [HttpPost(ApiEndpointConstant.Authentication.Login)]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        try
        {
            var token = await _authenticationService.Login(request);
            if (token == null)
            {
                return Unauthorized(new ErrorResponse()
                {
                    StatusCode = StatusCodes.Status401Unauthorized,
                    Error = MessageConstant.Authentication.InvalidUsernameOrPassword,
                    TimeStamp = DateTime.UtcNow
                });
            }
            return Ok(token);
        }
        catch (Exception e)
        {
            return StatusCode(500, new { Message = e.Message });
        }
    }
}