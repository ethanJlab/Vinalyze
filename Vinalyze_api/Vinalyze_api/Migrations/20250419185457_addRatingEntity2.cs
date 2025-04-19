using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vinalyze_api.Migrations
{
    /// <inheritdoc />
    public partial class addRatingEntity2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Rating",
                columns: new[] { "Id", "AccountId", "Value", "WineId" },
                values: new object[] { new Guid("e1f2a3b4-c5d6-7e8f-9a0b-1c2d3e4f5a6b"), new Guid("0f8fad5b-d9cb-469f-a165-70867728950e"), 5, new Guid("7c9e6679-7425-40de-944b-e07fc1f90ae7") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Rating",
                keyColumn: "Id",
                keyValue: new Guid("e1f2a3b4-c5d6-7e8f-9a0b-1c2d3e4f5a6b"));
        }
    }
}
