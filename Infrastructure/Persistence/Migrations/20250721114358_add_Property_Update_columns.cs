using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class add_Property_Update_columns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Properties",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "UpdatedByUserId",
                table: "Properties",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Properties_UpdatedByUserId",
                table: "Properties",
                column: "UpdatedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_Users_UpdatedByUserId",
                table: "Properties",
                column: "UpdatedByUserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Properties_Users_UpdatedByUserId",
                table: "Properties");

            migrationBuilder.DropIndex(
                name: "IX_Properties_UpdatedByUserId",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserId",
                table: "Properties");
        }
    }
}
