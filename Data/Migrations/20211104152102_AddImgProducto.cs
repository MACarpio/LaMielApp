using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LaMielApp.Data.Migrations
{
    public partial class AddImgProducto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "image",
                table: "t_product",
                newName: "imagename");

            migrationBuilder.AddColumn<byte[]>(
                name: "Imagen",
                table: "t_product",
                type: "bytea",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Imagen",
                table: "t_product");

            migrationBuilder.RenameColumn(
                name: "imagename",
                table: "t_product",
                newName: "image");
        }
    }
}
