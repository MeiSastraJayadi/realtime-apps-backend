using RealTimeApps.Applications.Models;

namespace RealTimeApps.Applications.Ports;

public interface IIdentityService
{
    Task<RegisterResult> RegisterAsync(
        string username, string displayName, 
        string email, string password, CancellationToken ct); 
    Task<AuthenticatedUser?> ValidateCredentialAsync(
        string usernameOrEmail, string password, CancellationToken ct); 
}
