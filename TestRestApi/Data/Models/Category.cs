using System.ComponentModel.DataAnnotations;

namespace TestRestApi.Data.Models;

public class Category
{
    [Key]
    public int Id { get; set; }
    
    public string Name { get; set; }
    //public DateTime CreatedOn { get; set; } = DateTime.Now;
    public string? Notes { get; set; }

    public List<Item> Items { get; set; }

}
