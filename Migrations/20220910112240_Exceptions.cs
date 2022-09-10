using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScanOrganizer.Migrations
{
    public partial class Exceptions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FolderScanExceptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FolderScanId = table.Column<int>(type: "INTEGER", nullable: true),
                    HelpLink = table.Column<string>(type: "TEXT", nullable: true),
                    Source = table.Column<string>(type: "TEXT", nullable: true),
                    HResult = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FolderScanExceptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FolderScanExceptions_FolderScans_FolderScanId",
                        column: x => x.FolderScanId,
                        principalTable: "FolderScans",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_FolderScanExceptions_FolderScanId",
                table: "FolderScanExceptions",
                column: "FolderScanId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FolderScanExceptions");
        }
    }
}
