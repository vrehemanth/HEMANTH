using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EGI_Backend.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPendingCredit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("09aedc40-6905-4757-b98e-0d67819e2b9c"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("0a9e982f-b895-45c6-9881-95baa54d5c96"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("173d236d-88d1-46bf-ab90-41eaa9f3fbf1"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("1e476253-c632-48ed-9cf9-d0befdef904d"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("26991dd3-7628-4de9-91cc-532baad38920"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("2be333e0-3f6a-42d8-9645-8e11b49db0e1"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("2d2023eb-2763-41ee-b2b4-4ba3b18107e5"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("37e68c1e-2c63-457e-8f1a-8611466946ea"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("3905720e-37e2-444e-bf11-6d0283211f3e"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("45511029-b19d-4e14-afe4-48e150f1fb57"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("464a58c2-076b-4a8c-b3ee-210ba71cd416"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("4fa0b56a-1e81-4298-be3e-34d323aad732"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("613a6773-0f63-4a55-892f-3b0e0785f0ed"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("6196f18b-f9ea-47c0-83a9-24a640ef5370"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("6bb91c88-159b-44a4-9c09-bed7fd062d8d"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("7560de8b-d19a-4b6b-82b1-c161f9b18062"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("8ffbf434-f294-481d-bf05-b9d7f838c74c"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("909115a7-23fe-49a4-9b1d-3c23aa6efae2"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("c96f2833-1450-473d-88d5-cfd02bc421b0"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("cc42521e-610e-42f4-9252-76b17770c9d8"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("d0f9c4e3-d51e-46ab-bbdb-cdc948d57b95"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("d4a5cb0c-def5-4eaa-937e-e0647c4cf3f1"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("de88cef3-21a8-44b9-bc97-6a5268339b07"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("e001b7dc-a251-4068-a2f5-75e9514d900c"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("e39a3beb-e2ba-41d0-ada3-06e4546067d5"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("e84580d0-333f-44b6-b50c-d354d80a6a76"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("e845cc68-3257-45f1-9ccf-c9901fb2b46c"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("ef9080b3-b918-4aaa-91d5-f8691f9f6ddc"));

            migrationBuilder.AddColumn<decimal>(
                name: "PendingCredit",
                table: "PolicyAssignments",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 20, 4, 33, 44, 649, DateTimeKind.Utc).AddTicks(3223));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111112"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 20, 4, 33, 44, 649, DateTimeKind.Utc).AddTicks(3257));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111113"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 20, 4, 33, 44, 649, DateTimeKind.Utc).AddTicks(3263));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222221"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 20, 4, 33, 44, 649, DateTimeKind.Utc).AddTicks(3267));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 20, 4, 33, 44, 649, DateTimeKind.Utc).AddTicks(3271));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222223"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 20, 4, 33, 44, 649, DateTimeKind.Utc).AddTicks(3287));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222224"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 20, 4, 33, 44, 649, DateTimeKind.Utc).AddTicks(3291));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333331"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 20, 4, 33, 44, 649, DateTimeKind.Utc).AddTicks(3295));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333332"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 20, 4, 33, 44, 649, DateTimeKind.Utc).AddTicks(3299));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 20, 4, 33, 44, 649, DateTimeKind.Utc).AddTicks(3303));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333334"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 20, 4, 33, 44, 649, DateTimeKind.Utc).AddTicks(3306));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333335"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 20, 4, 33, 44, 649, DateTimeKind.Utc).AddTicks(3309));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444441"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 20, 4, 33, 44, 649, DateTimeKind.Utc).AddTicks(3312));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444442"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 20, 4, 33, 44, 649, DateTimeKind.Utc).AddTicks(3318));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444443"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 20, 4, 33, 44, 649, DateTimeKind.Utc).AddTicks(3321));

            migrationBuilder.InsertData(
                table: "PlanCoverages",
                columns: new[] { "Id", "CoverageAmount", "CoveredGroup", "InsurancePlanId", "IsActive", "Type" },
                values: new object[,]
                {
                    { new Guid("05f827fe-b0b0-4b21-b972-30b8fb758928"), 500000m, 1, new Guid("22222222-2222-2222-2222-222222222222"), true, 0 },
                    { new Guid("15120f86-d95f-41fb-8d6b-eb0109a938da"), 3000000m, 0, new Guid("33333333-3333-3333-3333-333333333334"), true, 1 },
                    { new Guid("2abe3a0b-35d3-4f63-a157-b9177c98f18a"), 500000m, 0, new Guid("33333333-3333-3333-3333-333333333335"), true, 3 },
                    { new Guid("39e6274c-3d7f-448a-88c2-e7ca6c467a30"), 200000m, 0, new Guid("11111111-1111-1111-1111-111111111111"), true, 0 },
                    { new Guid("42d88169-c231-4f4e-b377-83ec2d31238a"), 4000000m, 0, new Guid("44444444-4444-4444-4444-444444444443"), true, 2 },
                    { new Guid("43c82cb7-38e0-4601-b10e-124aa811571f"), 500000m, 1, new Guid("22222222-2222-2222-2222-222222222223"), true, 0 },
                    { new Guid("44b90e13-2eb5-4171-a5ed-5708db54e6ed"), 700000m, 1, new Guid("22222222-2222-2222-2222-222222222224"), true, 0 },
                    { new Guid("4eefff3d-2a2c-483d-ac97-4bab92c12b3d"), 2000000m, 0, new Guid("33333333-3333-3333-3333-333333333334"), true, 2 },
                    { new Guid("58ad9da2-3bb1-4041-a232-6469460b798d"), 1000000m, 0, new Guid("22222222-2222-2222-2222-222222222223"), true, 1 },
                    { new Guid("646949c2-de42-4ea0-9e2c-e9d190142f62"), 5000000m, 0, new Guid("44444444-4444-4444-4444-444444444443"), true, 1 },
                    { new Guid("6abf7dcd-faa7-4f47-be0b-69b6926a3a2c"), 4000000m, 0, new Guid("33333333-3333-3333-3333-333333333335"), true, 1 },
                    { new Guid("6cfade22-1a0f-4a88-8a94-fbc7aed8e472"), 500000m, 0, new Guid("11111111-1111-1111-1111-111111111113"), true, 1 },
                    { new Guid("755c706a-1d3c-45a5-98ce-cb308d5c0133"), 300000m, 1, new Guid("11111111-1111-1111-1111-111111111112"), true, 0 },
                    { new Guid("7eb06be3-90a3-4c1b-a604-823924edcba9"), 3000000m, 0, new Guid("33333333-3333-3333-3333-333333333335"), true, 2 },
                    { new Guid("8ae9c135-09cb-4abf-a425-7ed2aa11deac"), 300000m, 0, new Guid("22222222-2222-2222-2222-222222222221"), true, 0 },
                    { new Guid("9456eaa3-2883-4bcb-9931-ff1127dd6a2b"), 1500000m, 2, new Guid("33333333-3333-3333-3333-333333333335"), true, 0 },
                    { new Guid("9cb0e19e-b32c-4190-858b-085d51a21aa8"), 700000m, 1, new Guid("33333333-3333-3333-3333-333333333332"), true, 0 },
                    { new Guid("9d19578e-a3e0-49be-a228-269b2297afa4"), 300000m, 1, new Guid("11111111-1111-1111-1111-111111111113"), true, 0 },
                    { new Guid("a1d2d06d-95c0-4a71-b44d-1167ac40db3b"), 1000000m, 0, new Guid("22222222-2222-2222-2222-222222222224"), true, 2 },
                    { new Guid("a344e9c2-eb02-4f01-a969-50b84e1b03df"), 700000m, 1, new Guid("33333333-3333-3333-3333-333333333333"), true, 0 },
                    { new Guid("b6258b62-ef06-46c1-b021-90205cba8842"), 3000000m, 0, new Guid("44444444-4444-4444-4444-444444444442"), true, 1 },
                    { new Guid("c38f3564-b1e6-4c1e-aa97-3813f09cc2ec"), 2000000m, 2, new Guid("44444444-4444-4444-4444-444444444443"), true, 0 },
                    { new Guid("d30ba428-cf87-4b54-9bc6-4bb683ec1c8e"), 1500000m, 0, new Guid("22222222-2222-2222-2222-222222222224"), true, 1 },
                    { new Guid("d5fcff21-038f-4038-9687-c99514dde3f9"), 1000000m, 1, new Guid("33333333-3333-3333-3333-333333333334"), true, 0 },
                    { new Guid("e03e44e7-3abf-4a60-a780-56ec9c6ecabd"), 1000000m, 1, new Guid("44444444-4444-4444-4444-444444444442"), true, 0 },
                    { new Guid("e3372303-1a97-4b31-9264-00b42ecd6034"), 700000m, 1, new Guid("44444444-4444-4444-4444-444444444441"), true, 0 },
                    { new Guid("e3d80a51-39a8-4d5c-916f-5aaa0c7c0c82"), 500000m, 0, new Guid("33333333-3333-3333-3333-333333333331"), true, 0 },
                    { new Guid("fa61b891-1a33-4cae-89ed-cd183c2649bb"), 2000000m, 0, new Guid("33333333-3333-3333-3333-333333333333"), true, 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("05f827fe-b0b0-4b21-b972-30b8fb758928"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("15120f86-d95f-41fb-8d6b-eb0109a938da"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("2abe3a0b-35d3-4f63-a157-b9177c98f18a"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("39e6274c-3d7f-448a-88c2-e7ca6c467a30"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("42d88169-c231-4f4e-b377-83ec2d31238a"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("43c82cb7-38e0-4601-b10e-124aa811571f"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("44b90e13-2eb5-4171-a5ed-5708db54e6ed"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("4eefff3d-2a2c-483d-ac97-4bab92c12b3d"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("58ad9da2-3bb1-4041-a232-6469460b798d"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("646949c2-de42-4ea0-9e2c-e9d190142f62"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("6abf7dcd-faa7-4f47-be0b-69b6926a3a2c"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("6cfade22-1a0f-4a88-8a94-fbc7aed8e472"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("755c706a-1d3c-45a5-98ce-cb308d5c0133"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("7eb06be3-90a3-4c1b-a604-823924edcba9"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("8ae9c135-09cb-4abf-a425-7ed2aa11deac"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("9456eaa3-2883-4bcb-9931-ff1127dd6a2b"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("9cb0e19e-b32c-4190-858b-085d51a21aa8"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("9d19578e-a3e0-49be-a228-269b2297afa4"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("a1d2d06d-95c0-4a71-b44d-1167ac40db3b"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("a344e9c2-eb02-4f01-a969-50b84e1b03df"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("b6258b62-ef06-46c1-b021-90205cba8842"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("c38f3564-b1e6-4c1e-aa97-3813f09cc2ec"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("d30ba428-cf87-4b54-9bc6-4bb683ec1c8e"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("d5fcff21-038f-4038-9687-c99514dde3f9"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("e03e44e7-3abf-4a60-a780-56ec9c6ecabd"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("e3372303-1a97-4b31-9264-00b42ecd6034"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("e3d80a51-39a8-4d5c-916f-5aaa0c7c0c82"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("fa61b891-1a33-4cae-89ed-cd183c2649bb"));

            migrationBuilder.DropColumn(
                name: "PendingCredit",
                table: "PolicyAssignments");

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 20, 3, 11, 1, 175, DateTimeKind.Utc).AddTicks(178));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111112"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 20, 3, 11, 1, 175, DateTimeKind.Utc).AddTicks(234));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111113"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 20, 3, 11, 1, 175, DateTimeKind.Utc).AddTicks(241));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222221"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 20, 3, 11, 1, 175, DateTimeKind.Utc).AddTicks(247));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 20, 3, 11, 1, 175, DateTimeKind.Utc).AddTicks(253));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222223"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 20, 3, 11, 1, 175, DateTimeKind.Utc).AddTicks(364));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222224"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 20, 3, 11, 1, 175, DateTimeKind.Utc).AddTicks(370));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333331"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 20, 3, 11, 1, 175, DateTimeKind.Utc).AddTicks(376));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333332"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 20, 3, 11, 1, 175, DateTimeKind.Utc).AddTicks(381));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 20, 3, 11, 1, 175, DateTimeKind.Utc).AddTicks(390));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333334"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 20, 3, 11, 1, 175, DateTimeKind.Utc).AddTicks(395));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333335"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 20, 3, 11, 1, 175, DateTimeKind.Utc).AddTicks(400));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444441"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 20, 3, 11, 1, 175, DateTimeKind.Utc).AddTicks(406));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444442"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 20, 3, 11, 1, 175, DateTimeKind.Utc).AddTicks(412));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444443"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 20, 3, 11, 1, 175, DateTimeKind.Utc).AddTicks(417));

            migrationBuilder.InsertData(
                table: "PlanCoverages",
                columns: new[] { "Id", "CoverageAmount", "CoveredGroup", "InsurancePlanId", "IsActive", "Type" },
                values: new object[,]
                {
                    { new Guid("09aedc40-6905-4757-b98e-0d67819e2b9c"), 500000m, 0, new Guid("33333333-3333-3333-3333-333333333335"), true, 3 },
                    { new Guid("0a9e982f-b895-45c6-9881-95baa54d5c96"), 3000000m, 0, new Guid("33333333-3333-3333-3333-333333333334"), true, 1 },
                    { new Guid("173d236d-88d1-46bf-ab90-41eaa9f3fbf1"), 1500000m, 2, new Guid("33333333-3333-3333-3333-333333333335"), true, 0 },
                    { new Guid("1e476253-c632-48ed-9cf9-d0befdef904d"), 500000m, 1, new Guid("22222222-2222-2222-2222-222222222223"), true, 0 },
                    { new Guid("26991dd3-7628-4de9-91cc-532baad38920"), 2000000m, 0, new Guid("33333333-3333-3333-3333-333333333333"), true, 1 },
                    { new Guid("2be333e0-3f6a-42d8-9645-8e11b49db0e1"), 1500000m, 0, new Guid("22222222-2222-2222-2222-222222222224"), true, 1 },
                    { new Guid("2d2023eb-2763-41ee-b2b4-4ba3b18107e5"), 700000m, 1, new Guid("33333333-3333-3333-3333-333333333332"), true, 0 },
                    { new Guid("37e68c1e-2c63-457e-8f1a-8611466946ea"), 300000m, 1, new Guid("11111111-1111-1111-1111-111111111112"), true, 0 },
                    { new Guid("3905720e-37e2-444e-bf11-6d0283211f3e"), 5000000m, 0, new Guid("44444444-4444-4444-4444-444444444443"), true, 1 },
                    { new Guid("45511029-b19d-4e14-afe4-48e150f1fb57"), 700000m, 1, new Guid("44444444-4444-4444-4444-444444444441"), true, 0 },
                    { new Guid("464a58c2-076b-4a8c-b3ee-210ba71cd416"), 500000m, 0, new Guid("33333333-3333-3333-3333-333333333331"), true, 0 },
                    { new Guid("4fa0b56a-1e81-4298-be3e-34d323aad732"), 4000000m, 0, new Guid("33333333-3333-3333-3333-333333333335"), true, 1 },
                    { new Guid("613a6773-0f63-4a55-892f-3b0e0785f0ed"), 700000m, 1, new Guid("22222222-2222-2222-2222-222222222224"), true, 0 },
                    { new Guid("6196f18b-f9ea-47c0-83a9-24a640ef5370"), 1000000m, 1, new Guid("33333333-3333-3333-3333-333333333334"), true, 0 },
                    { new Guid("6bb91c88-159b-44a4-9c09-bed7fd062d8d"), 2000000m, 2, new Guid("44444444-4444-4444-4444-444444444443"), true, 0 },
                    { new Guid("7560de8b-d19a-4b6b-82b1-c161f9b18062"), 200000m, 0, new Guid("11111111-1111-1111-1111-111111111111"), true, 0 },
                    { new Guid("8ffbf434-f294-481d-bf05-b9d7f838c74c"), 2000000m, 0, new Guid("33333333-3333-3333-3333-333333333334"), true, 2 },
                    { new Guid("909115a7-23fe-49a4-9b1d-3c23aa6efae2"), 4000000m, 0, new Guid("44444444-4444-4444-4444-444444444443"), true, 2 },
                    { new Guid("c96f2833-1450-473d-88d5-cfd02bc421b0"), 3000000m, 0, new Guid("33333333-3333-3333-3333-333333333335"), true, 2 },
                    { new Guid("cc42521e-610e-42f4-9252-76b17770c9d8"), 1000000m, 0, new Guid("22222222-2222-2222-2222-222222222223"), true, 1 },
                    { new Guid("d0f9c4e3-d51e-46ab-bbdb-cdc948d57b95"), 1000000m, 0, new Guid("22222222-2222-2222-2222-222222222224"), true, 2 },
                    { new Guid("d4a5cb0c-def5-4eaa-937e-e0647c4cf3f1"), 1000000m, 1, new Guid("44444444-4444-4444-4444-444444444442"), true, 0 },
                    { new Guid("de88cef3-21a8-44b9-bc97-6a5268339b07"), 3000000m, 0, new Guid("44444444-4444-4444-4444-444444444442"), true, 1 },
                    { new Guid("e001b7dc-a251-4068-a2f5-75e9514d900c"), 300000m, 1, new Guid("11111111-1111-1111-1111-111111111113"), true, 0 },
                    { new Guid("e39a3beb-e2ba-41d0-ada3-06e4546067d5"), 700000m, 1, new Guid("33333333-3333-3333-3333-333333333333"), true, 0 },
                    { new Guid("e84580d0-333f-44b6-b50c-d354d80a6a76"), 500000m, 1, new Guid("22222222-2222-2222-2222-222222222222"), true, 0 },
                    { new Guid("e845cc68-3257-45f1-9ccf-c9901fb2b46c"), 500000m, 0, new Guid("11111111-1111-1111-1111-111111111113"), true, 1 },
                    { new Guid("ef9080b3-b918-4aaa-91d5-f8691f9f6ddc"), 300000m, 0, new Guid("22222222-2222-2222-2222-222222222221"), true, 0 }
                });
        }
    }
}
