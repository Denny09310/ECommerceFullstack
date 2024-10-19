namespace ECommerceFullstack.Client.Http.Models;

internal sealed class IdentityInfoResponse
{
    public string Email { get; set; } = default!;
    public bool IsEmailConfirmed { get; set; }
}
