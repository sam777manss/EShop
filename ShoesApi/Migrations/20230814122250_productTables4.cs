using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoesApi.Migrations
{
    public partial class productTables4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductImageTable_AddProductTable_AddProductTablesProductId",
                table: "ProductImageTable");

            migrationBuilder.DropIndex(
                name: "IX_ProductImageTable_AddProductTablesProductId",
                table: "ProductImageTable");

            migrationBuilder.DropColumn(
                name: "AddProductTablesProductId",
                table: "ProductImageTable");

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductImageTable_AddProductTable_ProductId",
                table: "ProductImageTable");

            migrationBuilder.DropIndex(
                name: "IX_ProductImageTable_ProductId",
                table: "ProductImageTable");

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

            migrationBuilder.CreateIndex(
                name: "IX_ProductImageTable_AddProductTablesProductId",
                table: "ProductImageTable",
                column: "AddProductTablesProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductImageTable_AddProductTable_AddProductTablesProductId",
                table: "ProductImageTable",
                column: "AddProductTablesProductId",
                principalTable: "AddProductTable",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
