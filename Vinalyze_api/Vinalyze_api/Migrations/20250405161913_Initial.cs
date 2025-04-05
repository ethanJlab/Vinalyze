using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vinalyze_api.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Account",
                keyColumn: "Id",
                keyValue: new Guid("0f8fad5b-d9cb-469f-a165-70867728950e"),
                column: "Password",
                value: "8c6976e5b5410415bde908bd4dee15dfb167a9c873fc4bb8a81f6f2ab448a918");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Account",
                keyColumn: "Id",
                keyValue: new Guid("0f8fad5b-d9cb-469f-a165-70867728950e"),
                column: "Password",
                value: "admin");
        }
    }
}
