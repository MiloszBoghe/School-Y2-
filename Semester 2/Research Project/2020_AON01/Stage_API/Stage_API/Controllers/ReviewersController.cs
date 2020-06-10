using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stage_API.Business.Interfaces;
using Stage_API.Business.Models;
using Stage_API.Data.IRepositories;
using Stage_API.Domain.Classes;
using System.Linq;

namespace Stage_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ReviewersController : ControllerBase
    {
        private readonly IReviewerRepository _reviewerRepository;
        private readonly IRequestHelper _helper;

        public ReviewersController(IReviewerRepository reviewerRepository, IRequestHelper helper)
        {
            _reviewerRepository = reviewerRepository;
            _helper = helper;
        }

        // GET: api/Reviewers
        [HttpGet]
        public IActionResult GetReviewers()
        {
            var user = _helper.GetUser(HttpContext);
            if (!user.IsCoordinator)
            {
                return Unauthorized();
            }
            var reviewers = _reviewerRepository.GetAll().Where(r => !r.IsCoordinator).Select(r => new ReviewerModel(r, user.Role)).ToList();
            return Ok(reviewers);
        }

        // GET: api/Reviewers/5
        [HttpGet("{id}")]
        public IActionResult GetReviewer(int id)
        {
            var user = _helper.GetUser(HttpContext);
            if (user.Id != id && !user.IsCoordinator) return Unauthorized();

            var reviewer = _reviewerRepository.GetById(id);
            if (reviewer == null)
            {
                return NotFound();
            }
            var reviewerModel = new ReviewerModel(reviewer, user.Role);

            return Ok(reviewerModel);

        }


        // PUT: api/Reviewers/5
        [HttpPut("{id}")]
        public IActionResult PutReviewer(int id, ReviewerModel reviewer)
        {
            var user = _helper.GetUser(HttpContext);
            if (user.Id != id) return Unauthorized();

            if (id != reviewer.Id)
            {
                return BadRequest();
            }
            var reviewerFound = _reviewerRepository.UpdateFavorieten(id, reviewer);
            if (!reviewerFound) return NotFound();

            return NoContent();
        }

    }
}
