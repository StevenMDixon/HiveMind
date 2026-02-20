using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HiveMind.Server.Migrations
{
    /// <inheritdoc />
    public partial class MediaItemShowChangeID : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MediaItems_MediaItemShows_ShowId",
                table: "MediaItems");

            migrationBuilder.RenameColumn(
                name: "ShowId",
                table: "MediaItems",
                newName: "MediaItemShowId");

            migrationBuilder.RenameIndex(
                name: "IX_MediaItems_ShowId",
                table: "MediaItems",
                newName: "IX_MediaItems_MediaItemShowId");

            migrationBuilder.AddForeignKey(
                name: "FK_MediaItems_MediaItemShows_MediaItemShowId",
                table: "MediaItems",
                column: "MediaItemShowId",
                principalTable: "MediaItemShows",
                principalColumn: "MediaItemShowId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MediaItems_MediaItemShows_MediaItemShowId",
                table: "MediaItems");

            migrationBuilder.RenameColumn(
                name: "MediaItemShowId",
                table: "MediaItems",
                newName: "ShowId");

            migrationBuilder.RenameIndex(
                name: "IX_MediaItems_MediaItemShowId",
                table: "MediaItems",
                newName: "IX_MediaItems_ShowId");

            migrationBuilder.AddForeignKey(
                name: "FK_MediaItems_MediaItemShows_ShowId",
                table: "MediaItems",
                column: "ShowId",
                principalTable: "MediaItemShows",
                principalColumn: "MediaItemShowId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
