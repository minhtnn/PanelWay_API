using AutoMapper;
using PanelWay_Backend.API.Payload.Requests.PanelTypes;
using PanelWay_Backend.API.Payload.Responses.PanelTypes;
using PanelWay_Backend.Domain.Entities;

namespace PanelWay_Backend.API.Mappers;

public class PanelTypeMapper : Profile
{
    public PanelTypeMapper()
    {
        CreateMap<CreatePanelTypeRequest, PanelType>();
        CreateMap<UpdatePanelTypeRequest, PanelType>();
        CreateMap<PanelType, PanelTypeResponse>();
    }
}