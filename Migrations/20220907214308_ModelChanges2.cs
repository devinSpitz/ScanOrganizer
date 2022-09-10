using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScanOrganizer.Migrations
{
    public partial class ModelChanges2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "JobType",
                table: "FolderScans");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "JobType",
                table: "FolderScans",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
