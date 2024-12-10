using AutoMapper;
using PanelWay_Backend.API.Payload.Requests.RentalLocations;
using PanelWay_Backend.API.Payload.Responses.RentalLocations;
using PanelWay_Backend.Domain.Entities;

namespace PanelWay_Backend.API.Mappers;

public class RentalLocationMapper : Profile
{
    public RentalLocationMapper()
    {
        CreateMap<CreateRentalLocationRequest, RentalLocation>();
        CreateMap<UpdateRentalLocationRequest, RentalLocation>();
        CreateMap<RentalLocation, RentalLocationResponse>();
    }
}