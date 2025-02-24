using AutoMapper;
using PanelWay_Backend.API.Payload.Requests.RentalLocations;
using PanelWay_Backend.API.Payload.Responses.RentalLocations;
using PanelWay_Backend.API.Services.Interfaces;
using PanelWay_Backend.Domain.Entities;
using PanelWay_Backend.Domain.Paginate;
using PanelWay_Backend.Repository.Interfaces;

namespace PanelWay_Backend.API.Services.Implements;

public class RentalLocationService : BaseService<RentalLocationService>, IRentalLocationService
{
    public RentalLocationService(IUnitOfWork<PanelWayDbContext> unitOfWork, ILogger<RentalLocationService> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(unitOfWork, logger, mapper, httpContextAccessor)
    {
    }

    public async Task<IPaginate<RentalLocationResponse>?> GetRentalLocationListPaging(int page, int size)
    {
        var responses = await _unitOfWork.GetRepository<RentalLocation>().GetPagingListAsync(
                page:page,
                size: size
            );
        return (responses != null) ? _mapper.Map<IPaginate<RentalLocationResponse>>(responses) : null;
    }

    public async Task<ICollection<RentalLocationResponse>?> GetRentalLocationListByLatLng(double minLat, double minLng, double maxLat, double maxLng)
    {
        var responses = await _unitOfWork.GetRepository<RentalLocation>().GetListAsync(
            predicate: x => (x.Latitude >= minLat && (x.Latitude <= maxLat) &&
                                        (x.Longitude >= minLng) && (x.Longitude <= maxLng))
            );
        return (responses != null) ? _mapper.Map<ICollection<RentalLocationResponse>>(responses) : null;
    }

    public async Task<RentalLocationResponse> GetRentalLocationById(Guid id)
    {
        var response = await _unitOfWork.GetRepository<RentalLocation>().SingleOrDefaultAsync(
            predicate: x => x.Id.Equals(id)
            );
        return (response != null) ? _mapper.Map<RentalLocationResponse>(response) : null;
    }

    public async Task<int> GetTotalRentalLocation()
    {
        var response = await _unitOfWork.GetRepository<RentalLocation>().CountAsync();
        return response;
    }

    public async Task<IPaginate<RentalLocationResponse>?> GetRentalLocationBySpaceProviderId(Guid spaceProviderId, int page, int size)
    {
        var response = await _unitOfWork.GetRepository<RentalLocation>().GetPagingListAsync(
            predicate: x => x.SpaceProviderId.Equals(spaceProviderId),
            page:page,
            size: size,
            orderBy: x => x.OrderByDescending(x => x.PostDate)
        );
        return (response != null) ? _mapper.Map<IPaginate<RentalLocationResponse>>(response) : null;
    }

    public Task<RentalLocationResponse> CreateRentalLocation(CreateRentalLocationRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<RentalLocationResponse> UpdateRentalLocation(UpdateRentalLocationRequest request)
    {
        throw new NotImplementedException();
    }
}