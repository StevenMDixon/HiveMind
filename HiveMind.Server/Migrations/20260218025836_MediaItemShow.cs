using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HiveMind.Server.Migrations
{
    /// <inheritdoc />
    public partial class MediaItemShow : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "MediaItems");

            migrationBuilder.DropColumn(
                name: "Season",
                table: "MediaItems");

            migrationBuilder.DropColumn(
                name: "Show",
                table: "MediaItems");

            migrationBuilder.AddColumn<int>(
                name: "SeasonNumber",
                table: "MediaItems",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ShowId",
                table: "MediaItems",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "MediaItemShows",
                columns: table => new
                {
                    MediaItemShowId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MediaItemShowTitle = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MediaItemShows", x => x.MediaItemShowId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MediaItems_ShowId",
                table: "MediaItems",
                column: "ShowId");

            migrationBuilder.AddForeignKey(
                name: "FK_MediaItems_MediaItemShows_ShowId",
                table: "MediaItems",
                column: "ShowId",
                principalTable: "MediaItemShows",
                principalColumn: "MediaItemShowId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MediaItems_MediaItemShows_ShowId",
                table: "MediaItems");

            migrationBuilder.DropTable(
                name: "MediaItemShows");

            migrationBuilder.DropIndex(
                name: "IX_MediaItems_ShowId",
                table: "MediaItems");

            migrationBuilder.DropColumn(
                name: "SeasonNumber",
                table: "MediaItems");

            migrationBuilder.DropColumn(
                name: "ShowId",
                table: "MediaItems");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "MediaItems",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Season",
                table: "MediaItems",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Show",
                table: "MediaItems",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
