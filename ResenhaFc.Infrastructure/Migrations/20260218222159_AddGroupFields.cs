using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResenhaFc.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddGroupFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BankAccountHolderName",
                table: "Groups",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BankName",
                table: "Groups",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CourtName",
                table: "Groups",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "EndTime",
                table: "Groups",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<string>(
                name: "FullAddress",
                table: "Groups",
                type: "nvarchar(400)",
                maxLength: 400,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "GameDate",
                table: "Groups",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsRecurring",
                table: "Groups",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "MonthlyFee",
                table: "Groups",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "PixKey",
                table: "Groups",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "PlayersLimitPerGame",
                table: "Groups",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "SingleGameFee",
                table: "Groups",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "StartTime",
                table: "Groups",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<string>(
                name: "VestColor",
                table: "Groups",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "WeekDay",
                table: "Groups",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BankAccountHolderName",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "BankName",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "CourtName",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "FullAddress",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "GameDate",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "IsRecurring",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "MonthlyFee",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "PixKey",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "PlayersLimitPerGame",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "SingleGameFee",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "VestColor",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "WeekDay",
                table: "Groups");
        }
    }
}
