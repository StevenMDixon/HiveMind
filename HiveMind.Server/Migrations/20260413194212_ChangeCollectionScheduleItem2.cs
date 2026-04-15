using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HiveMind.Server.Migrations
{
    /// <inheritdoc />
    public partial class ChangeCollectionScheduleItem2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Duration",
                table: "CollectionScheduleItems",
                newName: "PlayDuration");

            migrationBuilder.RenameColumn(
                name: "DisableIntroBump",
                table: "CollectionScheduleItems",
                newName: "PlayCount");

            migrationBuilder.RenameColumn(
                name: "DisableInterStitials",
                table: "CollectionScheduleItems",
                newName: "PadTo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PlayDuration",
                table: "CollectionScheduleItems",
                newName: "Duration");

            migrationBuilder.RenameColumn(
                name: "PlayCount",
                table: "CollectionScheduleItems",
                newName: "DisableIntroBump");

            migrationBuilder.RenameColumn(
                name: "PadTo",
                table: "CollectionScheduleItems",
                newName: "DisableInterStitials");
        }
    }
}
