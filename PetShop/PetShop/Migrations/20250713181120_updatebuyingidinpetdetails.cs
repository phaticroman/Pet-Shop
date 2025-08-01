﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetShop.Migrations
{
    /// <inheritdoc />
    public partial class updatebuyingidinpetdetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_petDetails_BuyingRecords_BuyingRecordId",
                table: "petDetails");

            migrationBuilder.AlterColumn<int>(
                name: "BuyingRecordId",
                table: "petDetails",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_petDetails_BuyingRecords_BuyingRecordId",
                table: "petDetails",
                column: "BuyingRecordId",
                principalTable: "BuyingRecords",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_petDetails_BuyingRecords_BuyingRecordId",
                table: "petDetails");

            migrationBuilder.AlterColumn<int>(
                name: "BuyingRecordId",
                table: "petDetails",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_petDetails_BuyingRecords_BuyingRecordId",
                table: "petDetails",
                column: "BuyingRecordId",
                principalTable: "BuyingRecords",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
