using Microsoft.EntityFrameworkCore.Migrations;
using System.Text.RegularExpressions;

#nullable disable

namespace ObedientChild.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MergeDeeds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DeedType",
                table: "Habits",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.Sql(@"
INSERT INTO public.""Habits"" (""Title"", ""Price"", ""ImageUrl"", ""DeedType"")
SELECT ""Title"", ""Price"", ""ImageUrl"", 1 FROM public.""GoodDeeds"";

INSERT INTO public.""Habits"" (""Title"", ""Price"", ""ImageUrl"", ""DeedType"")
SELECT ""Title"", ""Price"", ""ImageUrl"", 2 FROM public.""BadDeeds"";

INSERT INTO public.""Habits"" (""Title"", ""Price"", ""ImageUrl"", ""DeedType"")
SELECT ""Title"", ""Price"", ""ImageUrl"", 3 FROM public.""Rewards"";
            ");

            migrationBuilder.RenameTable(
                name: "Habits",
                newName: "Deeds");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "Deeds",
                newName: "Habits");

            migrationBuilder.DropColumn(
                name: "DeedType",
                table: "Habits");
        }
    }
}
