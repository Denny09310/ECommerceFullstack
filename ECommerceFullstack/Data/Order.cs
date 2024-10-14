using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceFullstack.Data;

public class Order
{
    [Column(TypeName = "datetime")]
    public DateTime CreatedAt { get; set; }

    [Key]
    public int Id { get; set; }

    public OrderStatus Status { get; set; }

    [ForeignKey(nameof(UserId))]
    public virtual User User { get; set; } = default!;

    public int UserId { get; set; }
}

public enum OrderStatus
{
    New
}