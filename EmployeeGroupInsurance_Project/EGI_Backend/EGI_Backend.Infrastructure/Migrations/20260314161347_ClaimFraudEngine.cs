using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EGI_Backend.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ClaimFraudEngine : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("36b0b21c-a5d2-402d-b342-4c2bc47de2f3"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("522f0eae-89e9-44a8-b528-82839f0d9b26"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("5c274872-23dc-4d4e-8856-a023b4153eba"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("5f9c5d09-e4b9-44af-afe5-b762d5601450"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("5fb8879b-f7a1-4e5b-ad03-03c394f2f2d2"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("659d7d2d-ced6-4b5e-beeb-1e3542573235"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("7fb999e4-4e09-4a0c-829c-1177e8e45f38"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("80f66c7a-54ec-42ea-b040-6c1bb12a185d"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("8125bc36-efe9-4a79-86f7-404257c6f5a3"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("872363db-af14-42eb-9dcf-192d6ebb74b5"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("a563eb00-be96-4af0-8af2-49719b57d26e"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("a768296a-65fe-443c-be6c-b09520ed97d8"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("a81de983-7025-42d1-8ec2-b709ed8d7407"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("b0ba297b-721d-4346-9a7d-85e84e710fca"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("b25a0552-b533-43f6-8555-baa861fc7104"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("b68027b1-d79d-4c56-9330-c18d59d3d5ca"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("b6c73fa6-6f20-4b56-863a-ed987354299f"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("bbecaf0d-2b02-4bcc-b0a5-c0a053c75853"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("bd561915-8d7c-4a98-b041-b5bb9bf10e9a"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("c2a3b4e5-30da-4090-87df-851971496517"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("cf770456-e9ca-43e3-8739-b7daed82ef64"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("d2a04a38-fe50-4893-8f73-98755916da28"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("d410477c-6101-47f5-99d9-45c3cc619d3b"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("e7a22e1a-c00d-4fc4-90f1-cfc32ff8f15c"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("ea4c6f85-61d0-49bb-837b-f117bf27f9ae"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("ed660b39-872c-4bbe-8eec-797db24eaf1a"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("f0709507-99e3-4639-a411-555b19f146f8"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("f803330f-98b3-4e22-8534-3734850fa1f2"));

            migrationBuilder.AddColumn<string>(
                name: "SubmissionToken",
                table: "PolicyEndorsements",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "CorporateClients",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "FraudAnalysis",
                table: "Claims",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FraudScore",
                table: "Claims",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsSuspectedFraud",
                table: "Claims",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 14, 16, 13, 45, 735, DateTimeKind.Utc).AddTicks(7746));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111112"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 14, 16, 13, 45, 735, DateTimeKind.Utc).AddTicks(7774));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111113"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 14, 16, 13, 45, 735, DateTimeKind.Utc).AddTicks(7780));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222221"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 14, 16, 13, 45, 735, DateTimeKind.Utc).AddTicks(7797));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 14, 16, 13, 45, 735, DateTimeKind.Utc).AddTicks(7802));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222223"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 14, 16, 13, 45, 735, DateTimeKind.Utc).AddTicks(7807));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222224"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 14, 16, 13, 45, 735, DateTimeKind.Utc).AddTicks(7811));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333331"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 14, 16, 13, 45, 735, DateTimeKind.Utc).AddTicks(7815));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333332"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 14, 16, 13, 45, 735, DateTimeKind.Utc).AddTicks(7820));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 14, 16, 13, 45, 735, DateTimeKind.Utc).AddTicks(7825));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333334"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 14, 16, 13, 45, 735, DateTimeKind.Utc).AddTicks(7828));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333335"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 14, 16, 13, 45, 735, DateTimeKind.Utc).AddTicks(7836));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444441"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 14, 16, 13, 45, 735, DateTimeKind.Utc).AddTicks(7840));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444442"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 14, 16, 13, 45, 735, DateTimeKind.Utc).AddTicks(7844));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444443"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 14, 16, 13, 45, 735, DateTimeKind.Utc).AddTicks(7848));

            migrationBuilder.InsertData(
                table: "PlanCoverages",
                columns: new[] { "Id", "CoverageAmount", "CoveredGroup", "InsurancePlanId", "IsActive", "Type" },
                values: new object[,]
                {
                    { new Guid("047d5c1d-0cbb-482a-a8f1-c0a5d2b93b85"), 700000m, 1, new Guid("22222222-2222-2222-2222-222222222224"), true, 0 },
                    { new Guid("2c6b1447-8fba-4994-9523-62f43d07a818"), 500000m, 1, new Guid("22222222-2222-2222-2222-222222222223"), true, 0 },
                    { new Guid("37f66b1c-0c08-47ab-a1f7-dd2a2299a340"), 500000m, 0, new Guid("33333333-3333-3333-3333-333333333335"), true, 3 },
                    { new Guid("4e381bab-d66b-4e18-9c30-22433ee338a3"), 4000000m, 0, new Guid("33333333-3333-3333-3333-333333333335"), true, 1 },
                    { new Guid("54575124-5f10-4fd0-9487-8100499165b6"), 3000000m, 0, new Guid("33333333-3333-3333-3333-333333333334"), true, 1 },
                    { new Guid("5497c171-ef93-4fc0-a000-f4addb9d87a9"), 1500000m, 2, new Guid("33333333-3333-3333-3333-333333333335"), true, 0 },
                    { new Guid("5ff7418c-34d1-46c2-8213-a20ce488e2aa"), 2000000m, 0, new Guid("33333333-3333-3333-3333-333333333333"), true, 1 },
                    { new Guid("60b312e1-ce77-4958-a2d4-687e40e67ec3"), 1000000m, 1, new Guid("44444444-4444-4444-4444-444444444442"), true, 0 },
                    { new Guid("7f8e44af-1544-478d-a4ae-401cecac5e98"), 1000000m, 0, new Guid("22222222-2222-2222-2222-222222222223"), true, 1 },
                    { new Guid("82e1ef72-35cd-475a-879b-dd2753eff603"), 200000m, 0, new Guid("11111111-1111-1111-1111-111111111111"), true, 0 },
                    { new Guid("865267b4-617a-44a7-ac9b-2022773755ad"), 500000m, 0, new Guid("33333333-3333-3333-3333-333333333331"), true, 0 },
                    { new Guid("946b6110-ea65-4ff3-a082-ede83a3ae9f7"), 500000m, 0, new Guid("11111111-1111-1111-1111-111111111113"), true, 1 },
                    { new Guid("951d8c60-e82d-4ea6-97a9-211a2eec0ec5"), 3000000m, 0, new Guid("33333333-3333-3333-3333-333333333335"), true, 2 },
                    { new Guid("9ca5cfa0-90c1-4519-85b9-5c47219876d5"), 300000m, 1, new Guid("11111111-1111-1111-1111-111111111113"), true, 0 },
                    { new Guid("ad620ca8-9880-4aea-b351-bde69cab3a3c"), 700000m, 1, new Guid("33333333-3333-3333-3333-333333333333"), true, 0 },
                    { new Guid("adceae39-1b6d-45ad-b087-b7b9c0087069"), 5000000m, 0, new Guid("44444444-4444-4444-4444-444444444443"), true, 1 },
                    { new Guid("ae2fdc95-f2bd-4626-b666-e5bf2cc5cb97"), 3000000m, 0, new Guid("44444444-4444-4444-4444-444444444442"), true, 1 },
                    { new Guid("b74190e6-549a-4faf-9d75-8332ed36f373"), 2000000m, 0, new Guid("33333333-3333-3333-3333-333333333334"), true, 2 },
                    { new Guid("c1c6ec37-d1ac-4e1b-981c-bdf6f4d38ca2"), 2000000m, 2, new Guid("44444444-4444-4444-4444-444444444443"), true, 0 },
                    { new Guid("c83b673c-406b-49b7-a1e8-4e83512e3986"), 500000m, 1, new Guid("22222222-2222-2222-2222-222222222222"), true, 0 },
                    { new Guid("cbe85e32-95eb-4e4b-94fd-6e8c36dd6034"), 300000m, 1, new Guid("11111111-1111-1111-1111-111111111112"), true, 0 },
                    { new Guid("cbf0a18f-5951-4d5c-916b-ce5bd49265e4"), 4000000m, 0, new Guid("44444444-4444-4444-4444-444444444443"), true, 2 },
                    { new Guid("d4f4b733-ff51-426f-9958-390b37de8c17"), 1500000m, 0, new Guid("22222222-2222-2222-2222-222222222224"), true, 1 },
                    { new Guid("dbd458be-a7a4-4810-a2ad-962f48f10b8c"), 300000m, 0, new Guid("22222222-2222-2222-2222-222222222221"), true, 0 },
                    { new Guid("e160b165-9a4a-438b-a5ba-8f2f12b2379e"), 700000m, 1, new Guid("33333333-3333-3333-3333-333333333332"), true, 0 },
                    { new Guid("e26b182f-be75-4b1e-9c5d-0b517e5972e7"), 1000000m, 1, new Guid("33333333-3333-3333-3333-333333333334"), true, 0 },
                    { new Guid("f8d6c249-6282-4af1-a811-7b0fb90727e8"), 1000000m, 0, new Guid("22222222-2222-2222-2222-222222222224"), true, 2 },
                    { new Guid("fbccc46c-6920-46b1-96a1-e74fe2774d4c"), 700000m, 1, new Guid("44444444-4444-4444-4444-444444444441"), true, 0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("047d5c1d-0cbb-482a-a8f1-c0a5d2b93b85"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("2c6b1447-8fba-4994-9523-62f43d07a818"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("37f66b1c-0c08-47ab-a1f7-dd2a2299a340"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("4e381bab-d66b-4e18-9c30-22433ee338a3"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("54575124-5f10-4fd0-9487-8100499165b6"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("5497c171-ef93-4fc0-a000-f4addb9d87a9"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("5ff7418c-34d1-46c2-8213-a20ce488e2aa"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("60b312e1-ce77-4958-a2d4-687e40e67ec3"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("7f8e44af-1544-478d-a4ae-401cecac5e98"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("82e1ef72-35cd-475a-879b-dd2753eff603"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("865267b4-617a-44a7-ac9b-2022773755ad"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("946b6110-ea65-4ff3-a082-ede83a3ae9f7"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("951d8c60-e82d-4ea6-97a9-211a2eec0ec5"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("9ca5cfa0-90c1-4519-85b9-5c47219876d5"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("ad620ca8-9880-4aea-b351-bde69cab3a3c"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("adceae39-1b6d-45ad-b087-b7b9c0087069"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("ae2fdc95-f2bd-4626-b666-e5bf2cc5cb97"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("b74190e6-549a-4faf-9d75-8332ed36f373"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("c1c6ec37-d1ac-4e1b-981c-bdf6f4d38ca2"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("c83b673c-406b-49b7-a1e8-4e83512e3986"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("cbe85e32-95eb-4e4b-94fd-6e8c36dd6034"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("cbf0a18f-5951-4d5c-916b-ce5bd49265e4"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("d4f4b733-ff51-426f-9958-390b37de8c17"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("dbd458be-a7a4-4810-a2ad-962f48f10b8c"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("e160b165-9a4a-438b-a5ba-8f2f12b2379e"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("e26b182f-be75-4b1e-9c5d-0b517e5972e7"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("f8d6c249-6282-4af1-a811-7b0fb90727e8"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("fbccc46c-6920-46b1-96a1-e74fe2774d4c"));

            migrationBuilder.DropColumn(
                name: "SubmissionToken",
                table: "PolicyEndorsements");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "CorporateClients");

            migrationBuilder.DropColumn(
                name: "FraudAnalysis",
                table: "Claims");

            migrationBuilder.DropColumn(
                name: "FraudScore",
                table: "Claims");

            migrationBuilder.DropColumn(
                name: "IsSuspectedFraud",
                table: "Claims");

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 14, 15, 18, 56, 223, DateTimeKind.Utc).AddTicks(7502));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111112"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 14, 15, 18, 56, 223, DateTimeKind.Utc).AddTicks(7521));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111113"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 14, 15, 18, 56, 223, DateTimeKind.Utc).AddTicks(7526));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222221"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 14, 15, 18, 56, 223, DateTimeKind.Utc).AddTicks(7530));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 14, 15, 18, 56, 223, DateTimeKind.Utc).AddTicks(7534));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222223"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 14, 15, 18, 56, 223, DateTimeKind.Utc).AddTicks(7547));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222224"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 14, 15, 18, 56, 223, DateTimeKind.Utc).AddTicks(7551));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333331"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 14, 15, 18, 56, 223, DateTimeKind.Utc).AddTicks(7555));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333332"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 14, 15, 18, 56, 223, DateTimeKind.Utc).AddTicks(7559));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 14, 15, 18, 56, 223, DateTimeKind.Utc).AddTicks(7562));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333334"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 14, 15, 18, 56, 223, DateTimeKind.Utc).AddTicks(7566));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333335"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 14, 15, 18, 56, 223, DateTimeKind.Utc).AddTicks(7569));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444441"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 14, 15, 18, 56, 223, DateTimeKind.Utc).AddTicks(7572));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444442"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 14, 15, 18, 56, 223, DateTimeKind.Utc).AddTicks(7579));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444443"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 14, 15, 18, 56, 223, DateTimeKind.Utc).AddTicks(7582));

            migrationBuilder.InsertData(
                table: "PlanCoverages",
                columns: new[] { "Id", "CoverageAmount", "CoveredGroup", "InsurancePlanId", "IsActive", "Type" },
                values: new object[,]
                {
                    { new Guid("36b0b21c-a5d2-402d-b342-4c2bc47de2f3"), 1000000m, 1, new Guid("44444444-4444-4444-4444-444444444442"), true, 0 },
                    { new Guid("522f0eae-89e9-44a8-b528-82839f0d9b26"), 4000000m, 0, new Guid("33333333-3333-3333-3333-333333333335"), true, 1 },
                    { new Guid("5c274872-23dc-4d4e-8856-a023b4153eba"), 700000m, 1, new Guid("22222222-2222-2222-2222-222222222224"), true, 0 },
                    { new Guid("5f9c5d09-e4b9-44af-afe5-b762d5601450"), 300000m, 1, new Guid("11111111-1111-1111-1111-111111111113"), true, 0 },
                    { new Guid("5fb8879b-f7a1-4e5b-ad03-03c394f2f2d2"), 3000000m, 0, new Guid("44444444-4444-4444-4444-444444444442"), true, 1 },
                    { new Guid("659d7d2d-ced6-4b5e-beeb-1e3542573235"), 2000000m, 0, new Guid("33333333-3333-3333-3333-333333333334"), true, 2 },
                    { new Guid("7fb999e4-4e09-4a0c-829c-1177e8e45f38"), 1000000m, 0, new Guid("22222222-2222-2222-2222-222222222224"), true, 2 },
                    { new Guid("80f66c7a-54ec-42ea-b040-6c1bb12a185d"), 1000000m, 1, new Guid("33333333-3333-3333-3333-333333333334"), true, 0 },
                    { new Guid("8125bc36-efe9-4a79-86f7-404257c6f5a3"), 2000000m, 2, new Guid("44444444-4444-4444-4444-444444444443"), true, 0 },
                    { new Guid("872363db-af14-42eb-9dcf-192d6ebb74b5"), 1500000m, 0, new Guid("22222222-2222-2222-2222-222222222224"), true, 1 },
                    { new Guid("a563eb00-be96-4af0-8af2-49719b57d26e"), 2000000m, 0, new Guid("33333333-3333-3333-3333-333333333333"), true, 1 },
                    { new Guid("a768296a-65fe-443c-be6c-b09520ed97d8"), 200000m, 0, new Guid("11111111-1111-1111-1111-111111111111"), true, 0 },
                    { new Guid("a81de983-7025-42d1-8ec2-b709ed8d7407"), 500000m, 0, new Guid("11111111-1111-1111-1111-111111111113"), true, 1 },
                    { new Guid("b0ba297b-721d-4346-9a7d-85e84e710fca"), 700000m, 1, new Guid("33333333-3333-3333-3333-333333333332"), true, 0 },
                    { new Guid("b25a0552-b533-43f6-8555-baa861fc7104"), 700000m, 1, new Guid("44444444-4444-4444-4444-444444444441"), true, 0 },
                    { new Guid("b68027b1-d79d-4c56-9330-c18d59d3d5ca"), 300000m, 1, new Guid("11111111-1111-1111-1111-111111111112"), true, 0 },
                    { new Guid("b6c73fa6-6f20-4b56-863a-ed987354299f"), 500000m, 0, new Guid("33333333-3333-3333-3333-333333333335"), true, 3 },
                    { new Guid("bbecaf0d-2b02-4bcc-b0a5-c0a053c75853"), 500000m, 0, new Guid("33333333-3333-3333-3333-333333333331"), true, 0 },
                    { new Guid("bd561915-8d7c-4a98-b041-b5bb9bf10e9a"), 300000m, 0, new Guid("22222222-2222-2222-2222-222222222221"), true, 0 },
                    { new Guid("c2a3b4e5-30da-4090-87df-851971496517"), 3000000m, 0, new Guid("33333333-3333-3333-3333-333333333334"), true, 1 },
                    { new Guid("cf770456-e9ca-43e3-8739-b7daed82ef64"), 3000000m, 0, new Guid("33333333-3333-3333-3333-333333333335"), true, 2 },
                    { new Guid("d2a04a38-fe50-4893-8f73-98755916da28"), 500000m, 1, new Guid("22222222-2222-2222-2222-222222222222"), true, 0 },
                    { new Guid("d410477c-6101-47f5-99d9-45c3cc619d3b"), 700000m, 1, new Guid("33333333-3333-3333-3333-333333333333"), true, 0 },
                    { new Guid("e7a22e1a-c00d-4fc4-90f1-cfc32ff8f15c"), 1000000m, 0, new Guid("22222222-2222-2222-2222-222222222223"), true, 1 },
                    { new Guid("ea4c6f85-61d0-49bb-837b-f117bf27f9ae"), 4000000m, 0, new Guid("44444444-4444-4444-4444-444444444443"), true, 2 },
                    { new Guid("ed660b39-872c-4bbe-8eec-797db24eaf1a"), 5000000m, 0, new Guid("44444444-4444-4444-4444-444444444443"), true, 1 },
                    { new Guid("f0709507-99e3-4639-a411-555b19f146f8"), 500000m, 1, new Guid("22222222-2222-2222-2222-222222222223"), true, 0 },
                    { new Guid("f803330f-98b3-4e22-8534-3734850fa1f2"), 1500000m, 2, new Guid("33333333-3333-3333-3333-333333333335"), true, 0 }
                });
        }
    }
}
