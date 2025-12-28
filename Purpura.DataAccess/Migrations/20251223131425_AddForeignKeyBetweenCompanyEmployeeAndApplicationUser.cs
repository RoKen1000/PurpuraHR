using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Purpura.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddForeignKeyBetweenCompanyEmployeeAndApplicationUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "CompanyEmployees",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "CompanyEmployees",
                keyColumn: "Id",
                keyValue: 1,
                column: "ApplicationUserId",
                value: null);

            migrationBuilder.UpdateData(
                table: "CompanyEmployees",
                keyColumn: "Id",
                keyValue: 2,
                column: "ApplicationUserId",
                value: null);

            migrationBuilder.UpdateData(
                table: "CompanyEmployees",
                keyColumn: "Id",
                keyValue: 3,
                column: "ApplicationUserId",
                value: null);

            migrationBuilder.UpdateData(
                table: "CompanyEmployees",
                keyColumn: "Id",
                keyValue: 4,
                column: "ApplicationUserId",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_CompanyEmployees_ApplicationUserId",
                table: "CompanyEmployees",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyEmployees_AspNetUsers_ApplicationUserId",
                table: "CompanyEmployees",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompanyEmployees_AspNetUsers_ApplicationUserId",
                table: "CompanyEmployees");

            migrationBuilder.DropIndex(
                name: "IX_CompanyEmployees_ApplicationUserId",
                table: "CompanyEmployees");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "CompanyEmployees");
        }
    }
}
