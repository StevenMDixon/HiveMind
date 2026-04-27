using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HiveMind.Server.Migrations
{
    /// <inheritdoc />
    public partial class ChageTableName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MediaItems_MediaItemShows_ShowId",
                table: "MediaItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MediaItemShows",
                table: "MediaItemShows");

            migrationBuilder.RenameTable(
                name: "MediaItemShows",
                newName: "Shows");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Shows",
                table: "Shows",
                column: "ShowId");

            migrationBuilder.AddForeignKey(
                name: "FK_MediaItems_Shows_ShowId",
                table: "MediaItems",
                column: "ShowId",
                principalTable: "Shows",
                principalColumn: "ShowId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MediaItems_Shows_ShowId",
                table: "MediaItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Shows",
                table: "Shows");

            migrationBuilder.RenameTable(
                name: "Shows",
                newName: "MediaItemShows");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MediaItemShows",
                table: "MediaItemShows",
                column: "ShowId");

            migrationBuilder.AddForeignKey(
                name: "FK_MediaItems_MediaItemShows_ShowId",
                table: "MediaItems",
                column: "ShowId",
                principalTable: "MediaItemShows",
                principalColumn: "ShowId");
        }
    }
}
