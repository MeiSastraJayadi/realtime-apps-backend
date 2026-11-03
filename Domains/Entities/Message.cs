namespace RealTimeApps.Domains.Entities;

public class Message
{
    public int Id { get; private set; }
    public string MessageContent { get; private set; } = string.Empty;
    public int ChannelId { get; private set; }
    public string PersonalUserId { get; private set; } = string.Empty;
    public DateTime CreatedAt { get; private set; }

    private Message() {}
    public Message(string message, int channelId, string personalUserId)
    {
        MessageContent = message; 
        ChannelId = channelId; 
        PersonalUserId = personalUserId;
    }
}
