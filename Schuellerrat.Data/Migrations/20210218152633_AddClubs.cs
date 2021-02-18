using Microsoft.EntityFrameworkCore.Migrations;

namespace Schuellerrat.Data.Migrations
{
    public partial class AddClubs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateTable(
                name: "Clubs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clubs", x => x.Id);
                });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Clubs_ClubId",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_Paragraphs_Clubs_ClubId",
                table: "Paragraphs");

            migrationBuilder.DropTable(
                name: "Clubs");

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
        }
    }
}
