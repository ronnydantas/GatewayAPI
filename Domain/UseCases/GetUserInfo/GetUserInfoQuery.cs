using MediatR;

namespace Domain.UseCases.GetUserInfo;

public class GetUserInfoQuery : IRequest<UserInfoViewModel>
{
}
