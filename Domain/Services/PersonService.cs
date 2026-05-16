using Domain.Services.Interfaces;
using Domain.Shareds;
using Domain.UseCases.UpdadePerson;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Domain.Services;

public class PersonService : IPersonService
{
    private readonly HttpClient _httpClient;

    public PersonService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<PersonViewModel> UpdateCliente(PersonCommand cliente, string token)
    {
        try
        {
            var body = new
            {
                cliente = new
                {
                    phone = cliente.Phone,
                    birthDate = cliente.BirthDate,
                    registrationDate = cliente.RegistrationDate,
                    allergy = cliente.Allergy,
                    observation = cliente.Observation,
                    gender = cliente.Gender
                }
            };

            var payload = JsonSerializer.Serialize(body,
                new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    WriteIndented = true
                });

            var request = new HttpRequestMessage(HttpMethod.Put, $"/user-service/api/v1/Person/{cliente.Id}");

            request.Headers.Authorization =new AuthenticationHeaderValue("Bearer", token);

            request.Content = new StringContent(payload, Encoding.UTF8, "application/json");

            var response = await _httpClient.SendAsync(request);

            var responseContent = await response.Content.ReadAsStringAsync();

            response.EnsureSuccessStatusCode();

            var result = JsonSerializer.Deserialize<ApiResponse<PersonViewModel>>( responseContent,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

            return result!.Data;
        }
        catch (Exception ex)
        {
            throw new ApplicationException( "Erro ao atualizar cliente.", ex);
        }
    }
}