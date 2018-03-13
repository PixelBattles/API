using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace PixelBattles.Server.DataStorage.Migrations
{
    public partial class Updated_Hub_UniqueIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Hub",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Hub_Name",
                table: "Hub",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Hub_Name",
                table: "Hub");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Hub",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
