using Microsoft.EntityFrameworkCore.Migrations;

namespace Schuellerrat.Data.Migrations
{
    public partial class ModifyClub : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Clubs_ClubId",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_Paragraphs_Clubs_ClubId",
                table: "Paragraphs");

            migrationBuilder.DropIndex(
                name: "IX_Paragraphs_ClubId",
                table: "Paragraphs");

            migrationBuilder.DropIndex(
                name: "IX_Images_ClubId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "ClubId",
                table: "Paragraphs");

            migrationBuilder.DropColumn(
                name: "ClubId",
                table: "Images");

            migrationBuilder.AddColumn<int>(
                name: "ArticleId",
                table: "Clubs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Leader",
                table: "Clubs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "MaxClass",
                table: "Clubs",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MinClass",
                table: "Clubs",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Time",
                table: "Clubs",
                type: "nvarchar(max)",
                nullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "Leader",
                table: "Clubs");

            migrationBuilder.DropColumn(
                name: "MaxClass",
                table: "Clubs");

            migrationBuilder.DropColumn(
                name: "MinClass",
                table: "Clubs");

            migrationBuilder.DropColumn(
                name: "Time",
                table: "Clubs");

            migrationBuilder.AddColumn<int>(
                name: "ClubId",
                table: "Paragraphs",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ClubId",
                table: "Images",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Paragraphs_ClubId",
                table: "Paragraphs",
                column: "ClubId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_ClubId",
                table: "Images",
                column: "ClubId");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Clubs_ClubId",
                table: "Images",
                column: "ClubId",
                principalTable: "Clubs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Paragraphs_Clubs_ClubId",
                table: "Paragraphs",
                column: "ClubId",
                principalTable: "Clubs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
