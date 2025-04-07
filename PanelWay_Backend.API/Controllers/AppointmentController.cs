using Microsoft.AspNetCore.Mvc;
using PanelWay_Backend.API.Constants;
using PanelWay_Backend.API.Enums;
using PanelWay_Backend.API.Payload.Requests.Appointments;
using PanelWay_Backend.API.Payload.Responses.Appointments;
using PanelWay_Backend.API.Services.Interfaces;
using PanelWay_Backend.API.Validators;
using PanelWay_Backend.Domain.Paginate;

namespace PanelWay_Backend.API.Controllers;

public class AppointmentController : BaseController<AppointmentController>
{
    private readonly IAppointmentService _appointmentService;
    public AppointmentController(ILogger<AppointmentController> logger, IAppointmentService appointmentService) : base(logger)
    {
        _appointmentService = appointmentService;
    }
    [CustomAuthorize(RoleEnum.Admin, RoleEnum.Manager)]
    [HttpGet(ApiEndpointConstant.Appointment.AppointmentApiEndpoint)]
    [ProducesResponseType(typeof(IPaginate<AppointmentResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAppointments([FromQuery] int size = 10, [FromQuery] int page = 1)
    {
        var responses = await _appointmentService.GetAppointmentListPaging(page, size);
        return (responses != null)? Ok(responses) : StatusCode(500, new {Message = MessageConstant.PanelWaySystem.SystemError});
    }
    [CustomAuthorize(RoleEnum.Admin, RoleEnum.Manager,RoleEnum.SpaceProvider, RoleEnum.AdvertisingClient)]
    [HttpGet(ApiEndpointConstant.Appointment.FindAppointmentByIdApiEndpoint)]
    [ProducesResponseType(typeof(AppointmentResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAppointmentById(Guid id)
    {
        var response = await _appointmentService.GetAppointmentById(id);
        return (response != null) ? Ok(response) : NotFound(new {Message = MessageConstant.Appointment.NotFindAppointment});
    }
    [CustomAuthorize(RoleEnum.SpaceProvider, RoleEnum.AdvertisingClient)]
    [HttpGet(ApiEndpointConstant.Appointment.AppointmentByAccountIdApiEndpoint)]
    [ProducesResponseType(typeof(ICollection<AppointmentResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(object), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult?> GetAppointmentByAccountId(Guid id, string role, DateTime? bookDate)
    {
        var responses = await _appointmentService.GetAppointmentByAccountId(id, role, bookDate);

        if (responses != null)
        {
            return Ok(responses);
        }

        return StatusCode(500, new { Message = MessageConstant.PanelWaySystem.SystemError });
    }
    [CustomAuthorize(RoleEnum.Admin, RoleEnum.Manager, RoleEnum.AdvertisingClient, RoleEnum.SpaceProvider)]
    [HttpGet(ApiEndpointConstant.Appointment.FindAppointmentByRentalLocationIdApiEndpoint)]
    [ProducesResponseType(typeof(AppointmentResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAppointmentByRentalLocationId(Guid id)
    {
        var responses = await _appointmentService.GetAppointmentByRentalLocationId(id);
        return (responses != null) ? Ok(responses) : NotFound(new {Message = MessageConstant.Appointment.NotFindAppointment});
    }

    [CustomAuthorize(RoleEnum.Admin, RoleEnum.Manager, RoleEnum.AdvertisingClient)]
    [HttpPost(ApiEndpointConstant.Appointment.AppointmentApiEndpoint)]
    [ProducesResponseType(typeof(AppointmentResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateNewAppointment(CreateAppointmentRequest request)
    {
        var response = await _appointmentService.CreateNewAppointment(request);
        return (response != null) ? Ok(response) : NotFound(new {Message = MessageConstant.Appointment.CreateAppointmentFail});
    }
    
    [HttpPatch(ApiEndpointConstant.Appointment.AppointmentApiEndpoint)]
    [ProducesResponseType(typeof(AppointmentResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateAppointment(UpdateAppointmentRequest request)
    {
        var response = await _appointmentService.UpdateAppointment(request);
        return (response != null) ? Ok(response) : NotFound(new {Message = MessageConstant.Appointment.CreateAppointmentFail});
    }
}