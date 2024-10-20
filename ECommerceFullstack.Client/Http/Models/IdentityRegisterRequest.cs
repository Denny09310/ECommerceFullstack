namespace ECommerceFullstack.Client.Http.Models;

internal sealed class IdentityRegisterRequest
{
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;
}
