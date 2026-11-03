namespace RealTimeApps.Applications.Models;

public record class ChannelDto(
    int Id, string Name, string? Topic, int ServerId, DateTime CreatedAt
);
