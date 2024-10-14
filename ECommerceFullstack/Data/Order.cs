using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ECommerceFullstack.Data;

public class Order
{
    [Column(TypeName = "datetime")]
    public DateTime CreatedAt { get; set; }

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public OrderStatus Status { get; set; }

    [ForeignKey(nameof(UserId))]
    [JsonIgnore]
    public virtual User User { get; set; } = default!;

    public int UserId { get; set; }
}

[JsonConverter(typeof(JsonStringEnumConverter<OrderStatus>))]
public enum OrderStatus
{
    New
}