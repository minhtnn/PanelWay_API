using AutoMapper;
using PanelWay_Backend.API.Payload.Requests.AdContents;
using PanelWay_Backend.API.Payload.Responses.AdContents;
using PanelWay_Backend.Domain.Entities;

namespace PanelWay_Backend.API.Mappers;

public class AdContentMapper : Profile
{
    public AdContentMapper()
    {
        CreateMap<CreateAdContentRequest, AdContent>();
        CreateMap<UpdateAdContentRequest, AdContent>();
        CreateMap<AdContent, AdContentResponse>();
    }
}