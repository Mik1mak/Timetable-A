using Microsoft.EntityFrameworkCore.Migrations;

namespace TimetableA.Entities.Migrations
{
    public partial class DisplayEmptyDays : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StartHour",
                table: "Timetables");

            migrationBuilder.DropColumn(
                name: "StopHour",
                table: "Timetables");

            migrationBuilder.RenameColumn(
                name: "ShowWeekend",
                table: "Timetables",
                newName: "DisplayEmptyDays");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DisplayEmptyDays",
                table: "Timetables",
                newName: "ShowWeekend");

            migrationBuilder.AddColumn<int>(
                name: "StartHour",
                table: "Timetables",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StopHour",
                table: "Timetables",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
