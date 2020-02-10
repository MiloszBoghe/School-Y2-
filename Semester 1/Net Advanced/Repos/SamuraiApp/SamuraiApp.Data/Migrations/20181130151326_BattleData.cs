using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SamuraiApp.Data.Migrations
{
    public partial class BattleData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Battles",
                columns: new[] { "Id", "EndDate", "Name", "StartDate" },
                values: new object[] { 1, new DateTime(902, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "The great battle", new DateTime(902, 1, 30, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Battles",
                columns: new[] { "Id", "EndDate", "Name", "StartDate" },
                values: new object[] { 2, new DateTime(904, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "The long battle", new DateTime(903, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Battles",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Battles",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
