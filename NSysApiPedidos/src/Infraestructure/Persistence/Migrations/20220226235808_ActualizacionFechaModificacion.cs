using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class ActualizacionFechaModificacion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "FechaModificacion",
                table: "PedidoDet",
                type: "datetime",
                nullable: false, // 1900, 1, 1, 00, 00, 00, 000
                defaultValue: new DateTime(1900, 1, 1, 00, 00, 00, 000, DateTimeKind.Unspecified),
                comment: "Fecha de Creacion");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FechaModificacion",
                table: "PedidoDet");
        }
    }
}
