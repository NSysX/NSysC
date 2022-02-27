using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class Inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PedidoDet",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Id Consecutivo del pedido")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdCliente = table.Column<int>(type: "int", nullable: false, comment: "El id del Cliente"),
                    IdEmpleado = table.Column<int>(type: "int", nullable: false, comment: "El id del Empleado"),
                    Cantidad = table.Column<double>(type: "float", nullable: false, comment: "Cantidad solicitada"),
                    IdProdMaestro = table.Column<int>(type: "int", nullable: false, comment: "Id del catalogo de ProdMaestro"),
                    IdMarca = table.Column<int>(type: "int", nullable: false, comment: "Id del Catalogo de Marcas"),
                    EsCadaUno = table.Column<bool>(type: "bit", nullable: false, comment: "Si el precio es por unidad y no por caja"),
                    Notas = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false, comment: "Notas para el que va a surtir el reglon del Pedido"),
                    Estatus = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: false, comment: "Estatus del articulo del Pedidio"),
                    FechaCreacion = table.Column<DateTime>(type: "datetime", nullable: false, comment: "Fecha de Creacion")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PedidoDet", x => x.Id);
                },
                comment: "Listado de Pedidos");

            migrationBuilder.CreateIndex(
                name: "IX_NoDuplicado",
                table: "PedidoDet",
                columns: new[] { "IdCliente", "IdProdMaestro", "IdMarca", "Estatus" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IXFK_PedidoDet_Cliente",
                table: "PedidoDet",
                column: "IdCliente");

            migrationBuilder.CreateIndex(
                name: "IXFK_PedidoDet_Empleado",
                table: "PedidoDet",
                column: "IdEmpleado");

            migrationBuilder.CreateIndex(
                name: "IXFK_PedidoDet_Marca",
                table: "PedidoDet",
                column: "IdMarca");

            migrationBuilder.CreateIndex(
                name: "IXFK_PedidoDet_ProdMaestro",
                table: "PedidoDet",
                column: "IdProdMaestro");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PedidoDet");
        }
    }
}
