using Microsoft.AspNetCore.Identity;

namespace RealTimeApps.Models;

public class PersonalUser : IdentityUser
{
    public string DisplayName { get; set; } = string.Empty;
    public string? AvatarUrl { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime LastSeenAt { get; set; }
    public ICollection<Server> Servers { get; set; } = null!;
    public ICollection<ServeMembers> ServeMembers { get; set; } = [];
}
