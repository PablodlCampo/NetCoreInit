using Microsoft.EntityFrameworkCore.Migrations;

namespace CapaDatos.Migrations
{
    public partial class Colores : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Primario",
                table: "Colores");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Colores",
                newName: "Id");

            migrationBuilder.InsertData(
                table: "Colores",
                columns: new[] { "Id", "Nombre" },
                values: new object[,]
                {
                    { 1, "Red" },
                    { 2, "Blue" },
                    { 3, "White" },
                    { 4, "Brown" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Colores",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Colores",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Colores",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Colores",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Colores",
                newName: "ID");

            migrationBuilder.AddColumn<bool>(
                name: "Primario",
                table: "Colores",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
