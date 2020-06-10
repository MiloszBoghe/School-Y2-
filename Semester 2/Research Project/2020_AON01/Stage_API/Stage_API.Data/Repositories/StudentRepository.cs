using Microsoft.EntityFrameworkCore;
using Stage_API.Business.Models;
using Stage_API.Data.IRepositories;
using Stage_API.Domain.Classes;
using Stage_API.Domain.Relations;
using System.Collections.Generic;
using System.Linq;

namespace Stage_API.Data.Repositories
{
    public class StudentRepository : GenericRepository<Student>, IStudentRepository
    {
        private readonly StageContext _context;

        public StudentRepository(StageContext context) : base(context)
        {
            _context = context;
        }

        public override Student GetById(int id)
        {
            var student = _context.Studenten.Include(s => s.FavorieteOpdrachten)
                .ThenInclude(ssf => ssf.Stagevoorstel).ThenInclude(s => s.Bedrijf)
                .Include(s => s.ToegewezenStageOpdracht).ThenInclude(s => s.Bedrijf)
                .FirstOrDefault(s => s.Id == id);

            return student;
        }

        public bool UpdateToegewezen(int id, StudentModel student)
        {
            var originalStudent = _context.Studenten.Where(s => s.Id == id).Include(s => s.ToegewezenStageOpdracht).FirstOrDefault();
            if (originalStudent == null)
            {
                return false;
            }

            if (originalStudent.ToegewezenStageOpdracht.Id == student.ToegewezenStageOpdracht.Id) return false;
            originalStudent.ToegewezenStageOpdracht = student.ToegewezenStageOpdracht;

            Save();
            return true;
        }

        public bool UpdateFavorieten(int id, StudentModel student)
        {
            var originalStudent = _context.Studenten.AsNoTracking().Where(s => s.Id == student.Id).Include(s => s.FavorieteOpdrachten).FirstOrDefault();
            if (originalStudent == null)
            {
                return false;
            }

            //Convert StudentModels collection of stagevoorstellen to a collection of relations to update the database:
            var newFavorieten = student.FavorieteOpdrachten.Select(fav => new StudentStagevoorstelFavoriet { StudentId = student.Id, StagevoorstelId = fav.Id }).ToList();

            UpdateFavorietenStudent(student.Id, newFavorieten);
            return true;
        }


        private void UpdateFavorietenStudent(int id, IReadOnlyCollection<StudentStagevoorstelFavoriet> newFavorieten)
        {
            var originalStudent = _context.Studenten
                .Include(s => s.FavorieteOpdrachten).First(s => s.Id == id);

            //Remove all favoriete studenten
            originalStudent.FavorieteOpdrachten = new List<StudentStagevoorstelFavoriet>();
            Save();

            if (!newFavorieten.Any()) return;
            //clear context and add updated list of favoriete studenten.
            _context.DetachEntries();
            newFavorieten.ToList().ForEach(relation => _context.Entry(relation).State = EntityState.Added);
            Save();
        }
    }
}