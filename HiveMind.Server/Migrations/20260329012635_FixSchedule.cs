using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HiveMind.Server.Migrations
{
    /// <inheritdoc />
    public partial class FixSchedule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_Channels_ChannelId",
                table: "Schedules");

            migrationBuilder.AlterColumn<int>(
                name: "ChannelId",
                table: "Schedules",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_Channels_ChannelId",
                table: "Schedules",
                column: "ChannelId",
                principalTable: "Channels",
                principalColumn: "ChannelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_Channels_ChannelId",
                table: "Schedules");

            migrationBuilder.AlterColumn<int>(
                name: "ChannelId",
                table: "Schedules",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_Channels_ChannelId",
                table: "Schedules",
                column: "ChannelId",
                principalTable: "Channels",
                principalColumn: "ChannelId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
