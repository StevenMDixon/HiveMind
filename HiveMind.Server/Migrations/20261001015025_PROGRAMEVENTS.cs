using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HiveMind.Server.Migrations
{
    /// <inheritdoc />
    public partial class PROGRAMEVENTS : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProgramEvents",
                columns: table => new
                {
                    EventId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    ProgramStrategyId = table.Column<int>(type: "INTEGER", nullable: true),
                    DateStart = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    DateEnd = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    QueryId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgramEvents", x => x.EventId);
                    table.ForeignKey(
                        name: "FK_ProgramEvents_ProgramStrategies_ProgramStrategyId",
                        column: x => x.ProgramStrategyId,
                        principalTable: "ProgramStrategies",
                        principalColumn: "ProgramStrategyId");
                    table.ForeignKey(
                        name: "FK_ProgramEvents_Queries_QueryId",
                        column: x => x.QueryId,
                        principalTable: "Queries",
                        principalColumn: "QueryId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProgramEvents_ProgramStrategyId",
                table: "ProgramEvents",
                column: "ProgramStrategyId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramEvents_QueryId",
                table: "ProgramEvents",
                column: "QueryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProgramEvents");
        }
    }
}
