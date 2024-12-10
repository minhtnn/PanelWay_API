using AutoMapper;
using PanelWay_Backend.API.Payload.Requests.RentalLocationPanelTypes;
using PanelWay_Backend.API.Payload.Responses.RentalLocationPanelTypes;
using PanelWay_Backend.Domain.Entities;

namespace PanelWay_Backend.API.Mappers;

public class RentalLocationPanelTypeMapper : Profile
{
    public RentalLocationPanelTypeMapper()
    {
        CreateMap<CreateRentalLocationPanelTypeRequest, RentalLocationPanelType>();
        CreateMap<UpdateRentalLocationPanelTypeRequest, RentalLocationPanelType>();
        CreateMap<RentalLocationPanelType, RentalLocationPanelTypeResponse>();
    }
}