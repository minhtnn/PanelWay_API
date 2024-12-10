using AutoMapper;
using PanelWay_Backend.API.Constants;
using PanelWay_Backend.API.Payload.Requests.Appointments;
using PanelWay_Backend.API.Payload.Responses.AdContents;
using PanelWay_Backend.API.Payload.Responses.Appointments;
using PanelWay_Backend.API.Services.Interfaces;
using PanelWay_Backend.Domain.Entities;
using PanelWay_Backend.Domain.Paginate;
using PanelWay_Backend.Repository.Interfaces;

namespace PanelWay_Backend.API.Services.Implements;

public class AppointmentService : BaseService<AppointmentService>, IAppointmentService
{
    public AppointmentService(IUnitOfWork<PanelWayDbContext> unitOfWork, ILogger<AppointmentService> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(unitOfWork, logger, mapper, httpContextAccessor)
    {   
    }

    public async Task<AppointmentResponse?> GetAppointmentById(Guid id)
    {
        var response = await _unitOfWork.GetRepository<Appointment>().SingleOrDefaultAsync
            (
                predicate: x => x.Id.Equals(id)
                );
        return (response != null) ? _mapper.Map<AppointmentResponse>(response) : null;
    }

    public async Task<IPaginate<AppointmentResponse>?> GetAppointmentListPaging(int page, int size)
    {
        var responses = await _unitOfWork.GetRepository<Appointment>().GetPagingListAsync(
                size: size,
                page: page,
                orderBy: x => x.OrderByDescending(x => x.BookingDate)
            );
        ;
        return (responses != null) ? _mapper.Map<IPaginate<AppointmentResponse>>(responses) : null;
    }

    public async Task<AppointmentResponse?> CreateNewAppointment(CreateAppointmentRequest request)
    {
        //Check empty code
        if (string.IsNullOrWhiteSpace(request.Code.Trim())) throw new BadHttpRequestException(MessageConstant.PanelWaySystem.EmptyField);
        //Check new Guid exists in DB
        var appointmentId = await _unitOfWork.GetRepository<Appointment>().SingleOrDefaultAsync
        (
            selector: x => x.Id,
            predicate: x => x.Id.Equals(request.Id)
        );
        //Re-generate Guid until it is new ones
        if (appointmentId != null && !appointmentId.Equals(Guid.Empty))
        {
            do
            {
                request.GetNewId();
                appointmentId = await _unitOfWork.GetRepository<AdContent>().SingleOrDefaultAsync
                (
                    selector: x => x.Id,
                    predicate: x => x.Id.Equals(request.Id)
                );
            } while (appointmentId != null && !appointmentId.Equals(Guid.Empty));
        }
        
        //Check if code exists
        var appointmentCode = await _unitOfWork.GetRepository<Appointment>().SingleOrDefaultAsync
        (
            selector: x => x.Code,
            predicate: x => x.Code.Equals(request.Code)
        );
        if (appointmentCode != null)
            throw new BadHttpRequestException(MessageConstant.Appointment.ExistAppointmentCode);

        //Add appointment
        var appointment = _mapper.Map<Appointment>(request);
        await _unitOfWork.GetRepository<Appointment>().InsertAsync(appointment);
        var isSuccessful = (await _unitOfWork.CommitAsync()) > 0;
        return isSuccessful ? _mapper.Map<AppointmentResponse>(appointment) : null;
    }

    public async Task<AppointmentResponse?> UpdateAppointment(UpdateAppointmentRequest request)
    {
        if (request.Id == null || request.Code == null)
            throw new BadHttpRequestException
                (MessageConstant.PanelWaySystem.EmptyField);
        var appointment = await _unitOfWork.GetRepository<Appointment>().SingleOrDefaultAsync(
            predicate: x => x.Id.Equals(request.Id) && x.Code.Equals(request.Code)
        );
        if (appointment == null) throw new BadHttpRequestException(MessageConstant.AdContent.NotFindAdContent);
        var updateAppointment = _mapper.Map<Appointment>(request);
        updateAppointment.AdContentId = appointment.AdContentId;
        updateAppointment.RentalLocationId = appointment.RentalLocationId;
        _unitOfWork.GetRepository<Appointment>().UpdateAsync(updateAppointment);
        var isSuccessful = (await _unitOfWork.CommitAsync()) > 0;
        return isSuccessful ? _mapper.Map<AppointmentResponse>(updateAppointment) : null;
    }
}