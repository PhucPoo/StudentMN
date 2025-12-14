using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentMN.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDBCty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 14, 15, 33, 32, 702, DateTimeKind.Local).AddTicks(7017));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 14, 15, 33, 32, 702, DateTimeKind.Local).AddTicks(7021));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 14, 15, 33, 32, 702, DateTimeKind.Local).AddTicks(7024));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 14, 15, 33, 32, 702, DateTimeKind.Local).AddTicks(7026));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 14, 15, 33, 32, 702, DateTimeKind.Local).AddTicks(7028));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 14, 15, 33, 32, 702, DateTimeKind.Local).AddTicks(7030));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 14, 15, 33, 32, 702, DateTimeKind.Local).AddTicks(7101));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 14, 15, 33, 32, 702, DateTimeKind.Local).AddTicks(7103));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 14, 15, 33, 32, 702, DateTimeKind.Local).AddTicks(7105));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 14, 15, 33, 32, 702, DateTimeKind.Local).AddTicks(7107));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 14, 15, 33, 32, 702, DateTimeKind.Local).AddTicks(7109));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 14, 15, 33, 32, 702, DateTimeKind.Local).AddTicks(7111));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 14, 15, 33, 32, 702, DateTimeKind.Local).AddTicks(7113));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 14, 15, 33, 32, 525, DateTimeKind.Local).AddTicks(538));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 14, 15, 33, 32, 525, DateTimeKind.Local).AddTicks(554));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 14, 15, 33, 32, 525, DateTimeKind.Local).AddTicks(556));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "Password" },
                values: new object[] { new DateTime(2025, 12, 14, 15, 33, 32, 702, DateTimeKind.Local).AddTicks(6344), "$2a$11$k1R6p4kBHPM5mP8xjlQbwemU3s6wRBOq9K/eXyu9/SDj/.fXKxTz." });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 10, 11, 29, 26, 412, DateTimeKind.Local).AddTicks(6684));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 10, 11, 29, 26, 412, DateTimeKind.Local).AddTicks(6686));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 10, 11, 29, 26, 412, DateTimeKind.Local).AddTicks(6687));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 10, 11, 29, 26, 412, DateTimeKind.Local).AddTicks(6689));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 10, 11, 29, 26, 412, DateTimeKind.Local).AddTicks(6690));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 10, 11, 29, 26, 412, DateTimeKind.Local).AddTicks(6691));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 10, 11, 29, 26, 412, DateTimeKind.Local).AddTicks(6731));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 10, 11, 29, 26, 412, DateTimeKind.Local).AddTicks(6732));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 10, 11, 29, 26, 412, DateTimeKind.Local).AddTicks(6734));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 10, 11, 29, 26, 412, DateTimeKind.Local).AddTicks(6735));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 10, 11, 29, 26, 412, DateTimeKind.Local).AddTicks(6736));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 10, 11, 29, 26, 412, DateTimeKind.Local).AddTicks(6738));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 10, 11, 29, 26, 412, DateTimeKind.Local).AddTicks(6739));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 10, 11, 29, 26, 228, DateTimeKind.Local).AddTicks(8367));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 10, 11, 29, 26, 228, DateTimeKind.Local).AddTicks(8369));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 10, 11, 29, 26, 228, DateTimeKind.Local).AddTicks(8370));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "Password" },
                values: new object[] { new DateTime(2025, 12, 10, 11, 29, 26, 412, DateTimeKind.Local).AddTicks(6380), "$2a$11$PHxMYswFkUk6IXe0h2r11eDbMSglcoV6ED6GHW4aJ1Eizvz217F6u" });
        }
    }
}
