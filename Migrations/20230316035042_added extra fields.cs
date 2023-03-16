using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RaffleKing.Migrations
{
    /// <inheritdoc />
    public partial class addedextrafields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "R_Active",
                table: "raffles",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "RD_Winners",
                table: "raffleDetails",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "R_Active",
                table: "raffles");

            migrationBuilder.DropColumn(
                name: "RD_Winners",
                table: "raffleDetails");
        }
    }
}
