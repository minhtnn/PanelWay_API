using PanelWay_Backend.API.Payload.Requests.PanelTypes;
using PanelWay_Backend.API.Payload.Responses.PanelTypes;
using PanelWay_Backend.Domain.Paginate;

namespace PanelWay_Backend.API.Services.Interfaces;

public interface IPanelTypeService
{
    Task<PanelTypeResponse> GetPanelTypeResponseById(Guid id);
    Task<ICollection<PanelTypeResponse>?> GetPanelTypeList();
    Task<PanelTypeResponse?> CreateNewPanelType(CreatePanelTypeRequest request);
    Task<PanelTypeResponse?> UpdatePanelType(UpdatePanelTypeRequest request);
}