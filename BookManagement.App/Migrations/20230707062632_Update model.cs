using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookManagement.App.Migrations
{
    /// <inheritdoc />
    public partial class Updatemodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsReturned",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "Pay",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "PricePerDay",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "ReturnDate",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "TotalBooks",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "TotalDate",
                table: "Bills");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsReturned",
                table: "Bills",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<long>(
                name: "Pay",
                table: "Bills",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "PricePerDay",
                table: "Bills",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<DateTime>(
                name: "ReturnDate",
                table: "Bills",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TotalBooks",
                table: "Bills",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TotalDate",
                table: "Bills",
                type: "int",
                nullable: true);
        }
    }
}
