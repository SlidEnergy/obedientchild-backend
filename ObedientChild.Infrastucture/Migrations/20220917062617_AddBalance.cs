using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ObedientChild.Infrastructure.Migrations
{
    public partial class AddBalance : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Children",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Balance",
                table: "Children",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BigGoalBalance",
                table: "Children",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BigGoalId",
                table: "Children",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DreamBalance",
                table: "Children",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DreamId",
                table: "Children",
                type: "integer",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Balance",
                table: "Children");

            migrationBuilder.DropColumn(
                name: "BigGoalBalance",
                table: "Children");

            migrationBuilder.DropColumn(
                name: "BigGoalId",
                table: "Children");

            migrationBuilder.DropColumn(
                name: "DreamBalance",
                table: "Children");

            migrationBuilder.DropColumn(
                name: "DreamId",
                table: "Children");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Children",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");
        }
    }
}
