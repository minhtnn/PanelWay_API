using Microsoft.AspNetCore.Mvc;
using PanelWay_Backend.API.Constants;
using PanelWay_Backend.API.Payload.Requests.PayOS;
using PanelWay_Backend.API.Services.Interfaces;

namespace PanelWay_Backend.API.Controllers;

public class CheckoutController : BaseController<CheckoutController>
{
    private readonly IPayOSService _payOsService;
    public CheckoutController(ILogger<CheckoutController> logger, IPayOSService payOsService) : base(logger)
    {
        _payOsService = payOsService;
    }

    [HttpPost(ApiEndpointConstant.PayOs.CreateQrApiEndpoint)]
    public async Task<IActionResult> CreatePaymentQR([FromBody]CreatePayOSRequest request)
    {
        var response = await _payOsService.CreateCheckoutLink(request);
        return (response != null) ? Ok(response) : NotFound();
    }
    
    [HttpGet(ApiEndpointConstant.PayOs.FindPayOSByOrderIdApiEndpoint)]
    public async Task<IActionResult> GetPaymentLinkInformation(long orderId)
    {
        var response = await _payOsService.GetPaymentLinkInformation(orderId);
        return (response != null) ? Ok(response) : NotFound();
    }
    
    [HttpPost(ApiEndpointConstant.PayOs.CancelPayOSByOrderIdApiEndpoint)]
    public async Task<IActionResult> CancelPaymentLinkInformation(long orderId)
    {
        var response = await _payOsService.CancelPaymentLink(orderId);
        return (response != null) ? Ok(response) : NotFound();
    }
}