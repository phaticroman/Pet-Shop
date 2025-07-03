using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetShop.Migrations
{
    /// <inheritdoc />
    public partial class OneToManyRelationshipBetweenPetdetailsAndCage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CageId",
                table: "petDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_petDetails_CageId",
                table: "petDetails",
                column: "CageId");

            migrationBuilder.AddForeignKey(
                name: "FK_petDetails_cages_CageId",
                table: "petDetails",
                column: "CageId",
                principalTable: "cages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_petDetails_cages_CageId",
                table: "petDetails");

            migrationBuilder.DropIndex(
                name: "IX_petDetails_CageId",
                table: "petDetails");

            migrationBuilder.DropColumn(
                name: "CageId",
                table: "petDetails");
        }
    }
}
