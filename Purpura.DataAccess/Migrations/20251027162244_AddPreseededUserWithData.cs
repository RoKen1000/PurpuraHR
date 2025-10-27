using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Purpura.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddPreseededUserWithData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "AnnualLeaveDays", "CompanyId", "ConcurrencyStamp", "DateCreated", "DateEdited", "DateOfBirth", "Discriminator", "Email", "EmailConfirmed", "FirstName", "Gender", "LastName", "LockoutEnabled", "LockoutEnd", "MiddleName", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Title", "TwoFactorEnabled", "UserName" },
                values: new object[] { "76e3fd12-3bfb-4b17-8e9c-d423bc4db479", 0, "456 Some Flat, Some Building, London, EFG 456", 22, 1, "547882807f6b46178371d130e78ad82f", new DateTime(2025, 10, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2025, 10, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "ApplicationUser", "joe@testuser.com", false, "Joe", 0, "Bloggs", true, null, null, "JOE@TESTUSER.COM", "JOE@TESTUSER.COM", "AQAAAAIAAYagAAAAEMDLGEfJC5d1G0oUxl/+O1sEv5+rHV+q2zEIdSq+7YLPHPFpZBWVtdfZJupJjb2v7g==", "123456789", false, "c8973de7-c6fe-4e96-a302-4ef7e5a1ab3a", 0, false, "joe@testuser.com" });

            migrationBuilder.UpdateData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "ExternalReference" },
                values: new object[] { new DateTime(2025, 10, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "8ef56a7d-3680-4ade-a318-f622eed03e0f" });

            migrationBuilder.UpdateData(
                table: "CompanyEmployees",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "ExternalReference" },
                values: new object[] { new DateTime(2025, 10, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "d3a90eeb-f595-499e-9922-259a23415d1a" });

            migrationBuilder.UpdateData(
                table: "CompanyEmployees",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateCreated", "ExternalReference" },
                values: new object[] { new DateTime(2025, 10, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "fa51f9f1-d340-44e3-88f1-a3c70791b212" });

            migrationBuilder.UpdateData(
                table: "CompanyEmployees",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DateCreated", "ExternalReference" },
                values: new object[] { new DateTime(2025, 10, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "ef4b775e-7e2e-4f67-a0f7-5b22448af2ac" });

            migrationBuilder.UpdateData(
                table: "CompanyEmployees",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "DateCreated", "ExternalReference" },
                values: new object[] { new DateTime(2025, 10, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "6975061e-bef8-447d-9018-236c85983edd" });

            migrationBuilder.InsertData(
                table: "AnnualLeave",
                columns: new[] { "Id", "DateCreated", "DateEdited", "Details", "EndDate", "ExternalReference", "StartDate", "Type", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 10, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, new DateTime(2025, 12, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "7ee66d63-c023-464b-b9e6-843b3b572b7c", new DateTime(2025, 11, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "76e3fd12-3bfb-4b17-8e9c-d423bc4db479" },
                    { 2, new DateTime(2025, 10, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis nec augue a arcu aliquam consequat a sit amet ante. Nullam eget tincidunt ante. Donec sed malesuada nibh. Cras rhoncus auctor lorem, vel ullamcorper ipsum egestas in. Cras lobortis justo enim, sed vulputate magna sagittis ac. Ut imperdiet sapien sed ante posuere porta. Praesent ultricies sagittis venenatis. Suspendisse potenti. Nulla viverra, mi ac pellentesque fringilla, purus tortor blandit enim, non lacinia augue lacus et felis.", new DateTime(2025, 11, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "5b7b7216-47e0-414c-9c1f-9b3f23e1864d", new DateTime(2025, 12, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, "76e3fd12-3bfb-4b17-8e9c-d423bc4db479" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserClaims",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "UserId" },
                values: new object[] { 1, "CompanyReference", "8ef56a7d-3680-4ade-a318-f622eed03e0f", "76e3fd12-3bfb-4b17-8e9c-d423bc4db479" });

            migrationBuilder.InsertData(
                table: "Goals",
                columns: new[] { "Id", "DateCreated", "DateEdited", "Description", "DueDate", "ExternalReference", "Name", "PercentageComplete", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 10, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis nec augue a arcu aliquam consequat a sit amet ante. Nullam eget tincidunt ante. Donec sed malesuada nibh. Cras rhoncus auctor lorem, vel ullamcorper ipsum egestas in. Cras lobortis justo enim, sed vulputate magna sagittis ac. Ut imperdiet sapien sed ante posuere porta. Praesent ultricies sagittis venenatis. Suspendisse potenti. Nulla viverra, mi ac pellentesque fringilla, purus tortor blandit enim, non lacinia augue lacus et felis.", new DateTime(2025, 11, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "89fb78d1-2d15-4985-bc50-6922a75c5a97", "Goal 1", 75, "76e3fd12-3bfb-4b17-8e9c-d423bc4db479" },
                    { 2, new DateTime(2025, 10, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis nec augue a arcu aliquam consequat a sit amet ante. Nullam eget tincidunt ante. Donec sed malesuada nibh. Cras rhoncus auctor lorem, vel ullamcorper ipsum egestas in. Cras lobortis justo enim, sed vulputate magna sagittis ac. Ut imperdiet sapien sed ante posuere porta. Praesent ultricies sagittis venenatis. Suspendisse potenti. Nulla viverra, mi ac pellentesque fringilla, purus tortor blandit enim, non lacinia augue lacus et felis.", new DateTime(2025, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "4ba5e837-ba4a-4e7b-9599-52c295a5000d", "Goal 2", 30, "76e3fd12-3bfb-4b17-8e9c-d423bc4db479" },
                    { 3, new DateTime(2025, 10, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis nec augue a arcu aliquam consequat a sit amet ante. Nullam eget tincidunt ante. Donec sed malesuada nibh. Cras rhoncus auctor lorem, vel ullamcorper ipsum egestas in. Cras lobortis justo enim, sed vulputate magna sagittis ac. Ut imperdiet sapien sed ante posuere porta. Praesent ultricies sagittis venenatis. Suspendisse potenti. Nulla viverra, mi ac pellentesque fringilla, purus tortor blandit enim, non lacinia augue lacus et felis.", new DateTime(2025, 11, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "0441a8b1-2778-475b-99aa-3cd5bbdb16c2", "Goal 3", 0, "76e3fd12-3bfb-4b17-8e9c-d423bc4db479" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AnnualLeave",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AnnualLeave",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Goals",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Goals",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Goals",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "76e3fd12-3bfb-4b17-8e9c-d423bc4db479");

            migrationBuilder.UpdateData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "ExternalReference" },
                values: new object[] { new DateTime(2025, 8, 4, 16, 32, 19, 964, DateTimeKind.Local).AddTicks(1007), "05595D26-131C-45EA-B78C-C4E912FC2438" });

            migrationBuilder.UpdateData(
                table: "CompanyEmployees",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "ExternalReference" },
                values: new object[] { new DateTime(2025, 8, 4, 16, 32, 19, 964, DateTimeKind.Local).AddTicks(1213), "5e53e0b1-69fb-4259-8806-bdc410ac330b" });

            migrationBuilder.UpdateData(
                table: "CompanyEmployees",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateCreated", "ExternalReference" },
                values: new object[] { new DateTime(2025, 8, 4, 16, 32, 19, 964, DateTimeKind.Local).AddTicks(1220), "83ce888e-1d39-47de-a851-7863ba6a6db7" });

            migrationBuilder.UpdateData(
                table: "CompanyEmployees",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DateCreated", "ExternalReference" },
                values: new object[] { new DateTime(2025, 8, 4, 16, 32, 19, 964, DateTimeKind.Local).AddTicks(1223), "ea654ce9-9a3e-4ed5-b9a0-b91179e71a36" });

            migrationBuilder.UpdateData(
                table: "CompanyEmployees",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "DateCreated", "ExternalReference" },
                values: new object[] { new DateTime(2025, 8, 4, 16, 32, 19, 964, DateTimeKind.Local).AddTicks(1227), "5416dd9d-85da-4f8a-b664-ef63f5db491a" });
        }
    }
}
