using PanelWay_Backend.API.Payload.Requests.RegulatoryApproval;
using PanelWay_Backend.API.Payload.Responses.RegulatoryApproval;
using PanelWay_Backend.Domain.Paginate;

namespace PanelWay_Backend.API.Services.Interfaces;

public interface IRegulatoryApprovalService
{
    Task<RegulatoryApprovalResponse?> GetRegulatoryApprovalById(Guid id);

    Task<IPaginate<RegulatoryApprovalResponse>?> GetRegulatoryApprovalByRentalLocationId(Guid rentalLocationId,
        int page, int size);
    Task<RegulatoryApprovalResponse> CreateNewRegulatoryApproval(CreateRegulatoryApprovalRequest request);
}