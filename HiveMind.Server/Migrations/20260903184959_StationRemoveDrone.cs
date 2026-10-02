using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HiveMind.Server.Migrations
{
    /// <inheritdoc />
    public partial class StationRemoveDrone : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                keyColumn: "StationId",
                keyValue: 1,
                column: "DroneId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Stations",
                keyColumn: "StationId",
                keyValue: 2,
                column: "DroneId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Stations",
                keyColumn: "StationId",
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
                principalColumn: "DroneId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                    DronesDroneId = table.Column<int>(type: "INTEGER", nullable: false),
                    StationsStationId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DroneStation", x => new { x.DronesDroneId, x.StationsStationId });
                    table.ForeignKey(
                        name: "FK_DroneStation_Drones_DronesDroneId",
                        column: x => x.DronesDroneId,
                        principalTable: "Drones",
                        principalColumn: "DroneId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DroneStation_Stations_StationsStationId",
                        column: x => x.StationsStationId,
                        principalTable: "Stations",
                        principalColumn: "StationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DroneStation_StationsStationId",
                table: "DroneStation",
                column: "StationsStationId");
        }
    }
}
