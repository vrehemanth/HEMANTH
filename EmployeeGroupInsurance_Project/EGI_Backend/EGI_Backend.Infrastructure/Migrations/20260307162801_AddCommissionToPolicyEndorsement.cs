using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EGI_Backend.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCommissionToPolicyEndorsement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("24c75336-6a13-47f2-bf31-3665a899afb7"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("2a764c81-a5dd-4a08-bea3-de1bd08a5d63"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("31f0f194-6138-4cd0-9573-79798957baab"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("342200e3-45db-44e7-9974-a933112eaf38"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("3614ab07-9bda-44f4-bc4d-64d9bc2147cd"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("413d2dcc-cb9f-4845-a1b5-c705e7d285d2"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("42ca29ae-6a97-4aed-94ab-0c4e9139b65b"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("4a0d1872-9524-46a2-91d7-fdff431eb8e9"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("4c876dc6-a0bc-4aac-b5f5-18549eb9a116"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("4c8ec6ef-dfe7-4f81-bb1e-bec06e1d5f25"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("51f7bdf0-7f56-4754-a42c-ebce0f463854"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("5b38aaa7-8662-4542-ba19-d1f4c47df48e"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("5e6e573a-2f81-4df8-a0f4-b163beeda8bb"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("64735a99-6db1-4706-a372-7060f70a4115"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("710ccd3a-9727-498c-80eb-5be04e064cac"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("7247780c-f84d-4d59-b748-b56981752a18"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("75d90d9b-99b1-4cde-bfce-2eecc7bc5592"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("8129c26e-217f-4c9c-abb4-302a22722504"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("9d9147f8-8c86-407f-b2e8-7b3aab27ebdb"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("a2ac23c7-6571-4896-9513-8fc75359751c"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("a769d4e5-f882-4792-a075-4ebfd7215545"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("bab685c0-604f-485a-bc90-0e18a52cbc91"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("c3a35b75-5f7a-40af-bbda-a6e30407fd5a"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("d2f357e1-b2ad-4516-84e1-f3797e8c6ced"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("f1cd9fb7-d173-43c3-bf10-c7535692a178"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("f4d1e431-ac52-434f-b2a9-669df6af9a6e"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("fbc4982d-47f8-4eae-a856-9f1593e61dd0"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("fcba9c4a-aecf-41fe-811d-04eb6d2bdab9"));

            migrationBuilder.AddColumn<decimal>(
                name: "CommissionAdjustment",
                table: "PolicyEndorsements",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 7, 16, 27, 59, 7, DateTimeKind.Utc).AddTicks(5370));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111112"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 7, 16, 27, 59, 7, DateTimeKind.Utc).AddTicks(5396));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111113"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 7, 16, 27, 59, 7, DateTimeKind.Utc).AddTicks(5402));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222221"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 7, 16, 27, 59, 7, DateTimeKind.Utc).AddTicks(5407));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 7, 16, 27, 59, 7, DateTimeKind.Utc).AddTicks(5427));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222223"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 7, 16, 27, 59, 7, DateTimeKind.Utc).AddTicks(5433));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222224"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 7, 16, 27, 59, 7, DateTimeKind.Utc).AddTicks(5438));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333331"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 7, 16, 27, 59, 7, DateTimeKind.Utc).AddTicks(5443));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333332"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 7, 16, 27, 59, 7, DateTimeKind.Utc).AddTicks(5447));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 7, 16, 27, 59, 7, DateTimeKind.Utc).AddTicks(5452));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333334"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 7, 16, 27, 59, 7, DateTimeKind.Utc).AddTicks(5456));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333335"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 7, 16, 27, 59, 7, DateTimeKind.Utc).AddTicks(5460));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444441"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 7, 16, 27, 59, 7, DateTimeKind.Utc).AddTicks(5468));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444442"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 7, 16, 27, 59, 7, DateTimeKind.Utc).AddTicks(5472));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444443"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 7, 16, 27, 59, 7, DateTimeKind.Utc).AddTicks(5477));

            migrationBuilder.InsertData(
                table: "PlanCoverages",
                columns: new[] { "Id", "CoverageAmount", "CoveredGroup", "InsurancePlanId", "IsActive", "Type" },
                values: new object[,]
                {
                    { new Guid("10c3a0fc-2496-447a-9438-d4f64085786b"), 300000m, 0, new Guid("22222222-2222-2222-2222-222222222221"), true, 0 },
                    { new Guid("1d5b6c82-6ba1-4284-a050-ac79a9f1280b"), 3000000m, 0, new Guid("33333333-3333-3333-3333-333333333335"), true, 2 },
                    { new Guid("2d8d13a5-3e73-4758-9087-6828cff282ee"), 2000000m, 0, new Guid("33333333-3333-3333-3333-333333333334"), true, 2 },
                    { new Guid("2e4900fd-1066-499a-aab7-0b11e8c4730e"), 3000000m, 0, new Guid("33333333-3333-3333-3333-333333333334"), true, 1 },
                    { new Guid("467598d4-b28e-4b16-821d-61bdb86df8e8"), 700000m, 1, new Guid("33333333-3333-3333-3333-333333333333"), true, 0 },
                    { new Guid("61fd69ec-1f0b-4d08-b2dd-df6aa5d62a1d"), 1500000m, 2, new Guid("33333333-3333-3333-3333-333333333335"), true, 0 },
                    { new Guid("69bfb53d-de57-43be-9c63-1cedcb522a4d"), 1500000m, 0, new Guid("22222222-2222-2222-2222-222222222224"), true, 1 },
                    { new Guid("765dadaf-16da-4a64-ab32-1f8b7b3c5f80"), 1000000m, 1, new Guid("33333333-3333-3333-3333-333333333334"), true, 0 },
                    { new Guid("7a9b9de3-5f56-4e84-bb92-ff5180e0824a"), 4000000m, 0, new Guid("44444444-4444-4444-4444-444444444443"), true, 2 },
                    { new Guid("82ea8bc0-cf25-446f-b2cf-79e4746f0d67"), 700000m, 1, new Guid("33333333-3333-3333-3333-333333333332"), true, 0 },
                    { new Guid("9131e02b-af67-44b7-9dd3-b6f76d18e85b"), 500000m, 1, new Guid("22222222-2222-2222-2222-222222222222"), true, 0 },
                    { new Guid("999cc802-5b26-492e-8c9d-0b1e2ffbd15a"), 300000m, 1, new Guid("11111111-1111-1111-1111-111111111113"), true, 0 },
                    { new Guid("9f6ab2f8-115b-4c34-8bba-2b7a5462ffae"), 700000m, 1, new Guid("22222222-2222-2222-2222-222222222224"), true, 0 },
                    { new Guid("9fdd1159-f4be-447c-b2e4-00befe1bea17"), 5000000m, 0, new Guid("44444444-4444-4444-4444-444444444443"), true, 1 },
                    { new Guid("a4269733-1db2-404f-8959-22d0ef0086aa"), 2000000m, 0, new Guid("33333333-3333-3333-3333-333333333333"), true, 1 },
                    { new Guid("a650b7ef-f42a-4160-b33e-100961d121f7"), 1000000m, 1, new Guid("44444444-4444-4444-4444-444444444442"), true, 0 },
                    { new Guid("ae45c386-23f1-4bf2-9e65-750034d03087"), 1000000m, 0, new Guid("22222222-2222-2222-2222-222222222223"), true, 1 },
                    { new Guid("ae5f89df-d13a-4bf2-8fcb-fa4fbfa6b6e8"), 500000m, 1, new Guid("22222222-2222-2222-2222-222222222223"), true, 0 },
                    { new Guid("b0c4fb21-004a-442d-973c-f5c9d6079497"), 500000m, 0, new Guid("33333333-3333-3333-3333-333333333335"), true, 3 },
                    { new Guid("c1688451-3aaf-49de-8fdb-af65916b8bab"), 500000m, 0, new Guid("11111111-1111-1111-1111-111111111113"), true, 1 },
                    { new Guid("c6bf6d08-593e-4988-9a9d-b995ad92f4c3"), 200000m, 0, new Guid("11111111-1111-1111-1111-111111111111"), true, 0 },
                    { new Guid("d102e081-fa12-4336-bb6d-a5f96d9f6e01"), 2000000m, 2, new Guid("44444444-4444-4444-4444-444444444443"), true, 0 },
                    { new Guid("e1d7d6c5-055d-4bbc-9781-bc76b4d7e9fc"), 500000m, 0, new Guid("33333333-3333-3333-3333-333333333331"), true, 0 },
                    { new Guid("e5edf4d8-3190-4e26-a37d-244e9a1602f8"), 4000000m, 0, new Guid("33333333-3333-3333-3333-333333333335"), true, 1 },
                    { new Guid("f66573cf-4b6e-44da-aac5-9f5f6c239485"), 300000m, 1, new Guid("11111111-1111-1111-1111-111111111112"), true, 0 },
                    { new Guid("f82f97c1-d57e-4880-ab80-382025d9843b"), 1000000m, 0, new Guid("22222222-2222-2222-2222-222222222224"), true, 2 },
                    { new Guid("fcb8816d-e99b-412c-bbaa-008e876c60be"), 700000m, 1, new Guid("44444444-4444-4444-4444-444444444441"), true, 0 },
                    { new Guid("ff5a1a5b-0baa-4bab-887c-91db24318e2a"), 3000000m, 0, new Guid("44444444-4444-4444-4444-444444444442"), true, 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("10c3a0fc-2496-447a-9438-d4f64085786b"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("1d5b6c82-6ba1-4284-a050-ac79a9f1280b"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("2d8d13a5-3e73-4758-9087-6828cff282ee"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("2e4900fd-1066-499a-aab7-0b11e8c4730e"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("467598d4-b28e-4b16-821d-61bdb86df8e8"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("61fd69ec-1f0b-4d08-b2dd-df6aa5d62a1d"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("69bfb53d-de57-43be-9c63-1cedcb522a4d"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("765dadaf-16da-4a64-ab32-1f8b7b3c5f80"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("7a9b9de3-5f56-4e84-bb92-ff5180e0824a"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("82ea8bc0-cf25-446f-b2cf-79e4746f0d67"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("9131e02b-af67-44b7-9dd3-b6f76d18e85b"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("999cc802-5b26-492e-8c9d-0b1e2ffbd15a"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("9f6ab2f8-115b-4c34-8bba-2b7a5462ffae"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("9fdd1159-f4be-447c-b2e4-00befe1bea17"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("a4269733-1db2-404f-8959-22d0ef0086aa"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("a650b7ef-f42a-4160-b33e-100961d121f7"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("ae45c386-23f1-4bf2-9e65-750034d03087"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("ae5f89df-d13a-4bf2-8fcb-fa4fbfa6b6e8"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("b0c4fb21-004a-442d-973c-f5c9d6079497"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("c1688451-3aaf-49de-8fdb-af65916b8bab"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("c6bf6d08-593e-4988-9a9d-b995ad92f4c3"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("d102e081-fa12-4336-bb6d-a5f96d9f6e01"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("e1d7d6c5-055d-4bbc-9781-bc76b4d7e9fc"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("e5edf4d8-3190-4e26-a37d-244e9a1602f8"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("f66573cf-4b6e-44da-aac5-9f5f6c239485"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("f82f97c1-d57e-4880-ab80-382025d9843b"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("fcb8816d-e99b-412c-bbaa-008e876c60be"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("ff5a1a5b-0baa-4bab-887c-91db24318e2a"));

            migrationBuilder.DropColumn(
                name: "CommissionAdjustment",
                table: "PolicyEndorsements");

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 5, 10, 45, 35, 698, DateTimeKind.Utc).AddTicks(3790));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111112"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 5, 10, 45, 35, 698, DateTimeKind.Utc).AddTicks(3806));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111113"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 5, 10, 45, 35, 698, DateTimeKind.Utc).AddTicks(3811));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222221"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 5, 10, 45, 35, 698, DateTimeKind.Utc).AddTicks(3815));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 5, 10, 45, 35, 698, DateTimeKind.Utc).AddTicks(3819));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222223"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 5, 10, 45, 35, 698, DateTimeKind.Utc).AddTicks(3822));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222224"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 5, 10, 45, 35, 698, DateTimeKind.Utc).AddTicks(3825));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333331"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 5, 10, 45, 35, 698, DateTimeKind.Utc).AddTicks(3838));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333332"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 5, 10, 45, 35, 698, DateTimeKind.Utc).AddTicks(3841));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 5, 10, 45, 35, 698, DateTimeKind.Utc).AddTicks(3844));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333334"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 5, 10, 45, 35, 698, DateTimeKind.Utc).AddTicks(3848));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333335"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 5, 10, 45, 35, 698, DateTimeKind.Utc).AddTicks(3851));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444441"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 5, 10, 45, 35, 698, DateTimeKind.Utc).AddTicks(3854));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444442"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 5, 10, 45, 35, 698, DateTimeKind.Utc).AddTicks(3857));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444443"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 5, 10, 45, 35, 698, DateTimeKind.Utc).AddTicks(3860));

            migrationBuilder.InsertData(
                table: "PlanCoverages",
                columns: new[] { "Id", "CoverageAmount", "CoveredGroup", "InsurancePlanId", "IsActive", "Type" },
                values: new object[,]
                {
                    { new Guid("24c75336-6a13-47f2-bf31-3665a899afb7"), 1000000m, 0, new Guid("22222222-2222-2222-2222-222222222223"), true, 1 },
                    { new Guid("2a764c81-a5dd-4a08-bea3-de1bd08a5d63"), 300000m, 1, new Guid("11111111-1111-1111-1111-111111111113"), true, 0 },
                    { new Guid("31f0f194-6138-4cd0-9573-79798957baab"), 500000m, 0, new Guid("33333333-3333-3333-3333-333333333331"), true, 0 },
                    { new Guid("342200e3-45db-44e7-9974-a933112eaf38"), 500000m, 0, new Guid("11111111-1111-1111-1111-111111111113"), true, 1 },
                    { new Guid("3614ab07-9bda-44f4-bc4d-64d9bc2147cd"), 4000000m, 0, new Guid("33333333-3333-3333-3333-333333333335"), true, 1 },
                    { new Guid("413d2dcc-cb9f-4845-a1b5-c705e7d285d2"), 2000000m, 2, new Guid("44444444-4444-4444-4444-444444444443"), true, 0 },
                    { new Guid("42ca29ae-6a97-4aed-94ab-0c4e9139b65b"), 200000m, 0, new Guid("11111111-1111-1111-1111-111111111111"), true, 0 },
                    { new Guid("4a0d1872-9524-46a2-91d7-fdff431eb8e9"), 1000000m, 0, new Guid("22222222-2222-2222-2222-222222222224"), true, 2 },
                    { new Guid("4c876dc6-a0bc-4aac-b5f5-18549eb9a116"), 1500000m, 0, new Guid("22222222-2222-2222-2222-222222222224"), true, 1 },
                    { new Guid("4c8ec6ef-dfe7-4f81-bb1e-bec06e1d5f25"), 5000000m, 0, new Guid("44444444-4444-4444-4444-444444444443"), true, 1 },
                    { new Guid("51f7bdf0-7f56-4754-a42c-ebce0f463854"), 3000000m, 0, new Guid("33333333-3333-3333-3333-333333333335"), true, 2 },
                    { new Guid("5b38aaa7-8662-4542-ba19-d1f4c47df48e"), 700000m, 1, new Guid("22222222-2222-2222-2222-222222222224"), true, 0 },
                    { new Guid("5e6e573a-2f81-4df8-a0f4-b163beeda8bb"), 500000m, 0, new Guid("33333333-3333-3333-3333-333333333335"), true, 3 },
                    { new Guid("64735a99-6db1-4706-a372-7060f70a4115"), 3000000m, 0, new Guid("44444444-4444-4444-4444-444444444442"), true, 1 },
                    { new Guid("710ccd3a-9727-498c-80eb-5be04e064cac"), 300000m, 1, new Guid("11111111-1111-1111-1111-111111111112"), true, 0 },
                    { new Guid("7247780c-f84d-4d59-b748-b56981752a18"), 3000000m, 0, new Guid("33333333-3333-3333-3333-333333333334"), true, 1 },
                    { new Guid("75d90d9b-99b1-4cde-bfce-2eecc7bc5592"), 700000m, 1, new Guid("33333333-3333-3333-3333-333333333333"), true, 0 },
                    { new Guid("8129c26e-217f-4c9c-abb4-302a22722504"), 500000m, 1, new Guid("22222222-2222-2222-2222-222222222223"), true, 0 },
                    { new Guid("9d9147f8-8c86-407f-b2e8-7b3aab27ebdb"), 300000m, 0, new Guid("22222222-2222-2222-2222-222222222221"), true, 0 },
                    { new Guid("a2ac23c7-6571-4896-9513-8fc75359751c"), 500000m, 1, new Guid("22222222-2222-2222-2222-222222222222"), true, 0 },
                    { new Guid("a769d4e5-f882-4792-a075-4ebfd7215545"), 2000000m, 0, new Guid("33333333-3333-3333-3333-333333333334"), true, 2 },
                    { new Guid("bab685c0-604f-485a-bc90-0e18a52cbc91"), 2000000m, 0, new Guid("33333333-3333-3333-3333-333333333333"), true, 1 },
                    { new Guid("c3a35b75-5f7a-40af-bbda-a6e30407fd5a"), 700000m, 1, new Guid("44444444-4444-4444-4444-444444444441"), true, 0 },
                    { new Guid("d2f357e1-b2ad-4516-84e1-f3797e8c6ced"), 700000m, 1, new Guid("33333333-3333-3333-3333-333333333332"), true, 0 },
                    { new Guid("f1cd9fb7-d173-43c3-bf10-c7535692a178"), 1500000m, 2, new Guid("33333333-3333-3333-3333-333333333335"), true, 0 },
                    { new Guid("f4d1e431-ac52-434f-b2a9-669df6af9a6e"), 4000000m, 0, new Guid("44444444-4444-4444-4444-444444444443"), true, 2 },
                    { new Guid("fbc4982d-47f8-4eae-a856-9f1593e61dd0"), 1000000m, 1, new Guid("33333333-3333-3333-3333-333333333334"), true, 0 },
                    { new Guid("fcba9c4a-aecf-41fe-811d-04eb6d2bdab9"), 1000000m, 1, new Guid("44444444-4444-4444-4444-444444444442"), true, 0 }
                });
        }
    }
}
