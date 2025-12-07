using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentMN.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMigrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 5, 7, 42, 45, 220, DateTimeKind.Local).AddTicks(5619));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 5, 7, 42, 45, 220, DateTimeKind.Local).AddTicks(5621));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 5, 7, 42, 45, 220, DateTimeKind.Local).AddTicks(5623));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 5, 7, 42, 45, 220, DateTimeKind.Local).AddTicks(5625));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 5, 7, 42, 45, 220, DateTimeKind.Local).AddTicks(5626));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 5, 7, 42, 45, 220, DateTimeKind.Local).AddTicks(5628));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 5, 7, 42, 45, 220, DateTimeKind.Local).AddTicks(5689));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 5, 7, 42, 45, 220, DateTimeKind.Local).AddTicks(5691));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 5, 7, 42, 45, 220, DateTimeKind.Local).AddTicks(5693));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 5, 7, 42, 45, 220, DateTimeKind.Local).AddTicks(5694));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 5, 7, 42, 45, 220, DateTimeKind.Local).AddTicks(5696));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 5, 7, 42, 45, 220, DateTimeKind.Local).AddTicks(5697));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 5, 7, 42, 45, 220, DateTimeKind.Local).AddTicks(5699));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 5, 7, 42, 45, 67, DateTimeKind.Local).AddTicks(9331));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 5, 7, 42, 45, 67, DateTimeKind.Local).AddTicks(9354));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 5, 7, 42, 45, 67, DateTimeKind.Local).AddTicks(9357));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "Password" },
                values: new object[] { new DateTime(2025, 12, 5, 7, 42, 45, 220, DateTimeKind.Local).AddTicks(5028), "$2a$11$tmqbpQbK1ttJOVKMXkq/JOH6q7pCGgXV48xW7UNwtT9OVCba3ojVa" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 3, 13, 26, 31, 908, DateTimeKind.Local).AddTicks(9672));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 3, 13, 26, 31, 908, DateTimeKind.Local).AddTicks(9674));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 3, 13, 26, 31, 908, DateTimeKind.Local).AddTicks(9676));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 3, 13, 26, 31, 908, DateTimeKind.Local).AddTicks(9677));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 3, 13, 26, 31, 908, DateTimeKind.Local).AddTicks(9678));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 3, 13, 26, 31, 908, DateTimeKind.Local).AddTicks(9680));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 3, 13, 26, 31, 908, DateTimeKind.Local).AddTicks(9799));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 3, 13, 26, 31, 908, DateTimeKind.Local).AddTicks(9801));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 3, 13, 26, 31, 908, DateTimeKind.Local).AddTicks(9802));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 3, 13, 26, 31, 908, DateTimeKind.Local).AddTicks(9803));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 3, 13, 26, 31, 908, DateTimeKind.Local).AddTicks(9804));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 3, 13, 26, 31, 908, DateTimeKind.Local).AddTicks(9805));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 3, 13, 26, 31, 908, DateTimeKind.Local).AddTicks(9807));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 3, 13, 26, 31, 734, DateTimeKind.Local).AddTicks(7609));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 3, 13, 26, 31, 734, DateTimeKind.Local).AddTicks(7611));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 3, 13, 26, 31, 734, DateTimeKind.Local).AddTicks(7613));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "Password" },
                values: new object[] { new DateTime(2025, 12, 3, 13, 26, 31, 908, DateTimeKind.Local).AddTicks(9231), "$2a$11$LMy.0Yi6dpINyCj/iPKl3.BF5yNIB4xdEz7893/jH.xL7/slWYfG6" });
        }
    }
}
