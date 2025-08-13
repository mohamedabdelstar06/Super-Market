
namespace TestRestApi.Data.Models;

public class Order
{
    public int Id { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.Now;

    public virtual ICollection<OrderItem> orderItems { get; set; } 

}
