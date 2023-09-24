namespace CouperfectServer.Domain.Entities;

public class Player : Entity
{
    public required string Email { get; set; } = string.Empty;
    public required string Name { get; set; } = string.Empty;
    public required byte[] PassswordHash { get; set; } = Array.Empty<byte>();
    public required byte[] PasswordSalt { get; set; } = Array.Empty<byte>();
    public virtual GameRoom? CurrentRoom { get; set; }
    public virtual ICollection<GameRoom> PlayedRooms { get; set; } = new HashSet<GameRoom>();
}
