using Authentication_And_Authorization.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Security.Claims;

namespace Authentication_And_Authorization.Controllers
{
    [Authorize(Policy = "Administrator")]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly AuthorizationAndContext _authorizationAndContext;

        public UserController(AuthorizationAndContext authorizationAndContext)
        {
            _authorizationAndContext = authorizationAndContext;
        }

        //For admin Only
        [HttpGet]
        public IActionResult AdminEndPoint()
        {
            var currentUser = GetCurrentUser();
            return Ok($"Hi you are an {currentUser.EmpRole}");
        }
        private Employee GetCurrentUser()
        {           
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                if (identity != null)
                {
                   var userClaims = identity.Claims;
                    return new Employee
                    {
                        EmpName = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value!,
                        EmpPassword = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value!,
                        EmpRole = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value!
                    };
                }
            return null;
        }
    }
}
       
                 
