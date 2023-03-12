using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RaffleKing.Migrations
{
    /// <inheritdoc />
    public partial class Addnewclasses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "profiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    U_Name = table.Column<string>(type: "TEXT", nullable: false),
                    U_UserNameShortCode = table.Column<string>(type: "TEXT", nullable: false),
                    U_Email = table.Column<string>(type: "TEXT", nullable: false),
                    U_Phone = table.Column<string>(type: "TEXT", nullable: false),
                    U_Password = table.Column<string>(type: "TEXT", nullable: false),
                    U_RoleType = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_profiles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "raffleDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RD_Raffle_Id = table.Column<int>(type: "INTEGER", nullable: false),
                    RD_User_Id = table.Column<int>(type: "INTEGER", nullable: false),
                    RD_Raffle_block = table.Column<int>(type: "INTEGER", nullable: false),
                    RD_BookedBy = table.Column<string>(type: "TEXT", nullable: false),
                    RD_Booked_Status = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_raffleDetails", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "profiles");

            migrationBuilder.DropTable(
                name: "raffleDetails");
        }
    }
}
