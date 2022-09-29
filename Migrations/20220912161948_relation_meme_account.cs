using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Memez.Migrations
{
    public partial class relation_meme_account : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MemezUserId",
                table: "Meme",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Meme_MemezUserId",
                table: "Meme",
                column: "MemezUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Meme_AspNetUsers_MemezUserId",
                table: "Meme",
                column: "MemezUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Meme_AspNetUsers_MemezUserId",
                table: "Meme");

            migrationBuilder.DropIndex(
                name: "IX_Meme_MemezUserId",
                table: "Meme");

            migrationBuilder.DropColumn(
                name: "MemezUserId",
                table: "Meme");
        }
    }
}
