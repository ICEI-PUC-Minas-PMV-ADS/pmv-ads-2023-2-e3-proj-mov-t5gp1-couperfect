namespace CouperfectServer.Domain.Entities;

public class Player : AuditableEntity
{
    public required string Email { get; set; } = string.Empty;
    public required string Name { get; set; } = string.Empty;
    public required byte[] PasswordHash { get; set; } = Array.Empty<byte>();
    public required byte[] PasswordSalt { get; set; } = Array.Empty<byte>();
    public virtual GameRoom? CurrentRoom { get; set; }
    public virtual GameRoom? OwnedRoom { get; set; }
    public virtual ICollection<GameRoom> PlayedRooms { get; set; } = new HashSet<GameRoom>();
    public virtual ICollection<GameRoom> BannedRooms { get; set; } = new HashSet<GameRoom>();
}
