using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ObedientChild.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddLifeEnergy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LifeEnergyAccounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Balance = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LifeEnergyAccounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LifeEnergyHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LifeEnergyAccountId = table.Column<int>(type: "integer", nullable: false),
                    Amount = table.Column<int>(type: "integer", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: true),
                    DateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LifeEnergyHistory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TrusteeLifeEnergyAccounts",
                columns: table => new
                {
                    TrusteeId = table.Column<int>(type: "integer", nullable: false),
                    LifeEnergyAccountId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrusteeLifeEnergyAccounts", x => new { x.TrusteeId, x.LifeEnergyAccountId });
                    table.ForeignKey(
                        name: "FK_TrusteeLifeEnergyAccounts_LifeEnergyAccounts_LifeEnergyAcco~",
                        column: x => x.LifeEnergyAccountId,
                        principalTable: "LifeEnergyAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TrusteeLifeEnergyAccounts_Trustee_TrusteeId",
                        column: x => x.TrusteeId,
                        principalTable: "Trustee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TrusteeLifeEnergyAccounts_LifeEnergyAccountId",
                table: "TrusteeLifeEnergyAccounts",
                column: "LifeEnergyAccountId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LifeEnergyHistory");

            migrationBuilder.DropTable(
                name: "TrusteeLifeEnergyAccounts");

            migrationBuilder.DropTable(
                name: "LifeEnergyAccounts");
        }
    }
}
