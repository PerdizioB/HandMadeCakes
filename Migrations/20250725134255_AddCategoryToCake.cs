using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HandMadeCakes.Migrations
{
    /// <inheritdoc />
    public partial class AddCategoryToCake : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaidAt",
                table: "Orders");

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Cake",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "Cake");

            migrationBuilder.AddColumn<DateTime>(
                name: "PaidAt",
                table: "Orders",
                type: "datetime2",
                nullable: true);
        }
    }
}
