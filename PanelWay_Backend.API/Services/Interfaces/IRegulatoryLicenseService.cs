using PanelWay_Backend.API.Payload.Requests.RegulatoryLicenses;
using PanelWay_Backend.API.Payload.Responses.RegulatoryLicenses;

namespace PanelWay_Backend.API.Services.Interfaces;

public interface IRegulatoryLicenseService
{
    Task<RegulatoryLicenseResponse?> GetRegulatoryLicenseById(Guid id);
    Task<ICollection<RegulatoryLicenseResponse>> GetRegulatoryLicenseByRegulatoryApprovalId(Guid id);
    Task<RegulatoryLicenseResponse> CreateNewRegulatoryLicense(CreateRegulatoryLicenseRequest request);
    Task<RegulatoryLicenseResponse> UpdateRegulatoryLicense(UpdateRegulatoryLicenseRequest request);
}