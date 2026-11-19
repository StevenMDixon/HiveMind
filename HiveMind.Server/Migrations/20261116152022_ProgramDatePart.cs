using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HiveMind.Server.Migrations
{
    /// <inheritdoc />
    public partial class ProgramDatePart : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProgramDatePart",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    StartTime = table.Column<TimeOnly>(type: "TEXT", nullable: false),
                    EndTime = table.Column<TimeOnly>(type: "TEXT", nullable: false),
                    ProgramStrategyId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgramDatePart", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProgramDatePart_ProgramStrategies_ProgramStrategyId",
                        column: x => x.ProgramStrategyId,
                        principalTable: "ProgramStrategies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProgramDatePart_ProgramStrategyId",
                table: "ProgramDatePart",
                column: "ProgramStrategyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProgramDatePart");
        }
    }
}
