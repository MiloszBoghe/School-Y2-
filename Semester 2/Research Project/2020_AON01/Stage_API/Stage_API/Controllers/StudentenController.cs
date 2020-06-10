using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stage_API.Business.Interfaces;
using Stage_API.Business.Models;
using Stage_API.Data.IRepositories;

namespace Stage_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class StudentenController : ControllerBase
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IRequestHelper _helper;

        public StudentenController(IStudentRepository studentRepository, IRequestHelper helper)
        {
            _studentRepository = studentRepository;
            _helper = helper;
        }

        // GET: api/Studenten
        [HttpGet]
        public IActionResult GetStudenten()
        {
            var user = _helper.GetUser(HttpContext);
            if (!user.IsCoordinator) { return Unauthorized(); }
            var studenten = _studentRepository.GetAll(s => s.FavorieteOpdrachten, s => s.ToegewezenStageOpdracht);
            return Ok(studenten);
        }


        // GET: api/Studenten/5
        [HttpGet("{id}")]
        public IActionResult GetStudent(int id)
        {
            var user = _helper.GetUser(HttpContext);

            if (user.Id != id && !user.IsCoordinator) return Unauthorized();

            var student = _studentRepository.GetById(id);
            if (student == null) return NotFound();

            var studentModel = new StudentModel(student);
            return Ok(studentModel);
        }


        // PUT: api/Studenten/5
        [HttpPut("{id}")]
        public IActionResult PutStudent(int id, StudentModel student)
        {
            var user = _helper.GetUser(HttpContext);

            if (id != student.Id) return BadRequest();
            if (user.IsCoordinator)
            {
                if (!_studentRepository.UpdateToegewezen(id, student)) return NotFound();
            }
            else if (user.Id != id)
            {
                return Unauthorized();
            }
            else
            {
                if (!_studentRepository.UpdateFavorieten(id, student)) return NotFound();
            }
            return NoContent();
        }
    }
}