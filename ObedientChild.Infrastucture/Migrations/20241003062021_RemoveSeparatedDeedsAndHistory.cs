using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ObedientChild.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveSeparatedDeedsAndHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CharacterTraits_BadDeeds_BadDeedId",
                table: "CharacterTraits");

            migrationBuilder.DropForeignKey(
                name: "FK_CharacterTraits_Habits_HabitId",
                table: "CharacterTraits");

            migrationBuilder.DropForeignKey(
                name: "FK_CharacterTraits_GoodDeeds_GoodDeedId",
                table: "CharacterTraits");

            migrationBuilder.DropForeignKey(
                name: "FK_ChildTasks_GoodDeeds_GoodDeedId",
                table: "ChildTasks");

            migrationBuilder.DropTable(
                name: "BadDeeds");

            migrationBuilder.DropTable(
                name: "CharacterTraitBadDeeds");

            migrationBuilder.DropTable(
                name: "CharacterTraitGoodDeeds");

            migrationBuilder.DropTable(
                name: "CharacterTraitHabits");

            migrationBuilder.DropTable(
                name: "GoodDeeds");

            migrationBuilder.DropTable(
                name: "LifeEnergyHistory");

            migrationBuilder.DropTable(
                name: "Rewards");

            migrationBuilder.DropIndex(
                name: "IX_CharacterTraits_BadDeedId",
                table: "CharacterTraits");

            migrationBuilder.DropIndex(
                name: "IX_CharacterTraits_GoodDeedId",
                table: "CharacterTraits");

            migrationBuilder.DropIndex(
                name: "IX_CharacterTraits_HabitId",
                table: "CharacterTraits");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CoinHistory",
                table: "CoinHistory");

            migrationBuilder.DropColumn(
                name: "BadDeedId",
                table: "CharacterTraits");

            migrationBuilder.DropColumn(
                name: "GoodDeedId",
                table: "CharacterTraits");

            migrationBuilder.DropColumn(
                name: "HabitId",
                table: "CharacterTraits");

            migrationBuilder.RenameTable(
                name: "CoinHistory",
                newName: "BalanceHistory");

            migrationBuilder.RenameColumn(
                name: "HabitId",
                table: "HabitHistory",
                newName: "DeedId");

            migrationBuilder.RenameColumn(
                name: "DeedType",
                table: "Deeds",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "GoodDeedId",
                table: "ChildTasks",
                newName: "DeedId");

            migrationBuilder.RenameIndex(
                name: "IX_ChildTasks_GoodDeedId",
                table: "ChildTasks",
                newName: "IX_ChildTasks_DeedId");

            migrationBuilder.RenameColumn(
                name: "HabitId",
                table: "ChildHabits",
                newName: "DeedId");

            migrationBuilder.RenameColumn(
                name: "ChildId",
                table: "BalanceHistory",
                newName: "EntityId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BalanceHistory",
                table: "BalanceHistory",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "CharacterTraitsDeeds",
                columns: table => new
                {
                    CharacterTraitId = table.Column<int>(type: "integer", nullable: false),
                    DeedId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterTraitsDeeds", x => new { x.CharacterTraitId, x.DeedId });
                    table.ForeignKey(
                        name: "FK_CharacterTraitsDeeds_CharacterTraits_CharacterTraitId",
                        column: x => x.CharacterTraitId,
                        principalTable: "CharacterTraits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CharacterTraitsDeeds_Deeds_DeedId",
                        column: x => x.DeedId,
                        principalTable: "Deeds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PersonalitiesCharacterTraits_CharacterTraitId",
                table: "PersonalitiesCharacterTraits",
                column: "CharacterTraitId");

            migrationBuilder.CreateIndex(
                name: "IX_DestiniesPersonalities_PersonalityId",
                table: "DestiniesPersonalities",
                column: "PersonalityId");

            migrationBuilder.CreateIndex(
                name: "IX_ChildHabits_ChildId",
                table: "ChildHabits",
                column: "ChildId");

            migrationBuilder.CreateIndex(
                name: "IX_CharacterTraitsDeeds_DeedId",
                table: "CharacterTraitsDeeds",
                column: "DeedId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChildHabits_Children_ChildId",
                table: "ChildHabits",
                column: "ChildId",
                principalTable: "Children",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChildHabits_Deeds_DeedId",
                table: "ChildHabits",
                column: "DeedId",
                principalTable: "Deeds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChildTasks_Deeds_DeedId",
                table: "ChildTasks",
                column: "DeedId",
                principalTable: "Deeds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DestiniesPersonalities_Destinies_DestinyId",
                table: "DestiniesPersonalities",
                column: "DestinyId",
                principalTable: "Destinies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DestiniesPersonalities_Personalities_PersonalityId",
                table: "DestiniesPersonalities",
                column: "PersonalityId",
                principalTable: "Personalities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonalitiesCharacterTraits_CharacterTraits_CharacterTrait~",
                table: "PersonalitiesCharacterTraits",
                column: "CharacterTraitId",
                principalTable: "CharacterTraits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonalitiesCharacterTraits_Personalities_PersonalityId",
                table: "PersonalitiesCharacterTraits",
                column: "PersonalityId",
                principalTable: "Personalities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChildHabits_Children_ChildId",
                table: "ChildHabits");

            migrationBuilder.DropForeignKey(
                name: "FK_ChildHabits_Deeds_DeedId",
                table: "ChildHabits");

            migrationBuilder.DropForeignKey(
                name: "FK_ChildTasks_Deeds_DeedId",
                table: "ChildTasks");

            migrationBuilder.DropForeignKey(
                name: "FK_DestiniesPersonalities_Destinies_DestinyId",
                table: "DestiniesPersonalities");

            migrationBuilder.DropForeignKey(
                name: "FK_DestiniesPersonalities_Personalities_PersonalityId",
                table: "DestiniesPersonalities");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonalitiesCharacterTraits_CharacterTraits_CharacterTrait~",
                table: "PersonalitiesCharacterTraits");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonalitiesCharacterTraits_Personalities_PersonalityId",
                table: "PersonalitiesCharacterTraits");

            migrationBuilder.DropTable(
                name: "CharacterTraitsDeeds");

            migrationBuilder.DropIndex(
                name: "IX_PersonalitiesCharacterTraits_CharacterTraitId",
                table: "PersonalitiesCharacterTraits");

            migrationBuilder.DropIndex(
                name: "IX_DestiniesPersonalities_PersonalityId",
                table: "DestiniesPersonalities");

            migrationBuilder.DropIndex(
                name: "IX_ChildHabits_ChildId",
                table: "ChildHabits");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BalanceHistory",
                table: "BalanceHistory");

            migrationBuilder.RenameTable(
                name: "BalanceHistory",
                newName: "CoinHistory");

            migrationBuilder.RenameColumn(
                name: "DeedId",
                table: "HabitHistory",
                newName: "HabitId");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Deeds",
                newName: "DeedType");

            migrationBuilder.RenameColumn(
                name: "DeedId",
                table: "ChildTasks",
                newName: "GoodDeedId");

            migrationBuilder.RenameIndex(
                name: "IX_ChildTasks_DeedId",
                table: "ChildTasks",
                newName: "IX_ChildTasks_GoodDeedId");

            migrationBuilder.RenameColumn(
                name: "DeedId",
                table: "ChildHabits",
                newName: "HabitId");

            migrationBuilder.RenameColumn(
                name: "EntityId",
                table: "CoinHistory",
                newName: "ChildId");

            migrationBuilder.AddColumn<int>(
                name: "BadDeedId",
                table: "CharacterTraits",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GoodDeedId",
                table: "CharacterTraits",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "HabitId",
                table: "CharacterTraits",
                type: "integer",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CoinHistory",
                table: "CoinHistory",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "BadDeeds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ImageUrl = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<int>(type: "integer", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BadDeeds", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CharacterTraitBadDeeds",
                columns: table => new
                {
                    CharacterTraitId = table.Column<int>(type: "integer", nullable: false),
                    BadDeedid = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterTraitBadDeeds", x => new { x.CharacterTraitId, x.BadDeedid });
                });

            migrationBuilder.CreateTable(
                name: "CharacterTraitGoodDeeds",
                columns: table => new
                {
                    CharacterTraitId = table.Column<int>(type: "integer", nullable: false),
                    GoodDeedId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterTraitGoodDeeds", x => new { x.CharacterTraitId, x.GoodDeedId });
                });

            migrationBuilder.CreateTable(
                name: "CharacterTraitHabits",
                columns: table => new
                {
                    CharacterTraitId = table.Column<int>(type: "integer", nullable: false),
                    HabitId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterTraitHabits", x => new { x.CharacterTraitId, x.HabitId });
                });

            migrationBuilder.CreateTable(
                name: "GoodDeeds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ImageUrl = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<int>(type: "integer", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoodDeeds", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LifeEnergyHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Amount = table.Column<int>(type: "integer", nullable: false),
                    DateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LifeEnergyAccountId = table.Column<int>(type: "integer", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LifeEnergyHistory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rewards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ImageUrl = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<int>(type: "integer", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rewards", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CharacterTraits_BadDeedId",
                table: "CharacterTraits",
                column: "BadDeedId");

            migrationBuilder.CreateIndex(
                name: "IX_CharacterTraits_GoodDeedId",
                table: "CharacterTraits",
                column: "GoodDeedId");

            migrationBuilder.CreateIndex(
                name: "IX_CharacterTraits_HabitId",
                table: "CharacterTraits",
                column: "HabitId");

            migrationBuilder.AddForeignKey(
                name: "FK_CharacterTraits_BadDeeds_BadDeedId",
                table: "CharacterTraits",
                column: "BadDeedId",
                principalTable: "BadDeeds",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CharacterTraits_Habits_HabitId",
                table: "CharacterTraits",
                column: "HabitId",
                principalTable: "Deeds",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CharacterTraits_GoodDeeds_GoodDeedId",
                table: "CharacterTraits",
                column: "GoodDeedId",
                principalTable: "GoodDeeds",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ChildTasks_GoodDeeds_GoodDeedId",
                table: "ChildTasks",
                column: "GoodDeedId",
                principalTable: "GoodDeeds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
