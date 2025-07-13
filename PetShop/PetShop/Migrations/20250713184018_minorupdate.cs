using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetShop.Migrations
{
    /// <inheritdoc />
    public partial class minorupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "petDetails");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "BuyingRecords",
                newName: "petName");

            migrationBuilder.AddColumn<string>(
                name: "customerName",
                table: "BuyingRecords",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "customerName",
                table: "BuyingRecords");

            migrationBuilder.RenameColumn(
                name: "petName",
                table: "BuyingRecords",
                newName: "Name");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "petDetails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
