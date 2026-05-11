using MediatR;

namespace Domain.UseCases.Identitys;

public class SignUpCommand : IRequest<SignUpVielModel>
{
    public string Username { get; set; } = string.Empty;

    public string FullName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    public string PasswordConfirm { get; set; } = string.Empty;
}