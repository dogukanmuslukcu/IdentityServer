using IdentityServer.Application.Abstractions;
using IdentityServer.Application.Interfaces;
using IdentityServer.Application.Services.Users;
using IdentityServer.Domain.Entities;
using Microsoft.AspNet.Identity;
// ...

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ITokenService _tokenService;

    public UserService(IUserRepository userRepository, IPasswordHasher passwordHasher, ITokenService tokenService)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _tokenService = tokenService;
    }

    public async Task<RegisterUserResponse> RegisterAsync(RegisterUserCommand request, CancellationToken cancellationToken = default)
    {
        var existingUser = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);
        if (existingUser is not null)
            throw new Exception("Bu email ile zaten bir kullanıcı kayıtlı.");

        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = request.Email,
            UserName = request.UserName,
            CreatedAt = DateTime.UtcNow
        };
        
        user.PasswordHash = _passwordHasher.HashPassword(request.Password);

        await _userRepository.AddAsync(user, cancellationToken);

        var (accessToken, refreshToken) = _tokenService.GenerateTokensAsync(user);

        return new RegisterUserResponse
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken
        };
    }
}
