using AutoMapper;
using PanelWay_Backend.API.Payload.Requests.Payments;
using PanelWay_Backend.API.Payload.Responses.Payments;
using PanelWay_Backend.API.Services.Interfaces;
using PanelWay_Backend.Domain.Entities;
using PanelWay_Backend.Repository.Interfaces;

namespace PanelWay_Backend.API.Services.Implements;

public class PaymentService : BaseService<PaymentService>, IPaymentService
{
    public PaymentService(IUnitOfWork<PanelWayDbContext> unitOfWork, ILogger<PaymentService> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(unitOfWork, logger, mapper, httpContextAccessor)
    {
    }

    public async Task<PaymentResponse?> GetPaymentById(Guid id)
    {
        var response = await _unitOfWork.GetRepository<Payment>().SingleOrDefaultAsync(
            predicate: x => x.Id.Equals(id)
            );
        return (response != null) ? _mapper.Map<PaymentResponse>(response) : null;
    }

    public Task<PaymentResponse> CreateNewPayment(CreatePaymentRequest request)
    {
        throw new NotImplementedException();
    }
}