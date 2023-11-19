using CouperfectServer.WebApp;
using System.Net.Sockets;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors();

await DependencyInjection.ConfigureBuilder(builder);

var app = builder.Build();

using var scope = app.Services.CreateScope();

await DependencyInjection.UseAppScope(scope.ServiceProvider);

await DependencyInjection.ConfigureApp(app);

app.UseCors(opt =>
{
    opt.AllowAnyHeader();
    opt.AllowAnyMethod();
    opt.AllowAnyOrigin();
});

//Setup local IP
var localIP = LocalIPAddress();
app.Urls.Add("http://" + localIP + ":5072");
app.Urls.Add("https://" + localIP + ":7072");

app.Run();
static string LocalIPAddress()
{
    using Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0);
    socket.Connect("8.8.8.8", 65530);
    if (socket.LocalEndPoint is IPEndPoint endPoint)
        return endPoint.Address.ToString();
    else
        return "127.0.0.1";
}