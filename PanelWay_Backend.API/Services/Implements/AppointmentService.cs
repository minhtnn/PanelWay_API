using AutoMapper;
using PanelWay_Backend.API.Constants;
using PanelWay_Backend.API.Enums;
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
    public AppointmentService(IUnitOfWork<PanelWayDbContext> unitOfWork, ILogger<AppointmentService> logger,
        IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(unitOfWork, logger, mapper,
        httpContextAccessor)
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

    public async Task<DateTime?> GetIsNearestBookingDate()
    {
        var response = (await _unitOfWork.GetRepository<Appointment>().GetListAsync(
            selector: x => x.BookingDate,
            predicate: x =>
                !(x.Status.Equals(nameof(AppointmentStatusEnum.Expired)) ||
                  x.Status.Equals(nameof(AppointmentStatusEnum.Cancel))),
            orderBy: x => x.OrderBy(x => x.BookingDate)
        )).Min();
        return response;
    }

    public async Task<ICollection<Appointment>> GetAppointmentsByBookingDate(DateTime? bookingDate)
    {
        var responses = await _unitOfWork.GetRepository<Appointment>().GetListAsync(
            predicate: x => x.BookingDate.Equals(bookingDate) &&
                            x.Status.Equals(nameof(AppointmentStatusEnum.Pending)) ||
                            x.Status.Equals(nameof(AppointmentStatusEnum.Confirmed))
        );
        return (responses);
    }

    public async Task<IPaginate<AppointmentResponse>?> GetAppointmentListPaging(int page, int size)
    {
        var responses = await _unitOfWork.GetRepository<Appointment>().GetPagingListAsync(
            // page: page,
            // size: size,
            orderBy: x => x.OrderByDescending(x => x.BookingDate)
        );
        var check = _mapper.Map<IPaginate<AppointmentResponse>>(responses);
        return (responses != null) ? _mapper.Map<IPaginate<AppointmentResponse>>(responses) : null;
    }

    public async Task<ICollection<AppointmentResponse>> GetAppointmentByRentalLocationId(Guid id)
    {
        var appointments = await _unitOfWork.GetRepository<Appointment>().GetListAsync
            (
                predicate: x => x.RentalLocationId.Equals(id)
                );
        return (appointments != null) ? _mapper.Map<ICollection<AppointmentResponse>>(appointments) : null;
    }

    public async Task<AppointmentResponse?> CreateNewAppointment(CreateAppointmentRequest request)
    {
        //Check empty code
        if (string.IsNullOrWhiteSpace(request.Code.Trim()))
            throw new BadHttpRequestException(MessageConstant.PanelWaySystem.EmptyField);
        //Check if the rental location is no more available
        var rentalLocationStatus = await _unitOfWork.GetRepository<RentalLocation>().SingleOrDefaultAsync
            (
                selector: x => x.Status,
                predicate: x => x.Id.Equals(request.RentalLocationId)
                );
        if (rentalLocationStatus!.Equals(nameof(RentalLocationStatusEnum.Unavailable))) throw new BadHttpRequestException(MessageConstant.RentalLocation.UnAvailableRentalLocation);
        //Check if ad content already register in rental location
        var adContentInAppoinntment = await _unitOfWork.GetRepository<Appointment>().SingleOrDefaultAsync
            (
                predicate: x => x.AdContentId.Equals(request.AdContentId) && x.RentalLocationId.Equals(request.RentalLocationId)
                );
        if (adContentInAppoinntment != null) throw new BadHttpRequestException(MessageConstant.Appointment.AdContentExistAppointment);
        //Check if rental location already has 5 appointments
        var appointments = await _unitOfWork.GetRepository<Appointment>().GetListAsync
        (
            predicate: x => x.RentalLocationId.Equals(request.RentalLocationId) && !(x.Status.Equals(nameof(AppointmentStatusEnum.Expired)))
        );
        if (appointments.Count >= 5) throw new BadHttpRequestException(MessageConstant.Appointment.ExceedAppointment);
        //Check new Guid exists in DB
        var appointmentId = await _unitOfWork.GetRepository<Appointment>().SingleOrDefaultAsync
        (
            selector: x => x.Id,
            predicate: x => x.Id.Equals(request.Id)
        );
        //Re-generate Guid until it is new ones
        if (appointmentId != null || !appointmentId.Equals(Guid.Empty))
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

    public async Task<bool> UpdateAppointments(ICollection<Appointment> requests)
    {
        _unitOfWork.GetRepository<Appointment>().UpdateRange(requests);
        var isSuccessful = (await _unitOfWork.CommitAsync()) > requests.Count;
        return isSuccessful;
    }
}