using CouperfectServer.Application.UseCases.GameRooms;
using CouperfectServer.Application.UseCases.GameRooms.Leave;
using CouperfectServer.WebApp.Services;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace CouperfectServer.WebApp;

[Authorize]
public partial class GameRoomHub : Hub<GameRoomHub.IGameRoomHubClient>
{
    public interface IGameRoomHubClient
    {
        Task Recieve(string rawNotification);
    }

    private readonly MediatR.ISender sender;
    private readonly RequestInfoService requestInfoService;
    private readonly IOptions<JsonOptions> jsonOptions;

    public GameRoomHub(MediatR.ISender sender, RequestInfoService requestInfoService, IOptions<JsonOptions> jsonOptions)
    {
        this.sender = sender;
        this.requestInfoService = requestInfoService;
        this.jsonOptions = jsonOptions;
    }

    public override async Task OnConnectedAsync()
    {
        requestInfoService.SetSignalRConnectionInfo(Context);
        await requestInfoService.SetRequestInfo(Context.ConnectionAborted);
        await base.OnConnectedAsync();
    }

    public async Task Send(string rawNotification)
    {
        requestInfoService.SetSignalRConnectionInfo(Context);
        await requestInfoService.SetRequestInfo(Context.ConnectionAborted);

        var request = JsonSerializer.Deserialize<GameRoomHubRequestBase>(rawNotification, options: jsonOptions.Value.SerializerOptions);
        var response = await sender.Send(request!, Context.ConnectionAborted);
        if (response is Result<GameRoomHubResponse> typedResponse)
        {
            if (typedResponse.IsSuccess)
            {
                var responseRaw = JsonSerializer.Serialize(typedResponse.Value, options: jsonOptions.Value.SerializerOptions);
                await Clients.Group(requestInfoService.RequestInfo!.RoomId.ToString()!).Recieve(responseRaw);
            }
            else
            {
                var responseRaw = JsonSerializer.Serialize(typedResponse.Errors, options: jsonOptions.Value.SerializerOptions);
                await Clients.Group(requestInfoService.RequestInfo!.RoomId.ToString()!).Recieve(responseRaw);
            }
        }
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        requestInfoService.SetSignalRConnectionInfo(Context);
        await requestInfoService.SetRequestInfo(Context.ConnectionAborted);
        if (requestInfoService.RequestInfo?.HubConnectionId is not null)
            await sender.Send(new LeaveGameRoomHubRequest());

        await base.OnDisconnectedAsync(exception);
    }
}