using Microsoft.EntityFrameworkCore.Migrations;

namespace Bank.Datalayer.Migrations
{
    public partial class fx : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Cellphone",
                table: "Customers",
                newName: "CellPhone");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CellPhone",
                table: "Customers",
                newName: "Cellphone");
        }
    }
}
