using PanelWay_Backend.API.Payload.Requests.Transactions;
using PanelWay_Backend.API.Payload.Responses.Transactions;
using PanelWay_Backend.Domain.Paginate;

namespace PanelWay_Backend.API.Services.Interfaces;

public interface ITransactionService
{
    Task<IPaginate<TransactionResponse>> GetTransactionPaging(string status, int page, int size);
    Task<double> GetTotalRevue();
    Task<TransactionResponse> GetTransactionById(Guid id);
    Task<IPaginate<TransactionResponse>> GetTransactionByAccountId(Guid id, int page, int size);
    Task<IPaginate<TransactionResponse>>GetTransactionByUserSubscriptionIdAndPaymentId(Guid userSubscriptionId, Guid paymentId, int page, int size);
    Task<TransactionResponse> CreateNewTransaction(CreateTransactionRequest request);
    Task<TransactionResponse> UpdateTransaction(UpdateTransactionRequest request);
}