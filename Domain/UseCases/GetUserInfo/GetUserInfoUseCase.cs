using Domain.Services.Interfaces;
using MediatR;

namespace Domain.UseCases.GetUserInfo;

public class GetUserInfoUseCase : IRequestHandler<GetUserInfoQuery, UserInfoViewModel>
{
    private readonly IIdentityService _identityService;
    private readonly ITokenAccessor _tokenAccessor;

    public GetUserInfoUseCase(IIdentityService identityService, ITokenAccessor tokenAccessor)
    {
        _identityService = identityService;
        _tokenAccessor = tokenAccessor; 
    }

    public async Task<UserInfoViewModel> Handle(GetUserInfoQuery query, CancellationToken cancellationToken)
    {
        var token = _tokenAccessor.GetToken();
        return await _identityService.GetUserInfo(token);
    }
}