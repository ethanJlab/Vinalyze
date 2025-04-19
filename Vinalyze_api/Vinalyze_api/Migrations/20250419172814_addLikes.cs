using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vinalyze_api.Migrations
{
    /// <inheritdoc />
    public partial class addLikes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LikedWines",
                table: "Account",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Account",
                keyColumn: "Id",
                keyValue: new Guid("0f8fad5b-d9cb-469f-a165-70867728950e"),
                column: "LikedWines",
                value: "[\"7c9e6679-7425-40de-944b-e07fc1f90ae7\"]");

            migrationBuilder.UpdateData(
                table: "Wine",
                keyColumn: "Id",
                keyValue: new Guid("7c9e6679-7425-40de-944b-e07fc1f90ae7"),
                column: "Description",
                value: "This was created from a plant somewhere on planet Earth. It is somewhere between 1 day old and 100 years old.");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LikedWines",
                table: "Account");

            migrationBuilder.UpdateData(
                table: "Wine",
                keyColumn: "Id",
                keyValue: new Guid("7c9e6679-7425-40de-944b-e07fc1f90ae7"),
                column: "Description",
                value: "This was created a plant somewhere on planet Earth. It is somewhere between 1 day old and 100 years old.");
        }
    }
}
