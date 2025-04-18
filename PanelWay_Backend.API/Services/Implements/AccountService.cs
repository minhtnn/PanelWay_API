using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Extensions;
using PanelWay_Backend.API.Enums;
using PanelWay_Backend.API.Payload.Requests.Accounts;
using PanelWay_Backend.API.Payload.Responses.Accounts;
using PanelWay_Backend.API.Services.Interfaces;
using PanelWay_Backend.Domain.Entities;
using PanelWay_Backend.Domain.Entities;
using PanelWay_Backend.Domain.Paginate;
using PanelWay_Backend.Repository.Interfaces;

namespace PanelWay_Backend.API.Services.Implements;

public class AccountService : BaseService<AccountService>, IAccountService
{
    public AccountService(IUnitOfWork<PanelWayDbContext> unitOfWork, ILogger<AccountService> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(unitOfWork, logger, mapper, httpContextAccessor)
    {
    }

    public async Task<IPaginate<AccountResponse>?> GetAccountsPaging(int size = 10,int page = 1)
    {
        var response = await _unitOfWork.GetRepository<Account>().GetPagingListAsync
        (
            predicate: x => !x.Role.Equals(nameof(RoleEnum.Manager)) && !x.Role.Equals(nameof(RoleEnum.Admin)),
            include: x => x.Include(x => x.User),
            size: size,
            page: page,
            orderBy: x => x.OrderByDescending(x => x.User.CreatedAt)
        );
        return (response != null)? _mapper.Map<IPaginate<AccountResponse>>(response) : null;
    }
    
    public async Task<AccountResponse?> GetAccountById(Guid id)
    {
        var response = await _unitOfWork.GetRepository<Account>().SingleOrDefaultAsync
            (
                predicate: x => x.Id.Equals(id),
                include: x => x.Include(x => x.User)
                );
        return (response != null)? _mapper.Map<AccountResponse>(response) : null;
    }

    public async Task<AccountResponse> GetAccountByUserId(Guid userId, string role = nameof(RoleEnum.AdvertisingClient))
    {
        var response = await _unitOfWork.GetRepository<Account>().SingleOrDefaultAsync
        (
            predicate: x => x.User.Id.Equals(userId) && x.Role.Equals(role),
            include: x => x.Include(x => x.User)
        );
        return (response != null) ? _mapper.Map<AccountResponse>(response) : null;
    }

    public Task<AccountResponse> CreateNewAccount(CreateAccountRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<AccountResponse> UpdateAccount(UpdateAccountRequest request)
    {
        throw new NotImplementedException();
    }
}