using CouperfectServer.Application.Contracts.CouperfectDb;
using CouperfectServer.Application.Extensions;
using CouperfectServer.Application.Extensions.FluentResultsExtensions;
using CouperfectServer.Application.Services;
using CouperfectServer.Domain.Entities;
using FluentResults;
using FluentValidation;

namespace CouperfectServer.Application.UseCases.Players.SingUp;

public record PlayerSignUpRequest(string Name, string Email, string PlainTextPassword) : IRequest<PlayerSignUpResponse> 
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

public record PlayerSignUpResponse(HashId Id, DateTime CreatedAt);

public class PlayerSignUpRequestHandler : IRequestHandler<PlayerSignUpRequest, PlayerSignUpResponse>
{
    private readonly ICouperfectDbUnitOfWork couperfectUnitOfWork;
    private readonly ICryptographyService cryptographyService;

    public PlayerSignUpRequestHandler(ICouperfectDbUnitOfWork couperfectUnitOfWork, ICryptographyService cryptographyService)
    {
        this.couperfectUnitOfWork = couperfectUnitOfWork;
        this.cryptographyService = cryptographyService;
    }

    public async Task<Result<PlayerSignUpResponse>> Handle(PlayerSignUpRequest request, CancellationToken cancellationToken)
    {
        var existingUser = await couperfectUnitOfWork.PlayerRepository.Find(request.Email, cancellationToken);

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

        await couperfectUnitOfWork.PlayerRepository.Create(player, cancellationToken);
        await couperfectUnitOfWork.SaveChangesAsync(cancellationToken);

        return new PlayerSignUpResponse(new HashId(player.Id), player.CreatedAt).ToResult();
    }
}