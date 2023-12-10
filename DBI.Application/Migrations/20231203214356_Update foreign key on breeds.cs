using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DBI.Application.Migrations
{
    public partial class Updateforeignkeyonbreeds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BreedTrainingProps_DogBreeds_DogBreedId",
                table: "BreedTrainingProps");

            migrationBuilder.DropIndex(
                name: "IX_BreedTrainingProps_DogBreedId",
                table: "BreedTrainingProps");

            migrationBuilder.RenameColumn(
                name: "DogBreedTrainingPropsId",
                table: "DogBreeds",
                newName: "BreedTrainingPropsId");

            migrationBuilder.CreateIndex(
                name: "IX_DogBreeds_BreedTrainingPropsId",
                table: "DogBreeds",
                column: "BreedTrainingPropsId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DogBreeds_BreedTrainingProps_BreedTrainingPropsId",
                table: "DogBreeds",
                column: "BreedTrainingPropsId",
                principalTable: "BreedTrainingProps",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DogBreeds_BreedTrainingProps_BreedTrainingPropsId",
                table: "DogBreeds");

            migrationBuilder.DropIndex(
                name: "IX_DogBreeds_BreedTrainingPropsId",
                table: "DogBreeds");

            migrationBuilder.RenameColumn(
                name: "BreedTrainingPropsId",
                table: "DogBreeds",
                newName: "DogBreedTrainingPropsId");

            migrationBuilder.CreateIndex(
                name: "IX_BreedTrainingProps_DogBreedId",
                table: "BreedTrainingProps",
                column: "DogBreedId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BreedTrainingProps_DogBreeds_DogBreedId",
                table: "BreedTrainingProps",
                column: "DogBreedId",
                principalTable: "DogBreeds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
