using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentACar.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTeamMember : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "TeamMembers",
                newName: "FullName");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "TeamMembers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "TeamMembers");

            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "TeamMembers",
                newName: "Name");
        }
    }
}
