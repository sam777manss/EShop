using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoesApi.Migrations
{
    public partial class UserCity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductImageTable_AddProductTable_ProductId",
                table: "ProductImageTable");

            migrationBuilder.DropForeignKey(
                name: "FK_UserCart_AddProductTable_ProductId",
                table: "UserCart");

            migrationBuilder.DropIndex(
                name: "IX_UserCart_ProductId",
                table: "UserCart");

            migrationBuilder.DropIndex(
                name: "IX_ProductImageTable_ProductId",
                table: "ProductImageTable");

            migrationBuilder.AddColumn<Guid>(
                name: "addProductTablesProductId",
                table: "UserCart",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ProductId",
                table: "ProductImageTable",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "AddProductTablesProductId",
                table: "ProductImageTable",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserCart_addProductTablesProductId",
                table: "UserCart",
                column: "addProductTablesProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductImageTable_AddProductTablesProductId",
                table: "ProductImageTable",
                column: "AddProductTablesProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductImageTable_AddProductTable_AddProductTablesProductId",
                table: "ProductImageTable",
                column: "AddProductTablesProductId",
                principalTable: "AddProductTable",
                principalColumn: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserCart_AddProductTable_addProductTablesProductId",
                table: "UserCart",
                column: "addProductTablesProductId",
                principalTable: "AddProductTable",
                principalColumn: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductImageTable_AddProductTable_AddProductTablesProductId",
                table: "ProductImageTable");

            migrationBuilder.DropForeignKey(
                name: "FK_UserCart_AddProductTable_addProductTablesProductId",
                table: "UserCart");

            migrationBuilder.DropIndex(
                name: "IX_UserCart_addProductTablesProductId",
                table: "UserCart");

            migrationBuilder.DropIndex(
                name: "IX_ProductImageTable_AddProductTablesProductId",
                table: "ProductImageTable");

            migrationBuilder.DropColumn(
                name: "addProductTablesProductId",
                table: "UserCart");

            migrationBuilder.DropColumn(
                name: "AddProductTablesProductId",
                table: "ProductImageTable");

            migrationBuilder.DropColumn(
                name: "City",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProductId",
                table: "ProductImageTable",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserCart_ProductId",
                table: "UserCart",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductImageTable_ProductId",
                table: "ProductImageTable",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductImageTable_AddProductTable_ProductId",
                table: "ProductImageTable",
                column: "ProductId",
                principalTable: "AddProductTable",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserCart_AddProductTable_ProductId",
                table: "UserCart",
                column: "ProductId",
                principalTable: "AddProductTable",
                principalColumn: "ProductId");
        }
    }
}
