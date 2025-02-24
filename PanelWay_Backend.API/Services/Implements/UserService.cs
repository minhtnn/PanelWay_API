using AutoMapper;
using PanelWay_Backend.API.Payload.Requests.Users;
using PanelWay_Backend.API.Payload.Responses.Users;
using PanelWay_Backend.API.Services.Interfaces;
using PanelWay_Backend.Domain.Entities;
using PanelWay_Backend.Repository.Interfaces;

namespace PanelWay_Backend.API.Services.Implements;

public class UserService : BaseService<UserService>, IUserService
{
    public UserService(IUnitOfWork<PanelWayDbContext> unitOfWork, ILogger<UserService> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(unitOfWork, logger, mapper, httpContextAccessor)
    {
    }

    public async Task<UserResponse> GetUserById(Guid id)
    {
        var response = await _unitOfWork.GetRepository<User>().SingleOrDefaultAsync
            (
                predicate: x => x.Id.Equals(id)
                );
        return (response != null) ? _mapper.Map<UserResponse>(response) : null;
    }

    public async Task<int> GetTotalUser()
    {
        var response = await _unitOfWork.GetRepository<User>().CountAsync();
        return response;
    }

    public async Task<int> GetTotalUserByDate(DateTime startDate, DateTime endDate)
    {
        var response = await _unitOfWork.GetRepository<User>().CountAsync(
            predicate: x => (x.CreatedAt >= startDate && x.CreatedAt <= endDate)
        );
        return response;
    }

    public async Task<int> GetTotalUserByAge(int minAge, int maxAge)
    {
        var response = await _unitOfWork.GetRepository<User>().CountAsync(
                predicate: x => (x.Age >= minAge && x.Age <= maxAge)
            );
        return response;
    }

    public Task<UserResponse> CreateNewUser(CreateUserRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<UserResponse> UpdateUser(UpdateUserRequest request)
    {
        throw new NotImplementedException();
    }
}