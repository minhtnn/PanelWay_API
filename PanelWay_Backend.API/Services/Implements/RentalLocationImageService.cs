using AutoMapper;
using PanelWay_Backend.API.Payload.Requests.RentalLocationImage;
using PanelWay_Backend.API.Payload.Responses.RentalLocationImages;
using PanelWay_Backend.API.Services.Interfaces;
using PanelWay_Backend.Domain.Entities;
using PanelWay_Backend.Repository.Interfaces;

namespace PanelWay_Backend.API.Services.Implements;

public class RentalLocationImageService : BaseService<RentalLocationImageService>, IRentalLocationImageService
{
    public RentalLocationImageService(IUnitOfWork<PanelWayDbContext> unitOfWork, ILogger<RentalLocationImageService> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(unitOfWork, logger, mapper, httpContextAccessor)
    {
    }

    public async Task<ICollection<RentalLocationImageResponse>> GetAllImagesByRentalLocationId(Guid id)
    {
        var responses = await _unitOfWork.GetRepository<RentalLocationImage>().GetListAsync(
                predicate: x => x.RentalLocationId.ToString()!.Equals(id.ToString())
            );
        return (responses != null)? _mapper.Map<ICollection<RentalLocationImageResponse>>(responses) : null;
    }

    public Task<RentalLocationImageResponse> AddImageByRentalLocationId(CreateRentalLocationImageRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<RentalLocationImageResponse> UpdateImageByRentalLocationId(UpdateRentalLocationImageRequest request)
    {
        throw new NotImplementedException();
    }
}