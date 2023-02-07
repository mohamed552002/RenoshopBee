using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RenoshopBee.Migrations
{
    /// <inheritdoc />
    public partial class ProductNumberOfSalesColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<int>(
                name: "NumOfSales",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumOfSales",
                table: "Products");
        }
    }
}
