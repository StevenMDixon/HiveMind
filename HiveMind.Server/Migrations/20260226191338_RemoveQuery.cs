using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HiveMind.Server.Migrations
{
    /// <inheritdoc />
    public partial class RemoveQuery : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QueryFilters_Query_QueryId",
                table: "QueryFilters");

            migrationBuilder.DropTable(
                name: "Query");

            migrationBuilder.RenameColumn(
                name: "QueryId",
                table: "QueryFilters",
                newName: "CollectionId");

            migrationBuilder.RenameIndex(
                name: "IX_QueryFilters_QueryId",
                table: "QueryFilters",
                newName: "IX_QueryFilters_CollectionId");

            migrationBuilder.AddForeignKey(
                name: "FK_QueryFilters_Collections_CollectionId",
                table: "QueryFilters",
                column: "CollectionId",
                principalTable: "Collections",
                principalColumn: "CollectionId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QueryFilters_Collections_CollectionId",
                table: "QueryFilters");

            migrationBuilder.RenameColumn(
                name: "CollectionId",
                table: "QueryFilters",
                newName: "QueryId");

            migrationBuilder.RenameIndex(
                name: "IX_QueryFilters_CollectionId",
                table: "QueryFilters",
                newName: "IX_QueryFilters_QueryId");

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

            migrationBuilder.CreateIndex(
                name: "IX_Query_CollectionId",
                table: "Query",
                column: "CollectionId");

            migrationBuilder.AddForeignKey(
                name: "FK_QueryFilters_Query_QueryId",
                table: "QueryFilters",
                column: "QueryId",
                principalTable: "Query",
                principalColumn: "QueryId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
