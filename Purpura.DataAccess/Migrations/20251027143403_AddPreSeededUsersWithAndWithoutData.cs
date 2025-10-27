using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Purpura.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddPreSeededUsersWithAndWithoutData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "AnnualLeaveDays", "CompanyId", "ConcurrencyStamp", "DateCreated", "DateEdited", "DateOfBirth", "Discriminator", "Email", "EmailConfirmed", "FirstName", "Gender", "LastName", "LockoutEnabled", "LockoutEnd", "MiddleName", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Title", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "85309188-6438-415b-bc87-57358ba9304f", 0, "456 Some Flat, Some Building, London, EFG 456", 22, 1, "d9a45ebb08fc420cad9cddfa1b98097d", new DateTime(2025, 10, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2025, 10, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "ApplicationUser", "joe@testuser.com", false, "Joe", 0, "Bloggs", true, null, null, "JOE@TESTUSER.COM", "JOE@TESTUSER.COM", "AQAAAAIAAYagAAAAEBDBF3HuAvIaFX1IkJzG0Can1yxHOGn5KgZ++TBk7rJGmppitLnPpQPw0cBi6kAF9Q==", "123456789", false, "c971bb39-e9ae-4947-9c33-8a560f1c4044", 0, false, "joe@testuser.com" },
                    { "906fe7a4-dc15-41e8-9124-21e59a1625ee", 0, "456 Some Flat, Some Building, London, EFG 456", 28, null, "7bd1a7f99dfe45ab8abe868c4d939b8f", new DateTime(2025, 10, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2025, 10, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "ApplicationUser", "john@testuser.com", false, "John", 0, "Doe", true, null, null, "JOHN@TESTUSER.COM", "JOHN@TESTUSER.COM", "AQAAAAIAAYagAAAAEP356a1BaUS0raAGc/zj4L44n7yX99bF2q2QRchJeCYDdh1ByKtvCVwoS+dwTkskDg==", "123456789", false, "d3c5b534-abbe-4c62-a3bb-ef3c6d09c09f", 0, false, "john@testuser.com" }
                });

            migrationBuilder.UpdateData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "ExternalReference" },
                values: new object[] { new DateTime(2025, 10, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "1b982125-5ebb-47db-a9c8-3f81a4641ee4" });

            migrationBuilder.UpdateData(
                table: "CompanyEmployees",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "ExternalReference" },
                values: new object[] { new DateTime(2025, 10, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "2614a97e-48e9-44f7-9a43-f58f7eca4532" });

            migrationBuilder.UpdateData(
                table: "CompanyEmployees",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateCreated", "ExternalReference" },
                values: new object[] { new DateTime(2025, 10, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "ccda355b-e06f-4394-b512-fefaa2f17737" });

            migrationBuilder.UpdateData(
                table: "CompanyEmployees",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DateCreated", "ExternalReference" },
                values: new object[] { new DateTime(2025, 10, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "cbd249d8-9eee-4e05-a8c7-ece2d523a1b4" });

            migrationBuilder.UpdateData(
                table: "CompanyEmployees",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "DateCreated", "ExternalReference" },
                values: new object[] { new DateTime(2025, 10, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "745f6a36-d62f-4d46-96d7-6da2b46f4a58" });

            migrationBuilder.InsertData(
                table: "AnnualLeave",
                columns: new[] { "Id", "DateCreated", "DateEdited", "Details", "EndDate", "ExternalReference", "StartDate", "Type", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 10, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, new DateTime(2025, 12, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "6af073b5-7e6f-409d-bcdd-3b77155b7eb5", new DateTime(2025, 11, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "85309188-6438-415b-bc87-57358ba9304f" },
                    { 2, new DateTime(2025, 10, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis nec augue a arcu aliquam consequat a sit amet ante. Nullam eget tincidunt ante. Donec sed malesuada nibh. Cras rhoncus auctor lorem, vel ullamcorper ipsum egestas in. Cras lobortis justo enim, sed vulputate magna sagittis ac. Ut imperdiet sapien sed ante posuere porta. Praesent ultricies sagittis venenatis. Suspendisse potenti. Nulla viverra, mi ac pellentesque fringilla, purus tortor blandit enim, non lacinia augue lacus et felis.", new DateTime(2025, 11, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "764e9290-59b8-48da-81d9-3f83bbc0dcea", new DateTime(2025, 12, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, "85309188-6438-415b-bc87-57358ba9304f" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserClaims",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "UserId" },
                values: new object[] { 1, "CompanyReference", "1b982125-5ebb-47db-a9c8-3f81a4641ee4", "85309188-6438-415b-bc87-57358ba9304f" });

            migrationBuilder.InsertData(
                table: "Goals",
                columns: new[] { "Id", "DateCreated", "DateEdited", "Description", "DueDate", "ExternalReference", "Name", "PercentageComplete", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 10, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis nec augue a arcu aliquam consequat a sit amet ante. Nullam eget tincidunt ante. Donec sed malesuada nibh. Cras rhoncus auctor lorem, vel ullamcorper ipsum egestas in. Cras lobortis justo enim, sed vulputate magna sagittis ac. Ut imperdiet sapien sed ante posuere porta. Praesent ultricies sagittis venenatis. Suspendisse potenti. Nulla viverra, mi ac pellentesque fringilla, purus tortor blandit enim, non lacinia augue lacus et felis.", new DateTime(2025, 11, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "8d8b5cdc-21d4-412d-a12d-1b262b85b922", "Goal 1", 75, "85309188-6438-415b-bc87-57358ba9304f" },
                    { 2, new DateTime(2025, 10, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis nec augue a arcu aliquam consequat a sit amet ante. Nullam eget tincidunt ante. Donec sed malesuada nibh. Cras rhoncus auctor lorem, vel ullamcorper ipsum egestas in. Cras lobortis justo enim, sed vulputate magna sagittis ac. Ut imperdiet sapien sed ante posuere porta. Praesent ultricies sagittis venenatis. Suspendisse potenti. Nulla viverra, mi ac pellentesque fringilla, purus tortor blandit enim, non lacinia augue lacus et felis.", new DateTime(2025, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "d4237405-b1f4-4e74-8ad5-04def0437143", "Goal 2", 30, "85309188-6438-415b-bc87-57358ba9304f" },
                    { 3, new DateTime(2025, 10, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis nec augue a arcu aliquam consequat a sit amet ante. Nullam eget tincidunt ante. Donec sed malesuada nibh. Cras rhoncus auctor lorem, vel ullamcorper ipsum egestas in. Cras lobortis justo enim, sed vulputate magna sagittis ac. Ut imperdiet sapien sed ante posuere porta. Praesent ultricies sagittis venenatis. Suspendisse potenti. Nulla viverra, mi ac pellentesque fringilla, purus tortor blandit enim, non lacinia augue lacus et felis.", new DateTime(2025, 11, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "912c9ea4-fa90-4b34-922f-54df9e5075cc", "Goal 3", 0, "85309188-6438-415b-bc87-57358ba9304f" }
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
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "906fe7a4-dc15-41e8-9124-21e59a1625ee");

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
                keyValue: "85309188-6438-415b-bc87-57358ba9304f");

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
