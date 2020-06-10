using Stage_API.Domain.Relations;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Stage_API.Domain.Classes
{
    public class Student : User
    {
        public ICollection<StudentStagevoorstelFavoriet> FavorieteOpdrachten { get; set; }
        public Stagevoorstel ToegewezenStageOpdracht { get; set; }
        [IgnoreDataMember]
        public int? StagevoorstelId { get; set; }
    }
}