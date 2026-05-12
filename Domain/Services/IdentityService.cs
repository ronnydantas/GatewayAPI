using Domain.Services.Interfaces;
using Domain.UseCases.Identitys;
using Domain.UseCases.Login;
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

    public async Task<SignInViewModel> SignIn(SignInCommand command)
    {
        try
        {

            var response = await _httpClient.PostAsJsonAsync("/identity-api/api/Auth/login", command);

            var responseContent = await response.Content.ReadAsStringAsync();

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<SignInViewModel>();

            return result!;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);

            throw new ApplicationException("Erro ao cadastrar usuário.", ex);
        }
    }

    public async Task<SignUpVielModel> SignUp(SignUpCommand command)
    {
        try
        {

            var response = await _httpClient.PostAsJsonAsync("/identity-api/api/Auth/register", command);

            var responseContent = await response.Content.ReadAsStringAsync();

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<SignUpVielModel>();

            return result!;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);

            throw new ApplicationException("Erro ao cadastrar usuário.", ex);
        }
    }
}