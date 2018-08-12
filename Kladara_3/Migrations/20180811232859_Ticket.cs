using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Kladara3.Migrations
{
    public partial class Ticket : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "PossibleGain",
                table: "Ticket",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "Bonus",
                table: "Ticket",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Ticket",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Bonus",
                table: "Ticket");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "Ticket");

            migrationBuilder.AlterColumn<int>(
                name: "PossibleGain",
                table: "Ticket",
                nullable: false,
                oldClrType: typeof(double));
        }
    }
}
