using System.ComponentModel.DataAnnotations.Schema;

namespace RealTimeApps.Models;

public class Server
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? AvatarUrl { get; set; }
    public string OwnerId  { get; set; } = string.Empty;
    [ForeignKey("OwnerId")]
    public PersonalUser Owner { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public ICollection<Channel> Channels { get; set; } = [];
    public ICollection<ServeMembers> ServeMembers { get; set; } = [];
}
