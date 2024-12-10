using PanelWay_Backend.API.Payload.Requests.Accounts;
using PanelWay_Backend.API.Payload.Responses.Accounts;

namespace PanelWay_Backend.API.Services.Interfaces;

public interface IAccountService
{
    Task<AccountResponse?> GetAccountById(Guid id);
    Task<AccountResponse> GetAccountByUserId(Guid id, string role);
    Task<AccountResponse> CreateNewAccount(CreateAccountRequest request);
    Task<AccountResponse> UpdateAccount(UpdateAccountRequest request);
}