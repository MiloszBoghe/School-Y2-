using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Stage_API.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bedrijfspromotors",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titel = table.Column<string>(nullable: false),
                    Naam = table.Column<string>(nullable: false),
                    Voornaam = table.Column<string>(nullable: false),
                    Telefoonnummer = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bedrijfspromotors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Contactpersonen",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titel = table.Column<string>(nullable: false),
                    Naam = table.Column<string>(nullable: false),
                    Voornaam = table.Column<string>(nullable: false),
                    Telefoonnummer = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contactpersonen", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ResetPasswordRequests",
                columns: table => new
                {
                    PasswordResetToken = table.Column<Guid>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    ResetRequestDateTime = table.Column<DateTime>(nullable: false),
                    IsConsumed = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResetPasswordRequests", x => x.PasswordResetToken);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    RoleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(nullable: false),
                    StagevoorstelId = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Text = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FavorietenStagevoorstellenReviewer",
                columns: table => new
                {
                    ReviewerId = table.Column<int>(nullable: false),
                    StagevoorstelId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavorietenStagevoorstellenReviewer", x => new { x.ReviewerId, x.StagevoorstelId });
                });

            migrationBuilder.CreateTable(
                name: "FavorietenStagevoorstellenStudent",
                columns: table => new
                {
                    StudentId = table.Column<int>(nullable: false),
                    StagevoorstelId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavorietenStagevoorstellenStudent", x => new { x.StudentId, x.StagevoorstelId });
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(nullable: false),
                    DateTime = table.Column<DateTime>(nullable: false),
                    ReceiverId = table.Column<int>(nullable: false),
                    SenderId = table.Column<int>(nullable: false),
                    ReceiverFirstName = table.Column<string>(nullable: true),
                    ReceiverLastName = table.Column<string>(nullable: true),
                    SenderFirstName = table.Column<string>(nullable: true),
                    SenderLastName = table.Column<string>(nullable: true),
                    EmailReceiver = table.Column<string>(nullable: false),
                    EmailSender = table.Column<string>(nullable: false),
                    UserId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(nullable: false),
                    Text = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    StatusVoorstel = table.Column<int>(nullable: false),
                    ReviewerId = table.Column<int>(nullable: false),
                    StagevoorstelId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Stagevoorstellen",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(nullable: false),
                    BedrijfId = table.Column<int>(nullable: true),
                    Titel = table.Column<string>(nullable: true),
                    Adres = table.Column<string>(nullable: true),
                    StagePostcode = table.Column<string>(nullable: true),
                    Gemeente = table.Column<string>(nullable: true),
                    StageITMedewerkers = table.Column<int>(nullable: false),
                    AfstudeerrichtingVoorkeur = table.Column<string>(nullable: true),
                    OpdrachtOmschrijving = table.Column<string>(nullable: true),
                    Omgeving = table.Column<string>(nullable: true),
                    OmgevingOmschrijving = table.Column<string>(nullable: true),
                    Randvoorwaarden = table.Column<string>(nullable: true),
                    Onderzoeksthema = table.Column<string>(nullable: true),
                    Verwachtingen = table.Column<string>(nullable: true),
                    AantalGewensteStagiairs = table.Column<int>(nullable: false),
                    GereserveerdeStudenten = table.Column<string>(nullable: true),
                    Bemerkingen = table.Column<string>(nullable: true),
                    Periode = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stagevoorstellen", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    Voornaam = table.Column<string>(nullable: true),
                    Naam = table.Column<string>(nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    IsBedrijf = table.Column<bool>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    Adres = table.Column<string>(nullable: true),
                    Postcode = table.Column<string>(nullable: true),
                    Gemeente = table.Column<string>(nullable: true),
                    AantalMedewerkers = table.Column<int>(nullable: true),
                    AantalITMedewerkers = table.Column<int>(nullable: true),
                    AantalBegeleidendeMedewerkers = table.Column<int>(nullable: true),
                    ContactpersoonId = table.Column<int>(nullable: true),
                    BedrijfspromotorId = table.Column<int>(nullable: true),
                    IsCoordinator = table.Column<bool>(nullable: true),
                    StagevoorstelId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Bedrijfspromotors_BedrijfspromotorId",
                        column: x => x.BedrijfspromotorId,
                        principalTable: "Bedrijfspromotors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Contactpersonen_ContactpersoonId",
                        column: x => x.ContactpersoonId,
                        principalTable: "Contactpersonen",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Stagevoorstellen_StagevoorstelId",
                        column: x => x.StagevoorstelId,
                        principalTable: "Stagevoorstellen",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "ToegewezenStagevoorstellenReviewer",
                columns: table => new
                {
                    ReviewerId = table.Column<int>(nullable: false),
                    StagevoorstelId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ToegewezenStagevoorstellenReviewer", x => new { x.ReviewerId, x.StagevoorstelId });
                    table.ForeignKey(
                        name: "FK_ToegewezenStagevoorstellenReviewer_AspNetUsers_ReviewerId",
                        column: x => x.ReviewerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ToegewezenStagevoorstellenReviewer_Stagevoorstellen_StagevoorstelId",
                        column: x => x.StagevoorstelId,
                        principalTable: "Stagevoorstellen",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { 3, "b3edda42-525e-48ca-a8cf-e6bc0c3ebb50", "bedrijf", "BEDRIJF" },
                    { 2, "07f55e06-0fbd-493a-8c86-aba43389e594", "student", "STUDENT" },
                    { 1, "be6b7ddb-14f5-45d6-8bd9-b674eb58919e", "reviewer", "REVIEWER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "IsBedrijf", "LockoutEnabled", "LockoutEnd", "Naam", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "Voornaam", "IsCoordinator" },
                values: new object[,]
                {
                    { 1, 0, "575a74b2-de85-42dc-8768-797d30f62b9f", "Reviewer", "Marijke.Willems@pxl.be", true, false, true, null, "Willems", "MARIJKE.WILLEMS@PXL.BE", "MARIJKE.WILLEMS@PXL.BE", "AQAAAAEAACcQAAAAEE3MADTcj5R9NBdPZGuSrqvcfisB2jZdZoPa3+AbxcGsaSOQAO6JOO5UxUQ+SerEAw==", null, false, "9e319446-0038-4677-8e09-f7d1087df9e9", false, "marijke.willems@pxl.be", "Marijke", true },
                    { 4, 0, "f0ae34f0-7c86-4969-9184-61345e73c929", "Reviewer", "arno.barzan@pxl.be", true, false, true, null, "Barzan", "ARNO.BARZAN@PXL.BE", "ARNO.BARZAN@PXL.BE", "AQAAAAEAACcQAAAAEE3MADTcj5R9NBdPZGuSrqvcfisB2jZdZoPa3+AbxcGsaSOQAO6JOO5UxUQ+SerEAw==", null, false, "a17f8e77-32a0-4f12-85d0-b71a95888d3f", false, "arno.barzan@pxl.be", "Arno", false },
                    { 3, 0, "b95ce85e-c6cd-4e80-ac9b-ae6d3815c856", "Reviewer", "Lowie.Vangaal@pxl.be", true, false, true, null, "Vangaal", "LOWIE.VANGAAL@PXL.BE", "LOWIE.VANGAAL@PXL.BE", "AQAAAAEAACcQAAAAEE3MADTcj5R9NBdPZGuSrqvcfisB2jZdZoPa3+AbxcGsaSOQAO6JOO5UxUQ+SerEAw==", null, false, "5463c628-f387-4c01-9150-7e436300b83b", false, "lowie.vangaal@pxl.be", "Lowie", false },
                    { 2, 0, "2b60579a-e7c1-4560-98da-c29157710dc1", "Reviewer", "Kris.Hermans@pxl.be", true, false, true, null, "Hermans", "KRIS.HERMANS@PXL.BE", "KRIS.HERMANS@PXL.BE", "AQAAAAEAACcQAAAAEE3MADTcj5R9NBdPZGuSrqvcfisB2jZdZoPa3+AbxcGsaSOQAO6JOO5UxUQ+SerEAw==", null, false, "cbf1aa26-9706-4c51-a3c0-9900d4ce1557", false, "kris.hermans@pxl.be", "Kris", false }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "IsBedrijf", "LockoutEnabled", "LockoutEnd", "Naam", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "Voornaam", "StagevoorstelId" },
                values: new object[,]
                {
                    { 5, 0, "b4e55d03-05a6-4144-9975-35a3be58d863", "Student", "pieter.janssen@student.pxl.be", true, false, true, null, "Janssen", "PIETER.JANSSEN@STUDENT.PXL.BE", "PIETER.JANSSEN@STUDENT.PXL.BE", "AQAAAAEAACcQAAAAEE3MADTcj5R9NBdPZGuSrqvcfisB2jZdZoPa3+AbxcGsaSOQAO6JOO5UxUQ+SerEAw==", null, false, "b41d570a-d5a6-4e79-8134-07dd3f1a90ec", false, "pieter.janssen@student.pxl.be", "Pieter", null },
                    { 10, 0, "7784c73b-6222-499c-931f-35c1fc92171c", "Student", "lennert.schoenmakers@student.pxl.be", true, false, true, null, "Schoenmakers", "LENNERT.SCHOENMAKERS@STUDENT.PXL.BE", "LENNERT.SCHOENMAKERS@STUDENT.PXL.BE", "AQAAAAEAACcQAAAAEE3MADTcj5R9NBdPZGuSrqvcfisB2jZdZoPa3+AbxcGsaSOQAO6JOO5UxUQ+SerEAw==", null, false, "8852c7e2-b971-4426-9ae1-8c9b5584a0a9", false, "lennert.schoenmakers@student.pxl.be", "Lennert", null }
                });

            migrationBuilder.InsertData(
                table: "Bedrijfspromotors",
                columns: new[] { "Id", "Email", "Naam", "Telefoonnummer", "Titel", "Voornaam" },
                values: new object[,]
                {
                    { 4, "promotie@dell.be", "Boma", "0469696969", "Dhr", "Balthazar" },
                    { 3, "promotie@microsoft.nl", "Waterslaeghers", "0479654321", "Dhr", "Xavier" },
                    { 2, "promotie@tesla.be", "Grimes", "016325485", "Mevrouw", "Claire" },
                    { 1, "promotie@cegeka.be", "van Brussel", "016325485", "Dhr", "Laurens" }
                });

            migrationBuilder.InsertData(
                table: "Contactpersonen",
                columns: new[] { "Id", "Email", "Naam", "Telefoonnummer", "Titel", "Voornaam" },
                values: new object[,]
                {
                    { 3, "contact@microsoft.nl", "Waterslaeghers", "0478514654", "Mevrouw", "Carmen" },
                    { 2, "contact@tesla.be", "Musk", "04765413212", "Dhr", "Elon" },
                    { 1, "contact@cegeka.be", "Koekenpan", "0486321465", "Dhr", "Jan" },
                    { 4, "contact@dell.be", "Vertongen", "0498765432", "Dhr", "Mark" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 1 },
                    { 3, 1 },
                    { 4, 1 },
                    { 5, 2 },
                    { 10, 2 }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "IsBedrijf", "LockoutEnabled", "LockoutEnd", "Naam", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "Voornaam", "AantalBegeleidendeMedewerkers", "AantalITMedewerkers", "AantalMedewerkers", "Adres", "BedrijfspromotorId", "ContactpersoonId", "Gemeente", "Postcode" },
                values: new object[,]
                {
                    { 11, 0, "45b9da70-51bc-4219-8411-bbb54091a2af", "Bedrijf", "contact@cegeka.be", true, true, true, null, "Cegeka", "CONTACT@CEGEKA.BE", "CONTACT@CEGEKA.BE", "AQAAAAEAACcQAAAAEE3MADTcj5R9NBdPZGuSrqvcfisB2jZdZoPa3+AbxcGsaSOQAO6JOO5UxUQ+SerEAw==", null, false, "0c759504-84d8-4772-918f-14688e2c1632", false, "contact@cegeka.be", null, 33, 200, 233, "Kempische Steenweg 307", 1, 1, "Hasselt", "3500" },
                    { 12, 0, "18ca2088-af4c-4d2c-bd33-d85bc8072c75", "Bedrijf", "contact@tesla.be", true, true, true, null, "Tesla", "CONTACT@TESLA.BE", "CONTACT@TESLA.BE", "AQAAAAEAACcQAAAAEE3MADTcj5R9NBdPZGuSrqvcfisB2jZdZoPa3+AbxcGsaSOQAO6JOO5UxUQ+SerEAw==", null, false, "77a836f4-31e6-478b-b052-7e5075c9edc6", false, "contact@tesla.be", null, 66, 600, 666, "Boomse Steenweg 8", 2, 2, "Aartselaar", "2630" },
                    { 13, 0, "aca9b812-a4f9-4ed1-8fb5-7dee4351f264", "Bedrijf", "contact@microsoft.nl", true, true, true, null, "Microsoft Nederland", "CONTACT@MICROSOFT.NL", "CONTACT@MICROSOFT.NL", "AQAAAAEAACcQAAAAEE3MADTcj5R9NBdPZGuSrqvcfisB2jZdZoPa3+AbxcGsaSOQAO6JOO5UxUQ+SerEAw==", null, false, "4089f88a-83a2-4e39-8c96-914fad6e82db", false, "contact@microsoft.nl", null, 365, 3648, 4013, "Evert van de Beekstraat 354", 3, 3, "Schiphol", "1118CZ" },
                    { 14, 0, "685b1b50-4d88-48de-9805-a49b0ffc2682", "Bedrijf", "contact@dell.be", true, true, true, null, "Dell", "CONTACT@DELL.BE", "CONTACT@DELL.BE", "AQAAAAEAACcQAAAAEE3MADTcj5R9NBdPZGuSrqvcfisB2jZdZoPa3+AbxcGsaSOQAO6JOO5UxUQ+SerEAw==", null, false, "b13501f8-4deb-4815-99f5-2362b0a9aeba", false, "contact@dell.be", null, 100, 1000, 1100, "Doornveld 130", 4, 4, "Asse", "1731" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[,]
                {
                    { 11, 3 },
                    { 12, 3 }
                });

            migrationBuilder.InsertData(
                table: "Stagevoorstellen",
                columns: new[] { "Id", "AantalGewensteStagiairs", "Adres", "AfstudeerrichtingVoorkeur", "BedrijfId", "Bemerkingen", "Date", "Gemeente", "GereserveerdeStudenten", "Omgeving", "OmgevingOmschrijving", "Onderzoeksthema", "OpdrachtOmschrijving", "Periode", "Randvoorwaarden", "StageITMedewerkers", "StagePostcode", "Status", "Titel", "Verwachtingen" },
                values: new object[,]
                {
                    { 1, 2, "Kempische Steenweg 307", "applicatieOntwikkeling", 11, "/", new DateTime(2020, 5, 21, 20, 16, 39, 526, DateTimeKind.Local).AddTicks(4942), "Hasselt", "/", "dotNet,web,mobile", "/", "/", "De stagiair zal werken aan interne software toepassingen.", 2, "Rijbewijs", 200, "3500", 2, ".Net Developer", "CV" },
                    { 6, 1, "Kempische Steenweg 307", "applicatieOntwikkeling", 11, "/", new DateTime(2020, 5, 21, 20, 16, 39, 529, DateTimeKind.Local).AddTicks(2380), "Hasselt", "/", "web,anders", "/", "/", "Instaan voor het onderhoud van het huidige platform én meewerken aan het uitbouwen en ontwikkelingen van nieuwe platformen en tools.", 1, "/", 200, "3500", 0, "PHP Developer", "vergoeding" },
                    { 2, 2, "Boomse Steenweg 8", "applicatieOntwikkeling,AI", 12, "/", new DateTime(2020, 5, 21, 20, 16, 39, 529, DateTimeKind.Local).AddTicks(2176), "Aartselaar", "/", "anders", "De stagiairs zullen hier voornamelijk werken met Python.", "Tesla autopiloot", "De stagiairs zullen leren werken met AI om autopiloot van Tesla te verbeteren. Voor vragen kunnen ze altijd terecht bij onze professionals.", 1, "Rijbewijs", 200, "2630", 2, "Develop the future! Advance Tesla's autopilot technology!", "vergoeding, sollicitatiegesprek" },
                    { 3, 2, "Boomse Steenweg 8", "applicatieOntwikkeling,netwerkBeheer", 12, "/", new DateTime(2020, 5, 21, 20, 16, 39, 529, DateTimeKind.Local).AddTicks(2358), "Aartselaar", "/", "java,dotNet,web,mobile,systemenNetwerken,softwareTesting,anders", @"Teamwerk in een open en dynamische sfeer.
                                        Begeleiding op maat! Jij geeft aan waar je interesses liggen. We bieden verschillende carrièrepaden aan, en kijken samen hoe we je verder kunnen helpen ontplooien.", "Onderhoud van alle storagecomponenten.", @"Als Enterprise Storage Engineer installeer en configureer je alle storagecomponenten van Cegeka of de klanten van Cegeka (Switches, SAN, NAS, …).
                                        Je werkt mee aan de verdere uitbouw van het storage platform en de dienstverlenging.
                                        Je zorgt ervoor dat de documentatie, instructies, procedures en kennis up - to - date worden gehouden.
                                        Je bepaalt mee de standaarden en best practices voor de infrastructuur / applicaties teneinde een duurzame oplossing te kunnen bieden in lijn met de verwachtingen van de klant.
                                        Samen met je collega’s sta je stand - by om 24 uur service te kunnen leveren.
                                        Je werkt mee aan het automatiseren van taken die momenteel manueel gebeuren.
                                        Je neemt op regelmatige tijdstippen zelfstandig een project op.", 1, "Beschikt over een rijbewijs.", 102, "2630", 2, "Enterprise Storage Engineer", "vergoeding" },
                    { 7, 1, "Boomse Steenweg 8", "applicatieOntwikkeling,AI", 12, "/", new DateTime(2020, 5, 21, 20, 16, 39, 529, DateTimeKind.Local).AddTicks(2385), "Aartselaar", "/", "anders", @"Windows Client (7, 8,10) Windows Server systemen – 
                                    begrip functionaliteiten AD, DNS, DHCP,…
                                    Goed inzicht in de basis netwerkconfiguratie
                                    Vlot Nederlands en Engels spreken, Frans is een plus..", "MS Office", @"Je werkt binnen een team op de ICT servicedesk aan het operationeel gedeelte van de ICT omgeving (voornamelijk Windows).
                                    Je behandelt ICT serviceaanvragen van gebruikers, lost storingen op en werkt zelfstandig of in team aan change requests binnen het domein van de computer- en netwerkinfrastructuur.
                                    Je rapporteert herhalende storingen en risico’s en denkt mee met jouw collega’s hoe die op te lossen en te vermijden.
                                    Je monitort de live systemen en grijpt in waar systeemstoringen zich voordoen of dreigen voor te doen.
                                    Je leert de job kennen door interne opleidingen, al doende en eventueel via externe opleidingen. Je kan uiteraard rekenen op de steun en uitleg van jouw collega’s.
                                    Je geeft sporadisch trainingen over nieuwe en bestaande softwarepakketten aan collega’s of klanten.", 1, "Social skills", 200, "2630", 0, "Helpdeskmedewerker", "vergoeding, sollicitatiegesprek" },
                    { 5, 1, "Evert van de Beekstraat 354", "netwerkBeheer", 13, "/", new DateTime(2020, 5, 21, 20, 16, 39, 529, DateTimeKind.Local).AddTicks(2375), "Schiphol", "/", "systemenNetwerken,softwareTesting", "Je staat in voor het operationeel beheer van het shared ICT infrastructuur platform dat opgezet is binnen de campus van Energyville, inclusief netwerk en security. Dit houdt onder meer in:Lokale aanwezigheid en assistentie bij het oplossen van incidenten met betrekking tot het shared platform;Assistentie bij oplossen van verzoeken met betrekking tot het shared platform(o.a.patching);Samenwerken met de EnergyVille leveranciers zoals SecureLink, Infrax, Konica, …Je staat in voor gebruikersadministratie: beheer van distributielijsten, security groups, …Je zorgt voor de goede werking van de technische componenten in de vergaderzalen(schermen, clickshares, telefoons).Je staat in voor het lokaal servermanagement: beheer van de verschillende hardware toestellen in de serverroom, opvolging hardware meldingen lokale servers, …", "Docker", @"Je bent het lokaal aanspreekpunt voor nieuwe ICT infrastructuur aanvragen. Deze worden ter bespreking voorgelegd om tot een gepaste oplossing te komen.
                                    Je coördineert de uitvoering van lokale ICT infrastructuurprojecten.
                                    Je staat in voor de specifieke ondersteuning van VITO medewerkers die werkzaam zijn binnen Energyville:
                                    Lokale logistieke en ICT technische ondersteuning met betrekking tot werkstations en mobiele toestellen in samenspraak met de servicedesk van VITO.
                                    Geven van ICT intro sessies in Genk
                                    Je bent lid van het ICT systeem- & netwerkteam van VITO en neemt deel aan de activiteiten en projecten binnen de scope van dit team.", 1, "/", 3612, "1118CZ", 0, "Cloud Native Engineer", "sollicitatiegesprek,CV" },
                    { 4, 1, "Doornveld 130", "applicatieOntwikkeling", 14, "/", new DateTime(2020, 5, 21, 20, 16, 39, 529, DateTimeKind.Local).AddTicks(2369), "Asse", "/", "java,dotNet", @"Ruime ervaring met professionele enterprise Java of .NET is een must.
                                    Ook beschik je over ervaring met analyse, design, implementatie en oplevering van applicaties.
                                    Jij kan de architectuur van complexe applicaties uittekenen, rekening houdend met alle niet-functionele vereisten zoals security, performantie, scalability, ... en je kan deze ook uitdragen naar het team.
                                    Met jouw expertise heb jij een duidelijk zicht op de (technische) risico’s wat betreft impact en probabiliteit en voorziet strategieën om deze te mitigeren.
                                    Je hebt een brede kennis van én ervaring met Web en Databasetechnologieën, communicatieprotocolen, integratietechnieken, gedistribueerde systemen, Devops, security, ….
                                    O.O., design patterns, domain-driven design, refactoring en unit testing zijn zeer vertrouwde begrippen voor jou.
                                    Scrum en eXtreme Programming zijn twee agile ontwikkelingsmethodologieën die je met plezier en kennis van zaken toepast.
                                    Binnen de resultaatsgerichte, no-nonsens aanpak van een groeiend bedrijf als Cegeka, voel jij je onmiddellijk thuis.
                                    Een sterke communicator, zowel binnen je team als naar de klant toe.
                                    Je bent leergierig, stressbestendig en pragmatisch.", "analyse, design, implementatie en oplevering van applicaties", @"Het sturen van de architectuur, het design en development van innovatieve ontwikkelingsprojecten voor klanten. Zowel voor mid-scale als large-scale applicaties.
                                    Uitwerken van complexe bedrijfstoepassingen. Je doet dit samen met een team van gedreven professionals. Jij draagt de technische verantwoordelijkheid.
                                    Hands-on development en coaching van developers o.a. via pair programming.
                                    Communiceren met klanten en meewerken aan offertes.
                                    Het opstarten van innovation centers rond de laatste trends binnen je vakdomein.", 3, "Uitgebreide kennis over Java en C#.", 186, "1731", 2, "Software Architect", "geen" },
                    { 8, 2, "Doornveld 130", "softwareManagement", 14, "/", new DateTime(2020, 5, 21, 20, 16, 39, 529, DateTimeKind.Local).AddTicks(2390), "Asse", "/", "softwareTesting", @"Een afwisselende jobinhoud bij één van de belangrijkste onafhankelijke spelers in het domein van adviesverlening
                                    De nodige kansen om je persoonlijk te ontplooien door een uniek en uitgebreid opleidingsaanbod
                                    Je werkt in een familiale, multidisciplinaire organisatie gelegen in een aantrekkelijke filevrije omgeving
                                    Leer je nieuwe collega’s alvast kennen (https://youtu.be/CVXH8OFfF1w)
                                    Kortom, wij bieden “maetwerk”", "Procesverbeteringen", @"Je bent verantwoordelijk voor het in kaart brengen van bedrijfsprocessen
                                    Je werkt de processen verder uit en implementeert deze in een Business Process Management softwarepakket (K2).
                                    Je beheert bestaande en nieuwe datasources en voegt deze samen tot een globale data omgeving
                                    Je voert functionele analyses uit en denkt mee met de organisatie en eindgebruiker
                                    Je documenteert en rapporteert rond projecten met alle betrokken partijen
                                    Je zoekt continu mee naar procesverbeteringen
                                    Je rapporteert rechtsreeks aan de IT Director
                                    Je blijft op de hoogte van de nieuwste ontwikkelingen en trends op technologisch vlak", 2, "Beheersing van een Business Process Management softwarepakket", 186, "1731", 1, "Business Process Analyst ", "CV" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "IsBedrijf", "LockoutEnabled", "LockoutEnd", "Naam", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "Voornaam", "StagevoorstelId" },
                values: new object[,]
                {
                    { 7, 0, "50b67514-4ee4-4b91-a070-2d753ee9d970", "Student", "brent.leemans@student.pxl.be", true, false, true, null, "Leemans", "BRENT.LEEMANS@STUDENT.PXL.BE", "BRENT.LEEMANS@STUDENT.PXL.BE", "AQAAAAEAACcQAAAAEE3MADTcj5R9NBdPZGuSrqvcfisB2jZdZoPa3+AbxcGsaSOQAO6JOO5UxUQ+SerEAw==", null, false, "6a222135-a6cd-4fca-b510-0caebc7e4677", false, "brent.leemans@student.pxl.be", "Brent", 1 },
                    { 9, 0, "fd691ebf-c93b-4be5-9b51-45c4df17077e", "Student", "kaan.ozdemi@student.pxl.be", true, false, true, null, "Ozdemir", "KAAN.OZDEMI@STUDENT.PXL.BE", "KAAN.OZDEMI@STUDENT.PXL.BE", "AQAAAAEAACcQAAAAEE3MADTcj5R9NBdPZGuSrqvcfisB2jZdZoPa3+AbxcGsaSOQAO6JOO5UxUQ+SerEAw==", null, false, "62e2343d-0fbb-4003-bda1-c475db1d2893", false, "kaan.ozdemi@student.pxl.be", "Kaan", 1 },
                    { 8, 0, "bf11f546-7d70-4fe3-a852-d724d00efc0e", "Student", "ruben.vlassak@student.pxl.be", true, false, true, null, "Vlassak", "RUBEN.VLASSAK@STUDENT.PXL.BE", "RUBEN.VLASSAK@STUDENT.PXL.BE", "AQAAAAEAACcQAAAAEE3MADTcj5R9NBdPZGuSrqvcfisB2jZdZoPa3+AbxcGsaSOQAO6JOO5UxUQ+SerEAw==", null, false, "61ccc054-24e4-417a-9c12-d17d1bd1d32a", false, "ruben.vlassak@student.pxl.be", "Ruben", 2 },
                    { 6, 0, "63d9783f-bb94-4884-af5a-72bace387a23", "Student", "milosz.boghe@student.pxl.be", true, false, true, null, "Boghe", "MILOSZ.BOGHE@STUDENT.PXL.BE", "MILOSZ.BOGHE@STUDENT.PXL.BE", "AQAAAAEAACcQAAAAEE3MADTcj5R9NBdPZGuSrqvcfisB2jZdZoPa3+AbxcGsaSOQAO6JOO5UxUQ+SerEAw==", null, false, "ce0c5f15-1170-4f8f-90c2-756cbc64bbb9", false, "milosz.boghe@student.pxl.be", "Milosz", 2 }
                });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "Date", "StagevoorstelId", "Text", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2020, 5, 20, 15, 48, 0, 0, DateTimeKind.Local), 1, "Het onderzoeksthema is niet ingevuld. Kan dit zo spoedig mogelijk ingevuld worden?", 1 },
                    { 2, new DateTime(2020, 5, 21, 8, 40, 0, 0, DateTimeKind.Local), 1, "We zullen er eens over nadenken en dit zo spoedig mogelijk in gaan vullen.", 11 },
                    { 3, new DateTime(2020, 5, 21, 9, 24, 0, 0, DateTimeKind.Local), 2, "Wat wordt er precies bedoeld met 'De stagiairs zullen leren werken met AI om autopiloot van Tesla te verbeteren'? Dit is niet voldoende, gelieve dit te verduidelijken.", 1 }
                });

            migrationBuilder.InsertData(
                table: "FavorietenStagevoorstellenReviewer",
                columns: new[] { "ReviewerId", "StagevoorstelId" },
                values: new object[,]
                {
                    { 3, 4 },
                    { 2, 1 },
                    { 4, 7 }
                });

            migrationBuilder.InsertData(
                table: "FavorietenStagevoorstellenStudent",
                columns: new[] { "StudentId", "StagevoorstelId" },
                values: new object[,]
                {
                    { 10, 4 },
                    { 5, 3 },
                    { 10, 2 }
                });

            migrationBuilder.InsertData(
                table: "Reviews",
                columns: new[] { "Id", "Date", "ReviewerId", "StagevoorstelId", "Status", "StatusVoorstel", "Text" },
                values: new object[,]
                {
                    { 7, new DateTime(2020, 5, 21, 20, 16, 39, 529, DateTimeKind.Local).AddTicks(5552), 4, 7, 2, 0, "Storingen van welke aard? Moeilijkheidsgraad?" },
                    { 1, new DateTime(2020, 5, 21, 20, 16, 39, 529, DateTimeKind.Local).AddTicks(3429), 2, 1, 2, 2, null },
                    { 4, new DateTime(2020, 5, 21, 20, 16, 39, 529, DateTimeKind.Local).AddTicks(5179), 3, 4, 0, 2, null },
                    { 5, new DateTime(2020, 5, 21, 20, 16, 39, 529, DateTimeKind.Local).AddTicks(5183), 3, 5, 2, 1, "Te grote verantwoordelijkheid.." },
                    { 2, new DateTime(2020, 5, 21, 20, 16, 39, 529, DateTimeKind.Local).AddTicks(5132), 2, 2, 2, 2, null },
                    { 6, new DateTime(2020, 5, 21, 20, 16, 39, 529, DateTimeKind.Local).AddTicks(5528), 4, 6, 2, 0, "Over welke tools gaat het hier precies? We hadden graag wat meer informatie zodat de studenten ook weten waar ze aan toe zijn." },
                    { 8, new DateTime(2020, 5, 21, 20, 16, 39, 529, DateTimeKind.Local).AddTicks(5555), 4, 8, 1, 1, "Slaat helemaal nergens op!" },
                    { 3, new DateTime(2020, 5, 21, 20, 16, 39, 529, DateTimeKind.Local).AddTicks(5175), 3, 3, 2, 2, null }
                });

            migrationBuilder.InsertData(
                table: "ToegewezenStagevoorstellenReviewer",
                columns: new[] { "ReviewerId", "StagevoorstelId" },
                values: new object[,]
                {
                    { 4, 7 },
                    { 4, 3 },
                    { 4, 6 },
                    { 3, 5 },
                    { 3, 3 },
                    { 3, 4 },
                    { 4, 8 },
                    { 2, 2 },
                    { 2, 1 },
                    { 3, 2 }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[,]
                {
                    { 7, 2 },
                    { 9, 2 },
                    { 6, 2 },
                    { 8, 2 }
                });

            migrationBuilder.InsertData(
                table: "FavorietenStagevoorstellenStudent",
                columns: new[] { "StudentId", "StagevoorstelId" },
                values: new object[,]
                {
                    { 7, 1 },
                    { 9, 2 },
                    { 6, 4 },
                    { 8, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_BedrijfspromotorId",
                table: "AspNetUsers",
                column: "BedrijfspromotorId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ContactpersoonId",
                table: "AspNetUsers",
                column: "ContactpersoonId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_StagevoorstelId",
                table: "AspNetUsers",
                column: "StagevoorstelId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_StagevoorstelId",
                table: "Comments",
                column: "StagevoorstelId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_UserId",
                table: "Comments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_FavorietenStagevoorstellenReviewer_StagevoorstelId",
                table: "FavorietenStagevoorstellenReviewer",
                column: "StagevoorstelId");

            migrationBuilder.CreateIndex(
                name: "IX_FavorietenStagevoorstellenStudent_StagevoorstelId",
                table: "FavorietenStagevoorstellenStudent",
                column: "StagevoorstelId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_UserId",
                table: "Messages",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_ReviewerId",
                table: "Reviews",
                column: "ReviewerId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_StagevoorstelId",
                table: "Reviews",
                column: "StagevoorstelId");

            migrationBuilder.CreateIndex(
                name: "IX_Stagevoorstellen_BedrijfId",
                table: "Stagevoorstellen",
                column: "BedrijfId");

            migrationBuilder.CreateIndex(
                name: "IX_ToegewezenStagevoorstellenReviewer_StagevoorstelId",
                table: "ToegewezenStagevoorstellenReviewer",
                column: "StagevoorstelId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_AspNetUsers_UserId",
                table: "Comments",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Stagevoorstellen_StagevoorstelId",
                table: "Comments",
                column: "StagevoorstelId",
                principalTable: "Stagevoorstellen",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FavorietenStagevoorstellenReviewer_AspNetUsers_ReviewerId",
                table: "FavorietenStagevoorstellenReviewer",
                column: "ReviewerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FavorietenStagevoorstellenReviewer_Stagevoorstellen_StagevoorstelId",
                table: "FavorietenStagevoorstellenReviewer",
                column: "StagevoorstelId",
                principalTable: "Stagevoorstellen",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FavorietenStagevoorstellenStudent_AspNetUsers_StudentId",
                table: "FavorietenStagevoorstellenStudent",
                column: "StudentId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FavorietenStagevoorstellenStudent_Stagevoorstellen_StagevoorstelId",
                table: "FavorietenStagevoorstellenStudent",
                column: "StagevoorstelId",
                principalTable: "Stagevoorstellen",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_AspNetUsers_UserId",
                table: "Messages",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_AspNetUsers_ReviewerId",
                table: "Reviews",
                column: "ReviewerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Stagevoorstellen_StagevoorstelId",
                table: "Reviews",
                column: "StagevoorstelId",
                principalTable: "Stagevoorstellen",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Stagevoorstellen_AspNetUsers_BedrijfId",
                table: "Stagevoorstellen",
                column: "BedrijfId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stagevoorstellen_AspNetUsers_BedrijfId",
                table: "Stagevoorstellen");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "FavorietenStagevoorstellenReviewer");

            migrationBuilder.DropTable(
                name: "FavorietenStagevoorstellenStudent");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "ResetPasswordRequests");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "ToegewezenStagevoorstellenReviewer");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Bedrijfspromotors");

            migrationBuilder.DropTable(
                name: "Contactpersonen");

            migrationBuilder.DropTable(
                name: "Stagevoorstellen");
        }
    }
}
