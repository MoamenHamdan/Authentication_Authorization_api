﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Authentication_Authorization_api.Migrations
{
    /// <inheritdoc />
    public partial class Addattributesfortheuser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateOnly>(
                name: "DOB",
                table: "AspNetUsers",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "AspNetUsers",
                type: "nvarchar(7)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LibraryID",
                table: "AspNetUsers",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DOB",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LibraryID",
                table: "AspNetUsers");
        }
    }
}
