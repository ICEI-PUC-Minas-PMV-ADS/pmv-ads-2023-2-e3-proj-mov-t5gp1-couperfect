using CouperfectServer.Application.Services;
using CouperfectServer.Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CouperfectServer.WebApp.Services.Token;

public partial class TokenService : ITokenService
{
    private readonly byte[] TokenKey;

    public TokenService(IOptions<Options> options)
    {
        TokenKey = options.Value.TokenKey;
    }

    public string GetToken(Player user)
    {
        var claims = new Claim[] { new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()) };

        var tokenHandler = new JwtSecurityTokenHandler();

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(TokenKey), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}
