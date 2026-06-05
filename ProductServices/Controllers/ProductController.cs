using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProductServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        [HttpGet("GetProduct")]
        public async Task<IActionResult> Get()
        {
            return Ok(new { Message = "Hello from Product Service!" });
        }
    }
}
