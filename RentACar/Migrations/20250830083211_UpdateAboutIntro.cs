using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentACar.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAboutIntro : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Heading",
                table: "AboutIntros",
                newName: "YearsExperienceLabel");

            migrationBuilder.RenameColumn(
                name: "Content",
                table: "AboutIntros",
                newName: "VehiclesFleetLabel");

            migrationBuilder.AddColumn<string>(
                name: "CompletedOrdersLabel",
                table: "AboutIntros",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "CompletedOrdersValue",
                table: "AboutIntros",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "AboutIntros",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "HappyCustomersLabel",
                table: "AboutIntros",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "HappyCustomersValue",
                table: "AboutIntros",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "AboutIntros",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "VehiclesFleetValue",
                table: "AboutIntros",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "YearsExperienceValue",
                table: "AboutIntros",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompletedOrdersLabel",
                table: "AboutIntros");

            migrationBuilder.DropColumn(
                name: "CompletedOrdersValue",
                table: "AboutIntros");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "AboutIntros");

            migrationBuilder.DropColumn(
                name: "HappyCustomersLabel",
                table: "AboutIntros");

            migrationBuilder.DropColumn(
                name: "HappyCustomersValue",
                table: "AboutIntros");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "AboutIntros");

            migrationBuilder.DropColumn(
                name: "VehiclesFleetValue",
                table: "AboutIntros");

            migrationBuilder.DropColumn(
                name: "YearsExperienceValue",
                table: "AboutIntros");

            migrationBuilder.RenameColumn(
                name: "YearsExperienceLabel",
                table: "AboutIntros",
                newName: "Heading");

            migrationBuilder.RenameColumn(
                name: "VehiclesFleetLabel",
                table: "AboutIntros",
                newName: "Content");
        }
    }
}
