using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoesApi.Migrations
{
    public partial class two : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserCart_AddProductTable_UserId",
                table: "UserCart");

            migrationBuilder.DropForeignKey(
                name: "FK_UserCart_AspNetUsers_AppUserId",
                table: "UserCart");

            migrationBuilder.DropIndex(
                name: "IX_UserCart_AppUserId",
                table: "UserCart");

            migrationBuilder.DropIndex(
                name: "IX_UserCart_UserId",
                table: "UserCart");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "UserCart");

            migrationBuilder.AddForeignKey(
                name: "FK_UserCart_AddProductTable_Id",
                table: "UserCart",
                column: "Id",
                principalTable: "AddProductTable",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserCart_AddProductTable_Id",
                table: "UserCart");

            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "UserCart",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserCart_AppUserId",
                table: "UserCart",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserCart_UserId",
                table: "UserCart",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserCart_AddProductTable_UserId",
                table: "UserCart",
                column: "UserId",
                principalTable: "AddProductTable",
                principalColumn: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserCart_AspNetUsers_AppUserId",
                table: "UserCart",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
