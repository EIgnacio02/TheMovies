using Microsoft.AspNetCore.Mvc;

namespace SL.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet("Login/{userName}")]
        public IActionResult Login(string userName)
        {
            ML.Result result = BL.Movie.LoginMovie(userName);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result);

            }
        }
    }
}
