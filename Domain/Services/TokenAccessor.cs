using Domain.Services.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Domain.Services;

public class TokenAccessor : ITokenAccessor
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public TokenAccessor(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string GetToken()
    {
        var authorizationHeader =
            _httpContextAccessor
                .HttpContext?
                .Request
                .Headers["Authorization"]
                .ToString();

        if (string.IsNullOrWhiteSpace(authorizationHeader))
            return string.Empty;

        if (!authorizationHeader.StartsWith("Bearer "))
            return string.Empty;

        return authorizationHeader["Bearer ".Length..].Trim();
    }
}