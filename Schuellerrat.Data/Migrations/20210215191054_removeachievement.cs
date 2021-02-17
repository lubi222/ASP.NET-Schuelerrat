using Microsoft.EntityFrameworkCore.Migrations;

namespace Schuellerrat.Data.Migrations
{
    public partial class removeachievement : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Achievements_AchievementId",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_Links_Achievements_AchievementId",
                table: "Links");

            migrationBuilder.DropForeignKey(
                name: "FK_Paragraphs_Achievements_AchievementId",
                table: "Paragraphs");

            migrationBuilder.DropTable(
                name: "Achievements");

            migrationBuilder.DropIndex(
                name: "IX_Paragraphs_AchievementId",
                table: "Paragraphs");

            migrationBuilder.DropIndex(
                name: "IX_Links_AchievementId",
                table: "Links");

            migrationBuilder.DropIndex(
                name: "IX_Images_AchievementId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "AchievementId",
                table: "Paragraphs");

            migrationBuilder.DropColumn(
                name: "AchievementId",
                table: "Links");

            migrationBuilder.DropColumn(
                name: "AchievementId",
                table: "Images");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AchievementId",
                table: "Paragraphs",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AchievementId",
                table: "Links",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AchievementId",
                table: "Images",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Achievements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Achievements", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Paragraphs_AchievementId",
                table: "Paragraphs",
                column: "AchievementId");

            migrationBuilder.CreateIndex(
                name: "IX_Links_AchievementId",
                table: "Links",
                column: "AchievementId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_AchievementId",
                table: "Images",
                column: "AchievementId");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Achievements_AchievementId",
                table: "Images",
                column: "AchievementId",
                principalTable: "Achievements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Links_Achievements_AchievementId",
                table: "Links",
                column: "AchievementId",
                principalTable: "Achievements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Paragraphs_Achievements_AchievementId",
                table: "Paragraphs",
                column: "AchievementId",
                principalTable: "Achievements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
