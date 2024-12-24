using AutoMapper;
using FirebaseAdmin.Auth;
using Microsoft.EntityFrameworkCore;
using PanelWay_Backend.API.Constants;
using PanelWay_Backend.API.Enums;
using PanelWay_Backend.API.Payload.Requests;
using PanelWay_Backend.API.Payload.Requests.Accounts;
using PanelWay_Backend.API.Payload.Requests.Authentication;
using PanelWay_Backend.API.Payload.Requests.Firebase;
using PanelWay_Backend.API.Payload.Requests.Users;
using PanelWay_Backend.API.Payload.Responses;
using PanelWay_Backend.API.Payload.Responses.Authentication;
using PanelWay_Backend.API.Services.Interfaces;
using PanelWay_Backend.API.Utils;
using PanelWay_Backend.Domain.Entities;
using PanelWay_Backend.Repository.Interfaces;

namespace PanelWay_Backend.API.Services.Implements;

public class AuthenticationService : BaseService<AuthenticationService>, IAuthenticationService
{
    public AuthenticationService(IUnitOfWork<PanelWayDbContext> unitOfWork, ILogger<AuthenticationService> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(unitOfWork, logger, mapper, httpContextAccessor)
    {
    }


    public async Task<string?> Login(LoginRequest request)
    {
        var account = await _unitOfWork.GetRepository<Account>().SingleOrDefaultAsync(
            predicate: x => x.User.Email.Equals(request.Email) 
                            && x.User.Password.Equals(request.Password)
                            && x.Role.Equals(request.Role),
            include: x => x.Include(x => x.User)
            );
        if (account == null) throw new BadHttpRequestException(MessageConstant.Authentication.InvalidUsernameOrPassword);
        var loginResponse = new LoginResponse()
        {
            Email = request.Email,
            Role = request.Role
        };
        return JwtUtil.GenerateJwtToken(loginResponse);
    }

    public Task<string?> SignUpForCustomer(SignUpRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ChangePasswordForCustomer(ChangePasswordRequest request)
    {
        throw new NotImplementedException();
    }

    public async Task<DataReponse> GetUser(VerifyTokenRequest request)
    {
        try
        {
            var firebaseToken = await FirebaseAuth.DefaultInstance.VerifyIdTokenAsync(request.IdToken);

            return new DataReponse()
            {
                Message = "Token is valid",
                Data = firebaseToken
            };
        }
        catch (Exception ex)
        {
            return new DataReponse()
            {
                Message = "Invalid Token",
                Data = "Error: " + ex.Message
            };
        }
    }
    public async Task<DataReponse> SaveNewUser(AuthenticationRequest request)
    {
        try
        {
            var userRecord = await FirebaseAuth.DefaultInstance.GetUserAsync(request.Uid);
            var existingUser = await _unitOfWork.GetRepository<User>().SingleOrDefaultAsync(
                predicate: x => x.Email.Equals(userRecord.Email)
                );
            if (existingUser != null) throw new BadHttpRequestException(MessageConstant.User.ExistUser);
            var newUserId = Guid.NewGuid();
            var existUserId = await _unitOfWork.GetRepository<User>().SingleOrDefaultAsync(
                selector: x => x.Id,
                predicate: x => x.Id.Equals(newUserId)
                );
            if (!existUserId.Equals(Guid.Empty))
            {
                do
                {
                    newUserId = Guid.NewGuid();
                    existUserId = await _unitOfWork.GetRepository<User>().SingleOrDefaultAsync(
                        selector: x => x.Id,
                        predicate: x => x.Id.Equals(newUserId)
                    );
                } while (!existUserId.Equals(Guid.Empty));
            }
            var newUserRequest = new CreateUserRequest()
            {
                Id = newUserId,
                FullName = userRecord.DisplayName,
                Email = userRecord.Email,
                PhoneNumber = userRecord.PhoneNumber,
                UserName = userRecord.Email?.Split('@')[0],
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                Status = "Active",
                VerificationStatus = userRecord.EmailVerified
            };
            var newUser = _mapper.Map<User>(newUserRequest);
            await _unitOfWork.GetRepository<User>().InsertAsync(newUser);
            var addUser = (await _unitOfWork.CommitAsync()) > 0;
            if (addUser) throw new BadHttpRequestException(MessageConstant.User.CreateUserFail);
            var newAccountId = Guid.NewGuid();
            var existAccountId = await _unitOfWork.GetRepository<User>().SingleOrDefaultAsync(
                selector: x => x.Id,
                predicate: x => x.Id.Equals(newUserId)
            );
            if (!existAccountId.Equals(Guid.Empty))
            {
                do
                {
                    newAccountId = Guid.NewGuid();
                    existAccountId = await _unitOfWork.GetRepository<User>().SingleOrDefaultAsync(
                        selector: x => x.Id,
                        predicate: x => x.Id.Equals(newAccountId)
                    );
                } while (!existAccountId.Equals(Guid.Empty));
            }
            var newAccountRequest = new CreateAccountRequest()
            {
                Id = newAccountId,
                Status = "Active",
                Role = nameof(RoleEnum.AdvertisingClient),
                IndividualPoint = 100,
                UserId = newUserId
            };
            var newAccount = _mapper.Map<Account>(newAccountRequest);
            await _unitOfWork.GetRepository<Account>().InsertAsync(newAccount);
            var addAccount = (await _unitOfWork.CommitAsync()) > 0;
            return (addAccount)? new DataReponse()
            {
                Message = "Token is valid",
                Data = userRecord
            } : new DataReponse()
            {
                Message = "Invalid Token",
            };
        }
        catch (Exception ex)
        {
            return new DataReponse()
            {
                Message = "Invalid Token",
                Data = "Error: " + ex.Message
            };
        }
    }
}