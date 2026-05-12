using Domain.Services.Interfaces;
using MediatR;

namespace Domain.UseCases.Login;

public class SignInUseCase : IRequestHandler<SignInCommand, SignInViewModel>
{
    private readonly IIdentityService _identityService;

    public SignInUseCase(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<SignInViewModel> Handle(SignInCommand request, CancellationToken cancellationToken)
    {
        return await _identityService.SignIn(request);
    }
}