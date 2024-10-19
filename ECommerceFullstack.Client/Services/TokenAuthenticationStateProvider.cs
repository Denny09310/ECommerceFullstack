using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using Refit;
using System.Security.Claims;

namespace ECommerceFullstack.Client.Services;

internal sealed class TokenAuthenticationStateProvider(IApiClient api, ILocalStorageService localStorage) : AuthenticationStateProvider
{
    private static readonly AuthenticationState _anonymous = new(new ClaimsPrincipal());

    private readonly IApiClient _api = api;
    private readonly ILocalStorageService _localStorage = localStorage;

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        try
        {
            var response = await _api.GetInfoAsync();

            if (!response.IsSuccessStatusCode)
            {
                return _anonymous;
            }

            var content = response.Content;

            IEnumerable<Claim> claims =
            [
                new Claim(ClaimTypes.Name, content.Email),
                new Claim(ClaimTypes.Email, content.Email),
            ];

            var identity = new ClaimsIdentity(claims, nameof(TokenAuthenticationStateProvider));
            var principal = new ClaimsPrincipal(identity);

            return new AuthenticationState(principal);
        }
        catch (ApiException)
        {
            return _anonymous;
        }
    }

    public async Task LoginAsync(string email, string password)
    {
        var response = await _api.LoginAsync(new IdentityLoginRequest(email, password));

        if (response.IsSuccessStatusCode)
        {
            var content = response.Content;

            _localStorage.SetItem("access_token", content.AccessToken);
            _localStorage.SetItem("refresh_token", content.RefreshToken);
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
    }
}