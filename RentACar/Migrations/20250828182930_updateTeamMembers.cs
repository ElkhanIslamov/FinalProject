using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentACar.Migrations
{
    /// <inheritdoc />
    public partial class updateTeamMembers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeamMembers_DirectorsBoards_DirectorsBoardId",
                table: "TeamMembers");

            migrationBuilder.DropIndex(
                name: "IX_TeamMembers_DirectorsBoardId",
                table: "TeamMembers");

            migrationBuilder.DropColumn(
                name: "DirectorsBoardId",
                table: "TeamMembers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DirectorsBoardId",
                table: "TeamMembers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TeamMembers_DirectorsBoardId",
                table: "TeamMembers",
                column: "DirectorsBoardId");

            migrationBuilder.AddForeignKey(
                name: "FK_TeamMembers_DirectorsBoards_DirectorsBoardId",
                table: "TeamMembers",
                column: "DirectorsBoardId",
                principalTable: "DirectorsBoards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
