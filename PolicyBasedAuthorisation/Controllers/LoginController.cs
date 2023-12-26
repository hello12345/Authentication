using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using PolicyBasedAuthorisation.ViewModel;

namespace PolicyBasedAuthorisation.Controllers
{
    public class LoginController : Controller
    {
        [AllowAnonymous]
        [HttpPost, Route("login")]
        public IActionResult Login(LoginDTO loginDTO)
        {
            try
            {
                if (string.IsNullOrEmpty(loginDTO.UserName) ||
                string.IsNullOrEmpty(loginDTO.Password))
                    return BadRequest("Username and/or Password not specified");
                if ((loginDTO.UserName.Equals("admin") &&
                loginDTO.Password.Equals("admin")) || (loginDTO.UserName.Equals("user") &&
                loginDTO.Password.Equals("user")))
                {
                    List<Claim> claim = new List<Claim>();
                    if (loginDTO.UserName == "admin")
                    {
                        claim = new List<Claim>()
                        {
                            new Claim(ClaimTypes.Name, "JohnDoe"),
                            new Claim(ClaimTypes.DateOfBirth, "1990-01-01"),
                            new Claim(ClaimTypes.Role, "Admin")
                        };
                    }
                    else {
                        claim = new List<Claim>()
                        {
                            new Claim(ClaimTypes.Name, "JohnDoe"),
                            new Claim(ClaimTypes.DateOfBirth, "2015-01-01"),
                            new Claim(ClaimTypes.Role, "Admin")
                        };
                    }
                    var secretKey = new SymmetricSecurityKey
                    (Encoding.UTF8.GetBytes("8B/nU8w60QXrtfiDB7Cdp6vwIZnhFRYhTJRKm1CuZ5AhxeOrDqPyqlo3LjfG7pfb"));
                    var signinCredentials = new SigningCredentials
                    (secretKey, SecurityAlgorithms.HmacSha256);
                    var jwtSecurityToken = new JwtSecurityToken(
                        issuer: "ABCXYZ",
                        audience: "http://localhost:51398",
                        claims: claim,
                        expires: DateTime.Now.AddMinutes(10),
                        signingCredentials: signinCredentials
                    );
                    return Ok(new JwtSecurityTokenHandler().
                    WriteToken(jwtSecurityToken));

                }
            }
            catch
            {
                return BadRequest
                ("An error occurred in generating the token");
            }
            return Unauthorized();
        }
    }
}
