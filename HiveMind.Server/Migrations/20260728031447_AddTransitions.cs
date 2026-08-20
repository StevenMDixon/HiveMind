using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HiveMind.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddTransitions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NodeDefinitions");

            migrationBuilder.CreateTable(
                name: "TransitionTemplates",
                columns: table => new
                {
                    TransitionTemplateId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    MatchShowBumps = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransitionTemplates", x => x.TransitionTemplateId);
                });

            migrationBuilder.CreateTable(
                name: "TransitionTemplateSlots",
                columns: table => new
                {
                    TransitionTemplateSlotId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Slot = table.Column<int>(type: "INTEGER", nullable: false),
                    TransitionTemplateId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransitionTemplateSlots", x => x.TransitionTemplateSlotId);
                    table.ForeignKey(
                        name: "FK_TransitionTemplateSlots_TransitionTemplates_TransitionTemplateId",
                        column: x => x.TransitionTemplateId,
                        principalTable: "TransitionTemplates",
                        principalColumn: "TransitionTemplateId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "TransitionTemplates",
                columns: new[] { "TransitionTemplateId", "MatchShowBumps", "Name" },
                values: new object[,]
                {
                    { 1, true, "Default MidRoll" },
                    { 2, true, "Default PostRoll" }
                });

            migrationBuilder.InsertData(
                table: "TransitionTemplateSlots",
                columns: new[] { "TransitionTemplateSlotId", "Slot", "TransitionTemplateId" },
                values: new object[,]
                {
                    { 1, 0, 1 },
                    { 2, 1, 1 },
                    { 3, 3, 1 },
                    { 4, 1, 1 },
                    { 5, 0, 1 },
                    { 6, 1, 2 },
                    { 7, 3, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_TransitionTemplateSlots_TransitionTemplateId",
                table: "TransitionTemplateSlots",
                column: "TransitionTemplateId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TransitionTemplateSlots");

            migrationBuilder.DropTable(
                name: "TransitionTemplates");

            migrationBuilder.CreateTable(
                name: "NodeDefinitions",
                columns: table => new
                {
                    NodeDefinitionsId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Json = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Type = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NodeDefinitions", x => x.NodeDefinitionsId);
                });
        }
    }
}
