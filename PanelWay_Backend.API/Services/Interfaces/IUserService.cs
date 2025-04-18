using PanelWay_Backend.API.Payload.Requests.Users;
using PanelWay_Backend.API.Payload.Responses.Users;
using PanelWay_Backend.Domain.Paginate;

namespace PanelWay_Backend.API.Services.Interfaces;

public interface IUserService
{
    Task<IPaginate<UserResponse>> GetUsers(int page, int size);
    Task<UserResponse> GetUserById(Guid id);
    Task<int> GetTotalUser();
    Task<int> GetTotalUserByDate(DateTime startDate, DateTime endDate);
    Task<int> GetTotalUserByAge(int minAge, int maxAge);
    Task<UserResponse> CreateNewUser(CreateUserRequest request);
    Task<UserResponse> UpdateUser(UpdateUserRequest request);
}