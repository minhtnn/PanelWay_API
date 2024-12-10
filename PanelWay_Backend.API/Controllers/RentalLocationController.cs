using Microsoft.AspNetCore.Mvc;
using PanelWay_Backend.API.Constants;
using PanelWay_Backend.API.Payload.Responses.RentalLocations;
using PanelWay_Backend.API.Services.Interfaces;

namespace PanelWay_Backend.API.Controllers;

public class RentalLocationController : BaseController<RentalLocationController>
{
    private readonly IRentalLocationService _rentalLocationService;
    public RentalLocationController(ILogger<RentalLocationController> logger, IRentalLocationService rentalLocationService) : base(logger)
    {
        _rentalLocationService = rentalLocationService;
    }

    [HttpGet(ApiEndpointConstant.RentalLocation.RentalLocationApiEndpoint)]
    [ProducesResponseType(typeof(ICollection<RentalLocationResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetRentalLocations([FromQuery] double minLat, 
        [FromQuery] double minLng, [FromQuery] double maxLat, [FromQuery] double maxLng)
    {
        var responses = await _rentalLocationService.GetRentalLocationListPaging(minLat, minLng, maxLat, maxLng);
        return (responses != null) ? Ok(responses) : NotFound(new {Message = MessageConstant.PanelWaySystem.SystemError});
    }
    [HttpGet(ApiEndpointConstant.RentalLocation.FindRentalLocationByIdApiEndpoint)]
    [ProducesResponseType(typeof(RentalLocationResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetRentalLocationById(Guid id)
    {
        var responses = await _rentalLocationService.GetRentalLocationById(id);
        return (responses != null) ? Ok(responses) : NotFound(new {Message = MessageConstant.RentalLocationPanelType.NotFindRentalLocationPanelType});
    }
}