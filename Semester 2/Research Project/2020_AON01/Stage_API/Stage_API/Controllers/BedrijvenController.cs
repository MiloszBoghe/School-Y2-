using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stage_API.Business.Interfaces;
using Stage_API.Data.IRepositories;

namespace Stage_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class BedrijvenController : ControllerBase
    {
        private readonly IBedrijfRepository _bedrijfRepository;
        private readonly IRequestHelper _helper;


        public BedrijvenController(IBedrijfRepository bedrijfRepository, IRequestHelper helper)
        {
            _bedrijfRepository = bedrijfRepository;
            _helper = helper;
        }

        // GET: api/Bedrijven
        [HttpGet]
        public IActionResult GetBedrijven()
        {
            var user = _helper.GetUser(HttpContext);
            if (!user.IsCoordinator)
            {
                return Unauthorized();
            }
            var bedrijven = _bedrijfRepository.GetAll();
            return Ok(bedrijven);
        }

    }
}
