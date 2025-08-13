using System.ComponentModel.DataAnnotations;

namespace TestRestApi.Data.Models;

public class Item
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public string? Notes { get; set; }
    public byte[]? Image { get; set; }

    public int CategoryId { get; set; }
    public Category category { get; set; }
    public ICollection<OrderItem>? OrderItems { get; set; }


}
