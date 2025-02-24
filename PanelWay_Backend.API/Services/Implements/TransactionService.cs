using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PanelWay_Backend.API.Enums;
using PanelWay_Backend.API.Payload.Requests.Transactions;
using PanelWay_Backend.API.Payload.Responses.Transactions;
using PanelWay_Backend.API.Services.Interfaces;
using PanelWay_Backend.Domain.Entities;
using PanelWay_Backend.Domain.Paginate;
using PanelWay_Backend.Repository.Interfaces;

namespace PanelWay_Backend.API.Services.Implements;

public class TransactionService : BaseService<TransactionService>, ITransactionService
{
    public TransactionService(IUnitOfWork<PanelWayDbContext> unitOfWork, ILogger<TransactionService> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(unitOfWork, logger, mapper, httpContextAccessor)
    {
    }

    public async Task<IPaginate<TransactionResponse>> GetTransactionPaging(string status, int page, int size)
    {
        var responses = await _unitOfWork.GetRepository<Transaction>().GetPagingListAsync(
                predicate: x => ((string.IsNullOrEmpty(status) || x.Status.Equals(status))),
                page: page,
                size: size
            );
        return (responses != null) ? _mapper.Map<IPaginate<TransactionResponse>>(responses) : null;;
    }
    
    public async Task<TransactionResponse> GetTransactionById(Guid id)
    {
        var response = await _unitOfWork.GetRepository<Transaction>().SingleOrDefaultAsync
            (
                predicate: x => x.Id.Equals(id)
                );
        return (response != null) ? _mapper.Map<TransactionResponse>(response) : null;
    }

    public async Task<IPaginate<TransactionResponse>> GetTransactionByAccountId(Guid id, int page, int size)
    {
        var responses = await _unitOfWork.GetRepository<Transaction>().GetPagingListAsync
            (
                predicate: x => x.AccountId.Equals(id),
                page : page,
                size: size
                );
        return (responses != null) ? _mapper.Map<IPaginate<TransactionResponse>>(responses) : null;
    }

    public async Task<IPaginate<TransactionResponse>> GetTransactionByUserSubscriptionIdAndPaymentId(Guid userSubscriptionId, Guid paymentId, int page, int size)
    {
        // var responses = await _unitOfWork.GetRepository<Transaction>().GetPagingListAsync
        // (
        //     predicate: x => x.UserSubscriptionId.Equals(userSubscriptionId) && x.PaymentId.Equals(paymentId),
        //     orderBy: x => x.OrderByDescending(x => x.TransactionDate),
        //     page:page,
        //     size:size
        // );
        // return (responses != null) ? _mapper.Map<IPaginate<TransactionResponse>>(responses) : null;
        throw new NotImplementedException();
    }
    public async Task<double> GetTotalRevue()
    {
        var response = (await _unitOfWork.GetRepository<Transaction>().GetListAsync(
            selector: x => x.Amount,
            predicate: x => x.Status.Equals(nameof(PayOSStatusEnum.PAID))
        )).Sum(n => n ?? 0);
        return response;
    }
    public Task<TransactionResponse> CreateNewTransaction(CreateTransactionRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<TransactionResponse> UpdateTransaction(UpdateTransactionRequest request)
    {
        throw new NotImplementedException();
    }
}