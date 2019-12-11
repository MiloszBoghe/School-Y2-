using Microsoft.EntityFrameworkCore.Migrations;

namespace EFDEMO.Data.Migrations
{
    public partial class column_added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TestProperty",
                table: "Battles",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TestProperty",
                table: "Battles");
        }
    }
}
