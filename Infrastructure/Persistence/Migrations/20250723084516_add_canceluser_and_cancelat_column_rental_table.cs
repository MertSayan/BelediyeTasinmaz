using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class add_canceluser_and_cancelat_column_rental_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CancelAt",
                table: "Rentals",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CancelByUserId",
                table: "Rentals",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Rentals_CancelByUserId",
                table: "Rentals",
                column: "CancelByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rentals_Users_CancelByUserId",
                table: "Rentals",
                column: "CancelByUserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rentals_Users_CancelByUserId",
                table: "Rentals");

            migrationBuilder.DropIndex(
                name: "IX_Rentals_CancelByUserId",
                table: "Rentals");

            migrationBuilder.DropColumn(
                name: "CancelAt",
                table: "Rentals");

            migrationBuilder.DropColumn(
                name: "CancelByUserId",
                table: "Rentals");
        }
    }
}
