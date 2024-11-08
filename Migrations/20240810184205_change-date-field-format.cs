using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace webapitest.Migrations
{
    /// <inheritdoc />
    public partial class changedatefieldformat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Drop the existing column
            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "Thoughts");

            // Add a new column with the desired type
            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "Thoughts",
                type: "timestamp",
                nullable: false,
                defaultValue: DateTime.UtcNow); // You can adjust this default value as needed
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Drop the new column
            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "Thoughts");

            // Recreate the original column
            migrationBuilder.AddColumn<string>(
                name: "DateCreated",
                table: "Thoughts",
                type: "text",
                nullable: false,
                defaultValue: ""); // You might want to adjust this default value
        }
    }
}
