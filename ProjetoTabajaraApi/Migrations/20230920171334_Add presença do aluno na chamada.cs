using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetoTabajaraApi.Migrations
{
    public partial class Addpresençadoalunonachamada : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "studentPresent",
                table: "Attendances",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "studentPresent",
                table: "Attendances");
        }
    }
}
