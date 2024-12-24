using Microsoft.AspNetCore.Mvc;
using PanelWay_Backend.API.Constants;
using PanelWay_Backend.API.Enums;
using PanelWay_Backend.API.Payload.Requests.RentalLocationPanelTypes;
using PanelWay_Backend.API.Payload.Responses.RentalLocationPanelTypes;
using PanelWay_Backend.API.Services.Interfaces;
using PanelWay_Backend.API.Validators;

namespace PanelWay_Backend.API.Controllers;

public class RentalLocationPanelTypeController : BaseController<RentalLocationPanelTypeController>
{
    private readonly IRentalLocationPanelTypeService _rentalLocationPanelTypeService;
    public RentalLocationPanelTypeController(ILogger<RentalLocationPanelTypeController> logger, IRentalLocationPanelTypeService rentalLocationPanelTypeService) : base(logger)
    {
        _rentalLocationPanelTypeService = rentalLocationPanelTypeService;
    }
    
    [CustomAuthorize(RoleEnum.Admin, RoleEnum.Manager, RoleEnum.AdvertisingClient, RoleEnum.SpaceProvider)]
    [HttpGet(ApiEndpointConstant.RentalLocationPanelType.FindRentalLocationPanelTypeByRentalLocationIdApiEndpoint)]
    [ProducesResponseType(typeof(ICollection<RentalLocationPanelTypeResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetRentalLocationPanelTypeById(Guid id)
    {
        var responses = await _rentalLocationPanelTypeService.GetRentalLocationPanelTypeByRentalLocationId(id);
        return (responses != null) ? Ok(responses) : NotFound(new {Message = MessageConstant.RentalLocationPanelType.NotFindRentalLocationPanelType});
    }

    [CustomAuthorize(RoleEnum.Admin, RoleEnum.Manager, RoleEnum.AdvertisingClient, RoleEnum.SpaceProvider)]
    [HttpPost(ApiEndpointConstant.RentalLocationPanelType.RentalLocationPanelTypeApiEndpoint)]
    [ProducesResponseType(typeof(RentalLocationPanelTypeResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetRentalLocationPanelTypeById(CreateRentalLocationPanelTypeRequest request)
    {
        var response = await _rentalLocationPanelTypeService.CreateNewRentalLocationPanelType(request);
        return (response != null) ? Ok(response) : NotFound(new {Message = MessageConstant.RentalLocationPanelType.NotFindRentalLocationPanelType});
    }
    
    [CustomAuthorize(RoleEnum.Admin, RoleEnum.Manager, RoleEnum.SpaceProvider)]
    [HttpDelete(ApiEndpointConstant.RentalLocationPanelType.RentalLocationPanelTypeApiEndpoint)]
    [ProducesResponseType(typeof(RentalLocationPanelTypeResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> DeleteRentalLocationPanelType(UpdateRentalLocationPanelTypeRequest request)
    {
        var response = await _rentalLocationPanelTypeService.UpdateRentalLocationPanelType(request);
        return (response != null) ? Ok(response) : NotFound(new {Message = MessageConstant.RentalLocationPanelType.NotFindRentalLocationPanelType});
    }
}