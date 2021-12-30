using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Product.Service.Controllers
{
    [ApiController]
    [Route("api/product")]
    public class ProductController : ControllerBase
    {
        [HttpGet]
        [Route("getbyid/{id}")]
        public async Task<IActionResult> GetById (int? id)
        {
            return Ok("Produto enviado!");
        }
    }
}
