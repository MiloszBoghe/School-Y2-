using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stage_API.Business.Interfaces;
using Stage_API.Business.Models;
using Stage_API.Data.IRepositories;
using Stage_API.Domain.Classes;
using Stage_API.Domain.enums;
using Stage_API.Domain.Relations;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Stage_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class StagevoorstellenController : ControllerBase
    {
        private readonly IStagevoorstelRepository _stagevoorstelRepository;
        private readonly IRequestHelper _helper;

        public StagevoorstellenController(IStagevoorstelRepository stagevoorstelRepository, IRequestHelper helper)
        {
            _stagevoorstelRepository = stagevoorstelRepository;
            _helper = helper;
        }

        // GET: api/Stagevoorstellen
        [HttpGet]
        public IActionResult GetStagevoorstellen()
        {
            var user = _helper.GetUser(HttpContext);

            if (user.Role != "student" && user.Role != "coordinator") return Unauthorized();

            var voorstellen = _stagevoorstelRepository
                .GetAll(s => s.Bedrijf, s => s.StudentenFavorieten, s=>s.Comments)
                .Select(s => new StagevoorstelModel(s, user.Role));

            if (user.Role == "student") voorstellen = voorstellen.Where(s => s.Status == BeoordelingStatus.Goedgekeurd);
            return Ok(voorstellen);

        }

        // GET: api/Stagevoorstellen/{id}
        [HttpGet("{id}")]
        public IActionResult GetStagevoorstel(int id)
        {
            var user = _helper.GetUser(HttpContext);
            var stagevoorstel = _stagevoorstelRepository.GetById(id);
            if (stagevoorstel == null)
            {
                return NotFound();
            }

            switch (user.Role)
            {
                case "student" when stagevoorstel.Status != BeoordelingStatus.Goedgekeurd:
                    return Unauthorized();
                case "bedrijf" when stagevoorstel.BedrijfId != user.Id:
                    return Unauthorized();
                case "reviewer" when stagevoorstel.ReviewersToegewezen.All(rt => rt.ReviewerId != user.Id):
                    return Unauthorized();
                default:
                    return Ok(new StagevoorstelModel(stagevoorstel, user.Role));
            }
        }

        // GET: api/Stagevoorstellen/Bedrijf/{id}
        [HttpGet("Bedrijf/{id}")]
        public IActionResult GetStagevoorstellenBedrijf(int id)
        {
            var user = _helper.GetUser(HttpContext);
            if (user.Role != "coordinator")
            {
                if (user.Role != "bedrijf" || user.Id != id) return Unauthorized();
            }
            var stagevoorstellen = _stagevoorstelRepository
                .Find(s => s.Bedrijf.Id == id, s => s.Bedrijf, s => s.Reviews, s => s.StudentenFavorieten, s=>s.Comments)
                .Select(s => new StagevoorstelModel(s, user.Role));

            return Ok(stagevoorstellen);
        }

        // PUT: api/Stagevoorstellen/{id}
        [HttpPut("{id}")]
        public IActionResult PutStagevoorstel(int id, StagevoorstelModel stagevoorstel)
        {
            var user = _helper.GetUser(HttpContext);

            if (id != stagevoorstel.Id) { return BadRequest(); }

            var originalVoorstel = _stagevoorstelRepository.GetById(id);

            if (originalVoorstel == null) { return NotFound("No stagevoorstel with id " + id); }
            if (user.Role != "coordinator" && user.Id != originalVoorstel.BedrijfId) return Unauthorized();

            //Because I'm using a DTO, I have to put all properties in a stagevoorstel object to update it properly.
            //The DTO is required to protect against overposting attacks.
            //Note that some properties are not changed by using the originalVoorstels property, otherwise the property becomes null if it's not in the model.
            var updatedStagevoorstel = stagevoorstel.Combine(originalVoorstel);


            if (user.Role == "coordinator")
            {
                updatedStagevoorstel.ReviewersToegewezen = stagevoorstel.ReviewersToegewezen.Select(rt => new ReviewerStagevoorstelToegewezen
                {
                    ReviewerId = rt.Id,
                    StagevoorstelId = id
                }).ToList();
                updatedStagevoorstel.StudentenToegewezen = stagevoorstel.StudentenToegewezen;
            }

            if (!_stagevoorstelRepository.Update(id, updatedStagevoorstel))
            {
                return NotFound();
            }
            return NoContent();



        }

        // POST: api/Stagevoorstellen
        [HttpPost]
        public IActionResult PostStagevoorstel(StagevoorstelModel stagevoorstelModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var user = _helper.GetUser(HttpContext);
            if (user.Role != "bedrijf" && user.Role != "coordinator") { return Unauthorized(); }

            var stagevoorstel = stagevoorstelModel.Make(user.Id);

            _stagevoorstelRepository.Add(stagevoorstel);

            return CreatedAtAction("GetStagevoorstel", new { id = stagevoorstel.Id }, stagevoorstel);
        }

        // DELETE: api/Stagevoorstellen/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteStagevoorstel(int id)
        {
            var user = _helper.GetUser(HttpContext);
            var stagevoorstel = _stagevoorstelRepository.GetById(id);
            if (!user.IsCoordinator && stagevoorstel.BedrijfId != user.Id) return Unauthorized();

            if (stagevoorstel == null)
            {
                return NotFound();
            }

            _stagevoorstelRepository.Remove(stagevoorstel);

            return NoContent();
        }

        // DELETE: api/Stagevoorstellen/
        [HttpDelete]
        public ActionResult DeleteStagevoorstellen(string ids)
        {
            if (string.IsNullOrEmpty(ids) || !Regex.Match(ids, "^[0-9]+(,[0-9]+)*$").Success) return BadRequest();
            var intArray = ids.Split(',').Select(i => Convert.ToInt32(i)).ToArray();

            var user = _helper.GetUser(HttpContext);
            if (!user.IsCoordinator && user.Role != "bedrijf") return Unauthorized();

            var stagevoorstellen = new Stagevoorstel[intArray.Length];
            var authorized = true;

            for (var i = 0; i < stagevoorstellen.Length; i++)
            {
                stagevoorstellen[i] = _stagevoorstelRepository.GetById(intArray[i]);
                if (stagevoorstellen[i] == null) return NotFound();
                if (user.IsCoordinator) continue;
                if (stagevoorstellen[i].BedrijfId != user.Id) authorized = false;
            }

            if (!authorized) return Unauthorized();
            _stagevoorstelRepository.RemoveRange(stagevoorstellen);

            return NoContent();
        }


        // PATCH: api/Stagevoorstellen/Status/{id}
        [HttpPatch("Status/{id}")]
        public IActionResult PatchStagevoorstel(int id, [FromBody] int status)
        {
            var user = _helper.GetUser(HttpContext);
            if (!user.IsCoordinator) return Unauthorized();
            if (_stagevoorstelRepository.PatchStatus(id, status))
            {
                return NoContent();
            }
            return NotFound("No stagevoorstel with id " + id);

        }
    }
}
