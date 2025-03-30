﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Vinalyze_api.Controllers.Data;

#nullable disable

namespace Vinalyze_api.Migrations.WineDb
{
    [DbContext(typeof(WineDbContext))]
    partial class WineDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

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
                            Description = "This was created a plant somewhere on planet Earth. It is somewhere between 1 day old and 100 years old.",
                            FlavorProfile = "This tastes like dirt and feet.",
                            Name = "Sample Wine"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
