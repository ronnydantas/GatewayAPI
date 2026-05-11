using Domain.Services.Interfaces;
using Domain.UseCases.Identitys;
using System.Net.Http.Json;
using System.Text.Json;

namespace Domain.Services;

public class IdentityService : IIdentityService
{
    private readonly HttpClient _httpClient;

    public IdentityService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<SignUpVielModel> SignUp(SignUpCommand command)
    {
        try
        {
            // 🔥 VER JSON ENVIADO
            var commandJson = JsonSerializer.Serialize(command);

            Console.WriteLine("JSON ENVIADO:");
            Console.WriteLine(commandJson);

            var response = await _httpClient.PostAsJsonAsync("/identity-api/api/Auth/register", command);


            // 🔥 VER STATUS
            Console.WriteLine($"STATUS: {response.StatusCode}");

            // 🔥 VER BODY DE ERRO
            var responseContent = await response.Content.ReadAsStringAsync();

            Console.WriteLine("RESPOSTA:");
            Console.WriteLine(responseContent);

            response.EnsureSuccessStatusCode();

            var result = await response.Content
                .ReadFromJsonAsync<SignUpVielModel>();

            return result!;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);

            throw new ApplicationException(
                "Erro ao cadastrar usuário.",
                ex);
        }
    }
}