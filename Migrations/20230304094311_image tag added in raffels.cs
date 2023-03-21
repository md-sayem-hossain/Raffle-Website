using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RaffleKing.Migrations
{
    /// <inheritdoc />
    public partial class imagetagaddedinraffles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "R_MainImage",
                table: "raffles",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "R_ThumbnailImage",
                table: "raffles",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "R_MainImage",
                table: "raffles");

            migrationBuilder.DropColumn(
                name: "R_ThumbnailImage",
                table: "raffles");
        }
    }
}
