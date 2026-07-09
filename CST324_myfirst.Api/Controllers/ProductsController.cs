using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyGuitarShop.Data.Ado.Repository;

namespace CST324_myfirst.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(ILogger<ProductsController> logger, ProductRepo repo) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try 
            {
                var products = await repo.GetAllProductsAsync();

                return Ok(products.Select(products => products.ProductName));
            }

            catch (Exception ex)
            {
                logger.LogError(ex, "Error fetching Products");
                return StatusCode(StatusCodes.Status500InternalServerError);
                
            }
        }
    }
}
