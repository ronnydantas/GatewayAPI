using Domain.Services.Interfaces;
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

    public async Task<PersonViewModel> UpdateCliente(
        PersonCommand cliente,
        string token)
    {
        try
        {
            // =========================================
            // PAYLOAD EXATO ESPERADO PELA API
            // =========================================
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

            // =========================================
            // SERIALIZA JSON
            // =========================================
            var payload = JsonSerializer.Serialize(
                body,
                new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    WriteIndented = true
                });

            Console.WriteLine("PAYLOAD ENVIADO:");
            Console.WriteLine(payload);

            // =========================================
            // REQUEST
            // =========================================
            var request = new HttpRequestMessage(
                HttpMethod.Put,
                $"/user-service/api/v1/Person/{cliente.Id}");

            request.Headers.Authorization =
                new AuthenticationHeaderValue(
                    "Bearer",
                    token);

            request.Content = new StringContent(
                payload,
                Encoding.UTF8,
                "application/json");

            // =========================================
            // RESPONSE
            // =========================================
            var response =
                await _httpClient.SendAsync(request);

            var responseContent =
                await response.Content.ReadAsStringAsync();

            Console.WriteLine("STATUS:");
            Console.WriteLine(response.StatusCode);

            Console.WriteLine("RESPONSE:");
            Console.WriteLine(responseContent);

            response.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<PersonViewModel>(
                responseContent)!;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);

            throw new ApplicationException(
                "Erro ao atualizar cliente.",
                ex);
        }
    }
}