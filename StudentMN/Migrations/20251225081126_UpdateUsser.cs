using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentMN.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUsser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 25, 15, 11, 26, 19, DateTimeKind.Local).AddTicks(1676));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 25, 15, 11, 26, 19, DateTimeKind.Local).AddTicks(1678));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 25, 15, 11, 26, 19, DateTimeKind.Local).AddTicks(1679));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "Password" },
                values: new object[] { new DateTime(2025, 12, 25, 15, 11, 26, 134, DateTimeKind.Local).AddTicks(840), "$2a$11$KgcZgB.Rr4ALxPn88Y3dY.Paj.Ezn.Ki9fmPNGiG7hfFb2226hn6S" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 25, 14, 57, 19, 525, DateTimeKind.Local).AddTicks(8445));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 25, 14, 57, 19, 525, DateTimeKind.Local).AddTicks(8447));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 25, 14, 57, 19, 525, DateTimeKind.Local).AddTicks(8449));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "Password" },
                values: new object[] { new DateTime(2025, 12, 25, 14, 57, 19, 696, DateTimeKind.Local).AddTicks(1374), "$2a$11$JduJnEGBmWPBhxdDYZ284OVNzwIYSDdS/UApg8bFkFtRL32U3AxQW" });
        }
    }
}
