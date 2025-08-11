using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestRestApi.Data;
using TestRestApi.Data.Models;

namespace TestRestApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class CategoriesController : ControllerBase 
{
    public CategoriesController (ApplicationDBContext db)
    {
      _db = db;
    }

    private readonly ApplicationDBContext _db;

    [HttpGet]

    public async Task<IActionResult> GetCategories()
    {
        var categories = await _db.Categories.ToListAsync();
        return Ok(categories);
    }

    [HttpGet("{id}")]

    public async Task<IActionResult> GetCategory(int id)
    {
        var categories = await _db.Categories.SingleOrDefaultAsync(x => x.Id == id);
        if (categories== null)
        {
            return NotFound($"Category Id {id} Not Found");
        }

        return Ok(categories);
    }


    [HttpPost]
    public async Task<IActionResult> AddCategory(string category)
    {
        Category c = new() { Name = category };
        await _db.Categories.AddAsync(c);
        _db.SaveChanges();
        return Ok(c);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateCategory([FromBody]Category category)
    {
        var c = await _db.Categories.SingleOrDefaultAsync(x => x.Id == category.Id);
        if (c== null)
        {
            return NotFound($"Category Id {category.Id} Not Found");
        }
        c.Name = category.Name;
        
        _db.SaveChanges();
        return Ok(c);
    }
    //Add , replace ,remove,move  ,copy,test
    [HttpPatch("{id}")]
    public async Task<IActionResult>
        UpdateCategoryPatch([FromBody] JsonPatchDocument <Category> category,
        [FromRoute] int id)
    {
        var c = await _db.Categories.SingleOrDefaultAsync(x => x.Id == id);
        if (c== null)
        {
            return NotFound($"Category Id {id} Not Found");
        }

        category.ApplyTo(c);
        _db.SaveChanges();
        return Ok(c);
    }




    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        var c = await _db.Categories.SingleOrDefaultAsync(x => x.Id == id);
        if (c == null)
        {     
            return NotFound($"Category Id {id} Not Found");
        }
        _db.Categories.Remove(c);
        await _db.SaveChangesAsync();
        return Ok();
    }
    
     
}
