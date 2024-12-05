using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Purpura.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddUserIdPropertyToAnnualLeave : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnnualLeave_AspNetUsers_UserId",
                table: "AnnualLeave");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "AnnualLeave",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AnnualLeave_AspNetUsers_UserId",
                table: "AnnualLeave",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnnualLeave_AspNetUsers_UserId",
                table: "AnnualLeave");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "AnnualLeave",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_AnnualLeave_AspNetUsers_UserId",
                table: "AnnualLeave",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
