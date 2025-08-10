using Microsoft.AspNetCore.Mvc;
using PanelWay_Backend.API.Constants;
using PanelWay_Backend.API.Enums;
using PanelWay_Backend.API.Payload.Responses.RegulatoryLicenses;
using PanelWay_Backend.API.Services.Interfaces;
using PanelWay_Backend.API.Validators;

namespace PanelWay_Backend.API.Controllers;

public class RegulatoryLicenseController : BaseController<RegulatoryLicenseController>
{
    private readonly IRegulatoryLicenseService _regulatoryLicenseService;
    public RegulatoryLicenseController(ILogger<RegulatoryLicenseController> logger, IRegulatoryLicenseService regulatoryLicenseService) : base(logger)
    {
        _regulatoryLicenseService = regulatoryLicenseService;
    }
    
    [CustomAuthorize(RoleEnum.Admin, RoleEnum.Manager, RoleEnum.AdvertisingClient, RoleEnum.SpaceProvider)]
    [HttpGet(ApiEndpointConstant.RegulatoryLicense.FindRegulatoryLicenseByIdApiEndpoint)]
    [ProducesResponseType(typeof(RegulatoryLicenseResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetRegulatoryApprovalById(Guid id)
    {
        var responses = await _regulatoryLicenseService.GetRegulatoryLicenseById(id);
        return (responses != null) ? Ok(responses) : NotFound(new {Message = MessageConstant.RegulatoryApproval.NotFindRegulatoryApproval});
    }

    [CustomAuthorize(RoleEnum.Admin, RoleEnum.Manager, RoleEnum.AdvertisingClient, RoleEnum.SpaceProvider)]
    [HttpGet(ApiEndpointConstant.RegulatoryLicense.FindRegulatoryLicenseByRegulatoryApproveIdApiEndpoint)]
    [ProducesResponseType(typeof(ICollection<RegulatoryLicenseResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetRegulatoryApprovalByRegulatoryApproveId(Guid id)
    {
        var responses = await _regulatoryLicenseService.GetRegulatoryLicenseByRegulatoryApprovalId(id);
        return (responses != null)
            ? Ok(responses)
            : NotFound(new { Message = MessageConstant.RegulatoryLicense.NotFindRegulatoryLicense });
    }
}