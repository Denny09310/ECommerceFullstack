using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace WebApplication1.Data;

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

    [JsonIgnore]
    public string Password { get; set; } = default!;

    public Role Role { get; set; }
}

public enum Role
{
    User,
    Seller
}

