using RealTimeApps.Domains.Entities;

namespace RealTimeApps.Domains.Interfaces;

public interface IServerMemberRepository
{
    Task<bool> IsMemberAsync(string userId, int serverId, CancellationToken ct);
    Task<IReadOnlyList<ServerMember>> GetMemberAsync(int serverId, CancellationToken ct);
    Task AddAsync(ServerMember member, CancellationToken ct); 
    Task RemoveAsync(string userId, int serverId, CancellationToken ct); 
}
