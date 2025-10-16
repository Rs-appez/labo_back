using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addChief : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ChiefId",
                table: "Employees",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Chiefs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chiefs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Chiefs_Employees_Id",
                        column: x => x.Id,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_ChiefId",
                table: "Employees",
                column: "ChiefId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Chiefs_ChiefId",
                table: "Employees",
                column: "ChiefId",
                principalTable: "Chiefs",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Chiefs_ChiefId",
                table: "Employees");

            migrationBuilder.DropTable(
                name: "Chiefs");

            migrationBuilder.DropIndex(
                name: "IX_Employees_ChiefId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "ChiefId",
                table: "Employees");
        }
    }
}
