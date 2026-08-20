using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HiveMind.Server.Migrations
{
    /// <inheritdoc />
    public partial class MoveToNodeStyle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlockQueries");

            migrationBuilder.DropTable(
                name: "QueryLineupItems");

            migrationBuilder.DropTable(
                name: "LineupItems");

            migrationBuilder.DropTable(
                name: "Blocks");

            migrationBuilder.AlterColumn<int>(
                name: "Duration",
                table: "MediaItems",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "REAL");

            migrationBuilder.AddColumn<string>(
                name: "JsonData",
                table: "Lineups",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "NodeDefinitions",
                columns: table => new
                {
                    NodeDefinitionsId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Type = table.Column<int>(type: "INTEGER", nullable: false),
                    Json = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NodeDefinitions", x => x.NodeDefinitionsId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NodeDefinitions");

            migrationBuilder.DropColumn(
                name: "JsonData",
                table: "Lineups");

            migrationBuilder.AlterColumn<double>(
                name: "Duration",
                table: "MediaItems",
                type: "REAL",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.CreateTable(
                name: "BlockQueries",
                columns: table => new
                {
                    BlockQueryId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BlockId = table.Column<int>(type: "INTEGER", nullable: false),
                    Group = table.Column<int>(type: "INTEGER", nullable: false),
                    Index = table.Column<int>(type: "INTEGER", nullable: false),
                    PadTo = table.Column<int>(type: "INTEGER", nullable: false),
                    PlayCount = table.Column<int>(type: "INTEGER", nullable: false),
                    PlayDuration = table.Column<int>(type: "INTEGER", nullable: false),
                    PlayoutType = table.Column<int>(type: "INTEGER", nullable: false),
                    QueryId = table.Column<int>(type: "INTEGER", nullable: false),
                    QueryType = table.Column<int>(type: "INTEGER", nullable: false)
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
                name: "LineupItems",
                columns: table => new
                {
                    LineupItemId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BlockId = table.Column<int>(type: "INTEGER", nullable: true),
                    LineupId = table.Column<int>(type: "INTEGER", nullable: false),
                    Index = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Type = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LineupItems", x => x.LineupItemId);
                    table.ForeignKey(
                        name: "FK_LineupItems_Blocks_BlockId",
                        column: x => x.BlockId,
                        principalTable: "Blocks",
                        principalColumn: "BlockId");
                    table.ForeignKey(
                        name: "FK_LineupItems_Lineups_LineupId",
                        column: x => x.LineupId,
                        principalTable: "Lineups",
                        principalColumn: "LineupId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QueryLineupItems",
                columns: table => new
                {
                    QueryLineupItemId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BlockId = table.Column<int>(type: "INTEGER", nullable: true),
                    Group = table.Column<int>(type: "INTEGER", nullable: false),
                    Index = table.Column<int>(type: "INTEGER", nullable: false),
                    LineupItemId = table.Column<int>(type: "INTEGER", nullable: false),
                    PadTo = table.Column<int>(type: "INTEGER", nullable: false),
                    PlayCount = table.Column<int>(type: "INTEGER", nullable: false),
                    PlayDuration = table.Column<int>(type: "INTEGER", nullable: false),
                    PlayoutType = table.Column<int>(type: "INTEGER", nullable: false),
                    QueryId = table.Column<int>(type: "INTEGER", nullable: false),
                    QueryType = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QueryLineupItems", x => x.QueryLineupItemId);
                    table.ForeignKey(
                        name: "FK_QueryLineupItems_Blocks_BlockId",
                        column: x => x.BlockId,
                        principalTable: "Blocks",
                        principalColumn: "BlockId");
                    table.ForeignKey(
                        name: "FK_QueryLineupItems_LineupItems_LineupItemId",
                        column: x => x.LineupItemId,
                        principalTable: "LineupItems",
                        principalColumn: "LineupItemId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LineupItems_BlockId",
                table: "LineupItems",
                column: "BlockId");

            migrationBuilder.CreateIndex(
                name: "IX_LineupItems_LineupId",
                table: "LineupItems",
                column: "LineupId");

            migrationBuilder.CreateIndex(
                name: "IX_QueryLineupItems_BlockId",
                table: "QueryLineupItems",
                column: "BlockId");

            migrationBuilder.CreateIndex(
                name: "IX_QueryLineupItems_LineupItemId",
                table: "QueryLineupItems",
                column: "LineupItemId");
        }
    }
}
