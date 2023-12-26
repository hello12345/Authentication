using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CookieAuthentication_webApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SecureController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return Ok(new { Message = "Login successful" });
        }
    }
}
