using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Purpura.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class RemoveCurrentDatePropertyFromAnnualLeave : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentDate",
                table: "AnnualLeave");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CurrentDate",
                table: "AnnualLeave",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
