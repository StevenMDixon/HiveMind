using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HiveMind.Server.Migrations
{
    /// <inheritdoc />
    public partial class DroneManyStations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stations_Drones_DroneId",
                table: "Stations");

            migrationBuilder.DropIndex(
                name: "IX_Stations_DroneId",
                table: "Stations");

            migrationBuilder.DropColumn(
                name: "DroneId",
                table: "Stations");

            migrationBuilder.CreateTable(
                name: "DroneStation",
                columns: table => new
                {
                    DronesId = table.Column<int>(type: "INTEGER", nullable: false),
                    StationsId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DroneStation", x => new { x.DronesId, x.StationsId });
                    table.ForeignKey(
                        name: "FK_DroneStation_Drones_DronesId",
                        column: x => x.DronesId,
                        principalTable: "Drones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DroneStation_Stations_StationsId",
                        column: x => x.StationsId,
                        principalTable: "Stations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DroneStation_StationsId",
                table: "DroneStation",
                column: "StationsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DroneStation");

            migrationBuilder.AddColumn<int>(
                name: "DroneId",
                table: "Stations",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Stations",
                keyColumn: "Id",
                keyValue: 1,
                column: "DroneId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Stations",
                keyColumn: "Id",
                keyValue: 2,
                column: "DroneId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Stations",
                keyColumn: "Id",
                keyValue: 3,
                column: "DroneId",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_Stations_DroneId",
                table: "Stations",
                column: "DroneId");

            migrationBuilder.AddForeignKey(
                name: "FK_Stations_Drones_DroneId",
                table: "Stations",
                column: "DroneId",
                principalTable: "Drones",
                principalColumn: "Id");
        }
    }
}
