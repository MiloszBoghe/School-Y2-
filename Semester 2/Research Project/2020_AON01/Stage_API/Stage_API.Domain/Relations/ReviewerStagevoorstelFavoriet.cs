using Stage_API.Domain.Classes;

namespace Stage_API.Domain.Relations
{
    public class ReviewerStagevoorstelFavoriet
    {
        public Reviewer Reviewer { get; set; }
        public int ReviewerId { get; set; }
        public Stagevoorstel Stagevoorstel { get; set; }
        public int StagevoorstelId { get; set; }
    }
}
