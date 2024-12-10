using AutoMapper;
using PanelWay_Backend.API.Payload.Requests.RegulatoryApproval;
using PanelWay_Backend.API.Payload.Responses.RegulatoryApproval;
using PanelWay_Backend.API.Services.Interfaces;
using PanelWay_Backend.Domain.Entities;
using PanelWay_Backend.Domain.Paginate;
using PanelWay_Backend.Repository.Interfaces;

namespace PanelWay_Backend.API.Services.Implements;

public class RegulatoryApprovalService : BaseService<RegulatoryApprovalService>, IRegulatoryApprovalService
{
    public RegulatoryApprovalService(IUnitOfWork<PanelWayDbContext> unitOfWork, ILogger<RegulatoryApprovalService> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(unitOfWork, logger, mapper, httpContextAccessor)
    {
    }

    public async Task<RegulatoryApprovalResponse?> GetRegulatoryApprovalById(Guid id)
    {
        var response = await _unitOfWork.GetRepository<RegulatoryApproval>().SingleOrDefaultAsync
            (
                predicate: x => x.Id.Equals(id)
                );
        return (response != null) ? _mapper.Map<RegulatoryApprovalResponse>(response) : null;
    }

    public async Task<IPaginate<RegulatoryApprovalResponse>?> GetRegulatoryApprovalByRentalLocationId(Guid rentalLocationId, int page, int size)
    {
        var responses = await _unitOfWork.GetRepository<RegulatoryApproval>().GetPagingListAsync
        (
            predicate: x => x.RentalLocationId.Equals(rentalLocationId),
            page: page,
            size: size,
            orderBy: x => x.OrderByDescending(x => x.IssueDate)
        );
        return (responses != null) ? _mapper.Map<IPaginate<RegulatoryApprovalResponse>>(responses) : null;
    }
    
    public Task<RegulatoryApprovalResponse> CreateNewRegulatoryApproval(CreateRegulatoryApprovalRequest request)
    {
        throw new NotImplementedException();
    }
}