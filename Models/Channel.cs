using System.ComponentModel.DataAnnotations.Schema;

namespace RealTimeApps.Models;

public class Channel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Topic { get; set; } 
    public int ServerId { get; set; }
    [ForeignKey("ServerId")]
    public Server Server { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public ICollection<Message> Messages { get; set; } = [];

}
