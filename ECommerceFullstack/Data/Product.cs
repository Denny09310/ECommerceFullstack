using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Data;

public class Product
{
    [Column(TypeName = "datetime")]
    public DateTime? DeletedAt { get; set; }

    public string? Description { get; set; } = default!;

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [StringLength(255)]
    public string? Image { get; set; } = default!;

    [StringLength(255)]
    public string Name { get; set; } = default!;

    public decimal Price { get; set; }
}