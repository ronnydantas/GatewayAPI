using Domain.UseCases.Identitys;
using Domain.UseCases.Login;

namespace Domain.Services.Interfaces;

public interface IIdentityService
{
    Task<SignUpVielModel> SignUp(SignUpCommand command);
    Task<SignInViewModel> SignIn(SignInCommand command);
}
