using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Purpura.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddEmailPropertyToCompanyEmployeeAndUpdateSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "CompanyEmployees",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "CompanyEmployees",
                keyColumn: "Id",
                keyValue: 1,
                column: "Email",
                value: "allan@jlb.com");

            migrationBuilder.UpdateData(
                table: "CompanyEmployees",
                keyColumn: "Id",
                keyValue: 2,
                column: "Email",
                value: "sophie@jlb.com");

            migrationBuilder.UpdateData(
                table: "CompanyEmployees",
                keyColumn: "Id",
                keyValue: 3,
                column: "Email",
                value: "mark@jlb.com");

            migrationBuilder.UpdateData(
                table: "CompanyEmployees",
                keyColumn: "Id",
                keyValue: 4,
                column: "Email",
                value: "gerrard@jlb.com");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "CompanyEmployees");
        }
    }
}
