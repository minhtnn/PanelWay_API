using Microsoft.AspNetCore.Mvc;
using PanelWay_Backend.API.Constants;
using PanelWay_Backend.API.Enums;
using PanelWay_Backend.API.Payload.Responses.Transactions;
using PanelWay_Backend.API.Services.Interfaces;
using PanelWay_Backend.API.Validators;
using PanelWay_Backend.Domain.Paginate;

namespace PanelWay_Backend.API.Controllers;

public class TransactionController : BaseController<TransactionController>
{
    private readonly ITransactionService _transactionService;
    public TransactionController(ILogger<TransactionController> logger, ITransactionService transactionService) : base(logger)
    {
        _transactionService = transactionService;
    }

    [CustomAuthorize(RoleEnum.Admin, RoleEnum.Manager)]
    [HttpGet(ApiEndpointConstant.Transaction.TransactionApiEndpoint)]
    [ProducesResponseType(typeof(IPaginate<TransactionResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetTransactionPaging(string? status, [FromQuery] int page = 1,
        [FromQuery] int size = 10)
    {
        var responses = await _transactionService.GetTransactionPaging(status, page, size);
        return (responses != null) ? Ok(responses) : StatusCode(500, new {Message = MessageConstant.PanelWaySystem.SystemError});
    }

    [CustomAuthorize(RoleEnum.Admin, RoleEnum.AdvertisingClient, RoleEnum.SpaceProvider)]
    [HttpGet(ApiEndpointConstant.Transaction.FindTransactionByAccountIdApiEndpoint)]
    [ProducesResponseType(typeof(IPaginate<TransactionResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetTransactionByAccountId(Guid id,[FromQuery] int page = 1,[FromQuery] int size = 10)
    {
        var responses = await _transactionService.GetTransactionByAccountId(id, page, size);
        return (responses != null) ? Ok(responses) : NotFound(new {Message = MessageConstant.Transaction.NotFindTransaction});
    }
    
    [CustomAuthorize(RoleEnum.Admin, RoleEnum.AdvertisingClient, RoleEnum.SpaceProvider)]
    [HttpGet(ApiEndpointConstant.Transaction.FindTransactionByUserSubscriptionIdAndPaymentIdApiEndpoint)]
    [ProducesResponseType(typeof(IPaginate<TransactionResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetTransactionByUserSubscriptionIdAndPaymentIdGetTransactionByUserSubscriptionIdAndPaymentId
        (Guid userSubscriptionId, Guid paymentId,[FromQuery] int page = 1,[FromQuery] int size = 10)
    {
        var responses = await _transactionService.GetTransactionByUserSubscriptionIdAndPaymentId
            (userSubscriptionId, paymentId, page, size);
        return (responses != null) ? Ok(responses) : NotFound(new {Message = MessageConstant.Transaction.NotFindTransaction});
    }
    
    [CustomAuthorize(RoleEnum.Admin, RoleEnum.AdvertisingClient, RoleEnum.SpaceProvider)]
    [HttpGet(ApiEndpointConstant.Transaction.FindTransactionByIdApiEndpoint)]
    [ProducesResponseType(typeof(TransactionResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetTransactionById(Guid id)
    {
        var responses = await _transactionService.GetTransactionById(id);
        return (responses != null) ? Ok(responses) : NotFound(new {Message = MessageConstant.Transaction.NotFindTransaction});
    }

    [CustomAuthorize(RoleEnum.Admin, RoleEnum.Manager)]
    [HttpGet(ApiEndpointConstant.Transaction.TotalRevenue)]
    [ProducesResponseType(typeof(double), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetTotalRevenue()
    {
        var response = await _transactionService.GetTotalRevue();
        return (response != null) ? Ok(response) : NotFound(new {Message = MessageConstant.Transaction.NotFindTransaction});
    }
}