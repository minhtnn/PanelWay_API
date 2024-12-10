using AutoMapper;
using PanelWay_Backend.API.Constants;
using PanelWay_Backend.API.Payload.Requests.PaymentTypes;
using PanelWay_Backend.API.Payload.Responses.PanelTypes;
using PanelWay_Backend.API.Payload.Responses.PaymentTypes;
using PanelWay_Backend.API.Services.Interfaces;
using PanelWay_Backend.Domain.Entities;
using PanelWay_Backend.Repository.Interfaces;

namespace PanelWay_Backend.API.Services.Implements;

public class PaymentTypeService : BaseService<PaymentTypeService>, IPaymentTypeService
{
    public PaymentTypeService(IUnitOfWork<PanelWayDbContext> unitOfWork, ILogger<PaymentTypeService> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(unitOfWork, logger, mapper, httpContextAccessor)
    {
    }

    public async Task<ICollection<PaymentTypeResponse>> GetPaymentTypeList()
    {
        var responses = await _unitOfWork.GetRepository<PaymentType>().GetListAsync();
        return _mapper.Map<ICollection<PaymentTypeResponse>>(responses);
    }

    public async Task<PaymentTypeResponse?> UpdatePaymentType(UpdatePaymentTypeRequest request)
    {
        //Check new Guid exists in DB
        var paymentTypeId = await _unitOfWork.GetRepository<PaymentType>().SingleOrDefaultAsync
        (
            selector: x => x.Id,
            predicate: x => x.Id.Equals(request.Id)
        );
        if (paymentTypeId == null) throw new BadHttpRequestException(MessageConstant.PanelType.NotFindPanelType);
        var updatePanelType = _mapper.Map<PaymentType>(request);
        _unitOfWork.GetRepository<PaymentType>().UpdateAsync(updatePanelType);
        var isSuccessful = (await _unitOfWork.CommitAsync()) > 0;
        return isSuccessful ? _mapper.Map<PaymentTypeResponse>(updatePanelType) : null;
    }
}