using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Purpura.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddPreSeededUserWithData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "AnnualLeaveDays", "CompanyId", "ConcurrencyStamp", "DateCreated", "DateEdited", "DateOfBirth", "Discriminator", "Email", "EmailConfirmed", "FirstName", "Gender", "LastName", "LockoutEnabled", "LockoutEnd", "MiddleName", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Title", "TwoFactorEnabled", "UserName" },
                values: new object[] { "25e77f88-0505-4866-8a6a-18e07c7fbb29", 0, "456 Some Flat, Some Building, London, EFG 456", 22, 1, "58789c22f7b84f28b8e2317b0130f8a8", new DateTime(2025, 10, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2025, 10, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "ApplicationUser", "test@testuser.com", false, "Joe", 0, "Bloggs", true, null, null, "TEST@TESTUSER.COM", "TEST@TESTUSER.COM", "AQAAAAIAAYagAAAAEBtXzH1x9B2EIzi3v/t/ZzhUqK2udSEPZxxGXQn2PcjGSlJjOmdxaBNGeNbwieMK9A==", "123456789", false, "ff815ff5-e3f2-42a8-b28e-37da126ccc2e", 0, false, "test@testuser.com" });

            migrationBuilder.UpdateData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "ExternalReference" },
                values: new object[] { new DateTime(2025, 10, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "4904231c-7b04-4b23-8ee0-6fbac9c01cfa" });

            migrationBuilder.UpdateData(
                table: "CompanyEmployees",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "ExternalReference" },
                values: new object[] { new DateTime(2025, 10, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "12afa956-02bc-483e-8dee-cf6eca8a734f" });

            migrationBuilder.UpdateData(
                table: "CompanyEmployees",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateCreated", "ExternalReference" },
                values: new object[] { new DateTime(2025, 10, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "e65c010b-e07f-40e6-ad14-a91054338035" });

            migrationBuilder.UpdateData(
                table: "CompanyEmployees",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DateCreated", "ExternalReference" },
                values: new object[] { new DateTime(2025, 10, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "1a1b6e4e-87e5-4a34-8225-4c293da14a6e" });

            migrationBuilder.UpdateData(
                table: "CompanyEmployees",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "DateCreated", "ExternalReference" },
                values: new object[] { new DateTime(2025, 10, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "a96d3678-0e04-4886-a58c-fe0c257759c0" });

            migrationBuilder.InsertData(
                table: "AnnualLeave",
                columns: new[] { "Id", "DateCreated", "DateEdited", "Details", "EndDate", "ExternalReference", "StartDate", "Type", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 10, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, new DateTime(2025, 12, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "ca5587c8-ed77-4e7a-b297-9d7257d9c601", new DateTime(2025, 11, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "25e77f88-0505-4866-8a6a-18e07c7fbb29" },
                    { 2, new DateTime(2025, 10, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis nec augue a arcu aliquam consequat a sit amet ante. Nullam eget tincidunt ante. Donec sed malesuada nibh. Cras rhoncus auctor lorem, vel ullamcorper ipsum egestas in. Cras lobortis justo enim, sed vulputate magna sagittis ac. Ut imperdiet sapien sed ante posuere porta. Praesent ultricies sagittis venenatis. Suspendisse potenti. Nulla viverra, mi ac pellentesque fringilla, purus tortor blandit enim, non lacinia augue lacus et felis.", new DateTime(2025, 11, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "cb00af12-1e14-478b-b172-6403f3cb10be", new DateTime(2025, 12, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, "25e77f88-0505-4866-8a6a-18e07c7fbb29" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserClaims",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "UserId" },
                values: new object[] { 1, "CompanyReference", "4904231c-7b04-4b23-8ee0-6fbac9c01cfa", "25e77f88-0505-4866-8a6a-18e07c7fbb29" });

            migrationBuilder.InsertData(
                table: "Goals",
                columns: new[] { "Id", "DateCreated", "DateEdited", "Description", "DueDate", "ExternalReference", "Name", "PercentageComplete", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 10, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis nec augue a arcu aliquam consequat a sit amet ante. Nullam eget tincidunt ante. Donec sed malesuada nibh. Cras rhoncus auctor lorem, vel ullamcorper ipsum egestas in. Cras lobortis justo enim, sed vulputate magna sagittis ac. Ut imperdiet sapien sed ante posuere porta. Praesent ultricies sagittis venenatis. Suspendisse potenti. Nulla viverra, mi ac pellentesque fringilla, purus tortor blandit enim, non lacinia augue lacus et felis.", new DateTime(2025, 11, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "ab075746-f1c7-4a78-9c05-755b3d85ea0c", "Goal 1", 75, "25e77f88-0505-4866-8a6a-18e07c7fbb29" },
                    { 2, new DateTime(2025, 10, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis nec augue a arcu aliquam consequat a sit amet ante. Nullam eget tincidunt ante. Donec sed malesuada nibh. Cras rhoncus auctor lorem, vel ullamcorper ipsum egestas in. Cras lobortis justo enim, sed vulputate magna sagittis ac. Ut imperdiet sapien sed ante posuere porta. Praesent ultricies sagittis venenatis. Suspendisse potenti. Nulla viverra, mi ac pellentesque fringilla, purus tortor blandit enim, non lacinia augue lacus et felis.", new DateTime(2025, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "cfcd427a-88bd-45c8-a0bf-0b3260b6cbcf", "Goal 2", 30, "25e77f88-0505-4866-8a6a-18e07c7fbb29" },
                    { 3, new DateTime(2025, 10, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis nec augue a arcu aliquam consequat a sit amet ante. Nullam eget tincidunt ante. Donec sed malesuada nibh. Cras rhoncus auctor lorem, vel ullamcorper ipsum egestas in. Cras lobortis justo enim, sed vulputate magna sagittis ac. Ut imperdiet sapien sed ante posuere porta. Praesent ultricies sagittis venenatis. Suspendisse potenti. Nulla viverra, mi ac pellentesque fringilla, purus tortor blandit enim, non lacinia augue lacus et felis.", new DateTime(2025, 11, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "ebf1be0a-606c-4fca-9585-4930f6b13d98", "Goal 3", 0, "25e77f88-0505-4866-8a6a-18e07c7fbb29" }
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
                keyValue: "25e77f88-0505-4866-8a6a-18e07c7fbb29");

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
