using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RaffleKing.Migrations
{
    /// <inheritdoc />
    public partial class addedcarttable2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Carts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    User_id = table.Column<int>(type: "INTEGER", nullable: false),
                    User_Email = table.Column<string>(type: "TEXT", nullable: false),
                    address = table.Column<string>(type: "TEXT", nullable: false),
                    country = table.Column<string>(type: "TEXT", nullable: false),
                    state = table.Column<string>(type: "TEXT", nullable: false),
                    zip = table.Column<string>(type: "TEXT", nullable: false),
                    cc_name = table.Column<string>(type: "TEXT", nullable: false),
                    cc_number = table.Column<string>(type: "TEXT", nullable: false),
                    cc_expiration = table.Column<string>(type: "TEXT", nullable: false),
                    cc_cvv = table.Column<string>(type: "TEXT", nullable: false),
                    blockid = table.Column<int>(type: "INTEGER", nullable: false),
                    raffleid = table.Column<int>(type: "INTEGER", nullable: false),
                    winnerName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carts", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Carts");
        }
    }
}
