using System.Diagnostics;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PanelWay_Backend.API.Constants;
using PanelWay_Backend.API.Enums;
using PanelWay_Backend.API.Payload.Requests.AdContents;
using PanelWay_Backend.API.Payload.Responses.AdContents;
using PanelWay_Backend.API.Services.Interfaces;
using PanelWay_Backend.Domain.Entities;
using PanelWay_Backend.Repository.Interfaces;

namespace PanelWay_Backend.API.Services.Implements;

public class AdContentService : BaseService<AdContentService>, IAdContentService
{
    public AdContentService(IUnitOfWork<PanelWayDbContext> unitOfWork, ILogger<AdContentService> logger, IMapper mapper,
        IHttpContextAccessor httpContextAccessor) : base(unitOfWork, logger, mapper, httpContextAccessor)
    {
    }

    public async Task<AdContentResponse> GetAdContentById(Guid id)
    {
        var response = await _unitOfWork.GetRepository<AdContent>().SingleOrDefaultAsync
        (
            predicate: x => x.Id.Equals(id)
        );
        return (response != null) ? _mapper.Map<AdContentResponse>(response) : null;
    }

    public async Task<ICollection<AdContentResponse>> GetAdContentByAdvertisingClientId(Guid advertisingClientId)
    {
        var response = await _unitOfWork.GetRepository<AdContent>().GetListAsync
        (
            predicate: x =>
                x.AdvertisingClient.Id.Equals(advertisingClientId) &&
                x.AdvertisingClient.Role.Equals(nameof(RoleEnum.AdvertisingClient)),
            include: x => x.Include(x => x.AdvertisingClient)
        );
        return (response != null) ? _mapper.Map<ICollection<AdContentResponse>>(response) : null;
    }

    public async Task<AdContentResponse?> CreateNewAdContent(CreateAdContentRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Code.Trim()))
            throw new BadHttpRequestException(MessageConstant.PanelWaySystem.EmptyField);

        var adContentId = await _unitOfWork.GetRepository<AdContent>().SingleOrDefaultAsync
        (
            selector: x => x.Id,
            predicate: x => x.Id.Equals(request.Id)
        );
        if (adContentId != null && !adContentId.Equals(Guid.Empty))
        {
            do
            {
                request.NewGuid();
                adContentId = await _unitOfWork.GetRepository<AdContent>().SingleOrDefaultAsync
                (
                    selector: x => x.Id,
                    predicate: x => x.Id.Equals(request.Id)
                );
            } while (adContentId != null && !adContentId.Equals(Guid.Empty));
        }

        ;
        var adContentCode = await _unitOfWork.GetRepository<AdContent>().SingleOrDefaultAsync
        (
            selector: x => x.Code,
            predicate: x => x.Code.Equals(request.Code)
        );
        if (adContentCode != null)
            throw new BadHttpRequestException(MessageConstant.AdContent.ExistAdContentCode);

        var adContent = _mapper.Map<AdContent>(request);
        await _unitOfWork.GetRepository<AdContent>().InsertAsync(adContent);
        var isSuccessful = (await _unitOfWork.CommitAsync()) > 0;
        return isSuccessful ? _mapper.Map<AdContentResponse>(adContent) : null;
    }

    public async Task<AdContentResponse?> UpdateAdContent(UpdateAdContentRequest request)
    {
        if (request.Id == null || request.Code == null)
            throw new BadHttpRequestException
                (MessageConstant.PanelWaySystem.EmptyField);
        var adContent = await _unitOfWork.GetRepository<AdContent>().SingleOrDefaultAsync(
            predicate: x => x.Id.Equals(request.Id) && x.Code.Equals(request.Code)
        );
        if (adContent == null) throw new BadHttpRequestException(MessageConstant.AdContent.NotFindAdContent);
        var updateAdContent = _mapper.Map<AdContent>(request);
        updateAdContent.AdvertisingClientId = adContent.AdvertisingClientId;
        _unitOfWork.GetRepository<AdContent>().UpdateAsync(updateAdContent);
        var isSuccessful = (await _unitOfWork.CommitAsync()) > 0;
        return isSuccessful ? _mapper.Map<AdContentResponse>(updateAdContent) : null;
    }
}