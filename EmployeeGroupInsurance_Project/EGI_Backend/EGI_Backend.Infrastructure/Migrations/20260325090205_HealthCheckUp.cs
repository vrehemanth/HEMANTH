using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EGI_Backend.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class HealthCheckUp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("2da365e5-cf7c-48f1-a4e7-53fc88468660"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("3d8538d2-cdbb-4e44-ae8b-14fdf59342c6"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("607c878d-3d47-4519-a00d-38fffdc4ed6e"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("6f522c44-ffdc-4383-830b-0ee284c78f3d"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("72eef32d-d74a-48bd-807e-9d0303ff85a4"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("7ba230ac-f48f-43c6-ba6f-27efc647fd65"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("87a88a28-531c-4986-8000-e619d8221509"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("891c1b30-de99-4034-959a-d259926eba81"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("951eeb73-1ed4-44b7-859e-336067309ad1"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("a3348cd3-9f04-431a-92b8-5f80edbd4b62"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("a3727241-c2ba-4a17-aaa2-7d1172e5072f"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("a7a05710-ffc8-4400-8677-8a8aa2e376f8"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("a9968c32-8731-489a-9d28-fad6a4a4edb4"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("aaeff4ed-0115-49e3-89bb-c18cfd99d3a9"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("af0510e2-afef-49fc-b766-fde17bec6508"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("bb7171bd-2cd6-436e-a13e-7e12ed161583"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("bd01b68c-726f-4aa3-9f8e-7c50bd5f0973"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("bfd8b616-cd2d-4a60-b18b-2ea76bd03647"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("c23bfb51-b57f-4d9a-9a6b-b320c0fad4f2"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("c2b86ddb-85bf-4d50-8936-bf111bd419d1"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("c4211525-250c-4c7b-a7bd-767576673de5"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("d326e8e6-cb18-45b3-b513-01148f059b32"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("da4231df-feef-4e5d-ba3f-7e2febe4fca3"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("df724ac8-93e8-486a-83e4-0130d1ffeea0"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("ef13416e-014e-4ce7-9830-c80b1cfae72d"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("f2ea174e-aff1-4cc0-a547-d641bb3e99f2"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("fa95a611-430d-44b0-94d1-1b9adac42d1b"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("fd401ee3-fbf0-46aa-a38d-0de032e1eff8"));

            migrationBuilder.AddColumn<int>(
                name: "HealthCheckupActualDependentCount",
                table: "CorporateClients",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HealthCheckupActualMemberCount",
                table: "CorporateClients",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "HealthCheckupHospitalId",
                table: "CorporateClients",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsHealthCheckupClaimPending",
                table: "CorporateClients",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastHealthCheckupDate",
                table: "CorporateClients",
                type: "datetime2",
                nullable: true);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "HealthCheckupActualDependentCount",
                table: "CorporateClients");

            migrationBuilder.DropColumn(
                name: "HealthCheckupActualMemberCount",
                table: "CorporateClients");

            migrationBuilder.DropColumn(
                name: "HealthCheckupHospitalId",
                table: "CorporateClients");

            migrationBuilder.DropColumn(
                name: "IsHealthCheckupClaimPending",
                table: "CorporateClients");

            migrationBuilder.DropColumn(
                name: "LastHealthCheckupDate",
                table: "CorporateClients");

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 25, 7, 28, 37, 699, DateTimeKind.Utc).AddTicks(6472));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111112"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 25, 7, 28, 37, 699, DateTimeKind.Utc).AddTicks(6500));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111113"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 25, 7, 28, 37, 699, DateTimeKind.Utc).AddTicks(6506));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222221"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 25, 7, 28, 37, 699, DateTimeKind.Utc).AddTicks(6510));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 25, 7, 28, 37, 699, DateTimeKind.Utc).AddTicks(6514));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222223"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 25, 7, 28, 37, 699, DateTimeKind.Utc).AddTicks(6518));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222224"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 25, 7, 28, 37, 699, DateTimeKind.Utc).AddTicks(6521));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333331"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 25, 7, 28, 37, 699, DateTimeKind.Utc).AddTicks(6536));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333332"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 25, 7, 28, 37, 699, DateTimeKind.Utc).AddTicks(6540));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 25, 7, 28, 37, 699, DateTimeKind.Utc).AddTicks(6543));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333334"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 25, 7, 28, 37, 699, DateTimeKind.Utc).AddTicks(6546));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333335"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 25, 7, 28, 37, 699, DateTimeKind.Utc).AddTicks(6549));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444441"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 25, 7, 28, 37, 699, DateTimeKind.Utc).AddTicks(6553));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444442"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 25, 7, 28, 37, 699, DateTimeKind.Utc).AddTicks(6609));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444443"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 25, 7, 28, 37, 699, DateTimeKind.Utc).AddTicks(6614));

            migrationBuilder.InsertData(
                table: "PlanCoverages",
                columns: new[] { "Id", "CoverageAmount", "CoveredGroup", "InsurancePlanId", "IsActive", "Type" },
                values: new object[,]
                {
                    { new Guid("2da365e5-cf7c-48f1-a4e7-53fc88468660"), 700000m, 1, new Guid("33333333-3333-3333-3333-333333333333"), true, 0 },
                    { new Guid("3d8538d2-cdbb-4e44-ae8b-14fdf59342c6"), 1000000m, 0, new Guid("22222222-2222-2222-2222-222222222224"), true, 2 },
                    { new Guid("607c878d-3d47-4519-a00d-38fffdc4ed6e"), 700000m, 1, new Guid("22222222-2222-2222-2222-222222222224"), true, 0 },
                    { new Guid("6f522c44-ffdc-4383-830b-0ee284c78f3d"), 5000000m, 0, new Guid("44444444-4444-4444-4444-444444444443"), true, 1 },
                    { new Guid("72eef32d-d74a-48bd-807e-9d0303ff85a4"), 500000m, 0, new Guid("11111111-1111-1111-1111-111111111113"), true, 1 },
                    { new Guid("7ba230ac-f48f-43c6-ba6f-27efc647fd65"), 500000m, 1, new Guid("22222222-2222-2222-2222-222222222223"), true, 0 },
                    { new Guid("87a88a28-531c-4986-8000-e619d8221509"), 2000000m, 2, new Guid("44444444-4444-4444-4444-444444444443"), true, 0 },
                    { new Guid("891c1b30-de99-4034-959a-d259926eba81"), 1000000m, 1, new Guid("44444444-4444-4444-4444-444444444442"), true, 0 },
                    { new Guid("951eeb73-1ed4-44b7-859e-336067309ad1"), 2000000m, 0, new Guid("33333333-3333-3333-3333-333333333334"), true, 2 },
                    { new Guid("a3348cd3-9f04-431a-92b8-5f80edbd4b62"), 300000m, 0, new Guid("22222222-2222-2222-2222-222222222221"), true, 0 },
                    { new Guid("a3727241-c2ba-4a17-aaa2-7d1172e5072f"), 500000m, 0, new Guid("33333333-3333-3333-3333-333333333335"), true, 3 },
                    { new Guid("a7a05710-ffc8-4400-8677-8a8aa2e376f8"), 1500000m, 0, new Guid("22222222-2222-2222-2222-222222222224"), true, 1 },
                    { new Guid("a9968c32-8731-489a-9d28-fad6a4a4edb4"), 3000000m, 0, new Guid("33333333-3333-3333-3333-333333333335"), true, 2 },
                    { new Guid("aaeff4ed-0115-49e3-89bb-c18cfd99d3a9"), 200000m, 0, new Guid("11111111-1111-1111-1111-111111111111"), true, 0 },
                    { new Guid("af0510e2-afef-49fc-b766-fde17bec6508"), 3000000m, 0, new Guid("44444444-4444-4444-4444-444444444442"), true, 1 },
                    { new Guid("bb7171bd-2cd6-436e-a13e-7e12ed161583"), 300000m, 1, new Guid("11111111-1111-1111-1111-111111111113"), true, 0 },
                    { new Guid("bd01b68c-726f-4aa3-9f8e-7c50bd5f0973"), 1000000m, 0, new Guid("22222222-2222-2222-2222-222222222223"), true, 1 },
                    { new Guid("bfd8b616-cd2d-4a60-b18b-2ea76bd03647"), 300000m, 1, new Guid("11111111-1111-1111-1111-111111111112"), true, 0 },
                    { new Guid("c23bfb51-b57f-4d9a-9a6b-b320c0fad4f2"), 4000000m, 0, new Guid("33333333-3333-3333-3333-333333333335"), true, 1 },
                    { new Guid("c2b86ddb-85bf-4d50-8936-bf111bd419d1"), 1500000m, 2, new Guid("33333333-3333-3333-3333-333333333335"), true, 0 },
                    { new Guid("c4211525-250c-4c7b-a7bd-767576673de5"), 500000m, 0, new Guid("33333333-3333-3333-3333-333333333331"), true, 0 },
                    { new Guid("d326e8e6-cb18-45b3-b513-01148f059b32"), 700000m, 1, new Guid("44444444-4444-4444-4444-444444444441"), true, 0 },
                    { new Guid("da4231df-feef-4e5d-ba3f-7e2febe4fca3"), 4000000m, 0, new Guid("44444444-4444-4444-4444-444444444443"), true, 2 },
                    { new Guid("df724ac8-93e8-486a-83e4-0130d1ffeea0"), 1000000m, 1, new Guid("33333333-3333-3333-3333-333333333334"), true, 0 },
                    { new Guid("ef13416e-014e-4ce7-9830-c80b1cfae72d"), 500000m, 1, new Guid("22222222-2222-2222-2222-222222222222"), true, 0 },
                    { new Guid("f2ea174e-aff1-4cc0-a547-d641bb3e99f2"), 3000000m, 0, new Guid("33333333-3333-3333-3333-333333333334"), true, 1 },
                    { new Guid("fa95a611-430d-44b0-94d1-1b9adac42d1b"), 700000m, 1, new Guid("33333333-3333-3333-3333-333333333332"), true, 0 },
                    { new Guid("fd401ee3-fbf0-46aa-a38d-0de032e1eff8"), 2000000m, 0, new Guid("33333333-3333-3333-3333-333333333333"), true, 1 }
                });
        }
    }
}
