using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ObedientChild.Infrastructure.Migrations
{
    public partial class FixPriceProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Priсe",
                table: "Rewards",
                newName: "Price");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Rewards",
                newName: "Priсe");
        }
    }
}
