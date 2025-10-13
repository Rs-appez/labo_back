using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RenamePropretyNameIntoTheme : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Zones",
                newName: "Theme");

            migrationBuilder.RenameIndex(
                name: "IX_Zones_Name",
                table: "Zones",
                newName: "IX_Zones_Theme");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Theme",
                table: "Zones",
                newName: "Name");

            migrationBuilder.RenameIndex(
                name: "IX_Zones_Theme",
                table: "Zones",
                newName: "IX_Zones_Name");
        }
    }
}
