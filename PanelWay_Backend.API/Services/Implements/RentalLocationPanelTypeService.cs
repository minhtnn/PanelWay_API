using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PanelWay_Backend.API.Constants;
using PanelWay_Backend.API.Payload.Requests.RentalLocationPanelTypes;
using PanelWay_Backend.API.Payload.Responses.PanelTypes;
using PanelWay_Backend.API.Payload.Responses.RentalLocationPanelTypes;
using PanelWay_Backend.API.Services.Interfaces;
using PanelWay_Backend.Domain.Entities;
using PanelWay_Backend.Repository.Interfaces;

namespace PanelWay_Backend.API.Services.Implements;

public class RentalLocationPanelTypeService : BaseService<RentalLocationPanelTypeService>, IRentalLocationPanelTypeService
{
    public RentalLocationPanelTypeService(IUnitOfWork<PanelWayDbContext> unitOfWork, ILogger<RentalLocationPanelTypeService> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(unitOfWork, logger, mapper, httpContextAccessor)
    {
    }

    public async Task<ICollection<RentalLocationPanelTypeResponse>?> GetRentalLocationPanelTypeByRentalLocationId(Guid id)
    {
        if (id == null || id.Equals(Guid.Empty)) throw new BadHttpRequestException(MessageConstant.RentalLocationPanelType.EmptyRentalLocationPanelTypeId);
        var response = await _unitOfWork.GetRepository<RentalLocationPanelType>().GetListAsync
            (
                predicate: x=> x.RentalLocationId.Equals(id)
                );
        return (response.Count > 0)? _mapper.Map<ICollection<RentalLocationPanelTypeResponse>>(response) : null;
    }
    
    public async Task<RentalLocationPanelTypeResponse?> CreateNewRentalLocationPanelType(CreateRentalLocationPanelTypeRequest request)
    {
        //Check Rental location exist
        var rentalLocationId = await _unitOfWork.GetRepository<RentalLocation>().SingleOrDefaultAsync
            (
                selector:x => x.Id,
                predicate: x => x.Id.Equals(request.RentalLocationId)
                );
        if (rentalLocationId == null || rentalLocationId.Equals(Guid.Empty)) 
            throw new BadHttpRequestException(MessageConstant.RentalLocation.NotFindRentalLocation);
        //Check panel type exist
        var panelTypeId = await _unitOfWork.GetRepository<PanelType>().SingleOrDefaultAsync
            (
                selector: x => x.Id,
                predicate: x => x.Id.Equals(request.PanelTypeId)
                );
        if (panelTypeId == null || panelTypeId.Equals(Guid.Empty)) 
            throw new BadHttpRequestException(MessageConstant.PanelType.NotFindPanelType);
        
        //Check Rental location and panel type exist
        var rlpt = await _unitOfWork.GetRepository<RentalLocationPanelType>().SingleOrDefaultAsync(
                predicate: x => x.RentalLocationId.Equals(request.RentalLocationId) && x.PanelTypeId.Equals(request.PanelTypeId)
            );
        if (rlpt != null) throw new BadHttpRequestException(MessageConstant.RentalLocationPanelType.ExistRentalLocationPanelTypeId);
        //Add Rental location and panel type exist
        var rentalLocationPanelType = _mapper.Map<RentalLocationPanelType>(request);
        await _unitOfWork.GetRepository<RentalLocationPanelType>().InsertAsync(rentalLocationPanelType);
        var isSuccessful = (await _unitOfWork.CommitAsync()) > 0;
        return isSuccessful ? _mapper.Map<RentalLocationPanelTypeResponse>(rentalLocationPanelType) : null;
    }

    public async Task<RentalLocationPanelTypeResponse?> UpdateRentalLocationPanelType(UpdateRentalLocationPanelTypeRequest request)
    {
        //Check Rental location and panel type exist
        var rlpt = await _unitOfWork.GetRepository<RentalLocationPanelType>().SingleOrDefaultAsync(
            predicate: x => x.RentalLocationId.Equals(request.RentalLocationId) && x.PanelTypeId.Equals(request.PanelTypeId)
        );
        if (rlpt == null) throw new BadHttpRequestException(MessageConstant.RentalLocationPanelType.NotFindRentalLocationPanelType);
        _unitOfWork.GetRepository<RentalLocationPanelType>().DeleteAsync(rlpt);
        var isSuccessful = (await _unitOfWork.CommitAsync()) > 0;
        return isSuccessful ? _mapper.Map<RentalLocationPanelTypeResponse>(rlpt) : null;
    }
}