using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using PanelWay_Backend.API.Configurations;
using PanelWay_Backend.API.Constants;
using PanelWay_Backend.API.Payload;
using PanelWay_Backend.API.Payload.Requests.Authentication;
using PanelWay_Backend.API.Payload.Requests.Firebase;
using PanelWay_Backend.API.Payload.Responses.Authentication;
using PanelWay_Backend.API.Services.Interfaces;

namespace PanelWay_Backend.API.Controllers;

// [EnableCors(CorsConfig.PolicyName)]
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
    
    
    
    [HttpGet(ApiEndpointConstant.Firebase.FirebaseGetUser)]
    public async Task<IActionResult> GetUser([FromBody] VerifyTokenRequest request)
    {
        try
        {
            var userRecord = await _authenticationService.GetUser(request);
            return (userRecord.Data != null)? Ok(userRecord) : NotFound(MessageConstant.User.NotFindUser);
        }
        catch (Exception ex)
        {
            return NotFound(new
            {
                Message = MessageConstant.User.NotFindUser,
                Error = ex.Message
            });
        }
    }

    [HttpPost(ApiEndpointConstant.Authentication.SignUp)]
    public async Task<IActionResult> SignUp([FromBody] SignUpRequest request)
    {
        var response = await _authenticationService.SignUpForCustomer(request);
        if (response == null)
        {
            return Unauthorized(new ErrorResponse()
            {
                StatusCode = StatusCodes.Status401Unauthorized,
                Error = MessageConstant.Authentication.InvalidUsernameOrPassword,
                TimeStamp = DateTime.UtcNow
            });
        }
        return Ok(response);
    }
    
    // [HttpPost(ApiEndpointConstant.Authentication.SignUpUltra)]
    // public async Task<IActionResult> SignUpUltra([FromBody] List<SignUpRequest> requests)
    // {
    //     var responses = new List<AuthenticationResponse>();
    //     foreach (var request in requests)
    //     {
    //         var response = await _authenticationService.SignUpForCustomer(request);
    //         if (response != null)
    //         {
    //             responses.Add(response);
    //         }
    //     }
    //     
    //     if (responses == null)
    //     {
    //         return Unauthorized(new ErrorResponse()
    //         {
    //             StatusCode = StatusCodes.Status401Unauthorized,
    //             Error = MessageConstant.Authentication.InvalidUsernameOrPassword,
    //             TimeStamp = DateTime.UtcNow
    //         });
    //     }
    //     return Ok(responses);
    // }
    
    [HttpPost(ApiEndpointConstant.Authentication.UpdatePassword)]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
    {
        var response = await _authenticationService.ChangePasswordForCustomer(request);
        if (response == false)
        {
            return Unauthorized(new ErrorResponse()
            {
                StatusCode = StatusCodes.Status401Unauthorized,
                Error = MessageConstant.Authentication.InvalidUsernameOrPassword,
                TimeStamp = DateTime.UtcNow
            });
        }
        return Ok(response);
    }
    
    [HttpPost(ApiEndpointConstant.Firebase.FirebaseSaveUser)]
    public async Task<IActionResult> SaveUser([FromBody] AuthenticationRequest request)
    {
        var response = await _authenticationService.SaveNewUser(request);
        return (response.Data != null)? Ok(response) : NotFound(MessageConstant.User.NotFindUser);
    }
}