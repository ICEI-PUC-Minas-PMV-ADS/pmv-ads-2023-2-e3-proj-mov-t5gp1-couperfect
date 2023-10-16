namespace CouperfectServer.Domain.Entities;

public class GameRoom
{
    public Guid Guid { get; init; } = Guid.NewGuid();
    public bool IsInviteOnly { get; set; }
    public string Name { get; set; } = "Public lobby";
    public int AdminId { get; set; }
    public HashSet<(int playerId, string connectionId)> Players { get; set; } = new();
}
