namespace IdentityServer.Application.Services.Users;

public interface IUserService
{
    Task<RegisterUserResponse> RegisterAsync(RegisterUserCommand request, CancellationToken cancellationToken = default);
}
