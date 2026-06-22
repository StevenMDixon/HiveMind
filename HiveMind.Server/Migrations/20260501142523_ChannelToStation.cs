using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HiveMind.Server.Migrations
{
    /// <inheritdoc />
    public partial class ChannelToStation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Channels");

            migrationBuilder.CreateTable(
                name: "Stations",
                columns: table => new
                {
                    StationId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    StationNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    StationName = table.Column<string>(type: "TEXT", nullable: false),
                    StationLogo = table.Column<string>(type: "TEXT", nullable: false),
                    LineupId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stations", x => x.StationId);
                    table.ForeignKey(
                        name: "FK_Stations_Lineups_LineupId",
                        column: x => x.LineupId,
                        principalTable: "Lineups",
                        principalColumn: "LineupId");
                });

            migrationBuilder.UpdateData(
                table: "Settings",
                keyColumn: "SettingsId",
                keyValue: 1,
                column: "Name",
                value: "Import_Location");

            migrationBuilder.InsertData(
                table: "Stations",
                columns: new[] { "StationId", "LineupId", "StationLogo", "StationName", "StationNumber" },
                values: new object[,]
                {
                    { 1, null, "", "Test1", 1 },
                    { 2, null, "", "Test2", 2 },
                    { 3, null, "", "Test3", 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Stations_LineupId",
                table: "Stations",
                column: "LineupId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Stations");

            migrationBuilder.CreateTable(
                name: "Channels",
                columns: table => new
                {
                    ChannelId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    LineupId = table.Column<int>(type: "INTEGER", nullable: true),
                    ChannelName = table.Column<string>(type: "TEXT", nullable: false),
                    ChannelNumber = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Channels", x => x.ChannelId);
                    table.ForeignKey(
                        name: "FK_Channels_Lineups_LineupId",
                        column: x => x.LineupId,
                        principalTable: "Lineups",
                        principalColumn: "LineupId");
                });

            migrationBuilder.InsertData(
                table: "Channels",
                columns: new[] { "ChannelId", "ChannelName", "ChannelNumber", "LineupId" },
                values: new object[,]
                {
                    { 1, "Test1", 0, null },
                    { 2, "Test2", 0, null },
                    { 3, "Test3", 0, null }
                });

            migrationBuilder.UpdateData(
                table: "Settings",
                keyColumn: "SettingsId",
                keyValue: 1,
                column: "Name",
                value: "Import_Description");

            migrationBuilder.CreateIndex(
                name: "IX_Channels_LineupId",
                table: "Channels",
                column: "LineupId");
        }
    }
}
