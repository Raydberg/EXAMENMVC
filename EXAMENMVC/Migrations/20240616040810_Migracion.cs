using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EXAMENMVC.Migrations
{
    public partial class Migracion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Marcas",
                columns: table => new
                {
                    IDMARCA = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NOM_MARCA = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Marcas", x => x.IDMARCA);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    IdUsuario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreCompleto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Correo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Clave = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.IdUsuario);
                });

            migrationBuilder.CreateTable(
                name: "Modelos",
                columns: table => new
                {
                    IDMODELO = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NOM_MODELO = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MarcaIDMARCA = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modelos", x => x.IDMODELO);
                    table.ForeignKey(
                        name: "FK_Modelos_Marcas_MarcaIDMARCA",
                        column: x => x.MarcaIDMARCA,
                        principalTable: "Marcas",
                        principalColumn: "IDMARCA",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Vehiculos",
                columns: table => new
                {
                    IDVEHICULO = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NRO_PLACA = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    año = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModeloIDMODELO = table.Column<int>(type: "int", nullable: false),
                    Imagen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehiculos", x => x.IDVEHICULO);
                    table.ForeignKey(
                        name: "FK_Vehiculos_Modelos_ModeloIDMODELO",
                        column: x => x.ModeloIDMODELO,
                        principalTable: "Modelos",
                        principalColumn: "IDMODELO",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Marcas",
                columns: new[] { "IDMARCA", "NOM_MARCA" },
                values: new object[] { 1, "Toyota" });

            migrationBuilder.InsertData(
                table: "Marcas",
                columns: new[] { "IDMARCA", "NOM_MARCA" },
                values: new object[] { 2, "BMW" });

            migrationBuilder.InsertData(
                table: "Marcas",
                columns: new[] { "IDMARCA", "NOM_MARCA" },
                values: new object[] { 3, "Ford" });

            migrationBuilder.InsertData(
                table: "Modelos",
                columns: new[] { "IDMODELO", "MarcaIDMARCA", "NOM_MODELO" },
                values: new object[,]
                {
                    { 1, 1, "Corolla" },
                    { 2, 1, "Camry" },
                    { 3, 1, "RAV4" },
                    { 4, 2, "Serie 3" },
                    { 5, 2, "X5" },
                    { 6, 2, "i8" },
                    { 7, 3, "Mustang" },
                    { 8, 3, "F-150" },
                    { 9, 3, "Explorer" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Modelos_MarcaIDMARCA",
                table: "Modelos",
                column: "MarcaIDMARCA");

            migrationBuilder.CreateIndex(
                name: "IX_Vehiculos_ModeloIDMODELO",
                table: "Vehiculos",
                column: "ModeloIDMODELO");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Vehiculos");

            migrationBuilder.DropTable(
                name: "Modelos");

            migrationBuilder.DropTable(
                name: "Marcas");
        }
    }
}
