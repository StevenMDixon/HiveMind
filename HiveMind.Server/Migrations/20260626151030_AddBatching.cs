using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HiveMind.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddBatching : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ScheduleBatches",
                columns: table => new
                {
                    ScheduleBatchId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    StartDate = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    EndDate = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false),
                    ProgramStrategyId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduleBatches", x => x.ScheduleBatchId);
                });

            migrationBuilder.CreateTable(
                name: "ScheduleBatchItems",
                columns: table => new
                {
                    ScheduleBatchItemId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ScheduleBatchId = table.Column<int>(type: "INTEGER", nullable: false),
                    ScheduleDate = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    IsCompleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    OutputFilePath = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduleBatchItems", x => x.ScheduleBatchItemId);
                    table.ForeignKey(
                        name: "FK_ScheduleBatchItems_ScheduleBatches_ScheduleBatchId",
                        column: x => x.ScheduleBatchId,
                        principalTable: "ScheduleBatches",
                        principalColumn: "ScheduleBatchId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleBatchItems_ScheduleBatchId",
                table: "ScheduleBatchItems",
                column: "ScheduleBatchId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ScheduleBatchItems");

            migrationBuilder.DropTable(
                name: "ScheduleBatches");
        }
    }
}
