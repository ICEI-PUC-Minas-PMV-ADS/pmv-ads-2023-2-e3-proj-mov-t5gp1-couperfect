using CouperfectServer.WebApp;

var builder = WebApplication.CreateBuilder(args);

await DependencyInjection.ConfigureBuilder(builder);

var app = builder.Build();

using var scope = app.Services.CreateScope();

await DependencyInjection.UseAppScope(scope.ServiceProvider);

await DependencyInjection.ConfigureApp(app);

app.Run();