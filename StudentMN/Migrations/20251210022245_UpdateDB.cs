using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentMN.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "ClassId",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "Students",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "MajorId",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "Roles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "RolePermissions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "Permissions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "IsDelete" },
                values: new object[] { new DateTime(2025, 12, 10, 9, 22, 45, 320, DateTimeKind.Local).AddTicks(8095), false });

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "IsDelete" },
                values: new object[] { new DateTime(2025, 12, 10, 9, 22, 45, 320, DateTimeKind.Local).AddTicks(8097), false });

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "IsDelete" },
                values: new object[] { new DateTime(2025, 12, 10, 9, 22, 45, 320, DateTimeKind.Local).AddTicks(8099), false });

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "IsDelete" },
                values: new object[] { new DateTime(2025, 12, 10, 9, 22, 45, 320, DateTimeKind.Local).AddTicks(8100), false });

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "IsDelete" },
                values: new object[] { new DateTime(2025, 12, 10, 9, 22, 45, 320, DateTimeKind.Local).AddTicks(8101), false });

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "IsDelete" },
                values: new object[] { new DateTime(2025, 12, 10, 9, 22, 45, 320, DateTimeKind.Local).AddTicks(8102), false });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "IsDelete" },
                values: new object[] { new DateTime(2025, 12, 10, 9, 22, 45, 320, DateTimeKind.Local).AddTicks(8150), false });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "IsDelete" },
                values: new object[] { new DateTime(2025, 12, 10, 9, 22, 45, 320, DateTimeKind.Local).AddTicks(8151), false });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "IsDelete" },
                values: new object[] { new DateTime(2025, 12, 10, 9, 22, 45, 320, DateTimeKind.Local).AddTicks(8153), false });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "IsDelete" },
                values: new object[] { new DateTime(2025, 12, 10, 9, 22, 45, 320, DateTimeKind.Local).AddTicks(8154), false });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "IsDelete" },
                values: new object[] { new DateTime(2025, 12, 10, 9, 22, 45, 320, DateTimeKind.Local).AddTicks(8155), false });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "IsDelete" },
                values: new object[] { new DateTime(2025, 12, 10, 9, 22, 45, 320, DateTimeKind.Local).AddTicks(8156), false });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "IsDelete" },
                values: new object[] { new DateTime(2025, 12, 10, 9, 22, 45, 320, DateTimeKind.Local).AddTicks(8157), false });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "IsDelete" },
                values: new object[] { new DateTime(2025, 12, 10, 9, 22, 45, 151, DateTimeKind.Local).AddTicks(7861), false });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "IsDelete" },
                values: new object[] { new DateTime(2025, 12, 10, 9, 22, 45, 151, DateTimeKind.Local).AddTicks(7863), false });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "IsDelete" },
                values: new object[] { new DateTime(2025, 12, 10, 9, 22, 45, 151, DateTimeKind.Local).AddTicks(7864), false });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "IsDelete", "Password" },
                values: new object[] { new DateTime(2025, 12, 10, 9, 22, 45, 320, DateTimeKind.Local).AddTicks(7621), false, "$2a$11$EA18iDywnA3LA0g/mQVC5uypvSuwWhetaniEl9RqLjA6zq4ForHIu" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ClassId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "MajorId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "RolePermissions");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "Permissions");

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 8, 13, 25, 6, 816, DateTimeKind.Local).AddTicks(4301));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 8, 13, 25, 6, 816, DateTimeKind.Local).AddTicks(4303));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 8, 13, 25, 6, 816, DateTimeKind.Local).AddTicks(4304));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 8, 13, 25, 6, 816, DateTimeKind.Local).AddTicks(4305));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 8, 13, 25, 6, 816, DateTimeKind.Local).AddTicks(4307));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 8, 13, 25, 6, 816, DateTimeKind.Local).AddTicks(4308));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 8, 13, 25, 6, 816, DateTimeKind.Local).AddTicks(4340));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 8, 13, 25, 6, 816, DateTimeKind.Local).AddTicks(4342));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 8, 13, 25, 6, 816, DateTimeKind.Local).AddTicks(4343));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 8, 13, 25, 6, 816, DateTimeKind.Local).AddTicks(4344));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 8, 13, 25, 6, 816, DateTimeKind.Local).AddTicks(4346));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 8, 13, 25, 6, 816, DateTimeKind.Local).AddTicks(4347));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 8, 13, 25, 6, 816, DateTimeKind.Local).AddTicks(4348));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 8, 13, 25, 6, 648, DateTimeKind.Local).AddTicks(3574));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 8, 13, 25, 6, 648, DateTimeKind.Local).AddTicks(3576));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 8, 13, 25, 6, 648, DateTimeKind.Local).AddTicks(3577));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "Password" },
                values: new object[] { new DateTime(2025, 12, 8, 13, 25, 6, 816, DateTimeKind.Local).AddTicks(4113), "$2a$11$at6MM8aMNWyGrm8WOV7RBunhYReyt1p8vgD1JSKZwUsZ.PShhmclm" });
        }
    }
}
