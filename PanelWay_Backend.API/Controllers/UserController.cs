using Microsoft.AspNetCore.Mvc;
using PanelWay_Backend.API.Constants;
using PanelWay_Backend.API.Enums;
using PanelWay_Backend.API.Payload.Responses.Users;
using PanelWay_Backend.API.Services.Interfaces;
using PanelWay_Backend.API.Validators;
using PanelWay_Backend.Domain.Paginate;

namespace PanelWay_Backend.API.Controllers;

public class UserController : BaseController<UserController>
{
    private readonly IUserService _userService;
    public UserController(ILogger<UserController> logger, IUserService userService) : base(logger)
    {
        _userService = userService;
    }
    
    [CustomAuthorize(RoleEnum.Admin, RoleEnum.Manager)]
    [HttpGet(ApiEndpointConstant.User.UserApiEndpoint)]
    [ProducesResponseType(typeof(IPaginate<UserResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetUsers( [FromQuery] int page = 1, [FromQuery] int size = 10)
    {
        var responses = await _userService.GetUsers(page,size);
        return (responses != null) ? Ok(responses) : NotFound(new {Message = MessageConstant.User.NotFindUser});
    }
    
    [CustomAuthorize(RoleEnum.Admin, RoleEnum.Manager)]
    [HttpGet(ApiEndpointConstant.User.FindUserByIdApiEndpoint)]
    [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetUserById(Guid id)
    {
        var responses = await _userService.GetUserById(id);
        return (responses != null) ? Ok(responses) : NotFound(new {Message = MessageConstant.User.NotFindUser});
    }
    
    [CustomAuthorize(RoleEnum.Admin, RoleEnum.Manager)]
    [HttpGet(ApiEndpointConstant.User.UserTotalByAgeApiEndpoint)]
    [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetTotalUserByAge(int minAge, int maxAge)
    {
        var responses = await _userService.GetTotalUserByAge(minAge, maxAge);
        return (responses != null) ? Ok(responses) : NotFound(new {Message = MessageConstant.User.NotFindUser});
    }
    
    [CustomAuthorize(RoleEnum.Admin, RoleEnum.Manager)]
    [HttpGet(ApiEndpointConstant.User.UserTotalApiEndpoint)]
    [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetTotalUser()
    {
        var responses = await _userService.GetTotalUser();
        return (responses != null) ? Ok(responses) : NotFound(new {Message = MessageConstant.User.NotFindUser});
    }
    
    [CustomAuthorize(RoleEnum.Admin, RoleEnum.Manager)]
    [HttpGet(ApiEndpointConstant.User.UserTotalByDateApiEndpoint)]
    [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetNewTotalUserByDate(DateTime startDate, DateTime endDate)
    {
        var responses = await _userService.GetTotalUserByDate(startDate, endDate);
        return (responses != null) ? Ok(responses) : NotFound(new {Message = MessageConstant.User.NotFindUser});
    }
}