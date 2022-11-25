
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Authentication_And_Authorization.Models;
using Microsoft.EntityFrameworkCore;

namespace Authentication_And_Authorization.Controllers
{
        [Route("api/[controller]")]
        [ApiController]
        public class LoginController : ControllerBase
        {
            private readonly IConfiguration _config;
            private readonly AuthorizationAndContext _authorizationAndContext;
            public LoginController(IConfiguration config, AuthorizationAndContext authorizationAndContext)
            {
                _config = config;
                _authorizationAndContext = authorizationAndContext;

            }

            [AllowAnonymous]
            [HttpPost]
            public ActionResult Login([FromBody] Employee Login)
            {
            //var user = Authenticate(Login);
            Employee user = _authorizationAndContext.Employees.Where(x => x.EmpName == Login.EmpName && x.EmpPassword == Login.EmpPassword && x.EmpRole == Login.EmpRole).FirstOrDefault()!;
            if (user != null)
                {
                    var token = GenerateToken(Login);
                    return Ok(token);
                }

                return NotFound("user not found");
            }

            // To generate token
            private string GenerateToken(Employee user)
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
                var claims = new[]
                {
                new Claim(ClaimTypes.NameIdentifier,user.EmpName!),
                 new Claim(ClaimTypes.NameIdentifier,user.EmpPassword!),
                 new Claim(ClaimTypes.Role,user.EmpRole)
            };
                var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                    _config["Jwt:Audience"],
                    claims,
                    expires: DateTime.Now.AddMinutes(15),
                    signingCredentials: credentials);


                return new JwtSecurityTokenHandler().WriteToken(token);

            }

            //To authenticate user
            //private UserModel Authenticate(UserLogin userLogin)
            //{
            //    var currentUser = UserConstants.Users.FirstOrDefault(x => x.Username.ToLower() ==
            //        userLogin.UserName.ToLower() && x.Password == userLogin.Password);
            //    if (currentUser != null)
            //    {
            //        return currentUser;
            //    }
            //    return null!;
            //}
        }
}
