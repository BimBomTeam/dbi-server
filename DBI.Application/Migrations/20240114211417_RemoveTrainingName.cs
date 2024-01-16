using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DBI.Application.Migrations
{
    public partial class RemoveTrainingName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NameInTrainingDataset",
                table: "BreedTrainingProps");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NameInTrainingDataset",
                table: "BreedTrainingProps",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
