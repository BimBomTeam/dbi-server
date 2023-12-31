﻿// <auto-generated />
using DBI.Application;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DBI.Application.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20231203212330_Init")]
    partial class Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.25")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("DBI.Domain.Entities.Core.BreedTrainingProps", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("DogBreedId")
                        .HasColumnType("integer");

                    b.Property<int>("IdInTrainingDataset")
                        .HasColumnType("integer");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("NameInTrainingDataset")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("DogBreedId")
                        .IsUnique();

                    b.ToTable("BreedTrainingProps");
                });

            modelBuilder.Entity("DBI.Domain.Entities.Core.DogBreed", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("DogBreedTrainingPropsId")
                        .HasColumnType("integer");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("ShortDescription")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ShowName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("DogBreeds");
                });

            modelBuilder.Entity("DBI.Domain.Entities.Core.BreedTrainingProps", b =>
                {
                    b.HasOne("DBI.Domain.Entities.Core.DogBreed", "DogBreed")
                        .WithOne("BreedTrainingProps")
                        .HasForeignKey("DBI.Domain.Entities.Core.BreedTrainingProps", "DogBreedId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DogBreed");
                });

            modelBuilder.Entity("DBI.Domain.Entities.Core.DogBreed", b =>
                {
                    b.Navigation("BreedTrainingProps");
                });
#pragma warning restore 612, 618
        }
    }
}
