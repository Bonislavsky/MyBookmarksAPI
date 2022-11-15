using Microsoft.EntityFrameworkCore.Migrations;

namespace MyBookmarksAPI.DAL.Migrations
{
    public partial class Initial2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookMarks_Folders_FolderId",
                table: "BookMarks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BookMarks",
                table: "BookMarks");

            migrationBuilder.RenameTable(
                name: "BookMarks",
                newName: "Bookmarks");

            migrationBuilder.RenameIndex(
                name: "IX_BookMarks_FolderId",
                table: "Bookmarks",
                newName: "IX_Bookmarks_FolderId");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "nvarchar(45)",
                maxLength: 45,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "Bookmarks",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bookmarks",
                table: "Bookmarks",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookmarks_Folders_FolderId",
                table: "Bookmarks",
                column: "FolderId",
                principalTable: "Folders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookmarks_Folders_FolderId",
                table: "Bookmarks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Bookmarks",
                table: "Bookmarks");

            migrationBuilder.RenameTable(
                name: "Bookmarks",
                newName: "BookMarks");

            migrationBuilder.RenameIndex(
                name: "IX_Bookmarks_FolderId",
                table: "BookMarks",
                newName: "IX_BookMarks_FolderId");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(45)",
                oldMaxLength: 45,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "BookMarks",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookMarks",
                table: "BookMarks",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BookMarks_Folders_FolderId",
                table: "BookMarks",
                column: "FolderId",
                principalTable: "Folders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
