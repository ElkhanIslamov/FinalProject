using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentACar.Migrations
{
    /// <inheritdoc />
    public partial class AddDirectorBoards : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "TeamMembers");

            migrationBuilder.AddColumn<int>(
                name: "DirectorsBoardId",
                table: "TeamMembers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "DirectorsBoards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BackgroundImage = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DirectorsBoards", x => x.Id);
                });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeamMembers_DirectorsBoards_DirectorsBoardId",
                table: "TeamMembers");

            migrationBuilder.DropTable(
                name: "DirectorsBoards");

            migrationBuilder.DropIndex(
                name: "IX_TeamMembers_DirectorsBoardId",
                table: "TeamMembers");

            migrationBuilder.DropColumn(
                name: "DirectorsBoardId",
                table: "TeamMembers");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "TeamMembers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
