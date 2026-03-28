using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EGI_Backend.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class HealthCheckUpVerifiedAt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("004335a5-66b6-42a9-ae9b-160dc99d6b85"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("0644a5fe-ffe2-4457-8c40-e6c36cc89844"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("2a8cd5cf-4aa0-4ac6-8a1e-c65cb3fb3986"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("3781b471-376c-4b65-a8d8-ed4cb9cb6cf2"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("3d555d6e-7cd4-4066-b27c-cf0547055662"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("4b03d065-fa09-49c1-bdf4-a1add348dcb4"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("50c2a38d-0daf-4124-9ca4-006d366b2da6"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("539a91ec-a710-45ed-92ec-1fdb0525d32c"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("55283377-1e64-4857-a7a9-7b04676bc1d2"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("5a995d28-6e4d-409e-94a7-b4eccba10003"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("62cf7283-4e47-4442-a019-8d3bfb641765"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("720c290d-87f0-4710-a6fd-be141e2b3303"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("7dd24357-c8b0-4987-9f37-af6336d39c38"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("8a8309b3-3353-46cc-8836-7c084ba9922f"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("8ecd5337-3176-4915-830b-2c412bfff13d"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("8f3bc989-0526-4c60-afa8-7f5e52e3c8bf"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("9a4a2a2c-e7fe-4667-9461-f98581e319e5"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("9b0e6576-a3a3-4857-b366-7d988609009a"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("a409ea61-a3d2-467a-b538-3e32906d17b0"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("ac7cd55a-db3d-4f7b-a319-afd1c9051d6d"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("adf040d7-b6d1-4048-9c3a-41585cb585ea"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("af78c7a9-ba42-4d66-83bd-db2a94931fc3"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("bba44a25-fd29-4e34-a4c7-5c3a30b7e738"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("c064143c-2aae-4c4e-b5e0-aca4dbbb31bc"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("c6b48e59-a007-4c60-bc21-02dc50b33d86"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("ec2f261f-63d7-492e-a2e3-306645c69586"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("f060724a-35e4-4340-abe5-30a3088da3e1"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("fd2b1423-04c7-4c05-93f1-a09840696bb8"));

            migrationBuilder.AddColumn<DateTime>(
                name: "HealthCheckupVerifiedAt",
                table: "CorporateClients",
                type: "datetime2",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 25, 9, 24, 39, 675, DateTimeKind.Utc).AddTicks(3540));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111112"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 25, 9, 24, 39, 675, DateTimeKind.Utc).AddTicks(3594));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111113"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 25, 9, 24, 39, 675, DateTimeKind.Utc).AddTicks(3604));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222221"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 25, 9, 24, 39, 675, DateTimeKind.Utc).AddTicks(3611));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 25, 9, 24, 39, 675, DateTimeKind.Utc).AddTicks(3618));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222223"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 25, 9, 24, 39, 675, DateTimeKind.Utc).AddTicks(3624));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222224"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 25, 9, 24, 39, 675, DateTimeKind.Utc).AddTicks(3630));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333331"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 25, 9, 24, 39, 675, DateTimeKind.Utc).AddTicks(3639));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333332"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 25, 9, 24, 39, 675, DateTimeKind.Utc).AddTicks(3651));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 25, 9, 24, 39, 675, DateTimeKind.Utc).AddTicks(3656));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333334"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 25, 9, 24, 39, 675, DateTimeKind.Utc).AddTicks(3663));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333335"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 25, 9, 24, 39, 675, DateTimeKind.Utc).AddTicks(3669));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444441"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 25, 9, 24, 39, 675, DateTimeKind.Utc).AddTicks(3675));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444442"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 25, 9, 24, 39, 675, DateTimeKind.Utc).AddTicks(3681));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444443"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 25, 9, 24, 39, 675, DateTimeKind.Utc).AddTicks(3687));

            migrationBuilder.InsertData(
                table: "PlanCoverages",
                columns: new[] { "Id", "CoverageAmount", "CoveredGroup", "InsurancePlanId", "IsActive", "Type" },
                values: new object[,]
                {
                    { new Guid("2a8a5147-2c3c-4ae3-85cd-d3f1ecdef5f2"), 1000000m, 1, new Guid("33333333-3333-3333-3333-333333333334"), true, 0 },
                    { new Guid("467baf7f-00d8-478f-97f7-12f2cdc05151"), 200000m, 0, new Guid("11111111-1111-1111-1111-111111111111"), true, 0 },
                    { new Guid("46cc0a4c-304e-451c-9e83-ff8cb07ad24b"), 1500000m, 0, new Guid("22222222-2222-2222-2222-222222222224"), true, 1 },
                    { new Guid("527b230d-d1e9-448a-aaf0-29b25f16e4b8"), 1000000m, 0, new Guid("22222222-2222-2222-2222-222222222223"), true, 1 },
                    { new Guid("5a6e47c6-1cd1-4627-ac30-1f8c55b1e31e"), 2000000m, 0, new Guid("33333333-3333-3333-3333-333333333333"), true, 1 },
                    { new Guid("5f1255b7-f5bf-4063-8692-100826e575d0"), 300000m, 1, new Guid("11111111-1111-1111-1111-111111111112"), true, 0 },
                    { new Guid("66f51ee2-f55c-418e-b282-8888606d8a8e"), 700000m, 1, new Guid("33333333-3333-3333-3333-333333333333"), true, 0 },
                    { new Guid("67f50d80-d368-4404-8a5b-746885c0a01a"), 700000m, 1, new Guid("22222222-2222-2222-2222-222222222224"), true, 0 },
                    { new Guid("68be8520-29a5-40bf-bf95-2d5d0c88d843"), 500000m, 0, new Guid("11111111-1111-1111-1111-111111111113"), true, 1 },
                    { new Guid("731d14ac-8deb-45aa-80be-e90671612be2"), 500000m, 1, new Guid("22222222-2222-2222-2222-222222222222"), true, 0 },
                    { new Guid("747b75a2-1026-446e-92c1-81ad71931a7a"), 500000m, 0, new Guid("33333333-3333-3333-3333-333333333331"), true, 0 },
                    { new Guid("79110866-0483-4a8d-b3e9-89d6e24b0a82"), 3000000m, 0, new Guid("33333333-3333-3333-3333-333333333335"), true, 2 },
                    { new Guid("79352fce-bceb-426b-a1d6-b62f893698e6"), 500000m, 1, new Guid("22222222-2222-2222-2222-222222222223"), true, 0 },
                    { new Guid("7a048a96-aabd-4e95-9816-be11803cc51e"), 300000m, 0, new Guid("22222222-2222-2222-2222-222222222221"), true, 0 },
                    { new Guid("8560f0ee-4d5f-42da-b628-a4955370f2ba"), 4000000m, 0, new Guid("44444444-4444-4444-4444-444444444443"), true, 2 },
                    { new Guid("87304ca3-5fa0-490e-8026-4869d3a3d2f0"), 3000000m, 0, new Guid("33333333-3333-3333-3333-333333333334"), true, 1 },
                    { new Guid("8c530043-f60f-449e-b24d-2d5b37c74bf3"), 3000000m, 0, new Guid("44444444-4444-4444-4444-444444444442"), true, 1 },
                    { new Guid("9505bdf4-a035-43a5-9f55-3e74d1c039df"), 700000m, 1, new Guid("44444444-4444-4444-4444-444444444441"), true, 0 },
                    { new Guid("aa9931db-e2d2-49b6-a3d1-1b1349e7bef7"), 700000m, 1, new Guid("33333333-3333-3333-3333-333333333332"), true, 0 },
                    { new Guid("ace57c70-a641-4198-ae3c-0a90ad63e0a5"), 4000000m, 0, new Guid("33333333-3333-3333-3333-333333333335"), true, 1 },
                    { new Guid("aff37f9d-2eac-464c-897c-e86818e54416"), 1000000m, 0, new Guid("22222222-2222-2222-2222-222222222224"), true, 2 },
                    { new Guid("b0500ba1-ef6f-42f2-be94-f80cc088918b"), 1500000m, 2, new Guid("33333333-3333-3333-3333-333333333335"), true, 0 },
                    { new Guid("bbbde46a-7a83-4c3c-b6b1-cbeba0129b75"), 2000000m, 2, new Guid("44444444-4444-4444-4444-444444444443"), true, 0 },
                    { new Guid("c323e801-7c1a-4a91-8311-a41f9097e5b5"), 5000000m, 0, new Guid("44444444-4444-4444-4444-444444444443"), true, 1 },
                    { new Guid("d1c5dba2-84de-4da3-b178-7a6316482963"), 2000000m, 0, new Guid("33333333-3333-3333-3333-333333333334"), true, 2 },
                    { new Guid("e1378f4a-c873-4ebe-9b99-2bf4e6776627"), 300000m, 1, new Guid("11111111-1111-1111-1111-111111111113"), true, 0 },
                    { new Guid("e6b4210c-9218-4d74-9878-51cc17a6280d"), 500000m, 0, new Guid("33333333-3333-3333-3333-333333333335"), true, 3 },
                    { new Guid("ff78f928-d44f-47e7-b185-224ee8a218b8"), 1000000m, 1, new Guid("44444444-4444-4444-4444-444444444442"), true, 0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("2a8a5147-2c3c-4ae3-85cd-d3f1ecdef5f2"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("467baf7f-00d8-478f-97f7-12f2cdc05151"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("46cc0a4c-304e-451c-9e83-ff8cb07ad24b"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("527b230d-d1e9-448a-aaf0-29b25f16e4b8"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("5a6e47c6-1cd1-4627-ac30-1f8c55b1e31e"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("5f1255b7-f5bf-4063-8692-100826e575d0"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("66f51ee2-f55c-418e-b282-8888606d8a8e"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("67f50d80-d368-4404-8a5b-746885c0a01a"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("68be8520-29a5-40bf-bf95-2d5d0c88d843"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("731d14ac-8deb-45aa-80be-e90671612be2"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("747b75a2-1026-446e-92c1-81ad71931a7a"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("79110866-0483-4a8d-b3e9-89d6e24b0a82"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("79352fce-bceb-426b-a1d6-b62f893698e6"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("7a048a96-aabd-4e95-9816-be11803cc51e"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("8560f0ee-4d5f-42da-b628-a4955370f2ba"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("87304ca3-5fa0-490e-8026-4869d3a3d2f0"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("8c530043-f60f-449e-b24d-2d5b37c74bf3"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("9505bdf4-a035-43a5-9f55-3e74d1c039df"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("aa9931db-e2d2-49b6-a3d1-1b1349e7bef7"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("ace57c70-a641-4198-ae3c-0a90ad63e0a5"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("aff37f9d-2eac-464c-897c-e86818e54416"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("b0500ba1-ef6f-42f2-be94-f80cc088918b"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("bbbde46a-7a83-4c3c-b6b1-cbeba0129b75"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("c323e801-7c1a-4a91-8311-a41f9097e5b5"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("d1c5dba2-84de-4da3-b178-7a6316482963"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("e1378f4a-c873-4ebe-9b99-2bf4e6776627"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("e6b4210c-9218-4d74-9878-51cc17a6280d"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("ff78f928-d44f-47e7-b185-224ee8a218b8"));

            migrationBuilder.DropColumn(
                name: "HealthCheckupVerifiedAt",
                table: "CorporateClients");

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 25, 9, 2, 4, 491, DateTimeKind.Utc).AddTicks(2598));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111112"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 25, 9, 2, 4, 491, DateTimeKind.Utc).AddTicks(2621));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111113"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 25, 9, 2, 4, 491, DateTimeKind.Utc).AddTicks(2626));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222221"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 25, 9, 2, 4, 491, DateTimeKind.Utc).AddTicks(2630));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 25, 9, 2, 4, 491, DateTimeKind.Utc).AddTicks(2635));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222223"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 25, 9, 2, 4, 491, DateTimeKind.Utc).AddTicks(2638));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222224"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 25, 9, 2, 4, 491, DateTimeKind.Utc).AddTicks(2642));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333331"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 25, 9, 2, 4, 491, DateTimeKind.Utc).AddTicks(2646));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333332"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 25, 9, 2, 4, 491, DateTimeKind.Utc).AddTicks(2651));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 25, 9, 2, 4, 491, DateTimeKind.Utc).AddTicks(2655));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333334"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 25, 9, 2, 4, 491, DateTimeKind.Utc).AddTicks(2658));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333335"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 25, 9, 2, 4, 491, DateTimeKind.Utc).AddTicks(2661));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444441"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 25, 9, 2, 4, 491, DateTimeKind.Utc).AddTicks(2664));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444442"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 25, 9, 2, 4, 491, DateTimeKind.Utc).AddTicks(2668));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444443"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 25, 9, 2, 4, 491, DateTimeKind.Utc).AddTicks(2671));

            migrationBuilder.InsertData(
                table: "PlanCoverages",
                columns: new[] { "Id", "CoverageAmount", "CoveredGroup", "InsurancePlanId", "IsActive", "Type" },
                values: new object[,]
                {
                    { new Guid("004335a5-66b6-42a9-ae9b-160dc99d6b85"), 1500000m, 0, new Guid("22222222-2222-2222-2222-222222222224"), true, 1 },
                    { new Guid("0644a5fe-ffe2-4457-8c40-e6c36cc89844"), 4000000m, 0, new Guid("44444444-4444-4444-4444-444444444443"), true, 2 },
                    { new Guid("2a8cd5cf-4aa0-4ac6-8a1e-c65cb3fb3986"), 1000000m, 0, new Guid("22222222-2222-2222-2222-222222222223"), true, 1 },
                    { new Guid("3781b471-376c-4b65-a8d8-ed4cb9cb6cf2"), 700000m, 1, new Guid("33333333-3333-3333-3333-333333333332"), true, 0 },
                    { new Guid("3d555d6e-7cd4-4066-b27c-cf0547055662"), 500000m, 1, new Guid("22222222-2222-2222-2222-222222222223"), true, 0 },
                    { new Guid("4b03d065-fa09-49c1-bdf4-a1add348dcb4"), 300000m, 0, new Guid("22222222-2222-2222-2222-222222222221"), true, 0 },
                    { new Guid("50c2a38d-0daf-4124-9ca4-006d366b2da6"), 4000000m, 0, new Guid("33333333-3333-3333-3333-333333333335"), true, 1 },
                    { new Guid("539a91ec-a710-45ed-92ec-1fdb0525d32c"), 500000m, 0, new Guid("11111111-1111-1111-1111-111111111113"), true, 1 },
                    { new Guid("55283377-1e64-4857-a7a9-7b04676bc1d2"), 2000000m, 2, new Guid("44444444-4444-4444-4444-444444444443"), true, 0 },
                    { new Guid("5a995d28-6e4d-409e-94a7-b4eccba10003"), 1500000m, 2, new Guid("33333333-3333-3333-3333-333333333335"), true, 0 },
                    { new Guid("62cf7283-4e47-4442-a019-8d3bfb641765"), 700000m, 1, new Guid("33333333-3333-3333-3333-333333333333"), true, 0 },
                    { new Guid("720c290d-87f0-4710-a6fd-be141e2b3303"), 2000000m, 0, new Guid("33333333-3333-3333-3333-333333333333"), true, 1 },
                    { new Guid("7dd24357-c8b0-4987-9f37-af6336d39c38"), 500000m, 0, new Guid("33333333-3333-3333-3333-333333333335"), true, 3 },
                    { new Guid("8a8309b3-3353-46cc-8836-7c084ba9922f"), 3000000m, 0, new Guid("33333333-3333-3333-3333-333333333334"), true, 1 },
                    { new Guid("8ecd5337-3176-4915-830b-2c412bfff13d"), 300000m, 1, new Guid("11111111-1111-1111-1111-111111111112"), true, 0 },
                    { new Guid("8f3bc989-0526-4c60-afa8-7f5e52e3c8bf"), 500000m, 0, new Guid("33333333-3333-3333-3333-333333333331"), true, 0 },
                    { new Guid("9a4a2a2c-e7fe-4667-9461-f98581e319e5"), 700000m, 1, new Guid("44444444-4444-4444-4444-444444444441"), true, 0 },
                    { new Guid("9b0e6576-a3a3-4857-b366-7d988609009a"), 2000000m, 0, new Guid("33333333-3333-3333-3333-333333333334"), true, 2 },
                    { new Guid("a409ea61-a3d2-467a-b538-3e32906d17b0"), 200000m, 0, new Guid("11111111-1111-1111-1111-111111111111"), true, 0 },
                    { new Guid("ac7cd55a-db3d-4f7b-a319-afd1c9051d6d"), 3000000m, 0, new Guid("44444444-4444-4444-4444-444444444442"), true, 1 },
                    { new Guid("adf040d7-b6d1-4048-9c3a-41585cb585ea"), 500000m, 1, new Guid("22222222-2222-2222-2222-222222222222"), true, 0 },
                    { new Guid("af78c7a9-ba42-4d66-83bd-db2a94931fc3"), 700000m, 1, new Guid("22222222-2222-2222-2222-222222222224"), true, 0 },
                    { new Guid("bba44a25-fd29-4e34-a4c7-5c3a30b7e738"), 300000m, 1, new Guid("11111111-1111-1111-1111-111111111113"), true, 0 },
                    { new Guid("c064143c-2aae-4c4e-b5e0-aca4dbbb31bc"), 5000000m, 0, new Guid("44444444-4444-4444-4444-444444444443"), true, 1 },
                    { new Guid("c6b48e59-a007-4c60-bc21-02dc50b33d86"), 3000000m, 0, new Guid("33333333-3333-3333-3333-333333333335"), true, 2 },
                    { new Guid("ec2f261f-63d7-492e-a2e3-306645c69586"), 1000000m, 1, new Guid("33333333-3333-3333-3333-333333333334"), true, 0 },
                    { new Guid("f060724a-35e4-4340-abe5-30a3088da3e1"), 1000000m, 0, new Guid("22222222-2222-2222-2222-222222222224"), true, 2 },
                    { new Guid("fd2b1423-04c7-4c05-93f1-a09840696bb8"), 1000000m, 1, new Guid("44444444-4444-4444-4444-444444444442"), true, 0 }
                });
        }
    }
}
