using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class ActualizacionFechaModificacion3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaModificacion",
                table: "PedidoDet",
                type: "datetime2",
                nullable: false,
                comment: "Fecha de Creacion",
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldComment: "Fecha de Creacion");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaCreacion",
                table: "PedidoDet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1900, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                comment: "Fecha de Creacion",
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(1900, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldComment: "Fecha de Creacion");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaModificacion",
                table: "PedidoDet",
                type: "datetime",
                nullable: false,
                comment: "Fecha de Creacion",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldComment: "Fecha de Creacion");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaCreacion",
                table: "PedidoDet",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1900, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                comment: "Fecha de Creacion",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(1900, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldComment: "Fecha de Creacion");
        }
    }
}
