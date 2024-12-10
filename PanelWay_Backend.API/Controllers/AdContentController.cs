using Microsoft.AspNetCore.Mvc;
using PanelWay_Backend.API.Constants;
using PanelWay_Backend.API.Payload.Requests.AdContents;
using PanelWay_Backend.API.Payload.Responses.AdContents;
using PanelWay_Backend.API.Services.Interfaces;

namespace PanelWay_Backend.API.Controllers;

public class AdContentController : BaseController<AdContentController>
{
    private readonly IAdContentService _adContentService;
    public AdContentController(ILogger<AdContentController> logger, IAdContentService adContentService) : base(logger)
    {
        _adContentService = adContentService;
    }

    [HttpGet(ApiEndpointConstant.AdContent.FindAdContentByIdApiEndpoint)]
    [ProducesResponseType(typeof(AdContentResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAdContentById(Guid id)
    {
        var response = await _adContentService.GetAdContentById(id);
        return (response != null) ? Ok(response) : NotFound(new {Message = MessageConstant.AdContent.NotFindAdContent});
    }

    [HttpGet(ApiEndpointConstant.AdContent.FindAdContentByAdvertisingClientIdApiEndpoint)]
    [ProducesResponseType(typeof(ICollection<AdContentResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAdContentByAdvertisingClientId(Guid id)
    {
        var response = await _adContentService.GetAdContentByAdvertisingClientId(id);
        return  (response != null) ? Ok(response) : NotFound(new {Message = MessageConstant.AdContent.NotFindAdContent});
    }

    [HttpPost(ApiEndpointConstant.AdContent.AdContentApiEndpoint)]
    [ProducesResponseType(typeof(AdContentResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateNewAdContent(CreateAdContentRequest request)
    {
        var response = await _adContentService.CreateNewAdContent(request);
        return  (response != null) ? Ok(response) : NotFound(new {Message = MessageConstant.AdContent.CreateAdContentFail});
    }
    
    [HttpPatch(ApiEndpointConstant.AdContent.AdContentApiEndpoint)]
    [ProducesResponseType(typeof(AdContentResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateNewAdContent(UpdateAdContentRequest request)
    {
        var response = await _adContentService.UpdateAdContent(request);
        return  (response != null) ? Ok(response) : NotFound(new {Message = MessageConstant.AdContent.UpdateAdContentFail});
    }
}