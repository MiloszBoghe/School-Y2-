using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Stage_API.Domain;
using Stage_API.Domain.Classes;
using Stage_API.Domain.enums;
using Stage_API.Domain.Relations;
using System;
using System.Linq;

namespace Stage_API.Data
{
    public class StageContext : IdentityDbContext<User, Role, int>
    {
        public DbSet<Stagevoorstel> Stagevoorstellen { get; set; }
        public DbSet<Student> Studenten { get; set; }
        public DbSet<Bedrijf> Bedrijven { get; set; }
        public DbSet<Bedrijfspromotor> Bedrijfspromotors { get; set; }
        public DbSet<Reviewer> Reviewers { get; set; }
        public DbSet<Contactpersoon> Contactpersonen { get; set; }
        public DbSet<ReviewerStagevoorstelFavoriet> FavorietenStagevoorstellenReviewer { get; set; }
        public DbSet<ReviewerStagevoorstelToegewezen> ToegewezenStagevoorstellenReviewer { get; set; }
        public DbSet<StudentStagevoorstelFavoriet> FavorietenStagevoorstellenStudent { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Comment> Comments { get; set; }

        public DbSet<ResetPasswordRequest> ResetPasswordRequests { get; set; }

        public StageContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            #region PK
            builder.Entity<ReviewerStagevoorstelFavoriet>().HasKey(rs => new { rs.ReviewerId, rs.StagevoorstelId });
            builder.Entity<ReviewerStagevoorstelToegewezen>().HasKey(rs => new { rs.ReviewerId, rs.StagevoorstelId });
            builder.Entity<StudentStagevoorstelFavoriet>().HasKey(ss => new { ss.StudentId, ss.StagevoorstelId });
            builder.Entity<Stagevoorstel>().HasKey(s => s.Id);
            builder.Entity<Bedrijfspromotor>().HasKey(b => b.Id);
            builder.Entity<Contactpersoon>().HasKey(c => c.Id);
            builder.Entity<Review>().HasKey(f => f.Id);
            builder.Entity<Comment>().HasKey(c => c.Id);
            builder.Entity<ResetPasswordRequest>().HasKey(r => r.PasswordResetToken);
            #endregion

            #region one-to-many

            //relation - x reviews --> 1 stagevoorstel
            builder.Entity<Student>()
                .HasOne(s => s.ToegewezenStageOpdracht)
                .WithMany(s => s.StudentenToegewezen)
                .HasForeignKey(s => s.StagevoorstelId)
                .OnDelete(DeleteBehavior.SetNull);

            //relation - x reviews --> 1 stagevoorstel
            builder.Entity<Review>()
                .HasOne(r => r.Stagevoorstel)
                .WithMany(s => s.Reviews)
                .HasForeignKey(r => r.StagevoorstelId);

            //relation - 1 reviewer - x reviews
            builder.Entity<Review>()
                .HasOne(review => review.Reviewer)
                .WithMany(reviewer => reviewer.Reviews)
                .HasForeignKey(r => r.ReviewerId);

            //relation - x comments --> 1 stagevoorstel
            builder.Entity<Comment>()
                .HasOne(c => c.Stagevoorstel)
                .WithMany(s => s.Comments)
                .HasForeignKey(c => c.StagevoorstelId);

            //relation - x comments --> 1 user
            builder.Entity<Comment>()
                .HasOne(c => c.User)
                .WithMany(u => u.Comments)
                .HasForeignKey(c => c.UserId);

            //relation - 1 bedrijf - x stagevoorstellen
            builder.Entity<Bedrijf>()
                .HasMany(b => b.Stagevoorstellen)
                .WithOne(s => s.Bedrijf);

            #endregion

            #region many-to-many
            //Zorgt voor many to many relationships

            //x reviewers toegewezen --> <-- x stagevoorstellen
            builder.Entity<ReviewerStagevoorstelToegewezen>()
                .HasOne(rst => rst.Reviewer)
                .WithMany(rst => rst.ToegewezenVoorstellen)
                .HasForeignKey(rst => rst.ReviewerId);

            builder.Entity<ReviewerStagevoorstelToegewezen>()
                .HasOne(rst => rst.Stagevoorstel)
                .WithMany(rst => rst.ReviewersToegewezen)
                .HasForeignKey(rst => rst.StagevoorstelId);

            //x reviewers favorieten --> <-- x stagevoorstellen
            builder.Entity<ReviewerStagevoorstelFavoriet>()
                .HasOne(rsf => rsf.Reviewer)
                .WithMany(rsf => rsf.VoorkeurVoorstellen)
                .HasForeignKey(rsf => rsf.ReviewerId);

            builder.Entity<ReviewerStagevoorstelFavoriet>()
                .HasOne(rsf => rsf.Stagevoorstel)
                .WithMany(rsf => rsf.ReviewersFavorieten)
                .HasForeignKey(rsf => rsf.StagevoorstelId);

            //x reviewers toegewezen --> <-- x stagevoorstellen
            builder.Entity<StudentStagevoorstelFavoriet>()
                .HasOne(ssf => ssf.Student)
                .WithMany(ssf => ssf.FavorieteOpdrachten)
                .HasForeignKey(ssf => ssf.StudentId);

            builder.Entity<StudentStagevoorstelFavoriet>()
                .HasOne(ssf => ssf.Stagevoorstel)
                .WithMany(ssf => ssf.StudentenFavorieten)
                .HasForeignKey(ssf => ssf.StagevoorstelId);

            #endregion

            #region other
            var valueComparer = new ValueComparer<string[]>(
                (s1, s2) => s1.SequenceEqual(s2),
                s => s.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                c => c.ToArray());

            //om een string bij te houden in database maar deze te splitsen op ',' bij het ophalen van data (dus string array): 
            builder.Entity<Stagevoorstel>()
            .Property(e => e.AfstudeerrichtingVoorkeur)
            .HasConversion(
                v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries))
             .Metadata
            .SetValueComparer(valueComparer);
            builder.Entity<Stagevoorstel>()
            .Property(e => e.Omgeving)
            .HasConversion(
                v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries))
            .Metadata
            .SetValueComparer(valueComparer);
            builder.Entity<Stagevoorstel>()
            .Property(e => e.Verwachtingen)
            .HasConversion(
                v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries))
            .Metadata
            .SetValueComparer(valueComparer);

            #endregion

            //Seeding Data:
            #region Users (Reviewers, Studenten, Bedrijven)

            builder.Entity<Reviewer>().HasData(
                new Reviewer
                {
                    Id = 1,
                    UserName = "marijke.willems@pxl.be",
                    NormalizedUserName = "MARIJKE.WILLEMS@PXL.BE",
                    PasswordHash = "AQAAAAEAACcQAAAAEE3MADTcj5R9NBdPZGuSrqvcfisB2jZdZoPa3+AbxcGsaSOQAO6JOO5UxUQ+SerEAw==",
                    Voornaam = "Marijke",
                    Naam = "Willems",
                    Email = "Marijke.Willems@pxl.be",
                    NormalizedEmail = "MARIJKE.WILLEMS@PXL.BE",
                    EmailConfirmed = true,
                    IsCoordinator = true,
                    LockoutEnabled = true,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    ConcurrencyStamp = Guid.NewGuid().ToString()

                },
                new Reviewer
                {
                    Id = 2,
                    UserName = "kris.hermans@pxl.be",
                    NormalizedUserName = "KRIS.HERMANS@PXL.BE",
                    PasswordHash = "AQAAAAEAACcQAAAAEE3MADTcj5R9NBdPZGuSrqvcfisB2jZdZoPa3+AbxcGsaSOQAO6JOO5UxUQ+SerEAw==",
                    Voornaam = "Kris",
                    Naam = "Hermans",
                    Email = "Kris.Hermans@pxl.be",
                    NormalizedEmail = "KRIS.HERMANS@PXL.BE",
                    EmailConfirmed = true,
                    LockoutEnabled = true,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                },
                new Reviewer
                {
                    Id = 3,
                    UserName = "lowie.vangaal@pxl.be",
                    NormalizedUserName = "LOWIE.VANGAAL@PXL.BE",
                    PasswordHash = "AQAAAAEAACcQAAAAEE3MADTcj5R9NBdPZGuSrqvcfisB2jZdZoPa3+AbxcGsaSOQAO6JOO5UxUQ+SerEAw==",
                    Voornaam = "Lowie",
                    Naam = "Vangaal",
                    Email = "Lowie.Vangaal@pxl.be",
                    NormalizedEmail = "LOWIE.VANGAAL@PXL.BE",
                    EmailConfirmed = true,
                    LockoutEnabled = true,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                },
                new Reviewer
                {
                    Id = 4,
                    UserName = "arno.barzan@pxl.be",
                    NormalizedUserName = "ARNO.BARZAN@PXL.BE",
                    PasswordHash = "AQAAAAEAACcQAAAAEE3MADTcj5R9NBdPZGuSrqvcfisB2jZdZoPa3+AbxcGsaSOQAO6JOO5UxUQ+SerEAw==",
                    Voornaam = "Arno",
                    Naam = "Barzan",
                    Email = "arno.barzan@pxl.be",
                    NormalizedEmail = "ARNO.BARZAN@PXL.BE",
                    EmailConfirmed = true,
                    LockoutEnabled = true,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                }
            );

            builder.Entity<Student>().HasData(
                new Student
                {
                    Id = 5,
                    UserName = "pieter.janssen@student.pxl.be",
                    NormalizedUserName = "PIETER.JANSSEN@STUDENT.PXL.BE",
                    PasswordHash = "AQAAAAEAACcQAAAAEE3MADTcj5R9NBdPZGuSrqvcfisB2jZdZoPa3+AbxcGsaSOQAO6JOO5UxUQ+SerEAw==",
                    Voornaam = "Pieter",
                    Naam = "Janssen",
                    Email = "pieter.janssen@student.pxl.be",
                    NormalizedEmail = "PIETER.JANSSEN@STUDENT.PXL.BE",
                    EmailConfirmed = true,
                    LockoutEnabled = true,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                },
                new Student
                {
                    Id = 6,
                    UserName = "milosz.boghe@student.pxl.be",
                    NormalizedUserName = "MILOSZ.BOGHE@STUDENT.PXL.BE",
                    PasswordHash = "AQAAAAEAACcQAAAAEE3MADTcj5R9NBdPZGuSrqvcfisB2jZdZoPa3+AbxcGsaSOQAO6JOO5UxUQ+SerEAw==",
                    Voornaam = "Milosz",
                    Naam = "Boghe",
                    Email = "milosz.boghe@student.pxl.be",
                    NormalizedEmail = "MILOSZ.BOGHE@STUDENT.PXL.BE",
                    StagevoorstelId = 2,
                    EmailConfirmed = true,
                    LockoutEnabled = true,
                    AccessFailedCount = 0,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                },
                new Student
                {
                    Id = 7,
                    UserName = "brent.leemans@student.pxl.be",
                    NormalizedUserName = "BRENT.LEEMANS@STUDENT.PXL.BE",
                    PasswordHash = "AQAAAAEAACcQAAAAEE3MADTcj5R9NBdPZGuSrqvcfisB2jZdZoPa3+AbxcGsaSOQAO6JOO5UxUQ+SerEAw==",
                    Voornaam = "Brent",
                    Naam = "Leemans",
                    Email = "brent.leemans@student.pxl.be",
                    NormalizedEmail = "BRENT.LEEMANS@STUDENT.PXL.BE",
                    StagevoorstelId = 1,
                    EmailConfirmed = true,
                    LockoutEnabled = true,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                },
                new Student
                {
                    Id = 8,
                    UserName = "ruben.vlassak@student.pxl.be",
                    NormalizedUserName = "RUBEN.VLASSAK@STUDENT.PXL.BE",
                    PasswordHash = "AQAAAAEAACcQAAAAEE3MADTcj5R9NBdPZGuSrqvcfisB2jZdZoPa3+AbxcGsaSOQAO6JOO5UxUQ+SerEAw==",
                    Voornaam = "Ruben",
                    Naam = "Vlassak",
                    Email = "ruben.vlassak@student.pxl.be",
                    NormalizedEmail = "RUBEN.VLASSAK@STUDENT.PXL.BE",
                    StagevoorstelId = 2,
                    EmailConfirmed = true,
                    LockoutEnabled = true,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                },
                new Student
                {
                    Id = 9,
                    UserName = "kaan.ozdemi@student.pxl.be",
                    NormalizedUserName = "KAAN.OZDEMI@STUDENT.PXL.BE",
                    PasswordHash = "AQAAAAEAACcQAAAAEE3MADTcj5R9NBdPZGuSrqvcfisB2jZdZoPa3+AbxcGsaSOQAO6JOO5UxUQ+SerEAw==",
                    Voornaam = "Kaan",
                    Naam = "Ozdemir",
                    Email = "kaan.ozdemi@student.pxl.be",
                    NormalizedEmail = "KAAN.OZDEMI@STUDENT.PXL.BE",
                    StagevoorstelId = 1,
                    EmailConfirmed = true,
                    LockoutEnabled = true,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                },
                new Student
                {
                    Id = 10,
                    UserName = "lennert.schoenmakers@student.pxl.be",
                    NormalizedUserName = "LENNERT.SCHOENMAKERS@STUDENT.PXL.BE",
                    PasswordHash = "AQAAAAEAACcQAAAAEE3MADTcj5R9NBdPZGuSrqvcfisB2jZdZoPa3+AbxcGsaSOQAO6JOO5UxUQ+SerEAw==",
                    Voornaam = "Lennert",
                    Naam = "Schoenmakers",
                    Email = "lennert.schoenmakers@student.pxl.be",
                    NormalizedEmail = "LENNERT.SCHOENMAKERS@STUDENT.PXL.BE",
                    EmailConfirmed = true,
                    LockoutEnabled = true,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                }
            );

            builder.Entity<Bedrijf>().HasData(
                    new Bedrijf
                    {
                        Id = 11,
                        UserName = "contact@cegeka.be",
                        NormalizedUserName = "CONTACT@CEGEKA.BE",
                        Naam = "Cegeka",
                        Email = "contact@cegeka.be",
                        NormalizedEmail = "CONTACT@CEGEKA.BE",
                        PasswordHash = "AQAAAAEAACcQAAAAEE3MADTcj5R9NBdPZGuSrqvcfisB2jZdZoPa3+AbxcGsaSOQAO6JOO5UxUQ+SerEAw==",
                        Adres = "Kempische Steenweg 307",
                        Postcode = "3500",
                        Gemeente = "Hasselt",
                        AantalMedewerkers = 233,
                        AantalITMedewerkers = 200,
                        AantalBegeleidendeMedewerkers = 33,
                        ContactpersoonId = 1,
                        BedrijfspromotorId = 1,
                        IsBedrijf = true,
                        EmailConfirmed = true,
                        LockoutEnabled = true,
                        SecurityStamp = Guid.NewGuid().ToString(),
                        ConcurrencyStamp = Guid.NewGuid().ToString()
                    },
                    new Bedrijf
                    {
                        Id = 12,
                        UserName = "contact@tesla.be",
                        NormalizedUserName = "CONTACT@TESLA.BE",
                        Naam = "Tesla",
                        Email = "contact@tesla.be",
                        NormalizedEmail = "CONTACT@TESLA.BE",
                        PasswordHash = "AQAAAAEAACcQAAAAEE3MADTcj5R9NBdPZGuSrqvcfisB2jZdZoPa3+AbxcGsaSOQAO6JOO5UxUQ+SerEAw==",
                        Adres = "Boomse Steenweg 8",
                        Postcode = "2630",
                        Gemeente = "Aartselaar",
                        AantalMedewerkers = 666,
                        AantalITMedewerkers = 600,
                        AantalBegeleidendeMedewerkers = 66,
                        ContactpersoonId = 2,
                        BedrijfspromotorId = 2,
                        IsBedrijf = true,
                        EmailConfirmed = true,
                        LockoutEnabled = true,
                        SecurityStamp = Guid.NewGuid().ToString(),
                        ConcurrencyStamp = Guid.NewGuid().ToString()
                    },
                    new Bedrijf
                    {
                        Id = 13,
                        UserName = "contact@microsoft.nl",
                        NormalizedUserName = "CONTACT@MICROSOFT.NL",
                        Naam = "Microsoft Nederland",
                        Email = "contact@microsoft.nl",
                        NormalizedEmail = "CONTACT@MICROSOFT.NL",
                        PasswordHash = "AQAAAAEAACcQAAAAEE3MADTcj5R9NBdPZGuSrqvcfisB2jZdZoPa3+AbxcGsaSOQAO6JOO5UxUQ+SerEAw==",
                        Adres = "Evert van de Beekstraat 354",
                        Postcode = "1118CZ",
                        Gemeente = "Schiphol",
                        AantalMedewerkers = 4013,
                        AantalITMedewerkers = 3648,
                        AantalBegeleidendeMedewerkers = 365,
                        ContactpersoonId = 3,
                        BedrijfspromotorId = 3,
                        IsBedrijf = true,
                        EmailConfirmed = true,
                        LockoutEnabled = true,
                        SecurityStamp = Guid.NewGuid().ToString(),
                        ConcurrencyStamp = Guid.NewGuid().ToString()
                    },
                    new Bedrijf
                    {
                        Id = 14,
                        UserName = "contact@dell.be",
                        NormalizedUserName = "CONTACT@DELL.BE",
                        Naam = "Dell",
                        Email = "contact@dell.be",
                        NormalizedEmail = "CONTACT@DELL.BE",
                        PasswordHash = "AQAAAAEAACcQAAAAEE3MADTcj5R9NBdPZGuSrqvcfisB2jZdZoPa3+AbxcGsaSOQAO6JOO5UxUQ+SerEAw==",
                        Adres = "Doornveld 130",
                        Postcode = "1731",
                        Gemeente = "Asse",
                        AantalMedewerkers = 1100,
                        AantalITMedewerkers = 1000,
                        AantalBegeleidendeMedewerkers = 100,
                        ContactpersoonId = 4,
                        BedrijfspromotorId = 4,
                        IsBedrijf = true,
                        EmailConfirmed = true,
                        LockoutEnabled = true,
                        SecurityStamp = Guid.NewGuid().ToString(),
                        ConcurrencyStamp = Guid.NewGuid().ToString(),
                    }
            );
            builder.Entity<Contactpersoon>().HasData(
                new Contactpersoon
                {
                    Id = 1,
                    Titel = "Dhr",
                    Voornaam = "Jan",
                    Naam = "Koekenpan",
                    Telefoonnummer = "0486321465",
                    Email = "contact@cegeka.be"
                },
                new Contactpersoon
                {
                    Id = 2,
                    Titel = "Dhr",
                    Voornaam = "Elon",
                    Naam = "Musk",
                    Telefoonnummer = "0476541321",
                    Email = "contact@tesla.be"

                },
                new Contactpersoon
                {
                    Id = 3,
                    Titel = "Mevrouw",
                    Voornaam = "Carmen",
                    Naam = "Waterslaeghers",
                    Telefoonnummer = "0478514654",
                    Email = "contact@microsoft.nl"
                },
                new Contactpersoon
                {
                    Id = 4,
                    Titel = "Dhr",
                    Voornaam = "Mark",
                    Naam = "Vertongen",
                    Telefoonnummer = "0498765432",
                    Email = "contact@dell.be"
                }
            );

            builder.Entity<Bedrijfspromotor>().HasData(
                new Bedrijfspromotor
                {
                    Id = 1,
                    Titel = "Dhr",
                    Voornaam = "Laurens",
                    Naam = "van Brussel",
                    Telefoonnummer = "016325485",
                    Email = "promotie@cegeka.be"
                },
                new Bedrijfspromotor
                {
                    Id = 2,
                    Titel = "Mevrouw",
                    Naam = "Grimes",
                    Voornaam = "Claire",
                    Telefoonnummer = "016325485",
                    Email = "promotie@tesla.be"
                },
                new Bedrijfspromotor
                {
                    Id = 3,
                    Titel = "Dhr",
                    Voornaam = "Xavier",
                    Naam = "Waterslaeghers",
                    Telefoonnummer = "0479654321",
                    Email = "promotie@microsoft.nl"
                },
                new Bedrijfspromotor
                {
                    Id = 4,
                    Titel = "Dhr",
                    Voornaam = "Balthazar",
                    Naam = "Boma",
                    Telefoonnummer = "0469696969",
                    Email = "promotie@dell.be"
                }
            );

            #endregion

            #region Stagevoorstellen

            builder.Entity<Stagevoorstel>().HasData(
                new Stagevoorstel
                {
                    Id = 1,
                    Date = DateTime.Now,
                    BedrijfId = 11,
                    Titel = ".Net Developer",
                    Adres = "Kempische Steenweg 307",
                    StagePostcode = "3500",
                    Gemeente = "Hasselt",
                    StageITMedewerkers = 200,
                    AfstudeerrichtingVoorkeur = new[] { "applicatieOntwikkeling" },
                    OpdrachtOmschrijving = "De stagiair zal werken aan interne software toepassingen.",
                    Omgeving = new[] { "dotNet", "web", "mobile" },
                    OmgevingOmschrijving = "/",
                    Randvoorwaarden = "Rijbewijs",
                    Onderzoeksthema = "/",
                    Verwachtingen = new[] { "CV" },
                    AantalGewensteStagiairs = 2,
                    GereserveerdeStudenten = "/",
                    Bemerkingen = "/",
                    Periode = 2,
                    Status = BeoordelingStatus.Goedgekeurd,
                },
                new Stagevoorstel
                {
                    Id = 2,
                    Date = DateTime.Now,
                    BedrijfId = 12,
                    Titel = "Develop the future! Advance Tesla's autopilot technology!",
                    Adres = "Boomse Steenweg 8",
                    StagePostcode = "2630",
                    Gemeente = "Aartselaar",
                    StageITMedewerkers = 200,
                    AfstudeerrichtingVoorkeur = new[] { "applicatieOntwikkeling", "AI" },
                    OpdrachtOmschrijving =
                        "De stagiairs zullen leren werken met AI om autopiloot van Tesla te verbeteren. Voor vragen kunnen ze altijd terecht bij onze professionals.",
                    Omgeving = new[] { "anders" },
                    OmgevingOmschrijving = "De stagiairs zullen hier voornamelijk werken met Python.",
                    Randvoorwaarden = "Rijbewijs",
                    Onderzoeksthema = "Tesla autopiloot",
                    Verwachtingen = new[] { "vergoeding, sollicitatiegesprek" },
                    AantalGewensteStagiairs = 2,
                    GereserveerdeStudenten = "/",
                    Bemerkingen = "/",
                    Periode = 1,
                    Status = BeoordelingStatus.Goedgekeurd,
                },
                new Stagevoorstel
                {
                    Id = 3,
                    Date = DateTime.Now,
                    BedrijfId = 12,
                    Titel = "Enterprise Storage Engineer",
                    Adres = "Boomse Steenweg 8",
                    StagePostcode = "2630",
                    Gemeente = "Aartselaar",
                    StageITMedewerkers = 102,
                    AfstudeerrichtingVoorkeur = new[] { "applicatieOntwikkeling", "netwerkBeheer" },
                    OpdrachtOmschrijving =
                        @"Als Enterprise Storage Engineer installeer en configureer je alle storagecomponenten van Cegeka of de klanten van Cegeka (Switches, SAN, NAS, …).
                        Je werkt mee aan de verdere uitbouw van het storage platform en de dienstverlenging.
                        Je zorgt ervoor dat de documentatie, instructies, procedures en kennis up - to - date worden gehouden.
                        Je bepaalt mee de standaarden en best practices voor de infrastructuur / applicaties teneinde een duurzame oplossing te kunnen bieden in lijn met de verwachtingen van de klant.
                        Samen met je collega’s sta je stand - by om 24 uur service te kunnen leveren.
                        Je werkt mee aan het automatiseren van taken die momenteel manueel gebeuren.
                        Je neemt op regelmatige tijdstippen zelfstandig een project op.",
                    Omgeving = new[]
                        {"java", "dotNet", "web", "mobile", "systemenNetwerken", "softwareTesting", "anders"},
                    OmgevingOmschrijving =
                        @"Teamwerk in een open en dynamische sfeer.
                        Begeleiding op maat! Jij geeft aan waar je interesses liggen. We bieden verschillende carrièrepaden aan, en kijken samen hoe we je verder kunnen helpen ontplooien.",
                    Randvoorwaarden = "Beschikt over een rijbewijs.",
                    Onderzoeksthema = "Onderhoud van alle storagecomponenten.",
                    Verwachtingen = new[] { "vergoeding" },
                    AantalGewensteStagiairs = 2,
                    GereserveerdeStudenten = "/",
                    Bemerkingen = "/",
                    Periode = 1,
                    Status = BeoordelingStatus.Goedgekeurd,
                },
                new Stagevoorstel
                {
                    Id = 4,
                    Date = DateTime.Now,
                    BedrijfId = 14,
                    Titel = "Software Architect",
                    Adres = "Doornveld 130",
                    StagePostcode = "1731",
                    Gemeente = "Asse",
                    StageITMedewerkers = 186,
                    AfstudeerrichtingVoorkeur = new[] { "applicatieOntwikkeling" },
                    OpdrachtOmschrijving =
                        @"Het sturen van de architectuur, het design en development van innovatieve ontwikkelingsprojecten voor klanten. Zowel voor mid-scale als large-scale applicaties.
                    Uitwerken van complexe bedrijfstoepassingen. Je doet dit samen met een team van gedreven professionals. Jij draagt de technische verantwoordelijkheid.
                    Hands-on development en coaching van developers o.a. via pair programming.
                    Communiceren met klanten en meewerken aan offertes.
                    Het opstarten van innovation centers rond de laatste trends binnen je vakdomein.",
                    Omgeving = new[] { "java", "dotNet" },
                    OmgevingOmschrijving = @"Ruime ervaring met professionele enterprise Java of .NET is een must.
                    Ook beschik je over ervaring met analyse, design, implementatie en oplevering van applicaties.
                    Jij kan de architectuur van complexe applicaties uittekenen, rekening houdend met alle niet-functionele vereisten zoals security, performantie, scalability, ... en je kan deze ook uitdragen naar het team.
                    Met jouw expertise heb jij een duidelijk zicht op de (technische) risico’s wat betreft impact en probabiliteit en voorziet strategieën om deze te mitigeren.
                    Je hebt een brede kennis van én ervaring met Web en Databasetechnologieën, communicatieprotocolen, integratietechnieken, gedistribueerde systemen, Devops, security, ….
                    O.O., design patterns, domain-driven design, refactoring en unit testing zijn zeer vertrouwde begrippen voor jou.
                    Scrum en eXtreme Programming zijn twee agile ontwikkelingsmethodologieën die je met plezier en kennis van zaken toepast.
                    Binnen de resultaatsgerichte, no-nonsens aanpak van een groeiend bedrijf als Cegeka, voel jij je onmiddellijk thuis.
                    Een sterke communicator, zowel binnen je team als naar de klant toe.
                    Je bent leergierig, stressbestendig en pragmatisch.",
                    Randvoorwaarden = "Uitgebreide kennis over Java en C#.",
                    Onderzoeksthema = "analyse, design, implementatie en oplevering van applicaties",
                    Verwachtingen = new[] { "geen" },
                    AantalGewensteStagiairs = 1,
                    GereserveerdeStudenten = "/",
                    Bemerkingen = "/",
                    Periode = 3,
                    Status = BeoordelingStatus.Goedgekeurd,
                },
                new Stagevoorstel
                {
                    Id = 5,
                    Date = DateTime.Now,
                    BedrijfId = 13,
                    Titel = "Cloud Native Engineer",
                    Adres = "Evert van de Beekstraat 354",
                    StagePostcode = "1118CZ",
                    Gemeente = "Schiphol",
                    StageITMedewerkers = 3612,
                    AfstudeerrichtingVoorkeur = new[] { "netwerkBeheer" },
                    OpdrachtOmschrijving = @"Je bent het lokaal aanspreekpunt voor nieuwe ICT infrastructuur aanvragen. Deze worden ter bespreking voorgelegd om tot een gepaste oplossing te komen.
                    Je coördineert de uitvoering van lokale ICT infrastructuurprojecten.
                    Je staat in voor de specifieke ondersteuning van VITO medewerkers die werkzaam zijn binnen Energyville:
                    Lokale logistieke en ICT technische ondersteuning met betrekking tot werkstations en mobiele toestellen in samenspraak met de servicedesk van VITO.
                    Geven van ICT intro sessies in Genk
                    Je bent lid van het ICT systeem- & netwerkteam van VITO en neemt deel aan de activiteiten en projecten binnen de scope van dit team.",
                    Omgeving = new[] { "systemenNetwerken", "softwareTesting" },
                    OmgevingOmschrijving = @"Je staat in voor het operationeel beheer van het shared ICT infrastructuur platform dat opgezet is binnen de campus van Energyville, inclusief netwerk en security. Dit houdt onder meer in:" +
                    "Lokale aanwezigheid en assistentie bij het oplossen van incidenten met betrekking tot het shared platform;" +
                    "Assistentie bij oplossen van verzoeken met betrekking tot het shared platform(o.a.patching);" +
                    "Samenwerken met de EnergyVille leveranciers zoals SecureLink, Infrax, Konica, …" +
                    "Je staat in voor gebruikersadministratie: beheer van distributielijsten, security groups, …" +
                    "Je zorgt voor de goede werking van de technische componenten in de vergaderzalen(schermen, clickshares, telefoons)." +
                    "Je staat in voor het lokaal servermanagement: beheer van de verschillende hardware toestellen in de serverroom, opvolging hardware meldingen lokale servers, …",
                    Randvoorwaarden = "/",
                    Onderzoeksthema = "Docker",
                    Verwachtingen = new[] { "sollicitatiegesprek", "CV" },
                    AantalGewensteStagiairs = 1,
                    GereserveerdeStudenten = "/",
                    Bemerkingen = "/",
                    Periode = 1,
                    Status = BeoordelingStatus.NietBeoordeeld,
                },
                new Stagevoorstel
                {
                    Id = 6,
                    Date = DateTime.Now,
                    BedrijfId = 11,
                    Titel = "PHP Developer",
                    Adres = "Kempische Steenweg 307",
                    StagePostcode = "3500",
                    Gemeente = "Hasselt",
                    StageITMedewerkers = 200,
                    AfstudeerrichtingVoorkeur = new[] { "applicatieOntwikkeling" },
                    OpdrachtOmschrijving = "Instaan voor het onderhoud van het huidige platform én meewerken aan het uitbouwen en ontwikkelingen van nieuwe platformen en tools.",
                    Omgeving = new[] { "web", "anders" },
                    OmgevingOmschrijving = "/",
                    Randvoorwaarden = "/",
                    Onderzoeksthema = "/",
                    Verwachtingen = new[] { "vergoeding" },
                    AantalGewensteStagiairs = 1,
                    GereserveerdeStudenten = "/",
                    Bemerkingen = "/",
                    Periode = 1,
                    Status = BeoordelingStatus.NietBeoordeeld,
                },
                new Stagevoorstel
                {
                    Id = 7,
                    Date = DateTime.Now,
                    BedrijfId = 12,
                    Titel = "Helpdeskmedewerker",
                    Adres = "Boomse Steenweg 8",
                    StagePostcode = "2630",
                    Gemeente = "Aartselaar",
                    StageITMedewerkers = 200,
                    AfstudeerrichtingVoorkeur = new[] { "applicatieOntwikkeling", "AI" },
                    OpdrachtOmschrijving = @"Je werkt binnen een team op de ICT servicedesk aan het operationeel gedeelte van de ICT omgeving (voornamelijk Windows).
                    Je behandelt ICT serviceaanvragen van gebruikers, lost storingen op en werkt zelfstandig of in team aan change requests binnen het domein van de computer- en netwerkinfrastructuur.
                    Je rapporteert herhalende storingen en risico’s en denkt mee met jouw collega’s hoe die op te lossen en te vermijden.
                    Je monitort de live systemen en grijpt in waar systeemstoringen zich voordoen of dreigen voor te doen.
                    Je leert de job kennen door interne opleidingen, al doende en eventueel via externe opleidingen. Je kan uiteraard rekenen op de steun en uitleg van jouw collega’s.
                    Je geeft sporadisch trainingen over nieuwe en bestaande softwarepakketten aan collega’s of klanten.",
                    Omgeving = new[] { "anders" },
                    OmgevingOmschrijving = @"Windows Client (7, 8,10) Windows Server systemen – 
                    begrip functionaliteiten AD, DNS, DHCP,…
                    Goed inzicht in de basis netwerkconfiguratie
                    Vlot Nederlands en Engels spreken, Frans is een plus..",
                    Randvoorwaarden = "Social skills",
                    Onderzoeksthema = "MS Office",
                    Verwachtingen = new[] { "vergoeding, sollicitatiegesprek" },
                    AantalGewensteStagiairs = 1,
                    GereserveerdeStudenten = "/",
                    Bemerkingen = "/",
                    Periode = 1,
                    Status = BeoordelingStatus.NietBeoordeeld,
                },
                new Stagevoorstel
                {
                    Id = 8,
                    Date = DateTime.Now,
                    BedrijfId = 14,
                    Titel = "Business Process Analyst ",
                    Adres = "Doornveld 130",
                    StagePostcode = "1731",
                    Gemeente = "Asse",
                    StageITMedewerkers = 186,
                    AfstudeerrichtingVoorkeur = new[] { "softwareManagement" },
                    OpdrachtOmschrijving = @"Je bent verantwoordelijk voor het in kaart brengen van bedrijfsprocessen
                    Je werkt de processen verder uit en implementeert deze in een Business Process Management softwarepakket (K2).
                    Je beheert bestaande en nieuwe datasources en voegt deze samen tot een globale data omgeving
                    Je voert functionele analyses uit en denkt mee met de organisatie en eindgebruiker
                    Je documenteert en rapporteert rond projecten met alle betrokken partijen
                    Je zoekt continu mee naar procesverbeteringen
                    Je rapporteert rechtsreeks aan de IT Director
                    Je blijft op de hoogte van de nieuwste ontwikkelingen en trends op technologisch vlak",
                    Omgeving = new[] { "softwareTesting" },
                    OmgevingOmschrijving = @"Een afwisselende jobinhoud bij één van de belangrijkste onafhankelijke spelers in het domein van adviesverlening
                    De nodige kansen om je persoonlijk te ontplooien door een uniek en uitgebreid opleidingsaanbod
                    Je werkt in een familiale, multidisciplinaire organisatie gelegen in een aantrekkelijke filevrije omgeving
                    Leer je nieuwe collega’s alvast kennen (https://youtu.be/CVXH8OFfF1w)
                    Kortom, wij bieden “maetwerk”",
                    Randvoorwaarden = "Beheersing van een Business Process Management softwarepakket",
                    Onderzoeksthema = "Procesverbeteringen",
                    Verwachtingen = new[] { "CV" },
                    AantalGewensteStagiairs = 2,
                    GereserveerdeStudenten = "/",
                    Bemerkingen = "/",
                    Periode = 2,
                    Status = BeoordelingStatus.Afgekeurd,
                }
            );
            #endregion

            #region Reviews
            builder.Entity<Review>().HasData(
                new Review
                {
                    Id = 1,
                    Date = DateTime.Now,
                    Status = BeoordelingStatus.Goedgekeurd,
                    StatusVoorstel = BeoordelingStatus.Goedgekeurd,
                    ReviewerId = 2,
                    StagevoorstelId = 1
                },
                new Review
                {
                    Id = 2,
                    Date = DateTime.Now,
                    Status = BeoordelingStatus.Goedgekeurd,
                    StatusVoorstel = BeoordelingStatus.Goedgekeurd,
                    ReviewerId = 2,
                    StagevoorstelId = 2
                },
                new Review
                {
                    Id = 3,
                    Date = DateTime.Now,
                    Status = BeoordelingStatus.Goedgekeurd,
                    StatusVoorstel = BeoordelingStatus.Goedgekeurd,
                    ReviewerId = 3,
                    StagevoorstelId = 3
                },
                new Review
                {
                    Id = 4,
                    Date = DateTime.Now,
                    Status = BeoordelingStatus.NietBeoordeeld,
                    StatusVoorstel = BeoordelingStatus.Goedgekeurd,
                    ReviewerId = 3,
                    StagevoorstelId = 4
                },
                new Review
                {
                    Id = 5,
                    Date = DateTime.Now,
                    Text = "Te grote verantwoordelijkheid..",
                    Status = BeoordelingStatus.Goedgekeurd,
                    StatusVoorstel = BeoordelingStatus.Afgekeurd,
                    ReviewerId = 3,
                    StagevoorstelId = 5
                },
                new Review
                {
                    Id = 6,
                    Date = DateTime.Now,
                    Text = "Over welke tools gaat het hier precies? We hadden graag wat meer informatie zodat de studenten ook weten waar ze aan toe zijn.",
                    Status = BeoordelingStatus.Goedgekeurd,
                    StatusVoorstel = BeoordelingStatus.NietBeoordeeld,
                    ReviewerId = 4,
                    StagevoorstelId = 6
                },
                new Review
                {
                    Id = 7,
                    Date = DateTime.Now,
                    Text = "Storingen van welke aard? Moeilijkheidsgraad?",
                    Status = BeoordelingStatus.Goedgekeurd,
                    StatusVoorstel = BeoordelingStatus.NietBeoordeeld,
                    ReviewerId = 4,
                    StagevoorstelId = 7
                },
                new Review
                {
                    Id = 8,
                    Date = DateTime.Now,
                    Text = "Slaat helemaal nergens op!",
                    Status = BeoordelingStatus.Afgekeurd,
                    StatusVoorstel = BeoordelingStatus.Afgekeurd,
                    ReviewerId = 4,
                    StagevoorstelId = 8
                }
            );

            //Assign roles:
            builder.Entity<Role>().HasData(
                new Role
                {
                    Id = 1,
                    Name = "reviewer",
                    NormalizedName = "REVIEWER",
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                },
                new Role
                {
                    Id = 2,
                    Name = "student",
                    NormalizedName = "STUDENT",
                    ConcurrencyStamp = Guid.NewGuid().ToString()

                },
                new Role
                {
                    Id = 3,
                    Name = "bedrijf",
                    NormalizedName = "BEDRIJF",
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                }
            );

            var identityUserRoles = new IdentityUserRole<int>[12];
            for (var i = 1; i < identityUserRoles.Length + 1; i++)
            {
                identityUserRoles[i - 1] = new IdentityUserRole<int> { UserId = i };
                if (i <= 4)
                {
                    identityUserRoles[i - 1].RoleId = 1;
                }
                else if (i <= 10)
                {
                    identityUserRoles[i - 1].RoleId = 2;
                }
                else
                {
                    identityUserRoles[i - 1].RoleId = 3;
                }
            }

            builder.Entity<IdentityUserRole<int>>().HasData(identityUserRoles);

            #endregion

            #region relations

            //Fix relations between reviewer/voorstellen and studenten/voorstellen.
            builder.Entity<ReviewerStagevoorstelToegewezen>().HasData(
                new ReviewerStagevoorstelToegewezen
                {
                    ReviewerId = 2,
                    StagevoorstelId = 1
                },
                new ReviewerStagevoorstelToegewezen
                {
                    ReviewerId = 2,
                    StagevoorstelId = 2
                },
                new ReviewerStagevoorstelToegewezen
                {
                    ReviewerId = 3,
                    StagevoorstelId = 2
                },
                new ReviewerStagevoorstelToegewezen
                {
                    ReviewerId = 3,
                    StagevoorstelId = 3
                },
                new ReviewerStagevoorstelToegewezen
                {
                    ReviewerId = 3,
                    StagevoorstelId = 4
                },
                new ReviewerStagevoorstelToegewezen
                {
                    ReviewerId = 3,
                    StagevoorstelId = 5
                },
                new ReviewerStagevoorstelToegewezen
                {
                    ReviewerId = 4,
                    StagevoorstelId = 3
                },
                new ReviewerStagevoorstelToegewezen
                {
                    ReviewerId = 4,
                    StagevoorstelId = 6
                },
                new ReviewerStagevoorstelToegewezen
                {
                    ReviewerId = 4,
                    StagevoorstelId = 7
                },
                new ReviewerStagevoorstelToegewezen
                {
                    ReviewerId = 4,
                    StagevoorstelId = 8
                }
            );
            builder.Entity<ReviewerStagevoorstelFavoriet>().HasData(
                new ReviewerStagevoorstelFavoriet
                {
                    ReviewerId = 2,
                    StagevoorstelId = 1
                },
                new ReviewerStagevoorstelFavoriet
                {
                    ReviewerId = 3,
                    StagevoorstelId = 4
                },
                new ReviewerStagevoorstelFavoriet
                {
                    ReviewerId = 4,
                    StagevoorstelId = 7
                }
            );

            builder.Entity<StudentStagevoorstelFavoriet>().HasData(
                new StudentStagevoorstelFavoriet
                {
                    StudentId = 5,
                    StagevoorstelId = 3
                },
                new StudentStagevoorstelFavoriet
                {
                    StudentId = 6,
                    StagevoorstelId = 4
                },
                new StudentStagevoorstelFavoriet
                {
                    StudentId = 7,
                    StagevoorstelId = 1
                },
                new StudentStagevoorstelFavoriet
                {
                    StudentId = 8,
                    StagevoorstelId = 2
                },
                new StudentStagevoorstelFavoriet
                {
                    StudentId = 9,
                    StagevoorstelId = 2
                },
                new StudentStagevoorstelFavoriet
                {
                    StudentId = 10,
                    StagevoorstelId = 4
                },
                new StudentStagevoorstelFavoriet
                {
                    StudentId = 10,
                    StagevoorstelId = 2
                }
            );

            #endregion

            #region Comments
            builder.Entity<Comment>().HasData(
                new Comment
                {
                    Id = 1,
                    StagevoorstelId = 1,
                    UserId = 1,
                    Text = "Het onderzoeksthema is niet ingevuld. Kan dit zo spoedig mogelijk ingevuld worden?",
                    Date = DateTime.Today.AddHours(-8).AddMinutes(-12)
                },
                new Comment
                {
                    Id = 2,
                    StagevoorstelId = 1,
                    UserId = 11,
                    Text = "We zullen er eens over nadenken en dit zo spoedig mogelijk in gaan vullen.",
                    Date = DateTime.Today.AddHours(8).AddMinutes(40)
                },
                new Comment
                {
                    Id = 3,
                    StagevoorstelId = 2,
                    UserId = 1,
                    Text = "Wat wordt er precies bedoeld met 'De stagiairs zullen leren werken met AI om autopiloot van Tesla te verbeteren'? Dit is niet voldoende, gelieve dit te verduidelijken.",
                    Date = DateTime.Today.AddHours(9).AddMinutes(24)
                }
            );

            #endregion
        }

        public void DetachEntries()
        {
            foreach (var entityEntry in ChangeTracker.Entries())
            {
                Entry(entityEntry.Entity).State = EntityState.Detached;
            }
        }
    }
}