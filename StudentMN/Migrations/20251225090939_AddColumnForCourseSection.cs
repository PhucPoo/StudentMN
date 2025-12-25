using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentMN.Migrations
{
    public partial class AddColumnForCourseSection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Thêm cột Group
            migrationBuilder.AddColumn<int>(
                name: "Group",
                table: "CourseSections",
                type: "int",
                nullable: false,
                defaultValue: 0);

            // Thêm cột Remaining
            migrationBuilder.AddColumn<int>(
                name: "Remaining",
                table: "CourseSections",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Nếu rollback thì drop cột
            migrationBuilder.DropColumn(
                name: "Group",
                table: "CourseSections");

            migrationBuilder.DropColumn(
                name: "Remaining",
                table: "CourseSections");
        }
    }
}
