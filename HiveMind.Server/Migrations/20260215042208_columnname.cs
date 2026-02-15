using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HiveMind.Server.Migrations
{
    /// <inheritdoc />
    public partial class columnname : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Collections",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ChannelNumber",
                table: "Channels",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Channels",
                keyColumn: "ChannelId",
                keyValue: 1,
                column: "ChannelNumber",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Channels",
                keyColumn: "ChannelId",
                keyValue: 2,
                column: "ChannelNumber",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Channels",
                keyColumn: "ChannelId",
                keyValue: 3,
                column: "ChannelNumber",
                value: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Collections");

            migrationBuilder.DropColumn(
                name: "ChannelNumber",
                table: "Channels");
        }
    }
}
