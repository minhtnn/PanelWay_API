using AutoMapper;
using PanelWay_Backend.API.Payload.Requests.Accounts;
using PanelWay_Backend.API.Payload.Responses.Accounts;
using PanelWay_Backend.Domain.Entities;

namespace PanelWay_Backend.API.Mappers;

public class AccountMapper : Profile
{
    public AccountMapper()
    {
        CreateMap<CreateAccountRequest, Account>();
        CreateMap<UpdateAccountRequest, Account>();
        CreateMap<Account, AccountResponse>();
    }
}