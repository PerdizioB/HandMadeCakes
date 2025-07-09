using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HandMadeCakes.Migrations
{
    /// <inheritdoc />
    public partial class AddStatusToOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_CakeId",
                table: "OrderItems",
                column: "CakeId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Cake_CakeId",
                table: "OrderItems",
                column: "CakeId",
                principalTable: "Cake",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Cake_CakeId",
                table: "OrderItems");

            migrationBuilder.DropIndex(
                name: "IX_OrderItems_CakeId",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Orders");
        }
    }
}
