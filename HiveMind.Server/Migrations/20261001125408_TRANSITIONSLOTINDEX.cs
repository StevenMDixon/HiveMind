using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HiveMind.Server.Migrations
{
    /// <inheritdoc />
    public partial class TRANSITIONSLOTINDEX : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Index",
                table: "TransitionTemplateSlots",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "TransitionTemplateSlots",
                keyColumn: "TransitionTemplateSlotId",
                keyValue: 1,
                column: "Index",
                value: 0);

            migrationBuilder.UpdateData(
                table: "TransitionTemplateSlots",
                keyColumn: "TransitionTemplateSlotId",
                keyValue: 2,
                column: "Index",
                value: 0);

            migrationBuilder.UpdateData(
                table: "TransitionTemplateSlots",
                keyColumn: "TransitionTemplateSlotId",
                keyValue: 3,
                column: "Index",
                value: 0);

            migrationBuilder.UpdateData(
                table: "TransitionTemplateSlots",
                keyColumn: "TransitionTemplateSlotId",
                keyValue: 4,
                column: "Index",
                value: 0);

            migrationBuilder.UpdateData(
                table: "TransitionTemplateSlots",
                keyColumn: "TransitionTemplateSlotId",
                keyValue: 5,
                column: "Index",
                value: 0);

            migrationBuilder.UpdateData(
                table: "TransitionTemplateSlots",
                keyColumn: "TransitionTemplateSlotId",
                keyValue: 6,
                column: "Index",
                value: 0);

            migrationBuilder.UpdateData(
                table: "TransitionTemplateSlots",
                keyColumn: "TransitionTemplateSlotId",
                keyValue: 7,
                column: "Index",
                value: 0);

            migrationBuilder.UpdateData(
                table: "TransitionTemplateSlots",
                keyColumn: "TransitionTemplateSlotId",
                keyValue: 8,
                column: "Index",
                value: 0);

            migrationBuilder.UpdateData(
                table: "TransitionTemplateSlots",
                keyColumn: "TransitionTemplateSlotId",
                keyValue: 9,
                column: "Index",
                value: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Index",
                table: "TransitionTemplateSlots");
        }
    }
}
