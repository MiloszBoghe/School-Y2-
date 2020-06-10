using Stage_API.Domain.Classes;
using System;

namespace Stage_API.Domain.Relations
{
    public class ReviewerStagevoorstelToegewezen : IEquatable<ReviewerStagevoorstelToegewezen>
    {
        public Reviewer Reviewer { get; set; }
        public int ReviewerId { get; set; }
        public Stagevoorstel Stagevoorstel { get; set; }
        public int StagevoorstelId { get; set; }

        public bool Equals(ReviewerStagevoorstelToegewezen other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return ReviewerId == other.ReviewerId;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == this.GetType() && Equals((ReviewerStagevoorstelToegewezen)obj);
        }
        public override int GetHashCode()
        {
            return ReviewerId;
        }
    }
}
