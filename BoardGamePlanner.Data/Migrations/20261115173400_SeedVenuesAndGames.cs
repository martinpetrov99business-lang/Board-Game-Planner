using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BoardGamePlanner.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedVenuesAndGames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Games",
                columns: new[] { "Id", "Complexity", "Description", "ImageUrl", "MaxPlayers", "MinPlayers", "PlayTimeMinutes", "Title" },
                values: new object[,]
                {
                    { 1, 2, "Trade and build settlements on a modular island.", null, 4, 3, 90, "Catan" },
                    { 2, 1, "Collect train cards and connect cities by building railway routes across the map.", null, 5, 2, 60, "Ticket to Ride" },
                    { 3, 1, "Build a shared landscape with tiles and place followers to score points.", null, 5, 2, 45, "Carcassonne" },
                    { 4, 2, "Work together as a team of specialists to contain disease outbreaks around the world.", null, 4, 2, 60, "Pandemic" },
                    { 5, 3, "Manage resources and develop projects to make Mars suitable for human life.", null, 5, 1, 120, "Terraforming Mars" },
                    { 6, 2, "Compete in a strategic battle by moving pieces across the board to checkmate your opponent.", null, 2, 2, 30, "Chess" },
                    { 7, 3, "Explore a fantasy world through tactical cooperative adventures and evolving campaigns.", null, 4, 1, 120, "Gloomhaven" }
                });

            migrationBuilder.InsertData(
                table: "Venues",
                columns: new[] { "Id", "Address", "Capacity", "Name" },
                values: new object[,]
                {
                    { 1, "12 Main Street, Downtown", 30, "The Dice Tower Café" },
                    { 2, "25 Oak Street, City Center", 50, "Board Game Paradise" },
                    { 3, "8 River Road, West District", 40, "The Gaming Hub" },
                    { 4, "17 Park Avenue, East District", 25, "Meeple House" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
