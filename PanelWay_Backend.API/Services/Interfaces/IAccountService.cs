using PanelWay_Backend.API.Payload.Requests.Accounts;
using PanelWay_Backend.API.Payload.Responses.Accounts;
using PanelWay_Backend.Domain.Paginate;

namespace PanelWay_Backend.API.Services.Interfaces;

public interface IAccountService
{
    Task<IPaginate<AccountResponse>?> GetAccountsPaging(int size = 10, int page = 1);
    Task<AccountResponse?> GetAccountById(Guid id);
    Task<AccountResponse> GetAccountByUserId(Guid id, string role);
    Task<AccountResponse> CreateNewAccount(CreateAccountRequest request);
    Task<AccountResponse> UpdateAccount(UpdateAccountRequest request);
}