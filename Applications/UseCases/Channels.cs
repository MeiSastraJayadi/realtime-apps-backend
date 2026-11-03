using RealTimeApps.Applications.Models;
using RealTimeApps.Applications.Ports;
using RealTimeApps.Domains.Entities;
using RealTimeApps.Domains.Interfaces;

namespace RealTimeApps.Applications.UseCases;

public class Channels
{
    private readonly IChannelRepository _channelRepository;
    private readonly IServerRepository _serverRepository;
    private readonly IServerMemberRepository _serverMemberRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUser _currentUser;

    public Channels(
        IChannelRepository channelRepository, 
        IServerRepository serverRepository, 
        IServerMemberRepository serverMemberRepository, 
        IUnitOfWork unitOfWork, 
        ICurrentUser currentUser
    )
    {
        _channelRepository = channelRepository; 
        _serverRepository = serverRepository; 
        _serverMemberRepository = serverMemberRepository; 
        _unitOfWork = unitOfWork; 
        _currentUser = currentUser;
    }

    private string GetUserId()
    {
        if (!_currentUser.IsAuthenticated || string.IsNullOrWhiteSpace(_currentUser.UserId))
            throw new UnauthorizedAccessException("Authentication is required"); 
        
        return _currentUser.UserId;
    }

    private async Task EnsureOwnerAsync(string userId, int serverId, CancellationToken ct)
    {
        Server? server = await _serverRepository.GetByIdAsync(serverId, ct)
            ?? throw new KeyNotFoundException("Server was not found");
        
        if (server.OwnerId != userId)
            throw new UnauthorizedAccessException("Only the server owner can manage channels");
    }

    public async Task<ChannelDto> CreateAsync(
        int serverId, string name, string? topic, CancellationToken ct
    )
    {
        string userId = GetUserId(); 
        await EnsureOwnerAsync(userId, serverId, ct); 

        Channel channel = new Channel(name, topic, serverId); 
        await _channelRepository.AddAsync(channel, ct); 
        await _unitOfWork.SaveChangesAsync(ct); 

        return ToDto(channel);
    }

    public async Task<IReadOnlyList<ChannelDto>> GetByServerAsync(
        int serverId, CancellationToken ct
    )
    {
        string userId = GetUserId(); 

        if (!await _serverMemberRepository.IsMemberAsync(userId, serverId, ct))
            throw new UnauthorizedAccessException("You are not member of this server");
        
        IReadOnlyList<Channel> channels = await _channelRepository 
            .GetByServerAsync(serverId, ct);
        
        return channels.Select(ToDto).ToList();
    }

    private async Task<Channel> GetChannelAsync(int channelId, CancellationToken ct)
    {
        return await _channelRepository.GetByIdAsync(channelId, ct)
            ?? throw new KeyNotFoundException("Channel was not found");
    }

    public async Task RenameAsync(int channelId, string name, CancellationToken ct)
    {
        string userId = GetUserId(); 
        Channel channel = await GetChannelAsync(channelId, ct);
        await EnsureOwnerAsync(userId, channel.ServerId, ct);
        
        channel.Rename(name); 
        await _unitOfWork.SaveChangesAsync(ct);
    }

    public async Task ChangeTopicAsync(int channelId, string topic, CancellationToken ct)
    {
        string userId = GetUserId(); 
        Channel channel = await GetChannelAsync(channelId, ct); 
        await EnsureOwnerAsync(userId, channel.ServerId, ct); 

        channel.ChangeTopic(topic); 
        await _unitOfWork.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(int channelId, CancellationToken ct)
    {
        string userId = GetUserId(); 
        Channel channel = await GetChannelAsync(channelId, ct); 
        await EnsureOwnerAsync(userId, channel.ServerId, ct);

        _channelRepository.Remove(channel); 
        await _unitOfWork.SaveChangesAsync(ct);
    }

    private static ChannelDto ToDto(Channel channel) => 
        new(channel.Id, channel.Name, channel.Topic, channel.ServerId, channel.CreatedAt);
}
