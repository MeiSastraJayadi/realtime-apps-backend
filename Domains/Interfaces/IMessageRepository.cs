using RealTimeApps.Domains.Entities;

namespace RealTimeApps.Domains.Interfaces;

public interface IMessageRepository
{
    Task<Message?> GetByIdAsync(int id, CancellationToken ct); 
    Task<IReadOnlyList<Message>> GetPageAsync(int channelId, int? beforeId, int limit, CancellationToken ct);
    Task AddAsync(Message message, CancellationToken ct); 
    void Remove(Message message);
}
