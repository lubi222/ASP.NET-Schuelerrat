using Microsoft.EntityFrameworkCore.Migrations;

namespace Schuellerrat.Data.Migrations
{
    public partial class ModifyParagraphForeignKeys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Paragraphs_Articles_ArticleId",
                table: "Paragraphs");

            migrationBuilder.RenameColumn(
                name: "Content",
                table: "Paragraphs",
                newName: "Text");

            migrationBuilder.AlterColumn<int>(
                name: "ArticleId",
                table: "Paragraphs",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Paragraphs_Articles_ArticleId",
                table: "Paragraphs",
                column: "ArticleId",
                principalTable: "Articles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Paragraphs_Articles_ArticleId",
                table: "Paragraphs");

            migrationBuilder.RenameColumn(
                name: "Text",
                table: "Paragraphs",
                newName: "Content");

            migrationBuilder.AlterColumn<int>(
                name: "ArticleId",
                table: "Paragraphs",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Paragraphs_Articles_ArticleId",
                table: "Paragraphs",
                column: "ArticleId",
                principalTable: "Articles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
