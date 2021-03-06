﻿using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SoftwareHouse.DataAccess.Migrations
{
    public partial class fixesnavproperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "UserRatings",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserRatings_ApplicationUserId",
                table: "UserRatings",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserRatings_AspNetUsers_ApplicationUserId",
                table: "UserRatings",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserRatings_AspNetUsers_ApplicationUserId",
                table: "UserRatings");

            migrationBuilder.DropIndex(
                name: "IX_UserRatings_ApplicationUserId",
                table: "UserRatings");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "UserRatings");
        }
    }
}
