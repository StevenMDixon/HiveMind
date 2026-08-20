using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HiveMind.Server.Migrations
{
    /// <inheritdoc />
    public partial class GroupsAndSettings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Group",
                table: "MediaItems",
                type: "TEXT",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Settings",
                columns: new[] { "SettingsId", "Name", "Value" },
                values: new object[,]
                {
                    { 3, "Bump In Tag", "In" },
                    { 4, "Bump Out Tag", "Out" },
                    { 5, "Bump Generic Tag", "Generic" }
                });

            migrationBuilder.UpdateData(
                table: "TransitionTemplateSlots",
                keyColumn: "TransitionTemplateSlotId",
                keyValue: 1,
                column: "Slot",
                value: 1);

            migrationBuilder.UpdateData(
                table: "TransitionTemplateSlots",
                keyColumn: "TransitionTemplateSlotId",
                keyValue: 2,
                column: "Slot",
                value: 2);

            migrationBuilder.UpdateData(
                table: "TransitionTemplateSlots",
                keyColumn: "TransitionTemplateSlotId",
                keyValue: 3,
                column: "Slot",
                value: 4);

            migrationBuilder.UpdateData(
                table: "TransitionTemplateSlots",
                keyColumn: "TransitionTemplateSlotId",
                keyValue: 4,
                column: "Slot",
                value: 2);

            migrationBuilder.UpdateData(
                table: "TransitionTemplateSlots",
                keyColumn: "TransitionTemplateSlotId",
                keyValue: 6,
                column: "Slot",
                value: 2);

            migrationBuilder.UpdateData(
                table: "TransitionTemplateSlots",
                keyColumn: "TransitionTemplateSlotId",
                keyValue: 7,
                column: "Slot",
                value: 5);

            migrationBuilder.InsertData(
                table: "TransitionTemplateSlots",
                columns: new[] { "TransitionTemplateSlotId", "Slot", "TransitionTemplateId" },
                values: new object[,]
                {
                    { 8, 4, 2 },
                    { 9, 2, 2 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Settings",
                keyColumn: "SettingsId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Settings",
                keyColumn: "SettingsId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Settings",
                keyColumn: "SettingsId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "TransitionTemplateSlots",
                keyColumn: "TransitionTemplateSlotId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "TransitionTemplateSlots",
                keyColumn: "TransitionTemplateSlotId",
                keyValue: 9);

            migrationBuilder.DropColumn(
                name: "Group",
                table: "MediaItems");

            migrationBuilder.UpdateData(
                table: "TransitionTemplateSlots",
                keyColumn: "TransitionTemplateSlotId",
                keyValue: 1,
                column: "Slot",
                value: 0);

            migrationBuilder.UpdateData(
                table: "TransitionTemplateSlots",
                keyColumn: "TransitionTemplateSlotId",
                keyValue: 2,
                column: "Slot",
                value: 1);

            migrationBuilder.UpdateData(
                table: "TransitionTemplateSlots",
                keyColumn: "TransitionTemplateSlotId",
                keyValue: 3,
                column: "Slot",
                value: 3);

            migrationBuilder.UpdateData(
                table: "TransitionTemplateSlots",
                keyColumn: "TransitionTemplateSlotId",
                keyValue: 4,
                column: "Slot",
                value: 1);

            migrationBuilder.UpdateData(
                table: "TransitionTemplateSlots",
                keyColumn: "TransitionTemplateSlotId",
                keyValue: 6,
                column: "Slot",
                value: 1);

            migrationBuilder.UpdateData(
                table: "TransitionTemplateSlots",
                keyColumn: "TransitionTemplateSlotId",
                keyValue: 7,
                column: "Slot",
                value: 3);
        }
    }
}
