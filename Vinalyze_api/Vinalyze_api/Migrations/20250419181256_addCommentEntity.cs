using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vinalyze_api.Migrations
{
    /// <inheritdoc />
    public partial class addCommentEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LikedWines = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Comment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WineId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comment", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Wine",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FlavorProfile = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wine", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Account",
                columns: new[] { "Id", "Email", "LikedWines", "Password", "Username" },
                values: new object[] { new Guid("0f8fad5b-d9cb-469f-a165-70867728950e"), "admin@gmail.com", "[\"7c9e6679-7425-40de-944b-e07fc1f90ae7\"]", "8c6976e5b5410415bde908bd4dee15dfb167a9c873fc4bb8a81f6f2ab448a918", "admin" });

            migrationBuilder.InsertData(
                table: "Comment",
                columns: new[] { "Id", "AccountId", "Text", "WineId" },
                values: new object[] { new Guid("d1f8b2c3-4e5f-6a7b-8c9d-e0f1a2b3c4d5"), new Guid("0f8fad5b-d9cb-469f-a165-70867728950e"), "This wine is amazing! I love the flavor profile.", new Guid("7c9e6679-7425-40de-944b-e07fc1f90ae7") });

            migrationBuilder.InsertData(
                table: "Wine",
                columns: new[] { "Id", "Description", "FlavorProfile", "Name" },
                values: new object[] { new Guid("7c9e6679-7425-40de-944b-e07fc1f90ae7"), "This was created from a plant somewhere on planet Earth. It is somewhere between 1 day old and 100 years old.", "This tastes like dirt and feet.", "Sample Wine" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Account");

            migrationBuilder.DropTable(
                name: "Comment");

            migrationBuilder.DropTable(
                name: "Wine");
        }
    }
}
