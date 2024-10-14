using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ECommerceFullstack.Data;

[Index(nameof(Email), IsUnique = true)]
public class User
{
    public string? Address { get; set; }

    [EmailAddress]
    public string Email { get; set; } = default!;

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [StringLength(255)]
    public string? Name { get; set; } = default!;

    [InverseProperty(nameof(Order.User))]
    public virtual ICollection<Order> Orders { get; set; } = [];

    [JsonIgnore]
    public string Password { get; set; } = default!;

    public UserRole Role { get; set; }
}

[JsonConverter(typeof(JsonStringEnumConverter<UserRole>))]
public enum UserRole
{
    User,
    Seller
}