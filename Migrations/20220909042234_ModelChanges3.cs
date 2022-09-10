using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScanOrganizer.Migrations
{
    public partial class ModelChanges3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "DeleteSourceWhenFinished",
                table: "FolderScans",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeleteSourceWhenFinished",
                table: "FolderScans");
        }
    }
}
