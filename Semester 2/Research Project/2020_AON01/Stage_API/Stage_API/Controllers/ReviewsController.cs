using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stage_API.Business.Interfaces;
using Stage_API.Data.IRepositories;
using Stage_API.Domain.Classes;
using System;

namespace Stage_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IRequestHelper _helper;
        public ReviewsController(IReviewRepository reviewRepository, IRequestHelper helper)
        {
            _reviewRepository = reviewRepository;
            _helper = helper;
        }

        // GET: api/Reviews/Stagevoorstel/5
        [HttpGet("Stagevoorstel/{stagevoorstelId}")]
        public IActionResult GetReviewsByVoorstel(int stagevoorstelId)
        {
            var user = _helper.GetUser(HttpContext);
            if (!user.IsCoordinator && user.Role != "reviewer") return Unauthorized();

            var reviews = _reviewRepository.GetReviewsByVoorstel(stagevoorstelId);

            if (reviews == null)
            {
                return NotFound();
            }

            return Ok(reviews);
        }

        // PATCH: api/Reviews/Status/{id}
        [HttpPatch("Status/{id}")]
        public IActionResult PatchReview(int id, [FromBody] int status)
        {
            var user = _helper.GetUser(HttpContext);
            if (user.Role != "reviewer" && !user.IsCoordinator) return Unauthorized();

            _reviewRepository.PatchStatus(id, status);

            return NoContent();
        }
        // POST: api/Reviews
        [HttpPost]
        public IActionResult PostReview(Review review)
        {
            var user = _helper.GetUser(HttpContext);
            if (user.Role != "reviewer" && !user.IsCoordinator) return Unauthorized();

            review.ReviewerId = user.Id;
            review.Date = DateTime.Now;
            _reviewRepository.Add(review);
            return Ok("Created " + review);
        }

        // DELETE: api/Reviews/5
        [HttpDelete("{id}")]
        public IActionResult DeleteReview(int id)
        {
            var user = _helper.GetUser(HttpContext);
            if (!user.IsCoordinator) return Unauthorized();

            var review = _reviewRepository.GetById(id);
            if (review == null)
            {
                return NotFound();
            }

            _reviewRepository.Remove(review);

            return Ok("Review removed.");
        }

    }
}
