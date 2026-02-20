using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HiveMind.Server.Migrations
{
    /// <inheritdoc />
    public partial class MediaItemShowChangeIDNull : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MediaItems_MediaItemShows_MediaItemShowId",
                table: "MediaItems");

            migrationBuilder.AlterColumn<int>(
                name: "SeasonNumber",
                table: "MediaItems",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "MediaItemShowId",
                table: "MediaItems",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_MediaItems_MediaItemShows_MediaItemShowId",
                table: "MediaItems",
                column: "MediaItemShowId",
                principalTable: "MediaItemShows",
                principalColumn: "MediaItemShowId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MediaItems_MediaItemShows_MediaItemShowId",
                table: "MediaItems");

            migrationBuilder.AlterColumn<int>(
                name: "SeasonNumber",
                table: "MediaItems",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MediaItemShowId",
                table: "MediaItems",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_MediaItems_MediaItemShows_MediaItemShowId",
                table: "MediaItems",
                column: "MediaItemShowId",
                principalTable: "MediaItemShows",
                principalColumn: "MediaItemShowId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
