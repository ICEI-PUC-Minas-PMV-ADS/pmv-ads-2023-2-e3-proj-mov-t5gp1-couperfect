using CouperfectServer.Application.Services;
using CouperfectServer.Application.UseCases.GameRooms;
using CouperfectServer.WebApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace CouperfectServer.WebApp;

[Authorize]
public partial class GameRoomHub : Hub<IGameRoomHubClientService>
{
    private readonly MediatR.ISender sender;
    private readonly RequestInfoService requestInfoService;
    private readonly IOptions<JsonOptions> jsonOptions;

    public GameRoomHub(MediatR.ISender sender, RequestInfoService requestInfoService, IOptions<JsonOptions> jsonOptions)
    {
        this.sender = sender;
        this.requestInfoService = requestInfoService;
        this.jsonOptions = jsonOptions;
    }

    public override Task OnConnectedAsync()
    {
        requestInfoService.SetSignalRConnectionInfo(Context);
        return base.OnConnectedAsync();
    }

    public async Task Send(string rawNotification)
    {
        var request = JsonSerializer.Deserialize<GameRoomHubRequest>(rawNotification, options: jsonOptions.Value.SerializerOptions);
        _ = await sender.Send(request!, Context.ConnectionAborted);
    }
}