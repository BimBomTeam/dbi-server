using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DBI.Application.Migrations
{
    public partial class Historyentitycreated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DogBreeds_BreedTrainingProps_BreedTrainingPropsId",
                table: "DogBreeds");

            migrationBuilder.AlterColumn<int>(
                name: "BreedTrainingPropsId",
                table: "DogBreeds",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.CreateTable(
                name: "HistoryEntities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DogBreedId = table.Column<int>(type: "integer", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoryEntities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HistoryEntities_DogBreeds_DogBreedId",
                        column: x => x.DogBreedId,
                        principalTable: "DogBreeds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HistoryEntities_DogBreedId",
                table: "HistoryEntities",
                column: "DogBreedId");

            migrationBuilder.AddForeignKey(
                name: "FK_DogBreeds_BreedTrainingProps_BreedTrainingPropsId",
                table: "DogBreeds",
                column: "BreedTrainingPropsId",
                principalTable: "BreedTrainingProps",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DogBreeds_BreedTrainingProps_BreedTrainingPropsId",
                table: "DogBreeds");

            migrationBuilder.DropTable(
                name: "HistoryEntities");

            migrationBuilder.AlterColumn<int>(
                name: "BreedTrainingPropsId",
                table: "DogBreeds",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DogBreeds_BreedTrainingProps_BreedTrainingPropsId",
                table: "DogBreeds",
                column: "BreedTrainingPropsId",
                principalTable: "BreedTrainingProps",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
