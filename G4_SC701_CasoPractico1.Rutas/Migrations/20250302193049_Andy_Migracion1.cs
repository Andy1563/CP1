using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace G4_SC701_CasoPractico1.Rutas.Migrations
{
    /// <inheritdoc />
    public partial class Andy_Migracion1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ruta",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreRuta = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false),
                    IdParada = table.Column<int>(type: "int", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdUsuarioRegistro = table.Column<int>(type: "int", nullable: false),
                    IdVehiculo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ruta", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ruta_Usuarios_IdUsuarioRegistro",
                        column: x => x.IdUsuarioRegistro,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ruta_Vehiculos_IdVehiculo",
                        column: x => x.IdVehiculo,
                        principalTable: "Vehiculos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Boletos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdRuta = table.Column<int>(type: "int", nullable: false),
                    IdUsuario = table.Column<int>(type: "int", nullable: false),
                    FechaCompra = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Boletos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Boletos_Ruta_IdRuta",
                        column: x => x.IdRuta,
                        principalTable: "Ruta",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Boletos_Usuarios_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Horarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Horario = table.Column<DateTime>(type: "datetime2", nullable: false),
                    rutasId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Horarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Horarios_Ruta_rutasId",
                        column: x => x.rutasId,
                        principalTable: "Ruta",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Paradas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    rutaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Paradas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Paradas_Ruta_rutaId",
                        column: x => x.rutaId,
                        principalTable: "Ruta",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Boletos_IdRuta",
                table: "Boletos",
                column: "IdRuta");

            migrationBuilder.CreateIndex(
                name: "IX_Boletos_IdUsuario",
                table: "Boletos",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Horarios_rutasId",
                table: "Horarios",
                column: "rutasId");

            migrationBuilder.CreateIndex(
                name: "IX_Paradas_rutaId",
                table: "Paradas",
                column: "rutaId");

            migrationBuilder.CreateIndex(
                name: "IX_Ruta_IdUsuarioRegistro",
                table: "Ruta",
                column: "IdUsuarioRegistro");

            migrationBuilder.CreateIndex(
                name: "IX_Ruta_IdVehiculo",
                table: "Ruta",
                column: "IdVehiculo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Boletos");

            migrationBuilder.DropTable(
                name: "Horarios");

            migrationBuilder.DropTable(
                name: "Paradas");

            migrationBuilder.DropTable(
                name: "Ruta");
        }
    }
}
