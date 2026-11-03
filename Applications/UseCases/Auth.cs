using RealTimeApps.Applications.Models;
using RealTimeApps.Applications.Ports;

namespace RealTimeApps.Applications.UseCases;

public class Auth
{
    private readonly IIdentityService _identityService; 
    private readonly ITokenService _tokenService;

    public Auth(IIdentityService identityService, ITokenService tokenService)
    {
        _identityService = identityService;
        _tokenService = tokenService;
    }

    public Task<RegisterResult> RegisterAsync(
        string username, 
        string displayName, 
        string email, 
        string password, 
        CancellationToken ct
    )
    {
        return _identityService
            .RegisterAsync(username, displayName, email, password, ct);
    }

    public async Task<LoginResult?> LoginAsync(
        string usernameOrEmail, 
        string password,
        CancellationToken ct
    )
    {
        AuthenticatedUser? user = await _identityService
            .ValidateCredentialAsync(usernameOrEmail, password, ct);

        if (user is null) return null; 

        string? accessToken = _tokenService.CreateAccessToken(user.UserId, user.UserName);

        return new LoginResult(accessToken, user);
    }

    

}
