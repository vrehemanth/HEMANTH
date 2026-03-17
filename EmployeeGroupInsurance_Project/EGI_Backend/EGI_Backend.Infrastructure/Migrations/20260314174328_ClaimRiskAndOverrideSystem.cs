using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EGI_Backend.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ClaimRiskAndOverrideSystem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<Guid>(
                name: "FraudOverriddenBy",
                table: "Claims",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FraudOverrideReason",
                table: "Claims",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsFraudOverridden",
                table: "Claims",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 14, 17, 43, 27, 181, DateTimeKind.Utc).AddTicks(279));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111112"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 14, 17, 43, 27, 181, DateTimeKind.Utc).AddTicks(302));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111113"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 14, 17, 43, 27, 181, DateTimeKind.Utc).AddTicks(307));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222221"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 14, 17, 43, 27, 181, DateTimeKind.Utc).AddTicks(311));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 14, 17, 43, 27, 181, DateTimeKind.Utc).AddTicks(315));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222223"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 14, 17, 43, 27, 181, DateTimeKind.Utc).AddTicks(318));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222224"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 14, 17, 43, 27, 181, DateTimeKind.Utc).AddTicks(322));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333331"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 14, 17, 43, 27, 181, DateTimeKind.Utc).AddTicks(325));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333332"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 14, 17, 43, 27, 181, DateTimeKind.Utc).AddTicks(336));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 14, 17, 43, 27, 181, DateTimeKind.Utc).AddTicks(339));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333334"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 14, 17, 43, 27, 181, DateTimeKind.Utc).AddTicks(343));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333335"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 14, 17, 43, 27, 181, DateTimeKind.Utc).AddTicks(346));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444441"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 14, 17, 43, 27, 181, DateTimeKind.Utc).AddTicks(350));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444442"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 14, 17, 43, 27, 181, DateTimeKind.Utc).AddTicks(353));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444443"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 14, 17, 43, 27, 181, DateTimeKind.Utc).AddTicks(357));

            migrationBuilder.InsertData(
                table: "PlanCoverages",
                columns: new[] { "Id", "CoverageAmount", "CoveredGroup", "InsurancePlanId", "IsActive", "Type" },
                values: new object[,]
                {
                    { new Guid("05eaffcb-02e3-440b-ae5a-badde9d616ed"), 1500000m, 2, new Guid("33333333-3333-3333-3333-333333333335"), true, 0 },
                    { new Guid("06907414-a1fc-47e8-bbfd-eaadbefc96f1"), 3000000m, 0, new Guid("33333333-3333-3333-3333-333333333334"), true, 1 },
                    { new Guid("0acad9f6-3342-4cc8-a2e3-14d90a26a669"), 1500000m, 0, new Guid("22222222-2222-2222-2222-222222222224"), true, 1 },
                    { new Guid("148a5a04-8daa-4333-aa6b-01126fc0127a"), 300000m, 1, new Guid("11111111-1111-1111-1111-111111111112"), true, 0 },
                    { new Guid("2380e3d2-ab30-452f-b4c5-da25b2b1d0c7"), 3000000m, 0, new Guid("33333333-3333-3333-3333-333333333335"), true, 2 },
                    { new Guid("4201b5ff-88d6-436f-9bce-59047250c6f9"), 300000m, 1, new Guid("11111111-1111-1111-1111-111111111113"), true, 0 },
                    { new Guid("421f0bac-86dd-4ad1-84f5-c883ceb8ceb0"), 2000000m, 0, new Guid("33333333-3333-3333-3333-333333333334"), true, 2 },
                    { new Guid("4c03a66f-9908-4c89-b24b-65549b084ee5"), 4000000m, 0, new Guid("33333333-3333-3333-3333-333333333335"), true, 1 },
                    { new Guid("4c646c0e-7005-45c4-9e1d-507361096754"), 5000000m, 0, new Guid("44444444-4444-4444-4444-444444444443"), true, 1 },
                    { new Guid("591e443c-dbb3-48ec-905b-65645adac791"), 2000000m, 0, new Guid("33333333-3333-3333-3333-333333333333"), true, 1 },
                    { new Guid("675723b8-991a-4cd5-a1ad-10062f984647"), 500000m, 0, new Guid("33333333-3333-3333-3333-333333333335"), true, 3 },
                    { new Guid("7e8c64e3-8370-4cd7-b7f7-efc0b90239ed"), 700000m, 1, new Guid("33333333-3333-3333-3333-333333333333"), true, 0 },
                    { new Guid("838cffe7-1576-43c1-8d3f-d8e64496a86e"), 500000m, 0, new Guid("33333333-3333-3333-3333-333333333331"), true, 0 },
                    { new Guid("88f27751-d7a7-474c-9850-94eeccbc442e"), 500000m, 1, new Guid("22222222-2222-2222-2222-222222222222"), true, 0 },
                    { new Guid("8dae923c-4ec2-4168-8e27-27cf59f18521"), 4000000m, 0, new Guid("44444444-4444-4444-4444-444444444443"), true, 2 },
                    { new Guid("96540b14-5fe8-4348-93f9-a8305bf1c9e0"), 1000000m, 0, new Guid("22222222-2222-2222-2222-222222222224"), true, 2 },
                    { new Guid("a1224aa4-9c69-43e4-8451-1b88fd9871a5"), 500000m, 0, new Guid("11111111-1111-1111-1111-111111111113"), true, 1 },
                    { new Guid("a9781c22-3960-44ff-8ad2-834c84872aff"), 300000m, 0, new Guid("22222222-2222-2222-2222-222222222221"), true, 0 },
                    { new Guid("aa30f009-112a-49a0-b8c0-af675c606997"), 700000m, 1, new Guid("33333333-3333-3333-3333-333333333332"), true, 0 },
                    { new Guid("ad3f2fec-110b-4db4-a200-7ade3276fa07"), 700000m, 1, new Guid("44444444-4444-4444-4444-444444444441"), true, 0 },
                    { new Guid("ae3604ca-abc3-4f0a-b7c6-33388364bf84"), 1000000m, 0, new Guid("22222222-2222-2222-2222-222222222223"), true, 1 },
                    { new Guid("ae4fd54b-7b25-45fe-a845-6486835ea565"), 1000000m, 1, new Guid("44444444-4444-4444-4444-444444444442"), true, 0 },
                    { new Guid("b27cbcaf-eadd-478c-b59a-a9a6088c451c"), 200000m, 0, new Guid("11111111-1111-1111-1111-111111111111"), true, 0 },
                    { new Guid("d26067fd-43e0-4fa7-9e28-0d3fbd1befa3"), 2000000m, 2, new Guid("44444444-4444-4444-4444-444444444443"), true, 0 },
                    { new Guid("d4920b0b-ebd3-46e1-9664-1ed2e58308cb"), 1000000m, 1, new Guid("33333333-3333-3333-3333-333333333334"), true, 0 },
                    { new Guid("d7a10a76-1f90-41c3-843f-d01f23f790c9"), 3000000m, 0, new Guid("44444444-4444-4444-4444-444444444442"), true, 1 },
                    { new Guid("db50edfd-830f-4ac5-b298-8fb7a26280c5"), 500000m, 1, new Guid("22222222-2222-2222-2222-222222222223"), true, 0 },
                    { new Guid("e59a1dc3-2835-48ac-88c8-ccc9ff0336fe"), 700000m, 1, new Guid("22222222-2222-2222-2222-222222222224"), true, 0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("05eaffcb-02e3-440b-ae5a-badde9d616ed"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("06907414-a1fc-47e8-bbfd-eaadbefc96f1"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("0acad9f6-3342-4cc8-a2e3-14d90a26a669"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("148a5a04-8daa-4333-aa6b-01126fc0127a"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("2380e3d2-ab30-452f-b4c5-da25b2b1d0c7"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("4201b5ff-88d6-436f-9bce-59047250c6f9"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("421f0bac-86dd-4ad1-84f5-c883ceb8ceb0"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("4c03a66f-9908-4c89-b24b-65549b084ee5"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("4c646c0e-7005-45c4-9e1d-507361096754"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("591e443c-dbb3-48ec-905b-65645adac791"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("675723b8-991a-4cd5-a1ad-10062f984647"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("7e8c64e3-8370-4cd7-b7f7-efc0b90239ed"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("838cffe7-1576-43c1-8d3f-d8e64496a86e"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("88f27751-d7a7-474c-9850-94eeccbc442e"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("8dae923c-4ec2-4168-8e27-27cf59f18521"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("96540b14-5fe8-4348-93f9-a8305bf1c9e0"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("a1224aa4-9c69-43e4-8451-1b88fd9871a5"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("a9781c22-3960-44ff-8ad2-834c84872aff"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("aa30f009-112a-49a0-b8c0-af675c606997"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("ad3f2fec-110b-4db4-a200-7ade3276fa07"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("ae3604ca-abc3-4f0a-b7c6-33388364bf84"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("ae4fd54b-7b25-45fe-a845-6486835ea565"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("b27cbcaf-eadd-478c-b59a-a9a6088c451c"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("d26067fd-43e0-4fa7-9e28-0d3fbd1befa3"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("d4920b0b-ebd3-46e1-9664-1ed2e58308cb"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("d7a10a76-1f90-41c3-843f-d01f23f790c9"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("db50edfd-830f-4ac5-b298-8fb7a26280c5"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("e59a1dc3-2835-48ac-88c8-ccc9ff0336fe"));

            migrationBuilder.DropColumn(
                name: "FraudOverriddenBy",
                table: "Claims");

            migrationBuilder.DropColumn(
                name: "FraudOverrideReason",
                table: "Claims");

            migrationBuilder.DropColumn(
                name: "IsFraudOverridden",
                table: "Claims");

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
    }
}
