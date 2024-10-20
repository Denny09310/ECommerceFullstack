using ECommerceFullstack.Client.Http.Handlers;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Refit;

namespace ECommerceFullstack.Client.Http;

internal interface IApiClient
{
    [Get("/identity/manage/info")]
    Task<ApiResponse<IdentityInfoResponse>> GetInfoAsync();

    [Post("/identity/login")]
    Task<ApiResponse<IdentityLoginResponse>> LoginAsync(
        [Body] IdentityLoginRequest request,
        bool useCookies = false,
        bool useSessionCookies = false);

    [Post("/identity/register")]
    Task<HttpResponseMessage> RegisterAsync(
        [Body] IdentityRegisterRequest request);
}

internal static class ApiClientExtensions
{
    internal static IServiceCollection AddApiClient(this IServiceCollection services)
    {
        services.AddScoped<BearerTokenHandler>();

        services.AddRefitClient<IApiClient>()
            .ConfigureHttpClient((sp, client) =>
            {
                var environment = sp.GetRequiredService<IWebAssemblyHostEnvironment>();
                client.BaseAddress = new Uri(environment.BaseAddress + "api");
            })
            .AddHttpMessageHandler<BearerTokenHandler>();

        return services;
    }
}