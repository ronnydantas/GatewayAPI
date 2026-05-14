using Domain.UseCases.UpdadePerson;

namespace Domain.Services.Interfaces;

public interface IPersonService
{
    Task<PersonViewModel> UpdateCliente(PersonCommand cliente, string token);
}
