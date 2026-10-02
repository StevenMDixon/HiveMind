using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HiveMind.Server.Migrations
{
    /// <inheritdoc />
    public partial class TRANSITIONSLOTINDEX2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "TransitionTemplateSlots",
                keyColumn: "TransitionTemplateSlotId",
                keyValue: 2,
                column: "Index",
                value: 1);

            migrationBuilder.UpdateData(
                table: "TransitionTemplateSlots",
                keyColumn: "TransitionTemplateSlotId",
                keyValue: 3,
                column: "Index",
                value: 2);

            migrationBuilder.UpdateData(
                table: "TransitionTemplateSlots",
                keyColumn: "TransitionTemplateSlotId",
                keyValue: 4,
                column: "Index",
                value: 3);

            migrationBuilder.UpdateData(
                table: "TransitionTemplateSlots",
                keyColumn: "TransitionTemplateSlotId",
                keyValue: 5,
                column: "Index",
                value: 4);

            migrationBuilder.UpdateData(
                table: "TransitionTemplateSlots",
                keyColumn: "TransitionTemplateSlotId",
                keyValue: 7,
                column: "Index",
                value: 1);

            migrationBuilder.UpdateData(
                table: "TransitionTemplateSlots",
                keyColumn: "TransitionTemplateSlotId",
                keyValue: 8,
                column: "Index",
                value: 2);

            migrationBuilder.UpdateData(
                table: "TransitionTemplateSlots",
                keyColumn: "TransitionTemplateSlotId",
                keyValue: 9,
                column: "Index",
                value: 3);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}
