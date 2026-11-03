using System.ComponentModel.DataAnnotations.Schema;

namespace RealTimeApps.Models;

public class Message
{
    public int Id { get; set; }
    public string MessageContent { get; set; } = string.Empty;
    public int ChannelId { get; set; }
    [ForeignKey("ChannelId")]
    public Channel Channel { get; set; } = null!;
    public string PersonalUserId { get; set; } = string.Empty;
    [ForeignKey("PersonalUserId")]
    public PersonalUser PersonalUser { get; set; } = null!; 
    public DateTime CreatedAt { get; set; }
    
}
