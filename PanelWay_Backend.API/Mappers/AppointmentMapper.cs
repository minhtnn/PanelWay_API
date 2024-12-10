using AutoMapper;
using PanelWay_Backend.API.Payload.Requests.Appointments;
using PanelWay_Backend.API.Payload.Responses.Appointments;
using PanelWay_Backend.Domain.Entities;

namespace PanelWay_Backend.API.Mappers;

public class AppointmentMapper : Profile
{
    public AppointmentMapper()
    {
        CreateMap<CreateAppointmentRequest, Appointment>();
        CreateMap<UpdateAppointmentRequest, Appointment>();
        CreateMap<Appointment, AppointmentResponse>();
    }
}