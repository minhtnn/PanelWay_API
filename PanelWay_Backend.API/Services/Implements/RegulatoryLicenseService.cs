using AutoMapper;
using PanelWay_Backend.API.Payload.Requests.RegulatoryLicenses;
using PanelWay_Backend.API.Payload.Responses.RegulatoryLicenses;
using PanelWay_Backend.API.Services.Interfaces;
using PanelWay_Backend.Domain.Entities;
using PanelWay_Backend.Repository.Interfaces;

namespace PanelWay_Backend.API.Services.Implements;

public class RegulatoryLicenseService : BaseService<RegulatoryLicenseService>, IRegulatoryLicenseService
{
    public RegulatoryLicenseService(IUnitOfWork<PanelWayDbContext> unitOfWork, ILogger<RegulatoryLicenseService> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(unitOfWork, logger, mapper, httpContextAccessor)
    {
    }

    public async Task<RegulatoryLicenseResponse?> GetRegulatoryLicenseById(Guid id)
    {
        var response = await _unitOfWork.GetRepository<RegulatoryLicense>().SingleOrDefaultAsync
            (
                predicate: x => x.Id.Equals(id)
                );
        return (response != null) ? _mapper.Map<RegulatoryLicenseResponse>(response) : null;
    }

    public async Task<ICollection<RegulatoryLicenseResponse>> GetRegulatoryLicenseByRegulatoryApprovalId(Guid regulatoryApprovalId)
    {
        var responses = await _unitOfWork.GetRepository<RegulatoryLicense>().GetListAsync
            (
                predicate: x => x.RegulatoryApprovalId.Equals(regulatoryApprovalId)
                );
        return _mapper.Map<ICollection<RegulatoryLicenseResponse>>(responses);
    }

    public Task<RegulatoryLicenseResponse> CreateNewRegulatoryLicense(CreateRegulatoryLicenseRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<RegulatoryLicenseResponse> UpdateRegulatoryLicense(UpdateRegulatoryLicenseRequest request)
    {
        throw new NotImplementedException();
    }
}