using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HiveMind.Server.Migrations
{
    /// <inheritdoc />
    public partial class PROGRAMEVENTSDATECHANGE : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProgramEvents_Queries_QueryId",
                table: "ProgramEvents");

            migrationBuilder.DropColumn(
                name: "DateEnd",
                table: "ProgramEvents");

            migrationBuilder.RenameColumn(
                name: "DateStart",
                table: "ProgramEvents",
                newName: "EventDate");

            migrationBuilder.AlterColumn<int>(
                name: "QueryId",
                table: "ProgramEvents",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PromoDays",
                table: "ProgramEvents",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_ProgramEvents_Queries_QueryId",
                table: "ProgramEvents",
                column: "QueryId",
                principalTable: "Queries",
                principalColumn: "QueryId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProgramEvents_Queries_QueryId",
                table: "ProgramEvents");

            migrationBuilder.DropColumn(
                name: "PromoDays",
                table: "ProgramEvents");

            migrationBuilder.RenameColumn(
                name: "EventDate",
                table: "ProgramEvents",
                newName: "DateStart");

            migrationBuilder.AlterColumn<int>(
                name: "QueryId",
                table: "ProgramEvents",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<DateOnly>(
                name: "DateEnd",
                table: "ProgramEvents",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddForeignKey(
                name: "FK_ProgramEvents_Queries_QueryId",
                table: "ProgramEvents",
                column: "QueryId",
                principalTable: "Queries",
                principalColumn: "QueryId");
        }
    }
}
