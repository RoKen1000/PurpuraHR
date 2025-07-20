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
                values: new object[] { 1, "123 Some Street, Some Business Estate, London, ABC 123", new DateTime(2025, 7, 20, 13, 52, 18, 571, DateTimeKind.Local).AddTicks(109), null, "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis nec augue a arcu aliquam consequat a sit amet ante. Nullam eget tincidunt ante. Donec sed malesuada nibh. Cras rhoncus auctor lorem, vel ullamcorper ipsum egestas in. Cras lobortis justo enim, sed vulputate magna sagittis ac. Ut imperdiet sapien sed ante posuere porta. Praesent ultricies sagittis venenatis. Suspendisse potenti. Nulla viverra, mi ac pellentesque fringilla, purus tortor blandit enim, non lacinia augue lacus et felis.", "05595D26-131C-45EA-B78C-C4E912FC2438", "JLB Finance" });

            migrationBuilder.InsertData(
                table: "CompanyEmployees",
                columns: new[] { "Id", "CompanyId", "DateCreated", "DateEdited", "ExternalReference", "FirstName", "JobTitle", "LastName", "MiddleName" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2025, 7, 20, 13, 52, 18, 571, DateTimeKind.Local).AddTicks(314), null, "b68b8e2f-192d-4917-9741-5c22543c39f0", "Allan", "Chief Executive Officer", "Johnson", null },
                    { 2, 1, new DateTime(2025, 7, 20, 13, 52, 18, 571, DateTimeKind.Local).AddTicks(323), null, "51914932-fba6-4510-937f-720156736714", "Sophie", "Customer Service Representative", "Chapman", "Hortensia" },
                    { 3, 1, new DateTime(2025, 7, 20, 13, 52, 18, 571, DateTimeKind.Local).AddTicks(334), null, "32bd34b8-16ba-4224-b13e-7b5eb7bddaa0", "Mark", "Account Manager", "Corrigan", null },
                    { 4, 1, new DateTime(2025, 7, 20, 13, 52, 18, 571, DateTimeKind.Local).AddTicks(338), null, "5470cb2f-05f6-4ddb-ac11-de7a5d88c60b", "Gerrard", "Finance Auditor", "Matthew", null }
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
