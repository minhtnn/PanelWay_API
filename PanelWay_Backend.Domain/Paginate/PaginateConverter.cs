using AutoMapper;

namespace PanelWay_Backend.Domain.Paginate;

public class PaginateConverter<TSource, TDestination> : ITypeConverter<Paginate<TSource>, IPaginate<TDestination>>
{
    public IPaginate<TDestination> Convert(Paginate<TSource> source, IPaginate<TDestination> destination, ResolutionContext context)
    {
        var mappedItems = source.Items.Select(item => context.Mapper.Map<TDestination>(item)).ToList();
        return new Paginate<TDestination>(mappedItems, source.TotalPages, source.Size, source.Page);
    }
}