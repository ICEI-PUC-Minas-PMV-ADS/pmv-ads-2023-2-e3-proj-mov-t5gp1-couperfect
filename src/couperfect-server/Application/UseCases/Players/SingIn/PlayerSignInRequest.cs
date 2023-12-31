﻿using CouperfectServer.Application.Contracts.CouperfectDb;
using CouperfectServer.Application.Services;
using FluentResults;
using FluentValidation;

namespace CouperfectServer.Application.UseCases.Players.SingIn;

public sealed record PlayerSignInRequest(string Email, string PlainTextPassword) : IRequest<PlayerSignInResponse>
{
    public class Validator : AbstractValidator<PlayerSignInRequest>
    {
        public Validator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.PlainTextPassword).NotEmpty().Length(3, 255);
        }
    }
}

public sealed record PlayerSignInResponse(string Token, string Name);

public sealed class PlayerSignInRequestHandler : IRequestHandler<PlayerSignInRequest, PlayerSignInResponse>
{
    private readonly ICouperfectDbUnitOfWork dbUnitOfWork;
    private readonly ICryptographyService cryptographyService;
    private readonly ITokenService tokenService;

    public PlayerSignInRequestHandler(ICouperfectDbUnitOfWork dbUnitOfWork, ICryptographyService cryptographyService, ITokenService tokenService)
    {
        this.dbUnitOfWork = dbUnitOfWork;
        this.cryptographyService = cryptographyService;
        this.tokenService = tokenService;
    }

    public async Task<Result<PlayerSignInResponse>> Handle(PlayerSignInRequest request, CancellationToken cancellationToken)
    {
        var player = await dbUnitOfWork.PlayerRepository.Find(request.Email, cancellationToken);

        if (player is null)
            return Result.Fail(new ApplicationError("Incorrect user or password"));

        if (cryptographyService.CompareSaltedSHA512Hash(request.PlainTextPassword, player.PasswordHash, player.PasswordSalt) is false)
            return Result.Fail(new ApplicationError("Incorrect user or password"));

        var token = tokenService.GetToken(player);

        return new PlayerSignInResponse(token, new(player.Name)).ToResult();
    }
}