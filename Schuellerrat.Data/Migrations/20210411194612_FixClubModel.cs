using Microsoft.EntityFrameworkCore.Migrations;

namespace Schuellerrat.Data.Migrations
{
    public partial class FixClubModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clubs_Articles_ArticleId",
                table: "Clubs");

            migrationBuilder.DropIndex(
                name: "IX_Clubs_ArticleId",
                table: "Clubs");

            migrationBuilder.DropColumn(
                name: "ArticleId",
                table: "Clubs");

            migrationBuilder.AddColumn<string>(
                name: "ShortDescription",
                table: "Clubs",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShortDescription",
                table: "Clubs");

            migrationBuilder.AddColumn<int>(
                name: "ArticleId",
                table: "Clubs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Clubs_ArticleId",
                table: "Clubs",
                column: "ArticleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clubs_Articles_ArticleId",
                table: "Clubs",
                column: "ArticleId",
                principalTable: "Articles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
