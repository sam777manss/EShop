using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoesApi.Migrations
{
    public partial class four : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserCart_AddProductTable_Id",
                table: "UserCart");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "UserCart",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserCart_ProductId",
                table: "UserCart",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserCart_AddProductTable_ProductId",
                table: "UserCart",
                column: "ProductId",
                principalTable: "AddProductTable",
                principalColumn: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserCart_AddProductTable_ProductId",
                table: "UserCart");

            migrationBuilder.DropIndex(
                name: "IX_UserCart_ProductId",
                table: "UserCart");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "UserCart",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UserCart_AddProductTable_Id",
                table: "UserCart",
                column: "Id",
                principalTable: "AddProductTable",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
