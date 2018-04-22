using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SoftwareHouse.DataAccess.Migrations
{
    public partial class addratingenum : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Feedback",
                table: "UserRatings",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<int>(
                name: "StarType",
                table: "UserRatings",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StarType",
                table: "UserRatings");

            migrationBuilder.AlterColumn<string>(
                name: "Feedback",
                table: "UserRatings",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
