using PanelWay_Backend.API.Payload.Requests.RentalLocations;
using PanelWay_Backend.API.Payload.Responses.RentalLocations;
using PanelWay_Backend.Domain.Paginate;

namespace PanelWay_Backend.API.Services.Interfaces;

public interface IRentalLocationService
{
    Task<ICollection<RentalLocationResponse>?> GetRentalLocationListPaging(double minLat, double minLng, double maxLat, double maxLng);
    Task<RentalLocationResponse> GetRentalLocationById(Guid id);
    Task<RentalLocationResponse> CreateRentalLocation(CreateRentalLocationRequest request);
    Task<RentalLocationResponse> UpdateRentalLocation(UpdateRentalLocationRequest request);
}