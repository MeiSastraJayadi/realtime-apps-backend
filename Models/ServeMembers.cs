namespace RealTimeApps.Models;

public class ServeMembers
{
    public string PersonalUserId { get; set; } = string.Empty; 
    public int ServerId { get; set; }
    public DateTime JoinAt { get; set; }
}
