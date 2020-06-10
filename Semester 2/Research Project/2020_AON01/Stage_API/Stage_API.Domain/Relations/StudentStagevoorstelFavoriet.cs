using Stage_API.Domain.Classes;

namespace Stage_API.Domain.Relations
{
    public class StudentStagevoorstelFavoriet
    {
        public Student Student { get; set; }
        public int StudentId { get; set; }
        public Stagevoorstel Stagevoorstel { get; set; }
        public int StagevoorstelId { get; set; }
    }
}
