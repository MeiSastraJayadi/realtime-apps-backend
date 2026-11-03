namespace RealTimeApps.Applications.Models;

public record class ServerDto(
    int Id, string Name, string? AvatarUrl, string OwnerId, DateTime CreatedAt
);
