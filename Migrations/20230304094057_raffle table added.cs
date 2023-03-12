using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RaffleKing.Migrations
{
    /// <inheritdoc />
    public partial class raffletableadded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "raffles",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    R_Title = table.Column<string>(type: "TEXT", nullable: false),
                    R_DrawnAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    R_TicketPrice = table.Column<double>(type: "REAL", nullable: false),
                    R_Total_Available = table.Column<int>(type: "INTEGER", nullable: false),
                    R_Total_Booked = table.Column<int>(type: "INTEGER", nullable: false),
                    R_ShortDecription = table.Column<string>(type: "TEXT", nullable: false),
                    R_FullDecription = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_raffles", x => x.ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "raffles");
        }
    }
}
