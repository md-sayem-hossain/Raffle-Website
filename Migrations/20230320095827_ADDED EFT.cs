using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RaffleKing.Migrations
{
    /// <inheritdoc />
    public partial class ADDEDEFT : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "eft_branch",
                table: "Carts",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "eft_name",
                table: "Carts",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "eft_number",
                table: "Carts",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "eft_reference",
                table: "Carts",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "eft_branch",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "eft_name",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "eft_number",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "eft_reference",
                table: "Carts");
        }
    }
}
