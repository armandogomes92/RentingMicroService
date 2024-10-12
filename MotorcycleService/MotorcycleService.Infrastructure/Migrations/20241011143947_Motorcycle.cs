using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MotorcycleService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Motorcycle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Motorcycle",
                columns: table => new
                {
                    Identificador = table.Column<string>(type: "text", nullable: false),
                    Ano = table.Column<int>(type: "integer", maxLength: 4, nullable: false),
                    Modelo = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Placa = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Motorcycle", x => x.Identificador);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Motorcycle");
        }
    }
}
