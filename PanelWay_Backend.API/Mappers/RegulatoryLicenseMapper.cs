using AutoMapper;
using PanelWay_Backend.API.Payload.Requests.RegulatoryLicenses;
using PanelWay_Backend.API.Payload.Responses.RegulatoryLicenses;
using PanelWay_Backend.Domain.Entities;

namespace PanelWay_Backend.API.Mappers;

public class RegulatoryLicenseMapper : Profile
{
    public RegulatoryLicenseMapper()
    {
        CreateMap<CreateRegulatoryLicenseRequest, RegulatoryLicense>();
        CreateMap<UpdateRegulatoryLicenseRequest, RegulatoryLicense>();
        CreateMap<RegulatoryLicense, RegulatoryLicenseResponse>();
    }
}