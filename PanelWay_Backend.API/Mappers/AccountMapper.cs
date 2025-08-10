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
        CreateMap<Account, AccountResponse>()
            .ForMember(dest => dest.Id,
                otp => otp.MapFrom(src => src.Id))
            .ForMember(dest => dest.AvatarUrl, 
                otp => otp.MapFrom(src => src.AvatarUrl))
            .ForMember(dest => dest.Status, 
                otp => otp.MapFrom(src => src.Status))
            .ForMember(dest => dest.Role, 
                otp => otp.MapFrom(src => src.Role))
            .ForMember(dest => dest.IndividualPoint, 
                otp => otp.MapFrom(src => src.IndividualPoint))
            .ForMember(dest => dest.UserId, 
                otp => otp.MapFrom(src => src.UserId))
            .ForMember(dest => dest.FullName, 
                otp => otp.MapFrom(src => src.User.FullName))
            .ForMember(dest => dest.Gender, 
                otp => otp.MapFrom(src => src.User.Gender))
            .ForMember(dest => dest.Email, 
                otp => otp.MapFrom(src => src.User.Email))
            .ForMember(dest => dest.PhoneNumber, 
                otp => otp.MapFrom(src => src.User.PhoneNumber))
            .ForMember(dest => dest.Age, 
                otp => otp.MapFrom(src => src.User.Age))
            .ForMember(dest => dest.UserName, 
                otp => otp.MapFrom(src => src.User.UserName))
            .ForMember(dest => dest.Password, 
                otp => otp.MapFrom(src => src.User.Password))
            .ForMember(dest => dest.CreatedAt, 
                otp => otp.MapFrom(src => src.User.CreatedAt))
            .ForMember(dest => dest.UpdatedAt, 
                otp => otp.MapFrom(src => src.User.UpdatedAt))
            .ForMember(dest => dest.UserStatus, 
                otp => otp.MapFrom(src => src.User.Status))
            .ForMember(dest => dest.VerificationStatus, 
                otp => otp.MapFrom(src => src.User.VerificationStatus))
            ;
    }
}