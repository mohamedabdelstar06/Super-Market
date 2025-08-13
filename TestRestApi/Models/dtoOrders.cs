using System.ComponentModel.DataAnnotations;

namespace TestRestApi.Models;

public class dtoOrders
{
    public int OrderId { get; set; }
    [Required]
    public DateTime OrderDate { get; set; } = DateTime.Now;
    [MaxLength(100)]
    public string OrderName { get; set; }
    public ICollection<dtoOrdersItems> items { get; set; } = new List<dtoOrdersItems>();
}




public class dtoOrdersItems
{ 
    public int ItemId{ get; set; }
    public string? ItemName {get; set;}
    [Required]
    public decimal Price{ get; set; }
    public int Quantity{ get; set; }
}