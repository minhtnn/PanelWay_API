using Microsoft.AspNetCore.Mvc;
using PanelWay_Backend.API.Constants;
using PanelWay_Backend.API.Services.Interfaces;

namespace PanelWay_Backend.API.Controllers;

public class FirebaseController : BaseController<FirebaseController>
{
    private readonly IFirebaseService _firebaseService;
    public FirebaseController(ILogger<FirebaseController> logger, IFirebaseService firebaseService) : base(logger)
    {
        _firebaseService = firebaseService;
    }
    [HttpPost(ApiEndpointConstant.Firebase.FirebaseUploadApiEndpoint)]
    public async Task<IActionResult> UploadFile(IFormFile file, string? folderName)
    {
        try
        {
            var response = await _firebaseService.Upload(file, folderName);
            if (response != null)
            {
                var result = new
                {
                    Message = MessageConstant.Firebase.UploadSuccessful,
                    Data = response
                };
                return Ok(result);
            }
            return BadRequest(new { Message = MessageConstant.Firebase.UploadFail });
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
    [HttpGet(ApiEndpointConstant.Firebase.FirebaseDownload)]
    public async Task<IActionResult> DownloadFile(string fileName, string? filePath)
    {
        try
        {
            var response = await _firebaseService.Download(fileName, filePath);
            if (response != null)
            {
                var result = new
                {
                    Message = MessageConstant.Firebase.DownloadSuccessful,
                    Data = response
                };
                return Ok(result);
            }
            return BadRequest(new { Message = MessageConstant.Firebase.DownloadFail });
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
}