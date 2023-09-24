namespace CouperfectServer.Domain.Entities;

public class GameInvite : Entity
{
    public bool WasUsed { get; set; }
    public GameRoom Room { get; set; } = default!;
}
