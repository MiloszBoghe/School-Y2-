using Microsoft.AspNetCore.Identity;
using Stage_API.Domain.Classes;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Stage_API.Domain
{
    public class User : IdentityUser<int>
    {
        public override string Email { get; set; }
        public string Voornaam { get; set; }
        public string Naam { get; set; }

        [IgnoreDataMember]
        public override bool EmailConfirmed { get; set; }

        [IgnoreDataMember]
        public override bool TwoFactorEnabled { get; set; }

        [IgnoreDataMember]
        public override string PhoneNumber { get; set; }

        [IgnoreDataMember]
        public override bool PhoneNumberConfirmed { get; set; }

        [IgnoreDataMember]
        public override string PasswordHash { get; set; }

        [IgnoreDataMember]
        public override string SecurityStamp { get; set; }

        [IgnoreDataMember]
        public override bool LockoutEnabled { get; set; }

        [IgnoreDataMember]
        public override DateTimeOffset? LockoutEnd { get; set; }

        [IgnoreDataMember]
        public override int AccessFailedCount { get; set; }

        [IgnoreDataMember]
        public override string NormalizedEmail { get; set; }

        [IgnoreDataMember]
        public override string NormalizedUserName { get; set; }

        [IgnoreDataMember]
        public override string ConcurrencyStamp { get; set; }

        public ICollection<Comment> Comments { get; set; }
        public bool IsBedrijf { get; set; }

    }
}
