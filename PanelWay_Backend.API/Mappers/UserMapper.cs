using AutoMapper;
using PanelWay_Backend.API.Payload.Requests.Users;
using PanelWay_Backend.API.Payload.Responses.Users;
using PanelWay_Backend.Domain.Entities;

namespace PanelWay_Backend.API.Mappers;

public class UserMapper : Profile
{
    public UserMapper()
    {
        CreateMap<CreateUserRequest, User>();
        CreateMap<UpdateUserRequest, User>();
        CreateMap<User, UserResponse>();
    }
}