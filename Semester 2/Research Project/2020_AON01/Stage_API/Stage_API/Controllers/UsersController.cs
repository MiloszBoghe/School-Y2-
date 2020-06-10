using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stage_API.Business.Interfaces;
using Stage_API.Business.Models;
using Stage_API.Data.IRepositories;
using Stage_API.Domain.Classes;

namespace Stage_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public class UsersController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IRequestHelper _helper;

        public UsersController(IUserRepository userRepository, IRequestHelper helper)
        {
            _userRepository = userRepository;
            _helper = helper;
        }

        [HttpGet("All")]
        public IActionResult GetAllUsers()
        {
            var currentUser = _helper.GetUser(HttpContext);

            if (!currentUser.IsCoordinator) return Unauthorized();

            var users = _userRepository.GetAll();
            return Ok(users);
        }
        // GET: api/Users/Profile/5
        [HttpGet("Profile/{id}")]
        public IActionResult GetUserProfile(int id)
        {
            var currentUser = _helper.GetUser(HttpContext);
            if (currentUser.Id != id && !currentUser.IsCoordinator) return Unauthorized();

            var user = _userRepository.GetProfile(id);
            if (user == null) return NotFound();

            var profileModel = currentUser.Role == "bedrijf" || (currentUser.Role == "coordinator" && user.IsBedrijf) ? new ProfileModelBedrijf((Bedrijf)user) : new ProfileModel(user);

            return Ok(profileModel);
        }


        //Update for Reviewer/Student/Coordinator Profile
        // PUT: api/Users/Profile/5
        [HttpPut("Profile/{id}")]
        public IActionResult UpdateUserProfile(int id, ProfileModel userProfile)
        {
            var currentUser = _helper.GetUser(HttpContext);
            if (currentUser.Id != id && !currentUser.IsCoordinator) return Unauthorized();

            if (string.IsNullOrEmpty(userProfile.Voornaam))
            {
                return BadRequest("Voornaam is required!");
            }

            var exists = _userRepository.UpdateUser(id, userProfile);

            if (!exists)
            {
                return NotFound();
            }
            return NoContent();
        }

        //Update for Reviewer/Student/Coordinator Profile
        // PUT: api/Users/Profile/5
        [HttpPut("Profile/Bedrijf/{id}")]
        public IActionResult UpdateBedrijfProfile(int id, ProfileModelBedrijf bedrijfProfile)
        {
            var currentUser = _helper.GetUser(HttpContext);
            if (currentUser.Id != id && !currentUser.IsCoordinator) return Unauthorized();

            var valid = _userRepository.UpdateBedrijf(id, bedrijfProfile);
            if (!valid) return NotFound();

            return NoContent();
        }

        [HttpGet("Deactivated")]
        public IActionResult GetDeactivatedUsers()
        {
            var currentUser = _helper.GetUser(HttpContext);

            if (!currentUser.IsCoordinator)
            {
                return Unauthorized();
            }

            var users = _userRepository.Find(u => !u.EmailConfirmed && u.Id != currentUser.Id);
            return Ok(users);
        }

        [HttpGet("Activated")]
        public IActionResult GetActivatedUsers()
        {
            var currentUser = _helper.GetUser(HttpContext);
            if (!currentUser.IsCoordinator)
            {
                return Unauthorized();
            }
            var users = _userRepository.Find(u => u.EmailConfirmed && u.Id != currentUser.Id);
            return Ok(users);
        }

        [HttpPatch("Activation/{id}")]
        public IActionResult PatchUsers(int id, [FromBody] bool newEmailConfirmed)
        {
            var user = _helper.GetUser(HttpContext);
            if (!user.IsCoordinator) return Unauthorized();

            var exists = _userRepository.PatchEmailConfirmed(id, newEmailConfirmed);
            if (!exists) return NotFound("User with specified ID doesn't exist!");

            return NoContent();
        }

    }
}