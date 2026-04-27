using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HiveMind.Server.Migrations
{
    /// <inheritdoc />
    public partial class MigrateToLineupsAgain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QueryScheduleItems");

            migrationBuilder.DropTable(
                name: "ScheduleItems");

            migrationBuilder.DropTable(
                name: "Schedules");

            migrationBuilder.AddColumn<int>(
                name: "LineupId",
                table: "Channels",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Lineups",
                columns: table => new
                {
                    LineupId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    LineupName = table.Column<string>(type: "TEXT", nullable: false),
                    StartTime = table.Column<TimeOnly>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lineups", x => x.LineupId);
                });

            migrationBuilder.CreateTable(
                name: "LineupItems",
                columns: table => new
                {
                    LineupItemId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Index = table.Column<int>(type: "INTEGER", nullable: false),
                    Type = table.Column<string>(type: "TEXT", nullable: false),
                    LineupId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LineupItems", x => x.LineupItemId);
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
                    QueryId = table.Column<int>(type: "INTEGER", nullable: false),
                    LineupItemId = table.Column<int>(type: "INTEGER", nullable: false),
                    PlayDuration = table.Column<int>(type: "INTEGER", nullable: false),
                    PlayCount = table.Column<int>(type: "INTEGER", nullable: false),
                    PadTo = table.Column<int>(type: "INTEGER", nullable: false),
                    QueryType = table.Column<int>(type: "INTEGER", nullable: false),
                    PlayoutType = table.Column<int>(type: "INTEGER", nullable: false),
                    Index = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QueryLineupItems", x => x.QueryLineupItemId);
                    table.ForeignKey(
                        name: "FK_QueryLineupItems_LineupItems_LineupItemId",
                        column: x => x.LineupItemId,
                        principalTable: "LineupItems",
                        principalColumn: "LineupItemId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Channels",
                keyColumn: "ChannelId",
                keyValue: 1,
                column: "LineupId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Channels",
                keyColumn: "ChannelId",
                keyValue: 2,
                column: "LineupId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Channels",
                keyColumn: "ChannelId",
                keyValue: 3,
                column: "LineupId",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_Channels_LineupId",
                table: "Channels",
                column: "LineupId");

            migrationBuilder.CreateIndex(
                name: "IX_LineupItems_LineupId",
                table: "LineupItems",
                column: "LineupId");

            migrationBuilder.CreateIndex(
                name: "IX_QueryLineupItems_LineupItemId",
                table: "QueryLineupItems",
                column: "LineupItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Channels_Lineups_LineupId",
                table: "Channels",
                column: "LineupId",
                principalTable: "Lineups",
                principalColumn: "LineupId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Channels_Lineups_LineupId",
                table: "Channels");

            migrationBuilder.DropTable(
                name: "QueryLineupItems");

            migrationBuilder.DropTable(
                name: "LineupItems");

            migrationBuilder.DropTable(
                name: "Lineups");

            migrationBuilder.DropIndex(
                name: "IX_Channels_LineupId",
                table: "Channels");

            migrationBuilder.DropColumn(
                name: "LineupId",
                table: "Channels");

            migrationBuilder.CreateTable(
                name: "Schedules",
                columns: table => new
                {
                    ScheduleId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ChannelId = table.Column<int>(type: "INTEGER", nullable: true),
                    ScheduleName = table.Column<string>(type: "TEXT", nullable: false),
                    StartTime = table.Column<TimeOnly>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedules", x => x.ScheduleId);
                    table.ForeignKey(
                        name: "FK_Schedules_Channels_ChannelId",
                        column: x => x.ChannelId,
                        principalTable: "Channels",
                        principalColumn: "ChannelId");
                });

            migrationBuilder.CreateTable(
                name: "ScheduleItems",
                columns: table => new
                {
                    ScheduleItemId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ScheduleId = table.Column<int>(type: "INTEGER", nullable: false),
                    Index = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Type = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduleItems", x => x.ScheduleItemId);
                    table.ForeignKey(
                        name: "FK_ScheduleItems_Schedules_ScheduleId",
                        column: x => x.ScheduleId,
                        principalTable: "Schedules",
                        principalColumn: "ScheduleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QueryScheduleItems",
                columns: table => new
                {
                    QueryScheduleItemId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Index = table.Column<int>(type: "INTEGER", nullable: false),
                    PadTo = table.Column<int>(type: "INTEGER", nullable: false),
                    PlayCount = table.Column<int>(type: "INTEGER", nullable: false),
                    PlayDuration = table.Column<int>(type: "INTEGER", nullable: false),
                    PlayoutType = table.Column<int>(type: "INTEGER", nullable: false),
                    QueryId = table.Column<int>(type: "INTEGER", nullable: false),
                    QueryType = table.Column<int>(type: "INTEGER", nullable: false),
                    ScheduleItemId = table.Column<int>(type: "INTEGER", nullable: false)
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

            migrationBuilder.InsertData(
                table: "Schedules",
                columns: new[] { "ScheduleId", "ChannelId", "ScheduleName", "StartTime" },
                values: new object[] { 1, 1, "Test Schedule", new TimeOnly(0, 0, 0) });

            migrationBuilder.CreateIndex(
                name: "IX_QueryScheduleItems_ScheduleItemId",
                table: "QueryScheduleItems",
                column: "ScheduleItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleItems_ScheduleId",
                table: "ScheduleItems",
                column: "ScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_ChannelId",
                table: "Schedules",
                column: "ChannelId",
                unique: true);
        }
    }
}
