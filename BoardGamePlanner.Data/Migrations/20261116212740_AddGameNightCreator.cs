using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BoardGamePlanner.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddGameNightCreator : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatorId",
                table: "GameNights",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_GameNights_CreatorId",
                table: "GameNights",
                column: "CreatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_GameNights_AspNetUsers_CreatorId",
                table: "GameNights",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameNights_AspNetUsers_CreatorId",
                table: "GameNights");

            migrationBuilder.DropIndex(
                name: "IX_GameNights_CreatorId",
                table: "GameNights");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "GameNights");
        }
    }
}
