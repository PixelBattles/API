using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace PixelBattles.Server.DataStorage.Migrations
{
    public partial class Updated_Battle_RequiredName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Battle",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Battle_Name",
                table: "Battle",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Battle_Name",
                table: "Battle");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Battle",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
