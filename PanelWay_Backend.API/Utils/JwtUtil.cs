using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using PanelWay_Backend.API.Configurations;
using PanelWay_Backend.API.Constants;
using PanelWay_Backend.API.Enums;
using PanelWay_Backend.API.Payload.Responses.Authentication;
using PanelWay_Backend.Domain.Entities;

namespace PanelWay_Backend.API.Utils;

public class JwtUtil
{
    public static string? GenerateJwtToken(LoginResponse account)
    {
        JwtSecurityTokenHandler jwtHandler = new JwtSecurityTokenHandler();
        SymmetricSecurityKey secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtConfig.SecretKey));
        var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256Signature);
        List<Claim> claims = new List<Claim>()
        {
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Email, account.PhoneNumber!),
            new Claim(ClaimTypes.Role, account.Role!)
        };
        var expires = DateTime.Now.AddMinutes(30);
        var token = new JwtSecurityToken(SystemConstant.Name, null, claims, notBefore: DateTime.Now, expires, credentials);
        return jwtHandler.WriteToken(token);
    }
}