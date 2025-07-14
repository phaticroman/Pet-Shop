using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetShop.Migrations
{
    /// <inheritdoc />
    public partial class onetomanyrelationshipbetweenprtdetailsandselling : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PetDetailsId",
                table: "SellingRecords",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SellingRecords_PetDetailsId",
                table: "SellingRecords",
                column: "PetDetailsId");

            migrationBuilder.AddForeignKey(
                name: "FK_SellingRecords_petDetails_PetDetailsId",
                table: "SellingRecords",
                column: "PetDetailsId",
                principalTable: "petDetails",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SellingRecords_petDetails_PetDetailsId",
                table: "SellingRecords");

            migrationBuilder.DropIndex(
                name: "IX_SellingRecords_PetDetailsId",
                table: "SellingRecords");

            migrationBuilder.DropColumn(
                name: "PetDetailsId",
                table: "SellingRecords");
        }
    }
}
