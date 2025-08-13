
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestRestApi.Data;
using TestRestApi.Data.Models;
using TestRestApi.Models;

namespace TestRestApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class OrdersController : ControllerBase
{
    public OrdersController(ApplicationDBContext db)
    {
        _db =db;
    }
    private readonly ApplicationDBContext _db;
    [HttpGet]
    public async Task<IActionResult> GetAllOrders()
    {
        var orders = _db.Orders.ToArray();
        return Ok(orders);
    }

    [HttpGet("{orderId}")]
    public async Task<IActionResult> GetOrderById(int orderId)
    {
        var order = await _db.Orders.Where(x => x.Id == orderId).FirstOrDefaultAsync();
        if (order != null)
        {
            dtoOrders dto = new()
            {

                OrderId = order.Id,
                OrderDate  = order.CreatedDate,
            };
            if (order.orderItems != null && order.orderItems.Any())
            {
                foreach (var item in order.orderItems)
                {
                    dtoOrdersItems dtoItems = new()
                    {
                        ItemId = item.items.Id,
                        ItemName = item.items.Name,
                        Price = item.Price,
                        Quantity =1

                    };
                    dto.items.Add(dtoItems);
                }
            }
            return Ok(dto);

        }


        return NotFound($"Item id {order} not found!");
    }

    [HttpGet("[Action]/{orderId}")]
    public async Task<IActionResult> GetOrderItemById(int orderId)
    {


        return Ok();
    }


    // Data transfer Object

    [HttpPost]
    public async Task<IActionResult> AddOrder([FromBody]dtoOrders order)
    {
        if (ModelState.IsValid)
        {
            Order mdl = new()
            {
                CreatedDate = order.OrderDate,
                orderItems = new List<OrderItem>()
            };
            foreach (var item in order.items)
            {
                OrderItem orderItem = new()
                {
                    ItemId = item.ItemId,
                    Price = item.Price
                };
                mdl.orderItems.Add(orderItem);
            }

           
           await _db.Orders.AddAsync(mdl);
           await _db.SaveChangesAsync();
            order.OrderId = mdl.Id;
            return Ok(order);
        }
        return BadRequest();
       
    }
}
