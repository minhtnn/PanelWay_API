using Microsoft.AspNetCore.Mvc;
using PanelWay_Backend.API.Constants;
using PanelWay_Backend.API.Enums;
using PanelWay_Backend.API.Payload.Responses.AppointmentHistory;
using PanelWay_Backend.API.Services.Interfaces;
using PanelWay_Backend.API.Validators;
using PanelWay_Backend.Domain.Paginate;

namespace PanelWay_Backend.API.Controllers;

public class AppointmentHistoryController : BaseController<AppointmentHistoryController>
{
    private readonly IAppointmentHistoryService _appointmentHistoryService;
    public AppointmentHistoryController(ILogger<AppointmentHistoryController> logger, IAppointmentHistoryService appointmentHistoryService) : base(logger)
    {
        _appointmentHistoryService = appointmentHistoryService;
    }
    [CustomAuthorize(RoleEnum.Admin, RoleEnum.Manager, RoleEnum.AdvertisingClient, RoleEnum.SpaceProvider)]
    [HttpGet(ApiEndpointConstant.AppointmentHistory.FindAppointmentHistoryByIdApiEndpoint)]
    [ProducesResponseType(typeof(AppointmentHistoryResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAppointmentHistoryById(Guid id)
    {
        var response = await _appointmentHistoryService.GetAppointmentHistoryById(id);
        return (response != null) ? Ok(response) : NotFound(new {Message = MessageConstant.AppointmentHistory.NotFindAppointmentHistory});
    }
    
    [CustomAuthorize(RoleEnum.Admin, RoleEnum.Manager, RoleEnum.AdvertisingClient, RoleEnum.SpaceProvider)]
    [HttpGet(ApiEndpointConstant.AppointmentHistory.FindAppointmentHistoryByAppointmentIdApiEndpoint)]
    [ProducesResponseType(typeof(ICollection<AppointmentHistoryResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAppointmentHistoryByAppointmentId(Guid id)
    {
        var response = await _appointmentHistoryService.GetAppointmentHistoryByAppointmentId(id);
        return (response != null) ? Ok(response) : NotFound(new {Message = MessageConstant.AppointmentHistory.NotFindAppointmentHistory});
    }
}