namespace CouperfectServer.Domain.Entities;

public class Player : AuditableEntity
{
    public required string Email { get; set; } = string.Empty;
    public required string Name { get; set; } = string.Empty;
    public required byte[] PasswordHash { get; set; } = Array.Empty<byte>();
    public required byte[] PasswordSalt { get; set; } = Array.Empty<byte>();
    public Guid? CurrentRoom { get; set; }
    public DateTime? JoinedRoomAt { get; set; }
}
