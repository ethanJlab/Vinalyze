﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Vinalyze_api.Controllers.Data;

#nullable disable

namespace Vinalyze_api.Migrations
{
    [DbContext(typeof(VinalyzeDbContext))]
    [Migration("20250419185457_addRatingEntity2")]
    partial class addRatingEntity2
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Vinalyze_api.Account", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.PrimitiveCollection<string>("LikedWines")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Account");

                    b.HasData(
                        new
                        {
                            Id = new Guid("0f8fad5b-d9cb-469f-a165-70867728950e"),
                            Email = "admin@gmail.com",
                            LikedWines = "[\"7c9e6679-7425-40de-944b-e07fc1f90ae7\"]",
                            Password = "8c6976e5b5410415bde908bd4dee15dfb167a9c873fc4bb8a81f6f2ab448a918",
                            Username = "admin"
                        });
                });

            modelBuilder.Entity("Vinalyze_api.Comment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AccountId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Text")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("WineId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("Comment");

                    b.HasData(
                        new
                        {
                            Id = new Guid("d1f8b2c3-4e5f-6a7b-8c9d-e0f1a2b3c4d5"),
                            AccountId = new Guid("0f8fad5b-d9cb-469f-a165-70867728950e"),
                            Text = "This wine is amazing! I love the flavor profile.",
                            WineId = new Guid("7c9e6679-7425-40de-944b-e07fc1f90ae7")
                        });
                });

            modelBuilder.Entity("Vinalyze_api.Rating", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AccountId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Value")
                        .HasColumnType("int");

                    b.Property<Guid>("WineId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("Rating");

                    b.HasData(
                        new
                        {
                            Id = new Guid("e1f2a3b4-c5d6-7e8f-9a0b-1c2d3e4f5a6b"),
                            AccountId = new Guid("0f8fad5b-d9cb-469f-a165-70867728950e"),
                            Value = 5,
                            WineId = new Guid("7c9e6679-7425-40de-944b-e07fc1f90ae7")
                        });
                });

            modelBuilder.Entity("Vinalyze_api.Wine", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FlavorProfile")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Wine");

                    b.HasData(
                        new
                        {
                            Id = new Guid("7c9e6679-7425-40de-944b-e07fc1f90ae7"),
                            Description = "This was created from a plant somewhere on planet Earth. It is somewhere between 1 day old and 100 years old.",
                            FlavorProfile = "This tastes like dirt and feet.",
                            Name = "Sample Wine"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
