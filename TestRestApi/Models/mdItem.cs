namespace TestRestApi.Models;

public class mdItem
{
    public string Name { get; set; }
    public double Price { get; set; }
    public string? Notes { get; set; }
    public IFormFile Image { get; set; }
    public int CategoryId { get; set; }
}
