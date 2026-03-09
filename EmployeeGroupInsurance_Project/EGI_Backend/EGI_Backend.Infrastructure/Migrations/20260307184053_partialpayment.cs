using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EGI_Backend.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class partialpayment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<decimal>(
                name: "TotalPaid",
                table: "Invoices",
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
                value: new DateTime(2026, 3, 7, 18, 40, 51, 30, DateTimeKind.Utc).AddTicks(7944));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111112"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 7, 18, 40, 51, 30, DateTimeKind.Utc).AddTicks(7980));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111113"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 7, 18, 40, 51, 30, DateTimeKind.Utc).AddTicks(7988));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222221"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 7, 18, 40, 51, 30, DateTimeKind.Utc).AddTicks(7994));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 7, 18, 40, 51, 30, DateTimeKind.Utc).AddTicks(8001));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222223"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 7, 18, 40, 51, 30, DateTimeKind.Utc).AddTicks(8007));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222224"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 7, 18, 40, 51, 30, DateTimeKind.Utc).AddTicks(8013));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333331"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 7, 18, 40, 51, 30, DateTimeKind.Utc).AddTicks(8018));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333332"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 7, 18, 40, 51, 30, DateTimeKind.Utc).AddTicks(8029));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 7, 18, 40, 51, 30, DateTimeKind.Utc).AddTicks(8034));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333334"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 7, 18, 40, 51, 30, DateTimeKind.Utc).AddTicks(8040));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333335"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 7, 18, 40, 51, 30, DateTimeKind.Utc).AddTicks(8046));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444441"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 7, 18, 40, 51, 30, DateTimeKind.Utc).AddTicks(8051));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444442"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 7, 18, 40, 51, 30, DateTimeKind.Utc).AddTicks(8057));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444443"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 7, 18, 40, 51, 30, DateTimeKind.Utc).AddTicks(8062));

            migrationBuilder.InsertData(
                table: "PlanCoverages",
                columns: new[] { "Id", "CoverageAmount", "CoveredGroup", "InsurancePlanId", "IsActive", "Type" },
                values: new object[,]
                {
                    { new Guid("00778796-9dbb-41b1-89cb-dcda30683f22"), 1500000m, 0, new Guid("22222222-2222-2222-2222-222222222224"), true, 1 },
                    { new Guid("1a02d236-8615-4cf9-aa10-f01496e8fb8b"), 1000000m, 0, new Guid("22222222-2222-2222-2222-222222222223"), true, 1 },
                    { new Guid("1cad7e58-1fb4-4fc4-8af6-c8ac7b022a65"), 300000m, 0, new Guid("22222222-2222-2222-2222-222222222221"), true, 0 },
                    { new Guid("1f9eebe2-88cb-49d4-83d5-bfd2d250e38b"), 2000000m, 0, new Guid("33333333-3333-3333-3333-333333333333"), true, 1 },
                    { new Guid("2a3d0f0a-c90c-4e7f-b768-5f0928ce3681"), 1500000m, 2, new Guid("33333333-3333-3333-3333-333333333335"), true, 0 },
                    { new Guid("31512304-2a75-49d1-baae-9f837a5835f6"), 200000m, 0, new Guid("11111111-1111-1111-1111-111111111111"), true, 0 },
                    { new Guid("4e89b12a-bd9a-4248-b354-6055a3f36d20"), 2000000m, 2, new Guid("44444444-4444-4444-4444-444444444443"), true, 0 },
                    { new Guid("5cd758ae-a760-454e-9056-42bf416c8197"), 700000m, 1, new Guid("33333333-3333-3333-3333-333333333332"), true, 0 },
                    { new Guid("787e2b69-d0c5-4d0b-b52b-4840a884feb7"), 300000m, 1, new Guid("11111111-1111-1111-1111-111111111112"), true, 0 },
                    { new Guid("887a0c9d-44f6-494d-8bc6-b318a10817e2"), 3000000m, 0, new Guid("44444444-4444-4444-4444-444444444442"), true, 1 },
                    { new Guid("8a6d204f-a598-4d5c-ba9a-dd4b2e10667f"), 3000000m, 0, new Guid("33333333-3333-3333-3333-333333333334"), true, 1 },
                    { new Guid("8a8fb0d2-49ee-4953-acba-ed87d72029fb"), 500000m, 0, new Guid("33333333-3333-3333-3333-333333333335"), true, 3 },
                    { new Guid("8b654bce-11c2-4c1c-bbe2-3b52cd3845c7"), 700000m, 1, new Guid("44444444-4444-4444-4444-444444444441"), true, 0 },
                    { new Guid("95cc149b-4690-45d7-986d-b241d8cf40d9"), 2000000m, 0, new Guid("33333333-3333-3333-3333-333333333334"), true, 2 },
                    { new Guid("987110b9-6772-44aa-9654-7aa1ad457df6"), 5000000m, 0, new Guid("44444444-4444-4444-4444-444444444443"), true, 1 },
                    { new Guid("b0250418-4ddf-42f3-a029-0e21f8c0d2cd"), 500000m, 0, new Guid("11111111-1111-1111-1111-111111111113"), true, 1 },
                    { new Guid("b0e64624-08a7-47e3-b216-cac24779cc54"), 500000m, 1, new Guid("22222222-2222-2222-2222-222222222222"), true, 0 },
                    { new Guid("b176eaf2-b37a-452f-aa26-c81e685c874a"), 3000000m, 0, new Guid("33333333-3333-3333-3333-333333333335"), true, 2 },
                    { new Guid("bbedd8fc-9abc-4aeb-8181-cdf7ebaac0e2"), 700000m, 1, new Guid("33333333-3333-3333-3333-333333333333"), true, 0 },
                    { new Guid("c5c3671a-9b1f-456e-80c4-889e427549cc"), 4000000m, 0, new Guid("44444444-4444-4444-4444-444444444443"), true, 2 },
                    { new Guid("c68af59e-eb2e-4360-862b-26635118e968"), 1000000m, 0, new Guid("22222222-2222-2222-2222-222222222224"), true, 2 },
                    { new Guid("cbbd9281-8e88-4739-8fcd-4ab8a5df6323"), 1000000m, 1, new Guid("44444444-4444-4444-4444-444444444442"), true, 0 },
                    { new Guid("cce5b192-172d-4784-b410-f794fd2e3c14"), 700000m, 1, new Guid("22222222-2222-2222-2222-222222222224"), true, 0 },
                    { new Guid("d6318af2-ffb1-4463-855b-6779f7d47c63"), 1000000m, 1, new Guid("33333333-3333-3333-3333-333333333334"), true, 0 },
                    { new Guid("e5983d4c-32c5-477a-b1fc-a60893ed221a"), 500000m, 0, new Guid("33333333-3333-3333-3333-333333333331"), true, 0 },
                    { new Guid("e6a65b32-3dbd-4563-b35c-fe50320bc5a3"), 300000m, 1, new Guid("11111111-1111-1111-1111-111111111113"), true, 0 },
                    { new Guid("f7625738-f897-410f-b98c-aea0a92b8520"), 500000m, 1, new Guid("22222222-2222-2222-2222-222222222223"), true, 0 },
                    { new Guid("fbeb1868-2845-4e8f-b3f5-4cc8f054ad25"), 4000000m, 0, new Guid("33333333-3333-3333-3333-333333333335"), true, 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("00778796-9dbb-41b1-89cb-dcda30683f22"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("1a02d236-8615-4cf9-aa10-f01496e8fb8b"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("1cad7e58-1fb4-4fc4-8af6-c8ac7b022a65"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("1f9eebe2-88cb-49d4-83d5-bfd2d250e38b"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("2a3d0f0a-c90c-4e7f-b768-5f0928ce3681"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("31512304-2a75-49d1-baae-9f837a5835f6"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("4e89b12a-bd9a-4248-b354-6055a3f36d20"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("5cd758ae-a760-454e-9056-42bf416c8197"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("787e2b69-d0c5-4d0b-b52b-4840a884feb7"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("887a0c9d-44f6-494d-8bc6-b318a10817e2"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("8a6d204f-a598-4d5c-ba9a-dd4b2e10667f"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("8a8fb0d2-49ee-4953-acba-ed87d72029fb"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("8b654bce-11c2-4c1c-bbe2-3b52cd3845c7"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("95cc149b-4690-45d7-986d-b241d8cf40d9"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("987110b9-6772-44aa-9654-7aa1ad457df6"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("b0250418-4ddf-42f3-a029-0e21f8c0d2cd"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("b0e64624-08a7-47e3-b216-cac24779cc54"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("b176eaf2-b37a-452f-aa26-c81e685c874a"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("bbedd8fc-9abc-4aeb-8181-cdf7ebaac0e2"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("c5c3671a-9b1f-456e-80c4-889e427549cc"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("c68af59e-eb2e-4360-862b-26635118e968"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("cbbd9281-8e88-4739-8fcd-4ab8a5df6323"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("cce5b192-172d-4784-b410-f794fd2e3c14"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("d6318af2-ffb1-4463-855b-6779f7d47c63"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("e5983d4c-32c5-477a-b1fc-a60893ed221a"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("e6a65b32-3dbd-4563-b35c-fe50320bc5a3"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("f7625738-f897-410f-b98c-aea0a92b8520"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("fbeb1868-2845-4e8f-b3f5-4cc8f054ad25"));

            migrationBuilder.DropColumn(
                name: "TotalPaid",
                table: "Invoices");

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
    }
}
