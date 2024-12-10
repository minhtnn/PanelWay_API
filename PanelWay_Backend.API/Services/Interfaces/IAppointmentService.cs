using PanelWay_Backend.API.Payload.Requests.Appointments;
using PanelWay_Backend.API.Payload.Responses.Appointments;
using PanelWay_Backend.Domain.Paginate;

namespace PanelWay_Backend.API.Services.Interfaces;

public interface IAppointmentService
{
    Task<AppointmentResponse?> GetAppointmentById(Guid id);
    Task<IPaginate<AppointmentResponse>?> GetAppointmentListPaging(int page, int size);
    Task<AppointmentResponse?> CreateNewAppointment(CreateAppointmentRequest request);
    Task<AppointmentResponse?> UpdateAppointment(UpdateAppointmentRequest request);
}