namespace RealTimeApps.Applications.Ports;

public interface ITokenService
{
    string CreateAccessToken(string userId, string username);
    string CreateRefreshToken();
}
