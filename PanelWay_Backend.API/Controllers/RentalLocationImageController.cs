using Microsoft.AspNetCore.Mvc;
using PanelWay_Backend.API.Constants;
using PanelWay_Backend.API.Payload.Responses.RentalLocationImages;
using PanelWay_Backend.API.Services.Interfaces;

namespace PanelWay_Backend.API.Controllers;

public class RentalLocationImageController : BaseController<RentalLocationImageController>
{
    private readonly IRentalLocationImageService _rentalLocationImageService;
    public RentalLocationImageController(ILogger<RentalLocationImageController> logger, IRentalLocationImageService rentalLocationImageService) : base(logger)
    {
        _rentalLocationImageService = rentalLocationImageService;
    }

    [HttpGet(ApiEndpointConstant.RentalLocationImage.FindImageByRentalLocationIdApiEndpoint)]
    public async Task<IActionResult> GetImagesByRentalLocationId(Guid id)
    {
        var responses = await _rentalLocationImageService.GetAllImagesByRentalLocationId(id);
        return (responses != null) ? Ok(responses) : NotFound(new {Message = MessageConstant.PanelWaySystem.SystemError});
    }
}