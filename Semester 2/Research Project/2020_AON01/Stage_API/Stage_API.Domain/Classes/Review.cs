using Stage_API.Domain.enums;
using System;
using System.Runtime.Serialization;

namespace Stage_API.Domain.Classes
{
    public class Review
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Text { get; set; }
        public BeoordelingStatus Status { get; set; }
        public BeoordelingStatus StatusVoorstel { get; set; }
        public Reviewer Reviewer { get; set; }
        [IgnoreDataMember]
        public int ReviewerId { get; set; }
        public Stagevoorstel Stagevoorstel { get; set; }
        [IgnoreDataMember]
        public int StagevoorstelId { get; set; }
    }
}