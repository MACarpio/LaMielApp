using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LaMielApp.Data.Migrations
{
    public partial class addfechaentrega : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumeroTarjeta",
                table: "t_pago");

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaEntrega",
                table: "t_pago",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FechaEntrega",
                table: "t_pago");

            migrationBuilder.AddColumn<string>(
                name: "NumeroTarjeta",
                table: "t_pago",
                type: "text",
                nullable: true);
        }
    }
}
