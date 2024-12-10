using Microsoft.AspNetCore.Mvc;
using PanelWay_Backend.API.Constants;
using PanelWay_Backend.API.Payload.Requests.PaymentTypes;
using PanelWay_Backend.API.Payload.Responses.PaymentTypes;
using PanelWay_Backend.API.Services.Interfaces;

namespace PanelWay_Backend.API.Controllers;

public class PaymentTypeController : BaseController<PaymentTypeController>
{
    private readonly IPaymentTypeService _paymentTypeService;
    public PaymentTypeController(ILogger<PaymentTypeController> logger, IPaymentTypeService paymentTypeService) : base(logger)
    {
        _paymentTypeService = paymentTypeService;
    }
    [HttpGet(ApiEndpointConstant.PaymentType.PaymentTypeApiEndpoint)]
    [ProducesResponseType(typeof(ICollection<PaymentTypeResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetPaymentTypeList()
    {
        var responses = await _paymentTypeService.GetPaymentTypeList();
        return (responses != null) ? Ok(responses) : NotFound(new {Message = MessageConstant.PanelWaySystem.SystemError});
    }
    [HttpPatch(ApiEndpointConstant.PaymentType.PaymentTypeApiEndpoint)]
    [ProducesResponseType(typeof(PaymentTypeResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateNewPaymentType(UpdatePaymentTypeRequest request)
    {
        var response = await _paymentTypeService.UpdatePaymentType(request);
        return (response != null) ? Ok(response) : NotFound(new {Message = MessageConstant.PaymentType.UpdatePaymentTypeFail});
    }
}