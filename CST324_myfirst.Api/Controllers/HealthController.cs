using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyGuitarShop.Data.Ado.Factories;

namespace CST324_myfirst.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthController (ILogger<HealthController> logger,
        SqlConnectionFactory sqlConnectionFactory) : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok("Healthy");
            }

            catch (Exception)
            {
                logger.LogWarning("Health check failed unreasonably. ");

                return StatusCode(503, "Unhealthy");
                
            }
        }

        [HttpGet("db")]
        public IActionResult GetDbHealth()
        {
            try
            {
                using var connection = sqlConnectionFactory.OpenSqlConnection();
                return Ok("Healthy");
            }

            catch (Exception)
            {
                logger.LogCritical("Database Health check failed. ");

                return StatusCode(503, "Database Unhealthy");

            }
        }

    }
}
