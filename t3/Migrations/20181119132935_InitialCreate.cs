using Microsoft.EntityFrameworkCore.Migrations;

namespace t3.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Game",
                columns: table => new
                {
                    gameID = table.Column<string>(nullable: false),
                    player1 = table.Column<string>(nullable: true),
                    player2 = table.Column<string>(nullable: true),
                    board = table.Column<string>(nullable: true),
                    watchers = table.Column<int>(nullable: false),
                    turn = table.Column<int>(nullable: false),
                    p1_timestamp = table.Column<long>(nullable: false),
                    p2_timestamp = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Game", x => x.gameID);
                });

            migrationBuilder.CreateTable(
                name: "Player",
                columns: table => new
                {
                    playerID = table.Column<string>(nullable: false),
                    playerName = table.Column<string>(nullable: true),
                    passcode = table.Column<string>(nullable: true),
                    mmr = table.Column<int>(nullable: false),
                    wins = table.Column<int>(nullable: false),
                    games = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Player", x => x.playerID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Game");

            migrationBuilder.DropTable(
                name: "Player");
        }
    }
}
