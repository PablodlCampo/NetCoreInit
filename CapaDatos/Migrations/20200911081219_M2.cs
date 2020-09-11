using Microsoft.EntityFrameworkCore.Migrations;

namespace CapaDatos.Migrations
{
    public partial class M2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Colors",
                table: "Colors");

            migrationBuilder.RenameTable(
                name: "Colors",
                newName: "Colores");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Colores",
                table: "Colores",
                column: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Colores",
                table: "Colores");

            migrationBuilder.RenameTable(
                name: "Colores",
                newName: "Colors");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Colors",
                table: "Colors",
                column: "ID");
        }
    }
}
