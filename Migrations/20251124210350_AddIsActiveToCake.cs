using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HandMadeCakes.Migrations
{
    /// <inheritdoc />
    public partial class AddIsActiveToCake : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Cake",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Cake");
        }
    }
}
