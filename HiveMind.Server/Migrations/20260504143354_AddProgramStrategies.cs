using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HiveMind.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddProgramStrategies : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProgramStrategies",
                columns: table => new
                {
                    ProgramStrategyId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Active = table.Column<bool>(type: "INTEGER", nullable: false),
                    AdvancedDays = table.Column<int>(type: "INTEGER", nullable: false),
                    StartDate = table.Column<DateOnly>(type: "TEXT", nullable: true),
                    EndDate = table.Column<DateOnly>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgramStrategies", x => x.ProgramStrategyId);
                });

            migrationBuilder.CreateTable(
                name: "ProgramStrategyLineups",
                columns: table => new
                {
                    ProgramStrategyLineupId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProgramStrategyId = table.Column<int>(type: "INTEGER", nullable: true),
                    LineupId = table.Column<int>(type: "INTEGER", nullable: true),
                    SelectionType = table.Column<int>(type: "INTEGER", nullable: false),
                    SelectionOption = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgramStrategyLineups", x => x.ProgramStrategyLineupId);
                    table.ForeignKey(
                        name: "FK_ProgramStrategyLineups_Lineups_LineupId",
                        column: x => x.LineupId,
                        principalTable: "Lineups",
                        principalColumn: "LineupId");
                    table.ForeignKey(
                        name: "FK_ProgramStrategyLineups_ProgramStrategies_ProgramStrategyId",
                        column: x => x.ProgramStrategyId,
                        principalTable: "ProgramStrategies",
                        principalColumn: "ProgramStrategyId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProgramStrategyLineups_LineupId",
                table: "ProgramStrategyLineups",
                column: "LineupId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramStrategyLineups_ProgramStrategyId",
                table: "ProgramStrategyLineups",
                column: "ProgramStrategyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProgramStrategyLineups");

            migrationBuilder.DropTable(
                name: "ProgramStrategies");
        }
    }
}
