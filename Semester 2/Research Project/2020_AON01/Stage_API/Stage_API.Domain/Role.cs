using Microsoft.AspNetCore.Identity;

namespace Stage_API.Domain
{
    public class Role : IdentityRole<int>
    {
        public const string Reviewer = "reviewer";
        public const string Student = "student";
        public const string Bedrijf = "bedrijf";
    }
}
