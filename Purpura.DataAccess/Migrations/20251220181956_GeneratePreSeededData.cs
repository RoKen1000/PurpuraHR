using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Purpura.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class GeneratePreSeededData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "F9B314A8-3844-47B4-B9A5-CC3DB2DE35AE", null, "Manager", "MANAGER" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "AnnualLeaveDays", "CompanyId", "ConcurrencyStamp", "DateCreated", "DateEdited", "DateOfBirth", "Discriminator", "Email", "EmailConfirmed", "FirstName", "Gender", "LastName", "LockoutEnabled", "LockoutEnd", "MiddleName", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Title", "TwoFactorEnabled", "UserName" },
                values: new object[] { "18267BE3-DD54-45C4-8842-EEE2BAC13B3F", 0, "456 Some Flat, Some Building, London, EFG 456", 22, 1, "27B9DBA7-2167-4E33-8699-8CF850F7788F", new DateTime(2025, 10, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2025, 10, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "ApplicationUser", "joe@testuser.com", false, "Joe", 0, "Bloggs", true, null, null, "JOE@TESTUSER.COM", "JOE@TESTUSER.COM", "AQAAAAIAAYagAAAAEPlmXdK35aJhUN8CUUlPCQE0kZRpiBqCQI1cGUCvonoV0jC0MTUgLdiP3sVl3nug/Q==", "123456789", false, "9F317C5D-F614-4E5F-A30C-1DD85A685D6E", 0, false, "joe@testuser.com" });

            migrationBuilder.UpdateData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "ExternalReference" },
                values: new object[] { new DateTime(2025, 10, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "0AFA8D32-5A1F-4B32-9429-452A59523B27" });

            migrationBuilder.UpdateData(
                table: "CompanyEmployees",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "ExternalReference" },
                values: new object[] { new DateTime(2025, 10, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "64B9D90F-FADC-461D-96B8-C9467AF44894" });

            migrationBuilder.UpdateData(
                table: "CompanyEmployees",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateCreated", "ExternalReference" },
                values: new object[] { new DateTime(2025, 10, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "7CCEFDF3-89CF-4CFA-B641-1394EECB39D9" });

            migrationBuilder.UpdateData(
                table: "CompanyEmployees",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DateCreated", "ExternalReference" },
                values: new object[] { new DateTime(2025, 10, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "63454D95-3087-4681-A93A-6E2C8ED761F0" });

            migrationBuilder.UpdateData(
                table: "CompanyEmployees",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "DateCreated", "ExternalReference" },
                values: new object[] { new DateTime(2025, 10, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "57DA2F16-7990-4E6E-A47B-EC7949FB8B39" });

            migrationBuilder.InsertData(
                table: "AnnualLeave",
                columns: new[] { "Id", "DateCreated", "DateEdited", "Details", "EndDate", "ExternalReference", "StartDate", "Type", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 10, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, new DateTime(2025, 12, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "C06B1BA6-AB02-4B1D-A7D9-08F9A430844E", new DateTime(2025, 11, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "18267BE3-DD54-45C4-8842-EEE2BAC13B3F" },
                    { 2, new DateTime(2025, 10, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis nec augue a arcu aliquam consequat a sit amet ante. Nullam eget tincidunt ante. Donec sed malesuada nibh. Cras rhoncus auctor lorem, vel ullamcorper ipsum egestas in. Cras lobortis justo enim, sed vulputate magna sagittis ac. Ut imperdiet sapien sed ante posuere porta. Praesent ultricies sagittis venenatis. Suspendisse potenti. Nulla viverra, mi ac pellentesque fringilla, purus tortor blandit enim, non lacinia augue lacus et felis.", new DateTime(2025, 11, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "042005B1-AA6B-49CA-8AB5-32A291C3D2C4", new DateTime(2025, 12, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, "18267BE3-DD54-45C4-8842-EEE2BAC13B3F" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserClaims",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "UserId" },
                values: new object[] { 1, "CompanyReference", "0AFA8D32-5A1F-4B32-9429-452A59523B27", "18267BE3-DD54-45C4-8842-EEE2BAC13B3F" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "F9B314A8-3844-47B4-B9A5-CC3DB2DE35AE", "18267BE3-DD54-45C4-8842-EEE2BAC13B3F" });

            migrationBuilder.InsertData(
                table: "Goals",
                columns: new[] { "Id", "DateCreated", "DateEdited", "Description", "DueDate", "ExternalReference", "Name", "PercentageComplete", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 10, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis nec augue a arcu aliquam consequat a sit amet ante. Nullam eget tincidunt ante. Donec sed malesuada nibh. Cras rhoncus auctor lorem, vel ullamcorper ipsum egestas in. Cras lobortis justo enim, sed vulputate magna sagittis ac. Ut imperdiet sapien sed ante posuere porta. Praesent ultricies sagittis venenatis. Suspendisse potenti. Nulla viverra, mi ac pellentesque fringilla, purus tortor blandit enim, non lacinia augue lacus et felis.", new DateTime(2025, 11, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "4BF723F7-1BB7-4595-87A2-84F220882927", "Goal 1", 75, "18267BE3-DD54-45C4-8842-EEE2BAC13B3F" },
                    { 2, new DateTime(2025, 10, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis nec augue a arcu aliquam consequat a sit amet ante. Nullam eget tincidunt ante. Donec sed malesuada nibh. Cras rhoncus auctor lorem, vel ullamcorper ipsum egestas in. Cras lobortis justo enim, sed vulputate magna sagittis ac. Ut imperdiet sapien sed ante posuere porta. Praesent ultricies sagittis venenatis. Suspendisse potenti. Nulla viverra, mi ac pellentesque fringilla, purus tortor blandit enim, non lacinia augue lacus et felis.", new DateTime(2025, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "6BC4C3EE-5A44-4AA9-B9D7-DBE55597CB6B", "Goal 2", 30, "18267BE3-DD54-45C4-8842-EEE2BAC13B3F" },
                    { 3, new DateTime(2025, 10, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis nec augue a arcu aliquam consequat a sit amet ante. Nullam eget tincidunt ante. Donec sed malesuada nibh. Cras rhoncus auctor lorem, vel ullamcorper ipsum egestas in. Cras lobortis justo enim, sed vulputate magna sagittis ac. Ut imperdiet sapien sed ante posuere porta. Praesent ultricies sagittis venenatis. Suspendisse potenti. Nulla viverra, mi ac pellentesque fringilla, purus tortor blandit enim, non lacinia augue lacus et felis.", new DateTime(2025, 11, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "5441F99B-A8D7-4F73-A065-85935986749E", "Goal 3", 0, "18267BE3-DD54-45C4-8842-EEE2BAC13B3F" }
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
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "F9B314A8-3844-47B4-B9A5-CC3DB2DE35AE", "18267BE3-DD54-45C4-8842-EEE2BAC13B3F" });

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
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "F9B314A8-3844-47B4-B9A5-CC3DB2DE35AE");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "18267BE3-DD54-45C4-8842-EEE2BAC13B3F");

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
