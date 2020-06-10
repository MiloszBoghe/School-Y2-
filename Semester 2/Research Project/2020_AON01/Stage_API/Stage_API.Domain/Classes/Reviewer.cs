using Stage_API.Domain.Relations;
using System.Collections.Generic;

namespace Stage_API.Domain.Classes
{
    public class Reviewer : User
    {
        public ICollection<ReviewerStagevoorstelToegewezen> ToegewezenVoorstellen { get; set; }
        public ICollection<ReviewerStagevoorstelFavoriet> VoorkeurVoorstellen { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public bool IsCoordinator { get; set; }
    }
}