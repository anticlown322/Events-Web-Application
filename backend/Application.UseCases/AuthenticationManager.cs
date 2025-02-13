using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Domain.Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Application.Contracts;
using Application.Exceptions.Specific;
using Shared.DTO.User;

namespace Application.UseCases;

public class AuthenticationManager(
    UserManager<User> userManager,
    IConfiguration configuration)
    : IAuthenticationManager
{
    private User? _user;

    public async Task<TokenDto> CreateToken(User user, bool populateExp)
    {
        _user = user;
        
        var signingCredentials = GetSigningCredentials();
        var claims = await GetClaims();

        var tokenOptions = GenerateTokenOptions(signingCredentials, claims);

        var refreshToken = GenerateRefreshToken();

        _user.RefreshToken = refreshToken;

        if (populateExp)
            _user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);

        await userManager.UpdateAsync(_user);

        var accessToken = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

        return new TokenDto(accessToken, refreshToken);
    }
    
    public async Task<TokenDto> RefreshToken(TokenDto tokenDto)
    {
        var principal = GetPrincipalFromExpiredToken(tokenDto.AccessToken);
        var user = await userManager.FindByNameAsync(principal.Identity.Name);
    
        if (user == null || user.RefreshToken != tokenDto.RefreshToken ||
            user.RefreshTokenExpiryTime <= DateTime.Now)
            throw new RefreshTokenBadRequest();
        
        return await CreateToken(user, populateExp: false);
    }

    private string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }
    
    private SigningCredentials GetSigningCredentials()
    {
        var jwtSettings = configuration.GetSection("JwtSettings");
        var secretKey = jwtSettings.GetSection("validIssuer").Value;
        
        var key = Encoding.UTF8.GetBytes(secretKey!);
        var secret = new SymmetricSecurityKey(key);
        
        return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
    }

    private async Task<List<Claim>> GetClaims()
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, _user.UserName)
        };
        
        var roles = await userManager.GetRolesAsync(_user);
       
        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }
        
        return claims;
    }

    private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
    {
        var jwtSettings = configuration.GetSection("JwtSettings");
        var tokenOptions = new JwtSecurityToken
        (
            issuer: jwtSettings["validIssuer"],
            audience: jwtSettings["validAudience"],
            claims: claims,
            expires:
            DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings.GetSection("expires").Value)),
            signingCredentials: signingCredentials
        );
        return tokenOptions;
    }
    
    private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
    {
        var jwtSettings = configuration.GetSection("JwtSettings");
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtSettings.GetSection("validIssuer").Value)),
            ValidateLifetime = true,
            ValidIssuer = jwtSettings["validIssuer"],
            ValidAudience = jwtSettings["validAudience"]
        };
        
        var tokenHandler = new JwtSecurityTokenHandler();
        SecurityToken securityToken;
        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
        
        var jwtSecurityToken = securityToken as JwtSecurityToken;
        if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                StringComparison.InvariantCultureIgnoreCase))
        {
            throw new SecurityTokenException("Invalid token");
        }
        
        return principal;
    }
}