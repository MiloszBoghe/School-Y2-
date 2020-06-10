using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stage_API.Business.Models;
using System.Linq;

namespace Stage_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ClaimsController : ControllerBase
    {

        public ClaimsController()
        {

        }

        // GET api/Claims
        [HttpGet]
        public IActionResult GetClaims()
        {
            var claims = HttpContext.User.Claims.ToList();
            var claimModel = new ClaimModel(claims);
            return Ok(claimModel);
        }


    }
}
