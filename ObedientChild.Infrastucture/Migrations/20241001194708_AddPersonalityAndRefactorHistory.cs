using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ObedientChild.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPersonalityAndRefactorHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ChildId",
                table: "CoinHistory",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "BalanceType",
                table: "CoinHistory",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "CoinHistory",
                type: "text",
                nullable: true);

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
                name: "CharacterTraits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: true),
                    ImageUrl = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    BadDeedId = table.Column<int>(type: "integer", nullable: true),
                    GoodDeedId = table.Column<int>(type: "integer", nullable: true),
                    HabitId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterTraits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CharacterTraits_BadDeeds_BadDeedId",
                        column: x => x.BadDeedId,
                        principalTable: "BadDeeds",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CharacterTraits_GoodDeeds_GoodDeedId",
                        column: x => x.GoodDeedId,
                        principalTable: "GoodDeeds",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CharacterTraits_Habits_HabitId",
                        column: x => x.HabitId,
                        principalTable: "Habits",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Destinies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: true),
                    ImageUrl = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Destinies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DestiniesPersonalities",
                columns: table => new
                {
                    DestinyId = table.Column<int>(type: "integer", nullable: false),
                    PersonalityId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DestiniesPersonalities", x => new { x.DestinyId, x.PersonalityId });
                });

            migrationBuilder.CreateTable(
                name: "Personalities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    ImageUrl = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personalities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PersonalitiesCharacterTraits",
                columns: table => new
                {
                    PersonalityId = table.Column<int>(type: "integer", nullable: false),
                    CharacterTraitId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonalitiesCharacterTraits", x => new { x.PersonalityId, x.CharacterTraitId });
                });

            migrationBuilder.CreateTable(
                name: "CharacterTraitsLevel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: true),
                    CharacterTraitId = table.Column<int>(type: "integer", nullable: false),
                    Level = table.Column<int>(type: "integer", nullable: false),
                    NeedExperience = table.Column<int>(type: "integer", nullable: false),
                    ImageUrl = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterTraitsLevel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CharacterTraitsLevel_CharacterTraits_CharacterTraitId",
                        column: x => x.CharacterTraitId,
                        principalTable: "CharacterTraits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChildCharacterTraits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ChildId = table.Column<int>(type: "integer", nullable: false),
                    CharacterTraitId = table.Column<int>(type: "integer", nullable: false),
                    Level = table.Column<int>(type: "integer", nullable: false),
                    Experience = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChildCharacterTraits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChildCharacterTraits_CharacterTraits_CharacterTraitId",
                        column: x => x.CharacterTraitId,
                        principalTable: "CharacterTraits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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

            migrationBuilder.CreateIndex(
                name: "IX_CharacterTraitsLevel_CharacterTraitId",
                table: "CharacterTraitsLevel",
                column: "CharacterTraitId");

            migrationBuilder.CreateIndex(
                name: "IX_ChildCharacterTraits_CharacterTraitId",
                table: "ChildCharacterTraits",
                column: "CharacterTraitId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CharacterTraitBadDeeds");

            migrationBuilder.DropTable(
                name: "CharacterTraitGoodDeeds");

            migrationBuilder.DropTable(
                name: "CharacterTraitHabits");

            migrationBuilder.DropTable(
                name: "CharacterTraitsLevel");

            migrationBuilder.DropTable(
                name: "ChildCharacterTraits");

            migrationBuilder.DropTable(
                name: "Destinies");

            migrationBuilder.DropTable(
                name: "DestiniesPersonalities");

            migrationBuilder.DropTable(
                name: "Personalities");

            migrationBuilder.DropTable(
                name: "PersonalitiesCharacterTraits");

            migrationBuilder.DropTable(
                name: "CharacterTraits");

            migrationBuilder.DropColumn(
                name: "BalanceType",
                table: "CoinHistory");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "CoinHistory");

            migrationBuilder.AlterColumn<int>(
                name: "ChildId",
                table: "CoinHistory",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);
        }
    }
}
