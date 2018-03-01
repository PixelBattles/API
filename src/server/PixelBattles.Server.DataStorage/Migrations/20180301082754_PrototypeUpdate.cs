using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace PixelBattles.Server.DataStorage.Migrations
{
    public partial class PrototypeUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Game",
                columns: table => new
                {
                    GameId = table.Column<Guid>(nullable: false),
                    BattleId = table.Column<Guid>(nullable: false),
                    ChangeIndex = table.Column<int>(nullable: false),
                    Cooldown = table.Column<int>(nullable: false),
                    EndDateUTC = table.Column<DateTime>(nullable: false),
                    Height = table.Column<int>(nullable: false),
                    StartDateUTC = table.Column<DateTime>(nullable: false),
                    State = table.Column<byte[]>(nullable: true),
                    Width = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Game", x => x.GameId);
                    table.ForeignKey(
                        name: "FK_Game_Battle_BattleId",
                        column: x => x.BattleId,
                        principalTable: "Battle",
                        principalColumn: "BattleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Hub",
                columns: table => new
                {
                    HubId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hub", x => x.HubId);
                });

            migrationBuilder.CreateTable(
                name: "UserAction",
                columns: table => new
                {
                    GameId = table.Column<Guid>(nullable: false),
                    ChangeIndex = table.Column<int>(nullable: false),
                    Color = table.Column<int>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    XIndex = table.Column<int>(nullable: false),
                    YIndex = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAction", x => new { x.GameId, x.ChangeIndex });
                });

            migrationBuilder.CreateTable(
                name: "UserBattle",
                columns: table => new
                {
                    BattleId = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserBattle", x => new { x.BattleId, x.UserId });
                    table.ForeignKey(
                        name: "FK_UserBattle_Battle_BattleId",
                        column: x => x.BattleId,
                        principalTable: "Battle",
                        principalColumn: "BattleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Game_BattleId",
                table: "Game",
                column: "BattleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Game");

            migrationBuilder.DropTable(
                name: "Hub");

            migrationBuilder.DropTable(
                name: "UserAction");

            migrationBuilder.DropTable(
                name: "UserBattle");
        }
    }
}
