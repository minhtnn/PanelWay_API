using PanelWay_Backend.API.Payload.Requests.Users;
using PanelWay_Backend.API.Payload.Responses.Users;

namespace PanelWay_Backend.API.Services.Interfaces;

public interface IUserService
{
    Task<UserResponse> GetUserById(Guid id);
    Task<UserResponse> CreateNewUser(CreateUserRequest request);
    Task<UserResponse> UpdateUser(UpdateUserRequest request);
}