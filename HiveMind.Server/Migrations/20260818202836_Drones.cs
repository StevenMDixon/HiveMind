using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HiveMind.Server.Migrations
{
    /// <inheritdoc />
    public partial class Drones : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stations_Lineups_LineupId",
                table: "Stations");

            migrationBuilder.DropColumn(
                name: "LineupId",
                table: "SchedulingResults");

            migrationBuilder.RenameColumn(
                name: "LineupId",
                table: "Stations",
                newName: "StrategyProgramStrategyId");

            migrationBuilder.RenameIndex(
                name: "IX_Stations_LineupId",
                table: "Stations",
                newName: "IX_Stations_StrategyProgramStrategyId");

            migrationBuilder.CreateTable(
                name: "Drones",
                columns: table => new
                {
                    DroneId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    HostName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drones", x => x.DroneId);
                });

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

            migrationBuilder.AddForeignKey(
                name: "FK_Stations_ProgramStrategies_StrategyProgramStrategyId",
                table: "Stations",
                column: "StrategyProgramStrategyId",
                principalTable: "ProgramStrategies",
                principalColumn: "ProgramStrategyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stations_ProgramStrategies_StrategyProgramStrategyId",
                table: "Stations");

            migrationBuilder.DropTable(
                name: "DroneStation");

            migrationBuilder.DropTable(
                name: "Drones");

            migrationBuilder.RenameColumn(
                name: "StrategyProgramStrategyId",
                table: "Stations",
                newName: "LineupId");

            migrationBuilder.RenameIndex(
                name: "IX_Stations_StrategyProgramStrategyId",
                table: "Stations",
                newName: "IX_Stations_LineupId");

            migrationBuilder.AddColumn<int>(
                name: "LineupId",
                table: "SchedulingResults",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Stations_Lineups_LineupId",
                table: "Stations",
                column: "LineupId",
                principalTable: "Lineups",
                principalColumn: "LineupId");
        }
    }
}
