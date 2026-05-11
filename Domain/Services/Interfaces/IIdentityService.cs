using Domain.UseCases.Identitys;

namespace Domain.Services.Interfaces;

public interface IIdentityService
{
    Task<SignUpVielModel> SignUp(SignUpCommand command);
}
