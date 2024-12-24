using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PanelWay_Backend.API.Constants;
using PanelWay_Backend.API.Payload.Requests.Authentication;
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
}