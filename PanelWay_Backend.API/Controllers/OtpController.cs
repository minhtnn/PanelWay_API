using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using PanelWay_Backend.API.Constants;
using PanelWay_Backend.API.Payload.Requests.Accounts;
using PanelWay_Backend.API.Payload.Responses.Accounts;
using PanelWay_Backend.API.Services.Implements;
using PanelWay_Backend.API.Services.Interfaces;

namespace PanelWay_Backend.API.Controllers;
public class OtpController : BaseController<OtpController>
{
    private readonly IInfobipService _infobipService;
    private readonly IMemoryCache _cache;
    public OtpController(ILogger<OtpController> logger, IInfobipService infobipService, IMemoryCache cache) : base(logger)
    {
        _infobipService = infobipService;
        _cache = cache;
    }
    [HttpPost(ApiEndpointConstant.Otp.OtpSendSmsApiEndpoint)]
    public async Task<IActionResult> SendOtp([FromBody] OtpRequest request)
    {
        if (string.IsNullOrEmpty(request.PhoneNumber))
            return BadRequest("Số điện thoại không hợp lệ.");

        string otpCode = new Random().Next(1000, 9999).ToString();
        _cache.Set(request.PhoneNumber, otpCode, TimeSpan.FromMinutes(5));

        var result = await _infobipService.SendOtpAsync(request.PhoneNumber, otpCode);
        return StatusCode((result != null)?200:400,new OtpResponse
        {
            Message = (result != null)? "OTP đã được gửi." : "OTP gửi thất bại",
            OtpCode = (result != null)?otpCode: null,
            Status = (result != null)?200:400
        });
    }

    [HttpPost("verify")]
    public IActionResult VerifyOtp([FromBody] OtpVerifyRequest request)
    {
        if (_cache.TryGetValue(request.PhoneNumber, out string cachedOtp) && cachedOtp == request.OtpCode)
        {
            _cache.Remove(request.PhoneNumber);
            return StatusCode(200, new OtpResponse{ Message = "OTP hợp lệ." , Status = 200});
        }
        return BadRequest(new OtpResponse{ Message = "OTP không hợp lệ hoặc đã hết hạn." , Status = 400});
    }
}