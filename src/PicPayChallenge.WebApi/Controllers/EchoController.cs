using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PicPayChallenge.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class EchoController : Controller
    {
        [AllowAnonymous]
        public IActionResult Get()
        {
            return Ok(new { message = "App is running!" });
        }
    }
}