namespace RealTimeApps.Applications.Models;

public record class ServerMemberDto(
    string UserId, int ServerId, DateTime JoinedAt
);
