using AutoMapper;

namespace PanelWay_Backend.Domain.Paginate;

public class PaginateMapper : Profile
{
    public PaginateMapper()
    {
        CreateMap(typeof(Paginate<>), typeof(IPaginate<>))
            .ConvertUsing(typeof(PaginateConverter<,>));
    }
    
}