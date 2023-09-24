namespace CouperfectServer.Domain.Entities;

public class GameRoom : Entity
{
    public bool IsInviteOnly { get; set; }
    public string Name { get; set; } = "Public lobby";
    public virtual Player Admin { get; set; } = default!;
    public virtual ICollection<GameInvite> Invites { get; set; } = new HashSet<GameInvite>();
    public virtual ICollection<Player> Players { get; set; } = new HashSet<Player>();
    public virtual ICollection<Player> BannedPlayers { get; set; } = new HashSet<Player>();
}
