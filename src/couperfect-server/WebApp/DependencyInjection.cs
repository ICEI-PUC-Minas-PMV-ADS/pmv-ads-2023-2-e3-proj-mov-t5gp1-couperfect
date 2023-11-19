using CouperfectServer.Application;
using CouperfectServer.Application.Extensions.HashIds;
using CouperfectServer.Application.Services;
using CouperfectServer.Domain.Services;
using CouperfectServer.Infrastructure;
using CouperfectServer.Infrastructure.CouperfectDatabase;
using CouperfectServer.WebApp.Serialization;
using CouperfectServer.WebApp.Services;
using CouperfectServer.WebApp.Services.Token;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using System.Text;

namespace CouperfectServer.WebApp;

public static class DependencyInjection
{
    public static Task ConfigureBuilder(WebApplicationBuilder builder)
    {
        // Add services to the container.
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddDistributedMemoryCache();
        builder.Services.AddSingleton<IDateTimeService, DateTimeService>();
        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddSwaggerGen(c =>
        {
            c.MapType(typeof(HashId), () => new OpenApiSchema
            {
                Type = "string",
                Example = new OpenApiString("hashID")
            });
            c.AddSecurityDefinition(
                "token",
                new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer",
                    In = ParameterLocation.Header,
                    Name = HeaderNames.Authorization
                }
            );
            c.AddSecurityRequirement(
                new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "token"
                            },
                        },
                        Array.Empty<string>()
                    }
                }
            );
        });

        var key = Encoding.UTF8.GetBytes("ChaveAleatoriaCouperfect");
        builder.Services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(x =>
        {
            x.RequireHttpsMetadata = false;
            x.SaveToken = true;
            x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false
            };
            x.Events = new JwtBearerEvents
            {
                OnMessageReceived = context =>
                {
                    var accessToken = context.Request.Query["access_token"];
                    // If the request is for our hub...
                    var path = context.HttpContext.Request.Path;
                    if (!string.IsNullOrEmpty(accessToken) &&
                        (path.StartsWithSegments("/hubs/chat")))
                    {
                        // Read the token out of the query string
                        context.Token = accessToken;
                    }
                    return Task.CompletedTask;
                }
            };
        });
        builder.Services.AddAuthorization();

        builder.Services.AddHttpContextAccessor();
        builder.Services.AddScoped<ITokenService, TokenService>();
        builder.Services.AddScoped<RequestInfoService>();
        builder.Services.Configure<TokenService.Options>(opt => opt.TokenKey = key);
        builder.Services.AddScoped(sp => sp.GetRequiredService<RequestInfoService>() as IRequestInfoService);

        builder.Services.AddScoped<IGameRoomHubService, GameRoomHubService>();
        builder.Services.AddSignalR();

        builder.Services.AddCouperfectDb();
        builder.Services.AddCouperfectApp();
        builder.Services.AddCouperfectSerialization();

        return Task.CompletedTask;
    }

    public static Task ConfigureApp(WebApplication app)
    {
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        Endpoints.MapEndpoints(app);

        app.MapHub<GameRoomHub>("hubs/gamerooms");

        return Task.CompletedTask;
    }

    public static async Task UseAppScope(IServiceProvider services)
    {
        var context = services.GetRequiredService<CouperfectDbContext>();

        await context.Database.EnsureCreatedAsync();

        if (!context.Players.Any())
        {
            var cryptoService = services.GetRequiredService<ICryptographyService>();
            var dateTimeService = services.GetRequiredService<IDateTimeService>();
            var (passwordHash, salt) = cryptoService.GenerateSaltedSHA512Hash("Coup@123");

            await context.Players.AddAsync(
                new()
                {
                    Email = "couplayer@couperfect.com",
                    Name = "CouPlayer",
                    PasswordHash = passwordHash,
                    PasswordSalt = salt,
                    CreatedAt = dateTimeService.Now
                });

            await context.SaveChangesAsync();
        }
    }
}
