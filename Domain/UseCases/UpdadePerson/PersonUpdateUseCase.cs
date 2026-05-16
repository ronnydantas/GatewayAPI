using Domain.Services.Interfaces;
using MediatR;

namespace Domain.UseCases.UpdadePerson;

public class PersonUpdateUseCase : IRequestHandler<PersonCommand, PersonViewModel>
{
    private readonly IPersonService _personSevice;
    private readonly ITokenAccessor _tokenAccessor;
    public PersonUpdateUseCase(IPersonService personSevice, ITokenAccessor tokenAccessor)
    {
        _personSevice = personSevice;
        _tokenAccessor = tokenAccessor;
    }

    public async Task<PersonViewModel> Handle(PersonCommand request, CancellationToken cancellationToken)
    {
        var token = _tokenAccessor.GetToken();

        return await _personSevice.UpdateCliente(request, token);

    }
}