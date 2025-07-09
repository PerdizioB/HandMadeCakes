using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HandMadeCakes.Migrations
{
    /// <inheritdoc />
    public partial class CreateBD : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Valor",
                table: "Cake",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "Sabor",
                table: "Cake",
                newName: "Flavor");

            migrationBuilder.RenameColumn(
                name: "Descricao",
                table: "Cake",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "Capa",
                table: "Cake",
                newName: "Cover");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Cake",
                newName: "Valor");

            migrationBuilder.RenameColumn(
                name: "Flavor",
                table: "Cake",
                newName: "Sabor");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Cake",
                newName: "Descricao");

            migrationBuilder.RenameColumn(
                name: "Cover",
                table: "Cake",
                newName: "Capa");
        }
    }
}
