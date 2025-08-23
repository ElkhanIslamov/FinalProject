using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentACar.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCarImages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarImages_Cars_CarId",
                table: "CarImages");

            migrationBuilder.AlterColumn<int>(
                name: "CarId",
                table: "CarImages",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "CarImageId",
                table: "CarImages",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "CarImages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_CarImages_CarImageId",
                table: "CarImages",
                column: "CarImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_CarImages_CarImages_CarImageId",
                table: "CarImages",
                column: "CarImageId",
                principalTable: "CarImages",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CarImages_Cars_CarId",
                table: "CarImages",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarImages_CarImages_CarImageId",
                table: "CarImages");

            migrationBuilder.DropForeignKey(
                name: "FK_CarImages_Cars_CarId",
                table: "CarImages");

            migrationBuilder.DropIndex(
                name: "IX_CarImages_CarImageId",
                table: "CarImages");

            migrationBuilder.DropColumn(
                name: "CarImageId",
                table: "CarImages");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "CarImages");

            migrationBuilder.AlterColumn<int>(
                name: "CarId",
                table: "CarImages",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CarImages_Cars_CarId",
                table: "CarImages",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
