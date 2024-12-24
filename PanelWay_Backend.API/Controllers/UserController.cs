using Microsoft.AspNetCore.Mvc;
using PanelWay_Backend.API.Constants;
using PanelWay_Backend.API.Enums;
using PanelWay_Backend.API.Payload.Responses.Users;
using PanelWay_Backend.API.Services.Interfaces;
using PanelWay_Backend.API.Validators;

namespace PanelWay_Backend.API.Controllers;

public class UserController : BaseController<UserController>
{
    private readonly IUserService _userService;
    public UserController(ILogger<UserController> logger, IUserService userService) : base(logger)
    {
        _userService = userService;
    }
    
    [CustomAuthorize(RoleEnum.Admin, RoleEnum.Manager)]
    [HttpGet(ApiEndpointConstant.User.FindUserByIdApiEndpoint)]
    [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetUserById(Guid id)
    {
        var responses = await _userService.GetUserById(id);
        return (responses != null) ? Ok(responses) : NotFound(new {Message = MessageConstant.User.NotFindUser});
    }
}