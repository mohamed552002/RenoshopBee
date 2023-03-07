using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RenoshopBee.Migrations
{
    /// <inheritdoc />
    public partial class AddColumnsToOrderItemTbl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "orderItems",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Size",
                table: "orderItems",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Color",
                table: "orderItems");

            migrationBuilder.DropColumn(
                name: "Size",
                table: "orderItems");
        }
    }
}
