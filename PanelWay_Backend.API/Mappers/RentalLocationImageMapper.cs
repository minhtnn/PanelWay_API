using AutoMapper;
using PanelWay_Backend.API.Payload.Requests.RentalLocationImage;
using PanelWay_Backend.API.Payload.Responses.RentalLocationImages;
using PanelWay_Backend.Domain.Entities;

namespace PanelWay_Backend.API.Mappers;

public class RentalLocationImageMapper : Profile
{
    public RentalLocationImageMapper()
    {
        CreateMap<CreateRentalLocationImageRequest, RentalLocationImage>();
        CreateMap<UpdateRentalLocationImageRequest, RentalLocationImage>();
        CreateMap<RentalLocationImage, RentalLocationImageResponse>();
    }
}