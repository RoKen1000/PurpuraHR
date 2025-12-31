using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Purpura.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class FixAnnualLeaveStartDateEndDateErrorInSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AnnualLeave",
                keyColumn: "Id",
                keyValue: 2,
                column: "EndDate",
                value: new DateTime(2025, 12, 28, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AnnualLeave",
                keyColumn: "Id",
                keyValue: 2,
                column: "EndDate",
                value: new DateTime(2025, 11, 28, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
