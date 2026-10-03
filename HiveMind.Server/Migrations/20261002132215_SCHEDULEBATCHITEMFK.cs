using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HiveMind.Server.Migrations
{
    /// <inheritdoc />
    public partial class SCHEDULEBATCHITEMFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScheduleBatchItems_ScheduleBatches_ScheduleBatchId",
                table: "ScheduleBatchItems");

            migrationBuilder.AlterColumn<int>(
                name: "ScheduleBatchId",
                table: "ScheduleBatchItems",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Settings",
                columns: new[] { "SettingsId", "Name", "Value" },
                values: new object[] { 6, "Schedule Retention Days", "7" });

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduleBatchItems_ScheduleBatches_ScheduleBatchId",
                table: "ScheduleBatchItems",
                column: "ScheduleBatchId",
                principalTable: "ScheduleBatches",
                principalColumn: "ScheduleBatchId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScheduleBatchItems_ScheduleBatches_ScheduleBatchId",
                table: "ScheduleBatchItems");

            migrationBuilder.DeleteData(
                table: "Settings",
                keyColumn: "SettingsId",
                keyValue: 6);

            migrationBuilder.AlterColumn<int>(
                name: "ScheduleBatchId",
                table: "ScheduleBatchItems",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduleBatchItems_ScheduleBatches_ScheduleBatchId",
                table: "ScheduleBatchItems",
                column: "ScheduleBatchId",
                principalTable: "ScheduleBatches",
                principalColumn: "ScheduleBatchId");
        }
    }
}
