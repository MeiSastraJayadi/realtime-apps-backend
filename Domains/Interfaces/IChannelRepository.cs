using RealTimeApps.Domains.Entities;

namespace RealTimeApps.Domains.Interfaces;

public interface IChannelRepository
{
    Task<Channel?> GetByIdAsync(int id, CancellationToken ct);
    Task<IReadOnlyList<Channel>> GetByServerAsync(int serverId, CancellationToken ct);
    Task AddAsync(Channel channel, CancellationToken ct);
    void Remove(Channel channel);
}
