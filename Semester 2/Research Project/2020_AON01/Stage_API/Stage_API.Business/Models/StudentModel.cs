using Stage_API.Domain.Classes;
using System.Collections.Generic;
using System.Linq;

namespace Stage_API.Business.Models
{
    public class StudentModel
    {
        public int Id { get; set; }

        public ICollection<StagevoorstelModel> FavorieteOpdrachten { get; set; }
        public Stagevoorstel ToegewezenStageOpdracht { get; set; }

        public StudentModel()
        {

        }

        public StudentModel(Student student)
        {
            const string role = "student";
            Id = student.Id;
            FavorieteOpdrachten = student.FavorieteOpdrachten.Select(s => new StagevoorstelModel(s.Stagevoorstel, role)).ToList();
            ToegewezenStageOpdracht = student.ToegewezenStageOpdracht;
        }
    }
}
