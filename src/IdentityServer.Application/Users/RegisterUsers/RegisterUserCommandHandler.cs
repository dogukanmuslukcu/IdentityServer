using IdentityServer.Application.Abstractions;
using IdentityServer.Application.Services.Users;
using MediatR;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, RegisterUserResponse>
{
    private readonly IUserService _userService;
    private readonly ITokenService _tokenService;

    public RegisterUserCommandHandler(IUserService userService, ITokenService tokenService )
    {
        _userService = userService;
        _tokenService = tokenService;
    }

    public async Task<RegisterUserResponse> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
         var user = await _userService.RegisterAsync(request,cancellationToken);
         var accessToken = _tokenService.CreateAccessToken(user);
         var refreshToken = _tokenService.CreateRefreshToken();

         await _userService.AddRefreshTokenAsync(user, refreshToken);

         return new RegisterUserResponse
         {
             AccessToken = accessToken.Token,
             RefreshToken = refreshToken.Token,
             Email = user.Email,
             AccessTokenExpiration = accessToken.Expiration,
             RefreshTokenExpiration = refreshToken.Expiration
         };
    }
}
