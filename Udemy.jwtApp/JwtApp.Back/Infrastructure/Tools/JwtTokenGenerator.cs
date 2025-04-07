using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using JwtApp.Back.Core.Application.Dto;
using Microsoft.IdentityModel.Tokens;

namespace JwtApp.Back.Infrastructure.Tools;

public class JwtTokenGenerator
{
    public static TokenResponseDto GenerateJwtToken(CheckUserResponseDto dto)
    {
        var claims = new List<Claim>();
        if (!string.IsNullOrEmpty(dto.Role))
        {
            claims.Add(new Claim(ClaimTypes.Role, dto.Role));
        }

        claims.Add(new Claim(ClaimTypes.NameIdentifier, dto.Id.ToString()));
        if (!string.IsNullOrEmpty(dto.Username))
        {
            claims.Add(new Claim("Username", dto.Username));
        }

        var exprireDate = DateTime.UtcNow.AddMinutes(JwtTokenDefaults.Expire);
        
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtTokenDefaults.Key));
        SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(issuer: JwtTokenDefaults.ValidIssuer,
            audience: JwtTokenDefaults.ValidAudience, claims: claims, notBefore: DateTime.UtcNow,
            expires: exprireDate,
            signingCredentials: credentials);
        JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
        return new TokenResponseDto(handler.WriteToken(jwtSecurityToken), exprireDate);
    }
}