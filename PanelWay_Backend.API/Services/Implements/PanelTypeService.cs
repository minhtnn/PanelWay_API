using AutoMapper;
using PanelWay_Backend.API.Constants;
using PanelWay_Backend.API.Payload.Requests.PanelTypes;
using PanelWay_Backend.API.Payload.Responses.PanelTypes;
using PanelWay_Backend.API.Services.Interfaces;
using PanelWay_Backend.Domain.Entities;
using PanelWay_Backend.Domain.Paginate;
using PanelWay_Backend.Repository.Interfaces;

namespace PanelWay_Backend.API.Services.Implements;

public class PanelTypeService : BaseService<PanelTypeService>, IPanelTypeService
{
    public PanelTypeService(IUnitOfWork<PanelWayDbContext> unitOfWork, ILogger<PanelTypeService> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(unitOfWork, logger, mapper, httpContextAccessor)
    {
    }

    public async Task<PanelTypeResponse?> GetPanelTypeResponseById(Guid id)
    {
        var response = await _unitOfWork.GetRepository<PanelType>().SingleOrDefaultAsync(
            predicate: x => x.Id.Equals(id)
            );
        return (response != null)? _mapper.Map<PanelTypeResponse>(response) : null;
    }

    public async Task<ICollection<PanelTypeResponse>?> GetPanelTypeList()
    {
        var responses = await _unitOfWork.GetRepository<PanelType>().GetListAsync();
        return (responses != null)? _mapper.Map<ICollection<PanelTypeResponse>>(responses) : null;
    }

    public async Task<PanelTypeResponse?> CreateNewPanelType(CreatePanelTypeRequest request)
    {
        //Check empty code
        if (request.Id.Equals(Guid.Empty)) throw new BadHttpRequestException(MessageConstant.PanelWaySystem.EmptyField);
        //Check new Guid exists in DB
        var panelTypeId = await _unitOfWork.GetRepository<PanelType>().SingleOrDefaultAsync
        (
            selector: x => x.Id,
            predicate: x => x.Id.Equals(request.Id)
        );
        //Re-generate Guid until it is new ones
        if (panelTypeId != null && !panelTypeId.Equals(Guid.Empty))
        {
            do
            {
                request.GetNewId();
                panelTypeId = await _unitOfWork.GetRepository<AdContent>().SingleOrDefaultAsync
                (
                    selector: x => x.Id,
                    predicate: x => x.Id.Equals(request.Id)
                );
            } while (panelTypeId != null && !panelTypeId.Equals(Guid.Empty));
        }
        //Add appointment
        var panelType = _mapper.Map<PanelType>(request);
        await _unitOfWork.GetRepository<PanelType>().InsertAsync(panelType);
        var isSuccessful = (await _unitOfWork.CommitAsync()) > 0;
        return isSuccessful ? _mapper.Map<PanelTypeResponse>(panelType) : null;
    }

    public async Task<PanelTypeResponse?> UpdatePanelType(UpdatePanelTypeRequest request)
    {
        if (request.Id.Equals(Guid.Empty)) throw new BadHttpRequestException(MessageConstant.PanelWaySystem.EmptyField);
        //Check new Guid exists in DB
        var panelTypeId = await _unitOfWork.GetRepository<PanelType>().SingleOrDefaultAsync
        (
            selector: x => x.Id,
            predicate: x => x.Id.Equals(request.Id)
        );
        if (panelTypeId == null) throw new BadHttpRequestException(MessageConstant.PanelType.NotFindPanelType);
        var updatePanelType = _mapper.Map<PanelType>(request);
        _unitOfWork.GetRepository<PanelType>().UpdateAsync(updatePanelType);
        var isSuccessful = (await _unitOfWork.CommitAsync()) > 0;
        return isSuccessful ? _mapper.Map<PanelTypeResponse>(updatePanelType) : null;
    }
}