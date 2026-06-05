using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using OrderServices.Data;
using OrderServices.Dto;
using OrderServices.Handler;

namespace OrderServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController(AppDbContext _context, ProductServiceClient productServiceClient) : ControllerBase
    {
        [EnableRateLimiting("order-policy")]
        [Authorize]
        [HttpGet("GetOrders")]
        public IActionResult GetOrders()
        {
            var orders = _context.Orders.ToList();
            return Ok(orders);
        }

        [Authorize]
        [HttpPost("CreateOrders")]
        public IActionResult CreateOrders(OrderDto order)
        {
            var orders = new Entities.Order
            {
                IsPayment = order.IsPayment,
                Status = order.Status,
                UserId = order.UserId
            };
            _context.Orders.Add(orders);
            _context.SaveChanges();
            return Ok(order);
        }

        [Authorize]
        [HttpGet("GetProduct")]
        public async Task<IActionResult> GetProduct()
        {
            var data = await productServiceClient.GetProductAsync();

            return Ok(data);
        }
    }
}
