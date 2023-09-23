using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetoTabajaraApi.Migrations
{
    public partial class correçãoAttendance : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "studentPresent",
                table: "Attendances",
                newName: "StudentPresent");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StudentPresent",
                table: "Attendances",
                newName: "studentPresent");
        }
    }
}
