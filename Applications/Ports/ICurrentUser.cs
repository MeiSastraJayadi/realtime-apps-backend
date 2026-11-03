namespace RealTimeApps.Applications.Ports;

public interface ICurrentUser
{
    string? UserId { get; }
    bool IsAuthenticated { get; }
}
