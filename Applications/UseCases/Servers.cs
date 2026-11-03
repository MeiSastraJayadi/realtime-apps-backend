using RealTimeApps.Applications.Models;
using RealTimeApps.Applications.Ports;
using RealTimeApps.Domains.Entities;
using RealTimeApps.Domains.Interfaces;

namespace RealTimeApps.Applications.UseCases;

public class Servers
{
    private readonly IServerRepository _serverRepository; 
    private readonly IServerMemberRepository _serverMemberRepository;
    private readonly IUnitOfWork _unitOfWork; 
    private readonly ICurrentUser _currentUser;
    private readonly IRealtimeNotifier _realtimeNotifier;

    public Servers(
        IServerRepository serverRepository, 
        IServerMemberRepository serverMemberRepository, 
        IUnitOfWork unitOfWork, 
        ICurrentUser currentUser, 
        IRealtimeNotifier realtimeNotifier
    )
    {
        _serverRepository = serverRepository; 
        _serverMemberRepository = serverMemberRepository; 
        _unitOfWork = unitOfWork; 
        _currentUser = currentUser; 
        _realtimeNotifier = realtimeNotifier;
    }

    private string GetUserId()
    {
        if (!_currentUser.IsAuthenticated || string.IsNullOrWhiteSpace(_currentUser.UserId))
            throw new UnauthorizedAccessException("Authentication is required");
        
        return _currentUser.UserId;
    }


    public async Task<ServerDto> CreateAsync(
        string name, string? avatarUrl, CancellationToken ct
    )
    {
        string userId = GetUserId();
        Server server = new Server(name, userId, avatarUrl); 
        await _serverRepository.AddAsync(server, ct); 
        await _unitOfWork.SaveChangesAsync(ct);

        await _serverMemberRepository.AddAsync(new ServerMember(userId, server.Id), ct);
        await _unitOfWork.SaveChangesAsync(ct); 

        return ToDto(server);
    }

    private static ServerDto ToDto(Server server) => 
        new(server.Id, server.Name, server.AvatarUrl, server.OwnerId, server.CreatedAt);


    public async Task<IReadOnlyList<ServerDto>> GetMineAsync(CancellationToken ct)
    {
        string userId = GetUserId(); 
        IReadOnlyList<Server> servers = await _serverRepository.GetForUserAsync(userId, ct);
        return servers.Select(ToDto).ToList();
    }

    public async Task<IReadOnlyList<ServerMemberDto>> GetMemberAsync(
        int serverId, CancellationToken ct
    )
    {
        string userId = GetUserId();

        if (!await _serverMemberRepository.IsMemberAsync(userId, serverId, ct))
            throw new UnauthorizedAccessException("You are not the member of this server");
        

        IReadOnlyList<ServerMember> members = await _serverMemberRepository
            .GetMemberAsync(serverId, ct);
        
        return members  
            .Select(m => new ServerMemberDto(m.PersonalUserId, serverId, m.CreatedAt))
            .ToList();
    }

}
