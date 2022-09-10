using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScanOrganizer.Migrations
{
    public partial class ModelChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OcrTags");

            migrationBuilder.RenameColumn(
                name: "FolderType",
                table: "FolderScans",
                newName: "JobType");

            migrationBuilder.AddColumn<string>(
                name: "Languages",
                table: "FolderScans",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "SortTags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FindTag = table.Column<string>(type: "TEXT", nullable: false),
                    FolderName = table.Column<string>(type: "TEXT", nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    SortOrder = table.Column<int>(type: "INTEGER", nullable: false),
                    FolderType = table.Column<int>(type: "INTEGER", nullable: false),
                    UseRegex = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SortTags", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SortTags");

            migrationBuilder.DropColumn(
                name: "Languages",
                table: "FolderScans");

            migrationBuilder.RenameColumn(
                name: "JobType",
                table: "FolderScans",
                newName: "FolderType");

            migrationBuilder.CreateTable(
                name: "OcrTags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FindTag = table.Column<string>(type: "TEXT", nullable: false),
                    FolderName = table.Column<string>(type: "TEXT", nullable: false),
                    FolderType = table.Column<int>(type: "INTEGER", nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OcrTags", x => x.Id);
                });
        }
    }
}
