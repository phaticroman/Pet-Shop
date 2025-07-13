using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetShop.Migrations
{
    /// <inheritdoc />
    public partial class Addbuyinrecordidinpetdetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BuyingRecordId",
                table: "petDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_petDetails_BuyingRecordId",
                table: "petDetails",
                column: "BuyingRecordId");

            migrationBuilder.AddForeignKey(
                name: "FK_petDetails_BuyingRecords_BuyingRecordId",
                table: "petDetails",
                column: "BuyingRecordId",
                principalTable: "BuyingRecords",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_petDetails_BuyingRecords_BuyingRecordId",
                table: "petDetails");

            migrationBuilder.DropIndex(
                name: "IX_petDetails_BuyingRecordId",
                table: "petDetails");

            migrationBuilder.DropColumn(
                name: "BuyingRecordId",
                table: "petDetails");
        }
    }
}
