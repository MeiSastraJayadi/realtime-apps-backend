namespace RealTimeApps.Applications.Ports;

public interface IRealtimeNotifier
{
    Task MessageReceivedAsync(int channelId, object message, CancellationToken ct);
    Task MessageEditedAsync(int channelId, int messageId, CancellationToken ct); 
    Task MessageDeletedAsync(int channelId, int messageId, CancellationToken ct);
    Task MemberJoinedServerAsync(int serverId, string userId, CancellationToken ct); 
    Task MemberLeftServerAsync(int serverId, string userId, CancellationToken ct);
}
