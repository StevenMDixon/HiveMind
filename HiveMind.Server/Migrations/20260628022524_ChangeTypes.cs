using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HiveMind.Server.Migrations
{
    /// <inheritdoc />
    public partial class ChangeTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProgramStrategyLineUpId",
                table: "ScheduleBatchItems",
                type: "INTEGER",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProgramStrategyLineUpId",
                table: "ScheduleBatchItems");
        }
    }
}
