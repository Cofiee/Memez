using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Memez.Migrations
{
    public partial class votes_model : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Meme_AspNetUsers_MemezUserId",
                table: "Meme");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Meme",
                table: "Meme");

            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Meme");

            migrationBuilder.RenameTable(
                name: "Meme",
                newName: "Memes");

            migrationBuilder.RenameIndex(
                name: "IX_Meme_MemezUserId",
                table: "Memes",
                newName: "IX_Memes_MemezUserId");

            migrationBuilder.AddColumn<int>(
                name: "VotesSum",
                table: "Memes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Memes",
                table: "Memes",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Votes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MemeId = table.Column<int>(type: "int", nullable: false),
                    MemezUserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Votes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Votes_AspNetUsers_MemezUserId",
                        column: x => x.MemezUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Votes_Memes_MemeId",
                        column: x => x.MemeId,
                        principalTable: "Memes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Votes_MemeId",
                table: "Votes",
                column: "MemeId");

            migrationBuilder.CreateIndex(
                name: "IX_Votes_MemezUserId",
                table: "Votes",
                column: "MemezUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Memes_AspNetUsers_MemezUserId",
                table: "Memes",
                column: "MemezUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Memes_AspNetUsers_MemezUserId",
                table: "Memes");

            migrationBuilder.DropTable(
                name: "Votes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Memes",
                table: "Memes");

            migrationBuilder.DropColumn(
                name: "VotesSum",
                table: "Memes");

            migrationBuilder.RenameTable(
                name: "Memes",
                newName: "Meme");

            migrationBuilder.RenameIndex(
                name: "IX_Memes_MemezUserId",
                table: "Meme",
                newName: "IX_Meme_MemezUserId");

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Meme",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Meme",
                table: "Meme",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Meme_AspNetUsers_MemezUserId",
                table: "Meme",
                column: "MemezUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
