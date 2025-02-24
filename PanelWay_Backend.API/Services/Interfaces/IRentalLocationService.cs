using PanelWay_Backend.API.Payload.Requests.RentalLocations;
using PanelWay_Backend.API.Payload.Responses.RentalLocations;
using PanelWay_Backend.Domain.Paginate;

namespace PanelWay_Backend.API.Services.Interfaces;

public interface IRentalLocationService
{
    Task<IPaginate<RentalLocationResponse>?> GetRentalLocationListPaging(int page, int size);
    Task<ICollection<RentalLocationResponse>?> GetRentalLocationListByLatLng(double minLat, double minLng, double maxLat, double maxLng);
    Task<RentalLocationResponse> GetRentalLocationById(Guid id);
    Task<int> GetTotalRentalLocation();
    Task<RentalLocationResponse> CreateRentalLocation(CreateRentalLocationRequest request);
    Task<RentalLocationResponse> UpdateRentalLocation(UpdateRentalLocationRequest request);
}