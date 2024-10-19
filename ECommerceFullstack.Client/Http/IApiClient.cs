using ECommerceFullstack.Client.Http.Handlers;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Refit;

namespace ECommerceFullstack.Client.Http;

internal interface IApiClient
{
    [Get("/api/identity/manage/info")]
    Task<ApiResponse<IdentityInfoResponse>> GetInfoAsync();

    [Post("/api/identity/login")]
    Task<ApiResponse<IdentityLoginResponse>> LoginAsync(
        [Body] IdentityLoginRequest request,
        bool useCookies = false,
        bool useSessionCookies = false);
}

internal static class ApiClientExtensions
{
    internal static IServiceCollection AddApiClient(this IServiceCollection services)
    {
        services.AddScoped<BearerTokenHandler>();

        services.AddRefitClient<IApiClient>()
            .ConfigureHttpClient((sp, client) =>
            {
                client.BaseAddress = new(sp.GetRequiredService<IWebAssemblyHostEnvironment>().BaseAddress);
            })
            .AddHttpMessageHandler<BearerTokenHandler>();

        return services;
    }
}