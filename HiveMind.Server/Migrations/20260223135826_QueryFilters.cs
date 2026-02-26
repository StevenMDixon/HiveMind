using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HiveMind.Server.Migrations
{
    /// <inheritdoc />
    public partial class QueryFilters : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Query",
                table: "Collections");

            migrationBuilder.CreateTable(
                name: "Query",
                columns: table => new
                {
                    QueryId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CollectionId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Query", x => x.QueryId);
                    table.ForeignKey(
                        name: "FK_Query_Collections_CollectionId",
                        column: x => x.CollectionId,
                        principalTable: "Collections",
                        principalColumn: "CollectionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QueryFilters",
                columns: table => new
                {
                    QueryFilterId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Field = table.Column<string>(type: "TEXT", nullable: false),
                    Operator = table.Column<string>(type: "TEXT", nullable: false),
                    Value = table.Column<string>(type: "TEXT", nullable: false),
                    QueryId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QueryFilters", x => x.QueryFilterId);
                    table.ForeignKey(
                        name: "FK_QueryFilters_Query_QueryId",
                        column: x => x.QueryId,
                        principalTable: "Query",
                        principalColumn: "QueryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Query_CollectionId",
                table: "Query",
                column: "CollectionId");

            migrationBuilder.CreateIndex(
                name: "IX_QueryFilters_QueryId",
                table: "QueryFilters",
                column: "QueryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QueryFilters");

            migrationBuilder.DropTable(
                name: "Query");

            migrationBuilder.AddColumn<string>(
                name: "Query",
                table: "Collections",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
