using Domain.Services.Interfaces;
using Domain.UseCases.GetUserInfo;
using Domain.UseCases.Identitys;
using Domain.UseCases.Login;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace Domain.Services;

public class IdentityService : IIdentityService
{
    private readonly HttpClient _httpClient;

    public IdentityService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<UserInfoViewModel> GetUserInfo(string token)
    {
        try
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.GetAsync("/identity-api/api/Auth/me");

            var responseContent = await response.Content.ReadAsStringAsync();

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<UserInfoViewModel>();

            return result!;
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Erro ao buscar usuário.", ex);
        }
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

            throw new ApplicationException("Erro ao autenticar usuário.", ex);
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