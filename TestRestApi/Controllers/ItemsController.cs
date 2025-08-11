using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestRestApi.Data;
using TestRestApi.Data.Models;
using TestRestApi.Models;

namespace TestRestApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ItemsController : ControllerBase
{
    public ItemsController(ApplicationDBContext db)
    {
        _db = db;
    }
    private readonly ApplicationDBContext _db;

    [HttpGet]
    public async Task<IActionResult> GetItems()
    {
        var items = await _db.Items.ToListAsync();
        return Ok(items);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetItem(int id)
    {
        var i = await _db.Items.SingleOrDefaultAsync(x => x.Id == id);
        if (i == null)
        {
            return NotFound($"Item id {id} not found!");
        }

        return Ok();

    }

    [HttpGet("categories/{categoryId}/items")]
    public async Task<IActionResult> GetItemsByCategory(int categoryId)
    {
        var i = await _db.Items.Where(x => x.CategoryId == categoryId).ToListAsync();
        if (!i.Any())
        {
            return NotFound($"Category id {categoryId} has no items!");
        }

        return Ok(i);

    }

    [HttpPost]
    public async Task<IActionResult> AddItems([FromForm] mdItem md)
    {
        using var stream = new MemoryStream();
        await md.Image.CopyToAsync(stream);
        var item = new Item()
        {
            Name = md.Name,
            Price =md.Price,
            Notes = md.Notes,
            CategoryId = md.CategoryId,
            Image = stream.ToArray()

        };
        await _db.Items.AddAsync(item);
        await _db.SaveChangesAsync();
        return Ok(item);

    }
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateItem(int id, [FromForm] mdItem md)
    {
        var i = await _db.Items.FindAsync(id);
        if (i == null)
        {
            return NotFound($"Item id {id} not found");
        }
        var isCategoryExists = await _db.Categories.AnyAsync(x => x.Id == md.CategoryId);
        if (isCategoryExists == null)
        {
            return NotFound($"Category id {id} not Exist");
        }
        if (md.Image != null)
        {
            using var stream = new MemoryStream();
            await md.Image.CopyToAsync(stream);
            i.Image = stream.ToArray();
        }
        i.Name = md.Name;
        i.Price = md.Price;
        i.Notes = md.Notes;
        i.CategoryId = md.CategoryId;
        _db.SaveChanges();
        return Ok();
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteItem(int id)
    {
        var i = await _db.Items.SingleOrDefaultAsync(x => x.Id == id);
        if (i == null)
        {
            return NotFound($"Item id {id} not found");
        }
        _db.Items.Remove(i);
        await _db.SaveChangesAsync();
        return Ok();

    }


}
