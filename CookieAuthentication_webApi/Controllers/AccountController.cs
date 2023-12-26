using CookieAuthentication_webApi.ViewModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CookieAuthentication_webApi.Controllers
{
    public class AccountController : Controller
    {   
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (IsValidUser(model))
            {
                var claims = new[]
                {
                new Claim(ClaimTypes.Name, model.Username),
                // Add other claims as needed
            };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties
                {
                    // Configure additional properties, if needed
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

                return Ok(new { Message = "login successful" });
            }

            return Unauthorized(new { Message = "not valid login details" });
        }

        [HttpGet("logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Ok(new { Message = "Logout successful" });
        }

        [HttpGet("AccessDenied")]
        public IActionResult AccessDenied()
        {
            return Ok(new { Message = "you don't have access" });
        }
        private bool IsValidUser(LoginViewModel model)
        {
            // Replace this with your actual user validation logic, e.g., checking against a database
            return model.Username == "demo" && model.Password == "demo";
        }
    }
}
