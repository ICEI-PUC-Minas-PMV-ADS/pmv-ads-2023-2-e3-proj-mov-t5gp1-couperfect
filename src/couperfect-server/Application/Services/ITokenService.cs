using CouperfectServer.Domain.Entities;

namespace CouperfectServer.Application.Services;

public interface ITokenService
{
    string GetToken(Player user);
}
