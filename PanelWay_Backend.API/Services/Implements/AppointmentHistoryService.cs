using AutoMapper;
using PanelWay_Backend.API.Payload.Requests.AppointmentHistory;
using PanelWay_Backend.API.Payload.Responses.AppointmentHistory;
using PanelWay_Backend.API.Services.Interfaces;
using PanelWay_Backend.Domain.Entities;
using PanelWay_Backend.Repository.Interfaces;

namespace PanelWay_Backend.API.Services.Implements;

public class AppointmentHistoryService:BaseService<AppointmentHistory>, IAppointmentHistoryService
{
    public AppointmentHistoryService(IUnitOfWork<PanelWayDbContext> unitOfWork, ILogger<AppointmentHistory> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(unitOfWork, logger, mapper, httpContextAccessor)
    {
    }

    public async Task<AppointmentHistoryResponse?> GetAppointmentHistoryById(Guid id)
    {
        var response = await _unitOfWork.GetRepository<AppointmentHistory>().SingleOrDefaultAsync
            (
                predicate: x => x.Id.Equals(id)
                );
        return (response != null) ? _mapper.Map<AppointmentHistoryResponse>(response) : null;
    }

    public async Task<ICollection<AppointmentHistoryResponse>> GetAppointmentHistoryByAppointmentId(Guid appointmentId)
    {
        var response = await _unitOfWork.GetRepository<AppointmentHistory>().GetListAsync
        (
            predicate: x => x.Appointment.Id.Equals(appointmentId),
            orderBy: x => x.OrderByDescending(x => x.IssueDate)
        );
        return (response != null) ? _mapper.Map<ICollection<AppointmentHistoryResponse>>(response) : null;    }

    public Task<AppointmentHistoryResponse> CreateNewAppointmentHistory(CreateAppointmentHistoryRequest request)
    {
        throw new NotImplementedException();
    }
}