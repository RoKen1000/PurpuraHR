using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Purpura.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class CreateCompanyAndCompanyEmployeeEntitiesAndTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateEdited = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ExternalReference = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CompanyEmployees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MiddleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JobTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateEdited = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ExternalReference = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyEmployees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyEmployees_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "Address", "DateCreated", "DateEdited", "Details", "ExternalReference", "Name" },
                values: new object[] { 1, "123 Some Street, Some Business Estate, London, ABC 123", new DateTime(2025, 7, 20, 12, 14, 6, 834, DateTimeKind.Local).AddTicks(7485), null, "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis nec augue a arcu aliquam consequat a sit amet ante. Nullam eget tincidunt ante. Donec sed malesuada nibh. Cras rhoncus auctor lorem, vel ullamcorper ipsum egestas in. Cras lobortis justo enim, sed vulputate magna sagittis ac. Ut imperdiet sapien sed ante posuere porta. Praesent ultricies sagittis venenatis. Suspendisse potenti. Nulla viverra, mi ac pellentesque fringilla, purus tortor blandit enim, non lacinia augue lacus et felis.", "af669137-20ef-4f2d-b585-72f07ea5105f", "JLB Finance" });

            migrationBuilder.InsertData(
                table: "CompanyEmployees",
                columns: new[] { "Id", "CompanyId", "DateCreated", "DateEdited", "ExternalReference", "FirstName", "JobTitle", "LastName", "MiddleName" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2025, 7, 20, 12, 14, 6, 834, DateTimeKind.Local).AddTicks(7697), null, "b72cfcc6-a66c-4fc5-817a-74993e28b84b", "Allan", "Chief Executive Officer", "Johnson", null },
                    { 2, 1, new DateTime(2025, 7, 20, 12, 14, 6, 834, DateTimeKind.Local).AddTicks(7703), null, "9acc105c-ddfa-4f08-9206-de2036d0dae7", "Sophie", "Customer Service Representative", "Chapman", "Hortensia" },
                    { 3, 1, new DateTime(2025, 7, 20, 12, 14, 6, 834, DateTimeKind.Local).AddTicks(7707), null, "376d8869-bbfb-4890-99e2-4bc3b20d04a5", "Mark", "Account Manager", "Corrigan", null },
                    { 4, 1, new DateTime(2025, 7, 20, 12, 14, 6, 834, DateTimeKind.Local).AddTicks(7710), null, "53ad7543-42bc-4084-bf1b-dedbc61d06e9", "Gerrard", "Finance Auditor", "Matthew", null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompanyEmployees_CompanyId",
                table: "CompanyEmployees",
                column: "CompanyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompanyEmployees");

            migrationBuilder.DropTable(
                name: "Companies");
        }
    }
}
