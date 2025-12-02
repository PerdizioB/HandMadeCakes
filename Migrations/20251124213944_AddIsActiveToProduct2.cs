using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HandMadeCakes.Migrations
{
    /// <inheritdoc />
    public partial class AddIsActiveToProduct2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "Cake",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Cake_ProductId",
                table: "Cake",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cake_Product_ProductId",
                table: "Cake",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cake_Product_ProductId",
                table: "Cake");

            migrationBuilder.DropIndex(
                name: "IX_Cake_ProductId",
                table: "Cake");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Cake");
        }
    }
}
