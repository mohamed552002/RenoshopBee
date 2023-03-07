using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RenoshopBee.Migrations
{
    /// <inheritdoc />
    public partial class AddCreditCardTbl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "creditCards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CardNumber = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false),
                    CCV = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    ExpireDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_creditCards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_creditCards_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "security",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_creditCards_UserId",
                table: "creditCards",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "creditCards");
        }
    }
}
