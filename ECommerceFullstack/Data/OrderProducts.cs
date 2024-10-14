using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceFullstack.Data;

public class OrderProducts
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public virtual Order Order { get; set; } = default!;

    public int OrderId { get; set; }

    public decimal Price { get; set; }

    public virtual Product Product { get; set; } = default!;

    public int ProductId { get; set; }

    public int Quantity { get; set; }
}