using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HiveMind.Server.Migrations
{
    /// <inheritdoc />
    public partial class LibraryDefaultTags : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProgramDatePart_ProgramStrategies_ProgramStrategyId",
                table: "ProgramDatePart");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProgramDatePart",
                table: "ProgramDatePart");

            migrationBuilder.DeleteData(
                table: "Stations",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Stations",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Stations",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.RenameTable(
                name: "ProgramDatePart",
                newName: "ProgramDateParts");

            migrationBuilder.RenameIndex(
                name: "IX_ProgramDatePart_ProgramStrategyId",
                table: "ProgramDateParts",
                newName: "IX_ProgramDateParts_ProgramStrategyId");

            migrationBuilder.AddColumn<string>(
                name: "DefaultTags",
                table: "Libraries",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProgramDateParts",
                table: "ProgramDateParts",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProgramDateParts_ProgramStrategies_ProgramStrategyId",
                table: "ProgramDateParts",
                column: "ProgramStrategyId",
                principalTable: "ProgramStrategies",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProgramDateParts_ProgramStrategies_ProgramStrategyId",
                table: "ProgramDateParts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProgramDateParts",
                table: "ProgramDateParts");

            migrationBuilder.DropColumn(
                name: "DefaultTags",
                table: "Libraries");

            migrationBuilder.RenameTable(
                name: "ProgramDateParts",
                newName: "ProgramDatePart");

            migrationBuilder.RenameIndex(
                name: "IX_ProgramDateParts_ProgramStrategyId",
                table: "ProgramDatePart",
                newName: "IX_ProgramDatePart_ProgramStrategyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProgramDatePart",
                table: "ProgramDatePart",
                column: "Id");

            migrationBuilder.InsertData(
                table: "Stations",
                columns: new[] { "Id", "Logo", "Name", "Number", "StrategyId" },
                values: new object[,]
                {
                    { 1, "", "Test1", 1, null },
                    { 2, "", "Test2", 2, null },
                    { 3, "", "Test3", 3, null }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_ProgramDatePart_ProgramStrategies_ProgramStrategyId",
                table: "ProgramDatePart",
                column: "ProgramStrategyId",
                principalTable: "ProgramStrategies",
                principalColumn: "Id");
        }
    }
}
