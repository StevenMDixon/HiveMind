using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HiveMind.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddChannelDummy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Channels",
                columns: new[] { "ChannelID", "ChannelName" },
                values: new object[,]
                {
                    { 1, "Test1" },
                    { 2, "Test2" },
                    { 3, "Test3" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Channels",
                keyColumn: "ChannelID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Channels",
                keyColumn: "ChannelID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Channels",
                keyColumn: "ChannelID",
                keyValue: 3);
        }
    }
}
