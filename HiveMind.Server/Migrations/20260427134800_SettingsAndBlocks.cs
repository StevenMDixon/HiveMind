using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HiveMind.Server.Migrations
{
    /// <inheritdoc />
    public partial class SettingsAndBlocks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BlockId",
                table: "QueryLineupItems",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Group",
                table: "QueryLineupItems",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BlockId",
                table: "LineupItems",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BlockQueries",
                columns: table => new
                {
                    BlockQueryId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    QueryId = table.Column<int>(type: "INTEGER", nullable: false),
                    BlockId = table.Column<int>(type: "INTEGER", nullable: false),
                    PlayDuration = table.Column<int>(type: "INTEGER", nullable: false),
                    PlayCount = table.Column<int>(type: "INTEGER", nullable: false),
                    PadTo = table.Column<int>(type: "INTEGER", nullable: false),
                    QueryType = table.Column<int>(type: "INTEGER", nullable: false),
                    PlayoutType = table.Column<int>(type: "INTEGER", nullable: false),
                    Index = table.Column<int>(type: "INTEGER", nullable: false),
                    Group = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlockQueries", x => x.BlockQueryId);
                });

            migrationBuilder.CreateTable(
                name: "Blocks",
                columns: table => new
                {
                    BlockId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BlockName = table.Column<string>(type: "TEXT", nullable: false),
                    Logo = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blocks", x => x.BlockId);
                });

            migrationBuilder.CreateTable(
                name: "Settings",
                columns: table => new
                {
                    SettingsId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Value = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settings", x => x.SettingsId);
                });

            migrationBuilder.InsertData(
                table: "Settings",
                columns: new[] { "SettingsId", "Name", "Value" },
                values: new object[,]
                {
                    { 1, "Import_Description", "/" },
                    { 2, "Export_Location", "/" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_QueryLineupItems_BlockId",
                table: "QueryLineupItems",
                column: "BlockId");

            migrationBuilder.CreateIndex(
                name: "IX_LineupItems_BlockId",
                table: "LineupItems",
                column: "BlockId");

            migrationBuilder.AddForeignKey(
                name: "FK_LineupItems_Blocks_BlockId",
                table: "LineupItems",
                column: "BlockId",
                principalTable: "Blocks",
                principalColumn: "BlockId");

            migrationBuilder.AddForeignKey(
                name: "FK_QueryLineupItems_Blocks_BlockId",
                table: "QueryLineupItems",
                column: "BlockId",
                principalTable: "Blocks",
                principalColumn: "BlockId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LineupItems_Blocks_BlockId",
                table: "LineupItems");

            migrationBuilder.DropForeignKey(
                name: "FK_QueryLineupItems_Blocks_BlockId",
                table: "QueryLineupItems");

            migrationBuilder.DropTable(
                name: "BlockQueries");

            migrationBuilder.DropTable(
                name: "Blocks");

            migrationBuilder.DropTable(
                name: "Settings");

            migrationBuilder.DropIndex(
                name: "IX_QueryLineupItems_BlockId",
                table: "QueryLineupItems");

            migrationBuilder.DropIndex(
                name: "IX_LineupItems_BlockId",
                table: "LineupItems");

            migrationBuilder.DropColumn(
                name: "BlockId",
                table: "QueryLineupItems");

            migrationBuilder.DropColumn(
                name: "Group",
                table: "QueryLineupItems");

            migrationBuilder.DropColumn(
                name: "BlockId",
                table: "LineupItems");
        }
    }
}
