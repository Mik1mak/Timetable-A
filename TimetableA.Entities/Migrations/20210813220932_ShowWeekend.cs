using Microsoft.EntityFrameworkCore.Migrations;

namespace TimetableA.DataAccessLayer.EntityFramework.Migrations;

public partial class ShowWeekend : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<bool>(
            name: "ShowWeekend",
            table: "Timetables",
            type: "bit",
            nullable: false,
            defaultValue: false);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "ShowWeekend",
            table: "Timetables");
    }
}
