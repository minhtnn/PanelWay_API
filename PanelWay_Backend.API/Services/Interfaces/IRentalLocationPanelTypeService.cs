using PanelWay_Backend.API.Payload.Requests.RentalLocationPanelTypes;
using PanelWay_Backend.API.Payload.Responses.RentalLocationPanelTypes;

namespace PanelWay_Backend.API.Services.Interfaces;

public interface IRentalLocationPanelTypeService
{
    Task<ICollection<RentalLocationPanelTypeResponse>?> GetRentalLocationPanelTypeByRentalLocationId(Guid id);

    Task<RentalLocationPanelTypeResponse?> CreateNewRentalLocationPanelType(CreateRentalLocationPanelTypeRequest request);

    Task<RentalLocationPanelTypeResponse?> UpdateRentalLocationPanelType(UpdateRentalLocationPanelTypeRequest request);
}