using Microsoft.AspNetCore.Mvc;
using PanelWay_Backend.API.Constants;
using PanelWay_Backend.API.Enums;
using PanelWay_Backend.API.Payload.Responses.Accounts;
using PanelWay_Backend.API.Services.Interfaces;
using PanelWay_Backend.API.Validators;
using PanelWay_Backend.Domain.Paginate;

namespace PanelWay_Backend.API.Controllers;

public class AccountController : BaseController<AccountController>
{
    private readonly IAccountService _accountService;
    public AccountController(ILogger<AccountController> logger, IAccountService accountService) : base(logger)
    {
        _accountService = accountService;
    }
    
    [CustomAuthorize(RoleEnum.Admin, RoleEnum.Manager)]
    [HttpGet(ApiEndpointConstant.Account.AccountApiEndpoint)]
    [ProducesResponseType(typeof(IPaginate<AccountResponse>), StatusCodes.Status200OK)] 
    public async Task<IActionResult> GetAccounts([FromQuery] int size = 10, [FromQuery] int page = 1) 
    { 
        var response = await _accountService.GetAccountsPaging(size, page); 
        return (response != null)? Ok(response) : NotFound(new {Message = MessageConstant.Account.NotFindAccount});
    }
    
    [CustomAuthorize(RoleEnum.Admin, RoleEnum.Manager, RoleEnum.AdvertisingClient, RoleEnum.SpaceProvider)]
    [HttpGet(ApiEndpointConstant.Account.FindAccountByIdApiEndpoint)]
    [ProducesResponseType(typeof(AccountResponse), StatusCodes.Status200OK)] 
    public async Task<IActionResult> GetAccountById(Guid id) 
    { 
        var response = await _accountService.GetAccountById(id); 
        return (response != null)? Ok(response) : NotFound(new {Message = MessageConstant.Account.NotFindAccount});
    }
    [CustomAuthorize(RoleEnum.Admin, RoleEnum.Manager, RoleEnum.AdvertisingClient, RoleEnum.SpaceProvider)]
    [HttpGet(ApiEndpointConstant.Account.FindAccountByUserIdApiEndpoint)]
    [ProducesResponseType(typeof(AccountResponse), StatusCodes.Status200OK)] 
    public async Task<IActionResult> GetAccountByUserId(Guid id, string role) 
    { 
        var response = await _accountService.GetAccountByUserId(id, role); 
        return (response != null)? Ok(response) : NotFound(new {Message = MessageConstant.Account.NotFindAccount});
    }
}