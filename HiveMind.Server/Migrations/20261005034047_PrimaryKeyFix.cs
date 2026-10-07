using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HiveMind.Server.Migrations
{
    /// <inheritdoc />
    public partial class PrimaryKeyFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MediaItemTags_Tags_TagsTagId",
                table: "MediaItemTags");

            migrationBuilder.DropForeignKey(
                name: "FK_Stations_ProgramStrategies_StrategyProgramStrategyId",
                table: "Stations");

            migrationBuilder.RenameColumn(
                name: "TransitionTemplateSlotId",
                table: "TransitionTemplateSlots",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "TransitionTemplateId",
                table: "TransitionTemplates",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "TagName",
                table: "Tags",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "TagId",
                table: "Tags",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Tags_TagName",
                table: "Tags",
                newName: "IX_Tags_Name");

            migrationBuilder.RenameColumn(
                name: "StrategyProgramStrategyId",
                table: "Stations",
                newName: "StrategyId");

            migrationBuilder.RenameColumn(
                name: "StationNumber",
                table: "Stations",
                newName: "Number");

            migrationBuilder.RenameColumn(
                name: "StationName",
                table: "Stations",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "StationLogo",
                table: "Stations",
                newName: "Logo");

            migrationBuilder.RenameColumn(
                name: "StationId",
                table: "Stations",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Stations_StrategyProgramStrategyId",
                table: "Stations",
                newName: "IX_Stations_StrategyId");

            migrationBuilder.RenameColumn(
                name: "ShowTitle",
                table: "Shows",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "ShowId",
                table: "Shows",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "SettingsId",
                table: "Settings",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "SchedulingResultId",
                table: "SchedulingResults",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ProgramStrategyLineUpId",
                table: "ScheduleBatchItems",
                newName: "ProgramStrategyLineupId");

            migrationBuilder.RenameColumn(
                name: "ScheduleBatchItemId",
                table: "ScheduleBatchItems",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ScheduleBatchId",
                table: "ScheduleBatches",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "QueryFilterId",
                table: "QueryFilters",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "QueryType",
                table: "Queries",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "QueryId",
                table: "Queries",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ProgramStrategyLineupId",
                table: "ProgramStrategyLineups",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ProgramStrategyId",
                table: "ProgramStrategies",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "EventId",
                table: "ProgramEvents",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "TagsTagId",
                table: "MediaItemTags",
                newName: "TagsId");

            migrationBuilder.RenameIndex(
                name: "IX_MediaItemTags_TagsTagId",
                table: "MediaItemTags",
                newName: "IX_MediaItemTags_TagsId");

            migrationBuilder.RenameColumn(
                name: "MediaItemId",
                table: "MediaItems",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "LineupName",
                table: "Lineups",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "LineupId",
                table: "Lineups",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "LibraryType",
                table: "Libraries",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "LibraryPath",
                table: "Libraries",
                newName: "Path");

            migrationBuilder.RenameColumn(
                name: "LibraryName",
                table: "Libraries",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "LibraryId",
                table: "Libraries",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "DroneId",
                table: "Drones",
                newName: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MediaItemTags_Tags_TagsId",
                table: "MediaItemTags",
                column: "TagsId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Stations_ProgramStrategies_StrategyId",
                table: "Stations",
                column: "StrategyId",
                principalTable: "ProgramStrategies",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MediaItemTags_Tags_TagsId",
                table: "MediaItemTags");

            migrationBuilder.DropForeignKey(
                name: "FK_Stations_ProgramStrategies_StrategyId",
                table: "Stations");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "TransitionTemplateSlots",
                newName: "TransitionTemplateSlotId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "TransitionTemplates",
                newName: "TransitionTemplateId");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Tags",
                newName: "TagName");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Tags",
                newName: "TagId");

            migrationBuilder.RenameIndex(
                name: "IX_Tags_Name",
                table: "Tags",
                newName: "IX_Tags_TagName");

            migrationBuilder.RenameColumn(
                name: "StrategyId",
                table: "Stations",
                newName: "StrategyProgramStrategyId");

            migrationBuilder.RenameColumn(
                name: "Number",
                table: "Stations",
                newName: "StationNumber");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Stations",
                newName: "StationName");

            migrationBuilder.RenameColumn(
                name: "Logo",
                table: "Stations",
                newName: "StationLogo");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Stations",
                newName: "StationId");

            migrationBuilder.RenameIndex(
                name: "IX_Stations_StrategyId",
                table: "Stations",
                newName: "IX_Stations_StrategyProgramStrategyId");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Shows",
                newName: "ShowTitle");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Shows",
                newName: "ShowId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Settings",
                newName: "SettingsId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "SchedulingResults",
                newName: "SchedulingResultId");

            migrationBuilder.RenameColumn(
                name: "ProgramStrategyLineupId",
                table: "ScheduleBatchItems",
                newName: "ProgramStrategyLineUpId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "ScheduleBatchItems",
                newName: "ScheduleBatchItemId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "ScheduleBatches",
                newName: "ScheduleBatchId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "QueryFilters",
                newName: "QueryFilterId");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Queries",
                newName: "QueryType");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Queries",
                newName: "QueryId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "ProgramStrategyLineups",
                newName: "ProgramStrategyLineupId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "ProgramStrategies",
                newName: "ProgramStrategyId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "ProgramEvents",
                newName: "EventId");

            migrationBuilder.RenameColumn(
                name: "TagsId",
                table: "MediaItemTags",
                newName: "TagsTagId");

            migrationBuilder.RenameIndex(
                name: "IX_MediaItemTags_TagsId",
                table: "MediaItemTags",
                newName: "IX_MediaItemTags_TagsTagId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "MediaItems",
                newName: "MediaItemId");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Lineups",
                newName: "LineupName");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Lineups",
                newName: "LineupId");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Libraries",
                newName: "LibraryType");

            migrationBuilder.RenameColumn(
                name: "Path",
                table: "Libraries",
                newName: "LibraryPath");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Libraries",
                newName: "LibraryName");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Libraries",
                newName: "LibraryId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Drones",
                newName: "DroneId");

            migrationBuilder.AddForeignKey(
                name: "FK_MediaItemTags_Tags_TagsTagId",
                table: "MediaItemTags",
                column: "TagsTagId",
                principalTable: "Tags",
                principalColumn: "TagId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Stations_ProgramStrategies_StrategyProgramStrategyId",
                table: "Stations",
                column: "StrategyProgramStrategyId",
                principalTable: "ProgramStrategies",
                principalColumn: "ProgramStrategyId");
        }
    }
}
