using RealTimeApps.Domains.Entities;

namespace RealTimeApps.Domains.Interfaces;

public interface IServerRepository
{
    Task<Server?> GetByIdAsync(int id, CancellationToken ct); 
    Task<IReadOnlyList<Server>> GetForUserAsync(string userId, CancellationToken ct); 
    Task<bool> ExistAsync(int id, CancellationToken ct);
    void Remove(Server server);
}
