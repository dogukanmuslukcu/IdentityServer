using IdentityServer.Domain.Common;

namespace IdentityServer.Domain.Entities;

public class RefreshToken : IEntity<Guid>
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Token { get; set; } = string.Empty;
    public DateTime ExpiresAt { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public bool IsRevoked { get; set; } = false;

    // Foreign Key
    public Guid UserId { get; set; }
    // Navigation Property
    public User User { get; set; }
}
