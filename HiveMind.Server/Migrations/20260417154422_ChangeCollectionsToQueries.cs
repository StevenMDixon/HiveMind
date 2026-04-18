using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HiveMind.Server.Migrations
{
    /// <inheritdoc />
    public partial class ChangeCollectionsToQueries : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QueryFilters_Collections_CollectionId",
                table: "QueryFilters");

            migrationBuilder.DropTable(
                name: "Collections");

            migrationBuilder.DropTable(
                name: "CollectionScheduleItems");

            migrationBuilder.RenameColumn(
                name: "CollectionId",
                table: "QueryFilters",
                newName: "QueryId");

            migrationBuilder.RenameIndex(
                name: "IX_QueryFilters_CollectionId",
                table: "QueryFilters",
                newName: "IX_QueryFilters_QueryId");

            migrationBuilder.CreateTable(
                name: "Queries",
                columns: table => new
                {
                    QueryId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Queries", x => x.QueryId);
                });

            migrationBuilder.CreateTable(
                name: "QueryScheduleItems",
                columns: table => new
                {
                    QueryScheduleItemId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    QueryId = table.Column<int>(type: "INTEGER", nullable: false),
                    ScheduleItemId = table.Column<int>(type: "INTEGER", nullable: false),
                    PlayDuration = table.Column<int>(type: "INTEGER", nullable: false),
                    PlayCount = table.Column<int>(type: "INTEGER", nullable: false),
                    PadTo = table.Column<int>(type: "INTEGER", nullable: false),
                    QueryType = table.Column<int>(type: "INTEGER", nullable: false),
                    PlayoutType = table.Column<int>(type: "INTEGER", nullable: false),
                    Index = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QueryScheduleItems", x => x.QueryScheduleItemId);
                    table.ForeignKey(
                        name: "FK_QueryScheduleItems_ScheduleItems_ScheduleItemId",
                        column: x => x.ScheduleItemId,
                        principalTable: "ScheduleItems",
                        principalColumn: "ScheduleItemId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_QueryScheduleItems_ScheduleItemId",
                table: "QueryScheduleItems",
                column: "ScheduleItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_QueryFilters_Queries_QueryId",
                table: "QueryFilters",
                column: "QueryId",
                principalTable: "Queries",
                principalColumn: "QueryId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QueryFilters_Queries_QueryId",
                table: "QueryFilters");

            migrationBuilder.DropTable(
                name: "Queries");

            migrationBuilder.DropTable(
                name: "QueryScheduleItems");

            migrationBuilder.RenameColumn(
                name: "QueryId",
                table: "QueryFilters",
                newName: "CollectionId");

            migrationBuilder.RenameIndex(
                name: "IX_QueryFilters_QueryId",
                table: "QueryFilters",
                newName: "IX_QueryFilters_CollectionId");

            migrationBuilder.CreateTable(
                name: "Collections",
                columns: table => new
                {
                    CollectionId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Collections", x => x.CollectionId);
                });

            migrationBuilder.CreateTable(
                name: "CollectionScheduleItems",
                columns: table => new
                {
                    CollectionScheduleItemId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CollectionId = table.Column<int>(type: "INTEGER", nullable: false),
                    CollectionType = table.Column<int>(type: "INTEGER", nullable: false),
                    Index = table.Column<int>(type: "INTEGER", nullable: false),
                    PadTo = table.Column<int>(type: "INTEGER", nullable: false),
                    PlayCount = table.Column<int>(type: "INTEGER", nullable: false),
                    PlayDuration = table.Column<int>(type: "INTEGER", nullable: false),
                    PlayoutType = table.Column<int>(type: "INTEGER", nullable: false),
                    ScheduleItemId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollectionScheduleItems", x => x.CollectionScheduleItemId);
                    table.ForeignKey(
                        name: "FK_CollectionScheduleItems_ScheduleItems_ScheduleItemId",
                        column: x => x.ScheduleItemId,
                        principalTable: "ScheduleItems",
                        principalColumn: "ScheduleItemId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CollectionScheduleItems_ScheduleItemId",
                table: "CollectionScheduleItems",
                column: "ScheduleItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_QueryFilters_Collections_CollectionId",
                table: "QueryFilters",
                column: "CollectionId",
                principalTable: "Collections",
                principalColumn: "CollectionId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
