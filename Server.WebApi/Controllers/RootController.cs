using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Server.WebApi.Controllers
{
    [Route("/")]
    [ApiController]
    public class RootController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            // You can return a simple message or redirect to the Blazor app if needed.
            return Ok("Welcome to the API. Navigate to /api/ for API endpoints.");
        }
    }
}
