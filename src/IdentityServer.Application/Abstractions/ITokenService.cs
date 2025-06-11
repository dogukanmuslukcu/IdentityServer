namespace IdentityServer.Application.Abstractions;

using IdentityServer.Domain.Entities;

public interface ITokenService
{
    Task<(string AccessToken, string RefreshToken)> GenerateTokensAsync(User user);
}
