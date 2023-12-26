using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WindAuthontication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet]
        public string get()
        {
            return HttpContext.User.Identity.Name;
        }
    }
}
