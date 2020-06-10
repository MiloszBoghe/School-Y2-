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
    public class CommentsController : ControllerBase
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IRequestHelper _helper;

        public CommentsController(ICommentRepository commentRepository, IRequestHelper helper)
        {
            _commentRepository = commentRepository;
            _helper = helper;
        }


        // POST: api/Comments
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public ActionResult PostComment(Comment comment)
        {
            var user = _helper.GetUser(HttpContext);
            if (user.Role != "coordinator" && user.Role != "reviewer" && user.Role != "bedrijf") return Unauthorized();

            comment.Date = DateTime.Now;
            _commentRepository.Add(comment);
            return NoContent();
        }

        // DELETE: api/Comments/5
        /*[HttpDelete("{id}")]
        public ActionResult DeleteComment(int id)
        {
            var comment = _commentRepository.GetById(id);

            if (comment == null)
            {
                return NotFound();
            }

            _commentRepository.Remove(comment);

            return NoContent();
        }
        */
    }
}
