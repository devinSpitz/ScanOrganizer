using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScanOrganizer.Migrations
{
    public partial class renameField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Active",
                table: "OcrTags",
                newName: "IsActive");

            migrationBuilder.RenameColumn(
                name: "Active",
                table: "FolderScans",
                newName: "IsActive");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "OcrTags",
                newName: "Active");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "FolderScans",
                newName: "Active");
        }
    }
}
