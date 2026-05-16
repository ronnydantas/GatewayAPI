using MediatR;

namespace Domain.UseCases.UpdadePerson;

public class PersonCommand : IRequest<PersonViewModel>
{
    public string Id { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;

    public DateOnly BirthDate { get; set; }

    public DateTime RegistrationDate { get; set; }

    public bool Allergy { get; set; }

    public string Observation { get; set; } = string.Empty;

    public string Gender { get; set; } = string.Empty;

}
