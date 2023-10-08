using CouperfectServer.Infrastructure.CouperfectDatabase;
using CouperfectServer.Application;
using CouperfectServer.Domain.Services;
using CouperfectServer.WebApp.Serialization;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using CouperfectServer.Application.Extensions;
using CouperfectServer.Infrastructure;
using Microsoft.Net.Http.Headers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using CouperfectServer.WebApp.Services.Token;
using CouperfectServer.Application.Services;

namespace CouperfectServer.WebApp;

public static class DependencyInjection
{
    public static Task ConfigureBuilder(WebApplicationBuilder builder)
    {
        // Add services to the container.
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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

        builder.Services.Configure<TokenService.Options>(opt => opt.TokenKey = key);
        builder.Services.AddScoped<ITokenService, TokenService>();

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
        });
        builder.Services.AddAuthorization();

        builder.Services.AddCouperfectDb();
        builder.Services.AddCouperfectApp();
        builder.Services.AddHttpContextAccessor();
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

        return Task.CompletedTask;
    }

    public static async Task UseAppScope(IServiceProvider services)
    {
        var context = services.GetRequiredService<CouperfectDbContext>();

        await context.Database.EnsureCreatedAsync();
    }
}
