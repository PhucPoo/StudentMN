using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentMN.Migrations
{
    /// <inheritdoc />
    public partial class FixEnrollmentId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
               name: "NewId",
               table: "Enrollments",
               type: "int",
               nullable: false,
               defaultValue: 0)
               .Annotation("SqlServer:Identity", "1, 1");
            migrationBuilder.DropPrimaryKey(
              name: "PK_Enrollments",
              table: "Enrollments");
            migrationBuilder.DropColumn(
               name: "Id",
               table: "Enrollments");
            migrationBuilder.RenameColumn(
              name: "NewId",
              table: "Enrollments",
              newName: "Id");

            // 6. Add PK mới
            migrationBuilder.AddPrimaryKey(
                name: "PK_Enrollments",
                table: "Enrollments",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Revert lại
            migrationBuilder.DropPrimaryKey(
                name: "PK_Enrollments",
                table: "Enrollments");

            migrationBuilder.AddColumn<int>(
                name: "OldId",
                table: "Enrollments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Enrollments");

            migrationBuilder.RenameColumn(
                name: "OldId",
                table: "Enrollments",
                newName: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Enrollments",
                table: "Enrollments",
                column: "Id");
        }
    }
}
