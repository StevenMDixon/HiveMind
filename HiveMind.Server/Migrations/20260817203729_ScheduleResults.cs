using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HiveMind.Server.Migrations
{
    /// <inheritdoc />
    public partial class ScheduleResults : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SchedulingResults",
                columns: table => new
                {
                    SchedulingResultId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Path = table.Column<string>(type: "TEXT", nullable: false),
                    Date = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    ProgramStrategyId = table.Column<int>(type: "INTEGER", nullable: true),
                    LineupId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchedulingResults", x => x.SchedulingResultId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SchedulingResults");
        }
    }
}
