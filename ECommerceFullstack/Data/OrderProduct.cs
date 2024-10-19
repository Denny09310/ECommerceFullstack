using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceFullstack.Data;

public class OrderProduct
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public virtual Order Order { get; set; } = default!;

    public int OrderId { get; set; }

    public virtual Product Product { get; set; } = default!;

    public string ProductId { get; set; }

    public int Quantity { get; set; }
}