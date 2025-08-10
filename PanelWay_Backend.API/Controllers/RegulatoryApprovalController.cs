using Microsoft.AspNetCore.Mvc;
using PanelWay_Backend.API.Constants;
using PanelWay_Backend.API.Enums;
using PanelWay_Backend.API.Payload.Responses.RegulatoryApproval;
using PanelWay_Backend.API.Services.Interfaces;
using PanelWay_Backend.API.Validators;

namespace PanelWay_Backend.API.Controllers;

public class RegulatoryApprovalController : BaseController<RegulatoryApprovalController>
{
    private readonly IRegulatoryApprovalService _regulatoryApprovalService;
    public RegulatoryApprovalController(ILogger<RegulatoryApprovalController> logger, IRegulatoryApprovalService regulatoryApprovalService) : base(logger)
    {
        _regulatoryApprovalService = regulatoryApprovalService;
    }
    [CustomAuthorize(RoleEnum.Admin, RoleEnum.Manager, RoleEnum.AdvertisingClient, RoleEnum.SpaceProvider)]
    [HttpGet(ApiEndpointConstant.RegulatoryApproval.FindRegulatoryApprovalByIdApiEndpoint)]
    [ProducesResponseType(typeof(RegulatoryApprovalResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetRegulatoryApprovalById(Guid id)
    {
        var responses = await _regulatoryApprovalService.GetRegulatoryApprovalById(id);
        return (responses != null) ? Ok(responses) : NotFound(new {Message = MessageConstant.RegulatoryApproval.NotFindRegulatoryApproval});
    }
    
    [CustomAuthorize(RoleEnum.Admin, RoleEnum.Manager, RoleEnum.AdvertisingClient, RoleEnum.SpaceProvider)]
    [HttpGet(ApiEndpointConstant.RegulatoryApproval.FindRegulatoryApprovalByRentalLocationIdApiEndpoint)]
    [ProducesResponseType(typeof(ICollection<RegulatoryApprovalResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetRegulatoryApprovalByRentalLocationId(Guid id, [FromQuery] int page = 1, [FromQuery] int size = 10)
    {
        var responses = await _regulatoryApprovalService.GetRegulatoryApprovalByRentalLocationId(id, page, size);
        return (responses != null) ? Ok(responses) : NotFound(new {Message = MessageConstant.RegulatoryApproval.NotFindRegulatoryApproval});
    }
}