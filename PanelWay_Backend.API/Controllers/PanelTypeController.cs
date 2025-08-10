using Microsoft.AspNetCore.Mvc;
using PanelWay_Backend.API.Constants;
using PanelWay_Backend.API.Payload.Requests.PanelTypes;
using PanelWay_Backend.API.Payload.Responses.PanelTypes;
using PanelWay_Backend.API.Services.Interfaces;

namespace PanelWay_Backend.API.Controllers;

public class PanelTypeController:BaseController<PanelTypeController>
{
    private readonly IPanelTypeService _panelTypeService;
    public PanelTypeController(ILogger<PanelTypeController> logger, IPanelTypeService panelTypeService) : base(logger)
    {
        _panelTypeService = panelTypeService;
    }
    
    [HttpGet(ApiEndpointConstant.PanelType.PanelTypeApiEndpoint)]
    [ProducesResponseType(typeof(ICollection<PanelTypeResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetPanelTypeList()
    {
        var responses = await _panelTypeService.GetPanelTypeList();
        return (responses != null) ? Ok(responses) : NotFound(new {Message = MessageConstant.PanelWaySystem.SystemError});
    }
    
    [HttpGet(ApiEndpointConstant.PanelType.FindPanelTypeByIdApiEndpoint)]
    [ProducesResponseType(typeof(PanelTypeResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetPanelTypeById(Guid id)
    {
        var response = await _panelTypeService.GetPanelTypeResponseById(id);
        return (response != null) ? Ok(response) : NotFound(new {Message = MessageConstant.PanelType.NotFindPanelType});
    }

    [HttpPost(ApiEndpointConstant.PanelType.PanelTypeApiEndpoint)]
    [ProducesResponseType(typeof(PanelTypeResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateNewPanelType(CreatePanelTypeRequest request)
    {
        var response = await _panelTypeService.CreateNewPanelType(request);
        return (response != null) ? Ok(response) : NotFound(new {Message = MessageConstant.PanelType.CreatePanelTypeFail});
    }
    
    [HttpPatch(ApiEndpointConstant.PanelType.PanelTypeApiEndpoint)]
    [ProducesResponseType(typeof(PanelTypeResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateNewPanelType(UpdatePanelTypeRequest request)
    {
        var response = await _panelTypeService.UpdatePanelType(request);
        return (response != null) ? Ok(response) : NotFound(new {Message = MessageConstant.PanelType.UpdatePanelTypeFail});
    }
}