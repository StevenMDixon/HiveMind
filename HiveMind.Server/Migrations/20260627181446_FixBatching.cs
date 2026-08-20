using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HiveMind.Server.Migrations
{
    /// <inheritdoc />
    public partial class FixBatching : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScheduleBatchItems_ScheduleBatches_ScheduleBatchId",
                table: "ScheduleBatchItems");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "ProgramStrategies");

            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "ProgramStrategies",
                newName: "LastScheduleDate");

            migrationBuilder.AlterColumn<int>(
                name: "ScheduleBatchId",
                table: "ScheduleBatchItems",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "ProgramStrategyId",
                table: "ScheduleBatches",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleBatches_ProgramStrategyId",
                table: "ScheduleBatches",
                column: "ProgramStrategyId");

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduleBatches_ProgramStrategies_ProgramStrategyId",
                table: "ScheduleBatches",
                column: "ProgramStrategyId",
                principalTable: "ProgramStrategies",
                principalColumn: "ProgramStrategyId");

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduleBatchItems_ScheduleBatches_ScheduleBatchId",
                table: "ScheduleBatchItems",
                column: "ScheduleBatchId",
                principalTable: "ScheduleBatches",
                principalColumn: "ScheduleBatchId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScheduleBatches_ProgramStrategies_ProgramStrategyId",
                table: "ScheduleBatches");

            migrationBuilder.DropForeignKey(
                name: "FK_ScheduleBatchItems_ScheduleBatches_ScheduleBatchId",
                table: "ScheduleBatchItems");

            migrationBuilder.DropIndex(
                name: "IX_ScheduleBatches_ProgramStrategyId",
                table: "ScheduleBatches");

            migrationBuilder.RenameColumn(
                name: "LastScheduleDate",
                table: "ProgramStrategies",
                newName: "StartDate");

            migrationBuilder.AlterColumn<int>(
                name: "ScheduleBatchId",
                table: "ScheduleBatchItems",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ProgramStrategyId",
                table: "ScheduleBatches",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "EndDate",
                table: "ProgramStrategies",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduleBatchItems_ScheduleBatches_ScheduleBatchId",
                table: "ScheduleBatchItems",
                column: "ScheduleBatchId",
                principalTable: "ScheduleBatches",
                principalColumn: "ScheduleBatchId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
