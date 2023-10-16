using CouperfectServer.Application.Contracts.CouperfectDb;
using CouperfectServer.Application.Extensions.HashIds;
using CouperfectServer.Application.Services;
using CouperfectServer.Domain.Entities;
using FluentResults;
using FluentValidation;

namespace CouperfectServer.Application.UseCases.Players.SingUp;

public sealed record PlayerSignUpRequest(string Name, string Email, string PlainTextPassword) : IRequest<PlayerSignUpResponse>
{
    public class Validator : AbstractValidator<PlayerSignUpRequest>
    {
        public Validator()
        {
            RuleFor(x => x.Name).NotEmpty().Length(3, 255);
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.PlainTextPassword).NotEmpty().Length(3, 255);
        }
    }
}

public sealed record PlayerSignUpResponse(HashId Id, DateTime CreatedAt);

public sealed class PlayerSignUpRequestHandler : IRequestHandler<PlayerSignUpRequest, PlayerSignUpResponse>
{
    private readonly ICouperfectDbUnitOfWork dbUnitOfWork;
    private readonly ICryptographyService cryptographyService;

    public PlayerSignUpRequestHandler(ICouperfectDbUnitOfWork dbUnitOfWork, ICryptographyService cryptographyService)
    {
        this.dbUnitOfWork = dbUnitOfWork;
        this.cryptographyService = cryptographyService;
    }

    public async Task<Result<PlayerSignUpResponse>> Handle(PlayerSignUpRequest request, CancellationToken cancellationToken)
    {
        var existingUser = await dbUnitOfWork.PlayerRepository.Find(request.Email, cancellationToken);

        if (existingUser is not null)
            return Result.Fail(new ApplicationError("User already registred"));

        var (passwordHash, passwordSalt) = cryptographyService.GenerateSaltedSHA512Hash(request.PlainTextPassword);

        var player = new Player
        {
            Name = request.Name,
            Email = request.Email,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt
        };

        await dbUnitOfWork.PlayerRepository.Create(player, cancellationToken);
        await dbUnitOfWork.SaveChangesAsync(cancellationToken);

        return new PlayerSignUpResponse(new HashId(player.Id), player.CreatedAt).ToResult();
    }
}