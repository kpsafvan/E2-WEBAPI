using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WEBAPI_E2.Migrations
{
    /// <inheritdoc />
    public partial class ImageFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "Id",
                table: "Products",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ImageId",
                table: "Products",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_Id",
                table: "Products",
                column: "Id",
                unique: true,
                filter: "[Id] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Image_Id",
                table: "Products",
                column: "Id",
                principalTable: "Image",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Image_Id",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_Id",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "Products");
        }
    }
}
