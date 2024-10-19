using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ECommerceFullstack.Data;

public class Order
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public OrderStatus Status { get; set; }

    // Relationships
    [InverseProperty(nameof(OrderProduct.Order))]
    public virtual ICollection<OrderProduct> Products { get; set; } = [];

    [ForeignKey(nameof(UserId))]
    public virtual User User { get; set; } = default!;

    public int UserId { get; set; }

    // Computed total directly on the entity
    public decimal Total => Products.Sum(p => p.Product.Price * p.Quantity);
}

[JsonConverter(typeof(JsonStringEnumConverter<OrderStatus>))]
public enum OrderStatus
{
    New
}