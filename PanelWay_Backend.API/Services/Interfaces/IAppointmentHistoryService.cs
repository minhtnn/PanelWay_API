using PanelWay_Backend.API.Payload.Requests.AppointmentHistory;
using PanelWay_Backend.API.Payload.Responses.AppointmentHistory;

namespace PanelWay_Backend.API.Services.Interfaces;

public interface IAppointmentHistoryService
{
    Task<AppointmentHistoryResponse?> GetAppointmentHistoryById(Guid id);
    Task<ICollection<AppointmentHistoryResponse>> GetAppointmentHistoryByAppointmentId(Guid appointmentId);
    Task<AppointmentHistoryResponse> CreateNewAppointmentHistory(CreateAppointmentHistoryRequest request);
}