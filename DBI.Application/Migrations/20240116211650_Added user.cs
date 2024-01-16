using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DBI.Application.Migrations
{
    public partial class Addeduser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "HistoryEntities",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AvatarLink",
                table: "DogBreeds",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NormalizedName",
                table: "DogBreeds",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Username = table.Column<string>(type: "text", nullable: false),
                    Identifier = table.Column<string>(type: "text", nullable: false),
                    Role = table.Column<string>(type: "text", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HistoryEntities_UserId",
                table: "HistoryEntities",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_HistoryEntities_User_UserId",
                table: "HistoryEntities",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HistoryEntities_User_UserId",
                table: "HistoryEntities");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropIndex(
                name: "IX_HistoryEntities_UserId",
                table: "HistoryEntities");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "HistoryEntities");

            migrationBuilder.DropColumn(
                name: "AvatarLink",
                table: "DogBreeds");

            migrationBuilder.DropColumn(
                name: "NormalizedName",
                table: "DogBreeds");
        }
    }
}
