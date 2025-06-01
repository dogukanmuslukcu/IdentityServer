using MediatR;

public class RegisterUserCommand : IRequest<RegisterUserResponse>
{
    public string Email { get; set; }
    public string Password { get; set; }
}