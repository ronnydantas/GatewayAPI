using Domain.Services.Interfaces;
using MediatR;

namespace Domain.UseCases.Identitys;

public class SignUpUseCase : IRequestHandler<SignUpCommand, SignUpVielModel>
{
    private readonly IIdentityService _identityService;

    public SignUpUseCase(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<SignUpVielModel> Handle(SignUpCommand request, CancellationToken cancellationToken)
    {
        return await _identityService.SignUp(request);
    }
}