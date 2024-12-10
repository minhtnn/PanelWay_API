using AutoMapper;
using PanelWay_Backend.API.Payload.Requests.AppointmentHistory;
using PanelWay_Backend.API.Payload.Requests.Appointments;
using PanelWay_Backend.API.Payload.Responses.AppointmentHistory;
using PanelWay_Backend.API.Payload.Responses.Appointments;
using PanelWay_Backend.Domain.Entities;

namespace PanelWay_Backend.API.Mappers;

public class AppointmentHistoryMapper : Profile
{
    public AppointmentHistoryMapper()
    {
        CreateMap<CreateAppointmentHistoryRequest, AppointmentHistory>();
        CreateMap<AppointmentHistory, AppointmentHistoryResponse>();
    }
}