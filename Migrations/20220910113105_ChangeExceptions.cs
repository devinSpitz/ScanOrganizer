using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScanOrganizer.Migrations
{
    public partial class ChangeExceptions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HResult",
                table: "FolderScanExceptions");

            migrationBuilder.DropColumn(
                name: "HelpLink",
                table: "FolderScanExceptions");

            migrationBuilder.RenameColumn(
                name: "Source",
                table: "FolderScanExceptions",
                newName: "InnerExceptionMessage");

            migrationBuilder.AddColumn<string>(
                name: "Message",
                table: "FolderScanExceptions",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Message",
                table: "FolderScanExceptions");

            migrationBuilder.RenameColumn(
                name: "InnerExceptionMessage",
                table: "FolderScanExceptions",
                newName: "Source");

            migrationBuilder.AddColumn<int>(
                name: "HResult",
                table: "FolderScanExceptions",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "HelpLink",
                table: "FolderScanExceptions",
                type: "TEXT",
                nullable: true);
        }
    }
}
