using AutoMapper;
using PanelWay_Backend.API.Payload.Requests.RegulatoryApproval;
using PanelWay_Backend.API.Payload.Responses.RegulatoryApproval;
using PanelWay_Backend.Domain.Entities;

namespace PanelWay_Backend.API.Mappers;

public class RegulatoryApprovalMapper : Profile
{
    public RegulatoryApprovalMapper()
    {
        CreateMap<CreateRegulatoryApprovalRequest, RegulatoryApproval>();
        CreateMap<UpdateRegulatoryApprovalRequest, RegulatoryApproval>();
        CreateMap<RegulatoryApproval, RegulatoryApprovalResponse>();
    }
}