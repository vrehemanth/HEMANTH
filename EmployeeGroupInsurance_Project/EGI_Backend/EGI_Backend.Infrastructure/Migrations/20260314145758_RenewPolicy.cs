using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EGI_Backend.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RenewPolicy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("0405b3d3-15fe-4819-868a-3b25288b225f"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("0e5c9098-841d-41f3-9d82-3b32d6c44467"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("13f3d781-f7a3-4cf0-b365-b496369400c0"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("1a4f27eb-c684-4dbf-93ed-de084cddb3f8"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("22d22c32-f89b-4e43-bce8-87359077e236"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("34f50d81-df74-4e4c-ae5d-c4b7abaa31b8"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("35e675db-2009-4331-a7c2-cada9d135f3a"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("4ef49a6c-d56a-4e77-b758-cf0a57e9db2d"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("55d0a242-c6b2-4fd2-b4c8-065e70af659f"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("5972383b-1d94-4e61-8e49-c17867521c78"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("5e1d7386-d682-4481-8618-2f40e5ffa451"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("65b2cc9d-9cf2-49f5-bac7-f5e454278e78"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("862f6640-ca07-475b-a7f6-2d0d5002db32"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("995d7159-32c4-4933-841e-a88a4e343841"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("a5cca799-5ebd-41a1-aecc-3924da0bb46d"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("a73f0dce-5419-4a49-af75-dd4574fdd3f0"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("a9c57f94-3a20-40f3-a040-095eb85ac813"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("a9cafc26-420e-4b20-96b8-70d8c2c630d9"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("aa3e6e1e-4404-4d4c-9ebe-9c2474979309"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("c91d5737-7931-4f78-9cbe-71f3278b9015"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("cd807078-59b0-4ca0-af04-6c2769b9d7a1"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("ec887f3f-659d-4759-9c93-5e4086eb0f33"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("ed10653a-079b-4ff1-877e-2e2bb4d586a0"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("ee38d982-2dad-4bad-880c-b458b399c6b5"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("fa3aac78-817e-491e-a2e6-f34f19ff3d02"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("faee1721-1abb-44f2-b4c4-8c000328dcde"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("fb07e2e8-5ad5-4ce2-acc9-6b176e24f6d9"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("ffc65b0c-4497-473f-af0c-1661a85a913f"));

            migrationBuilder.AddColumn<Guid>(
                name: "CorporateClientId",
                table: "Members",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            // --- DATA RECOVERY: Link existing members to their corporate client ---
            migrationBuilder.Sql(
                "UPDATE m SET m.CorporateClientId = pa.CorporateClientId " +
                "FROM Members m INNER JOIN PolicyAssignments pa ON m.PolicyAssignmentId = pa.Id");

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 14, 14, 57, 56, 490, DateTimeKind.Utc).AddTicks(3548));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111112"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 14, 14, 57, 56, 490, DateTimeKind.Utc).AddTicks(3565));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111113"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 14, 14, 57, 56, 490, DateTimeKind.Utc).AddTicks(3576));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222221"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 14, 14, 57, 56, 490, DateTimeKind.Utc).AddTicks(3581));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 14, 14, 57, 56, 490, DateTimeKind.Utc).AddTicks(3585));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222223"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 14, 14, 57, 56, 490, DateTimeKind.Utc).AddTicks(3588));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222224"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 14, 14, 57, 56, 490, DateTimeKind.Utc).AddTicks(3592));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333331"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 14, 14, 57, 56, 490, DateTimeKind.Utc).AddTicks(3596));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333332"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 14, 14, 57, 56, 490, DateTimeKind.Utc).AddTicks(3600));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 14, 14, 57, 56, 490, DateTimeKind.Utc).AddTicks(3603));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333334"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 14, 14, 57, 56, 490, DateTimeKind.Utc).AddTicks(3609));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333335"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 14, 14, 57, 56, 490, DateTimeKind.Utc).AddTicks(3613));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444441"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 14, 14, 57, 56, 490, DateTimeKind.Utc).AddTicks(3616));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444442"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 14, 14, 57, 56, 490, DateTimeKind.Utc).AddTicks(3620));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444443"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 14, 14, 57, 56, 490, DateTimeKind.Utc).AddTicks(3623));

            migrationBuilder.InsertData(
                table: "PlanCoverages",
                columns: new[] { "Id", "CoverageAmount", "CoveredGroup", "InsurancePlanId", "IsActive", "Type" },
                values: new object[,]
                {
                    { new Guid("080a8062-330b-4511-b777-ee8a3eeb0182"), 500000m, 0, new Guid("33333333-3333-3333-3333-333333333331"), true, 0 },
                    { new Guid("0aeb2c97-bd2b-4efb-9c4d-5a7dc1f64f91"), 500000m, 1, new Guid("22222222-2222-2222-2222-222222222223"), true, 0 },
                    { new Guid("1069e099-fa26-4df7-876c-5e0765ee647b"), 2000000m, 0, new Guid("33333333-3333-3333-3333-333333333334"), true, 2 },
                    { new Guid("1b572827-50bd-4e8e-8750-260e6a318004"), 4000000m, 0, new Guid("44444444-4444-4444-4444-444444444443"), true, 2 },
                    { new Guid("1db525e5-d27b-4629-93c3-ca384d84765b"), 3000000m, 0, new Guid("33333333-3333-3333-3333-333333333335"), true, 2 },
                    { new Guid("24caf6b4-42dd-4bf6-9f2b-90f6293fc86c"), 500000m, 1, new Guid("22222222-2222-2222-2222-222222222222"), true, 0 },
                    { new Guid("449427be-07bb-4355-aea3-4049c4cb2cf8"), 700000m, 1, new Guid("33333333-3333-3333-3333-333333333333"), true, 0 },
                    { new Guid("471c9e1d-dcd0-4924-bd12-a649cf733f2f"), 1500000m, 0, new Guid("22222222-2222-2222-2222-222222222224"), true, 1 },
                    { new Guid("4d8161e9-30b0-41cf-97de-fdfb4f932cf0"), 2000000m, 2, new Guid("44444444-4444-4444-4444-444444444443"), true, 0 },
                    { new Guid("5871bbce-adb0-40f4-92b3-4663ced5bdcb"), 2000000m, 0, new Guid("33333333-3333-3333-3333-333333333333"), true, 1 },
                    { new Guid("5c6cf1f0-8efb-4550-9043-bbaf601b369b"), 5000000m, 0, new Guid("44444444-4444-4444-4444-444444444443"), true, 1 },
                    { new Guid("6935679d-ef4a-471b-a87d-55fc9838e106"), 300000m, 1, new Guid("11111111-1111-1111-1111-111111111113"), true, 0 },
                    { new Guid("6e1bf99e-ed61-4555-8b7a-00326a3ea487"), 1500000m, 2, new Guid("33333333-3333-3333-3333-333333333335"), true, 0 },
                    { new Guid("76bea3fb-6e50-4719-891b-cdec81aac401"), 500000m, 0, new Guid("11111111-1111-1111-1111-111111111113"), true, 1 },
                    { new Guid("7957a203-fefe-4e03-a7b1-ab81be774ca5"), 200000m, 0, new Guid("11111111-1111-1111-1111-111111111111"), true, 0 },
                    { new Guid("7c190227-e02b-41be-9583-d9d1a0e2f867"), 300000m, 1, new Guid("11111111-1111-1111-1111-111111111112"), true, 0 },
                    { new Guid("7c682480-da06-4e59-aafb-22ceccc72675"), 1000000m, 1, new Guid("33333333-3333-3333-3333-333333333334"), true, 0 },
                    { new Guid("873184dc-19c8-441d-90fe-a92e8d866fe7"), 3000000m, 0, new Guid("44444444-4444-4444-4444-444444444442"), true, 1 },
                    { new Guid("90ad8ac0-a6ff-434f-9c58-17cff8114e28"), 3000000m, 0, new Guid("33333333-3333-3333-3333-333333333334"), true, 1 },
                    { new Guid("a2ddab1e-dcf7-451a-93b4-8f34b15e9f2f"), 300000m, 0, new Guid("22222222-2222-2222-2222-222222222221"), true, 0 },
                    { new Guid("a3a8f1d7-a9de-4660-8eb6-9e4462cad65b"), 1000000m, 1, new Guid("44444444-4444-4444-4444-444444444442"), true, 0 },
                    { new Guid("adb02f2b-a44e-4561-be0b-aebcc5de51d6"), 500000m, 0, new Guid("33333333-3333-3333-3333-333333333335"), true, 3 },
                    { new Guid("d3daef60-b9ce-491f-b449-d78196b42784"), 4000000m, 0, new Guid("33333333-3333-3333-3333-333333333335"), true, 1 },
                    { new Guid("d9c6883f-96ac-4a10-850d-94841de68da9"), 1000000m, 0, new Guid("22222222-2222-2222-2222-222222222224"), true, 2 },
                    { new Guid("df2423a3-e228-41b6-b505-6d5b8820b74d"), 1000000m, 0, new Guid("22222222-2222-2222-2222-222222222223"), true, 1 },
                    { new Guid("e0d631ab-585d-431f-8f93-d822dd25d82c"), 700000m, 1, new Guid("22222222-2222-2222-2222-222222222224"), true, 0 },
                    { new Guid("e745a3d7-cc68-4c7d-941d-292c70d2f37e"), 700000m, 1, new Guid("33333333-3333-3333-3333-333333333332"), true, 0 },
                    { new Guid("f0302719-050a-42e1-868d-71829af749f2"), 700000m, 1, new Guid("44444444-4444-4444-4444-444444444441"), true, 0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Members_CorporateClientId",
                table: "Members",
                column: "CorporateClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Members_CorporateClients_CorporateClientId",
                table: "Members",
                column: "CorporateClientId",
                principalTable: "CorporateClients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Members_CorporateClients_CorporateClientId",
                table: "Members");

            migrationBuilder.DropIndex(
                name: "IX_Members_CorporateClientId",
                table: "Members");

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("080a8062-330b-4511-b777-ee8a3eeb0182"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("0aeb2c97-bd2b-4efb-9c4d-5a7dc1f64f91"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("1069e099-fa26-4df7-876c-5e0765ee647b"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("1b572827-50bd-4e8e-8750-260e6a318004"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("1db525e5-d27b-4629-93c3-ca384d84765b"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("24caf6b4-42dd-4bf6-9f2b-90f6293fc86c"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("449427be-07bb-4355-aea3-4049c4cb2cf8"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("471c9e1d-dcd0-4924-bd12-a649cf733f2f"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("4d8161e9-30b0-41cf-97de-fdfb4f932cf0"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("5871bbce-adb0-40f4-92b3-4663ced5bdcb"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("5c6cf1f0-8efb-4550-9043-bbaf601b369b"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("6935679d-ef4a-471b-a87d-55fc9838e106"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("6e1bf99e-ed61-4555-8b7a-00326a3ea487"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("76bea3fb-6e50-4719-891b-cdec81aac401"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("7957a203-fefe-4e03-a7b1-ab81be774ca5"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("7c190227-e02b-41be-9583-d9d1a0e2f867"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("7c682480-da06-4e59-aafb-22ceccc72675"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("873184dc-19c8-441d-90fe-a92e8d866fe7"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("90ad8ac0-a6ff-434f-9c58-17cff8114e28"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("a2ddab1e-dcf7-451a-93b4-8f34b15e9f2f"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("a3a8f1d7-a9de-4660-8eb6-9e4462cad65b"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("adb02f2b-a44e-4561-be0b-aebcc5de51d6"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("d3daef60-b9ce-491f-b449-d78196b42784"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("d9c6883f-96ac-4a10-850d-94841de68da9"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("df2423a3-e228-41b6-b505-6d5b8820b74d"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("e0d631ab-585d-431f-8f93-d822dd25d82c"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("e745a3d7-cc68-4c7d-941d-292c70d2f37e"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("f0302719-050a-42e1-868d-71829af749f2"));

            migrationBuilder.DropColumn(
                name: "CorporateClientId",
                table: "Members");

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 13, 10, 12, 41, 336, DateTimeKind.Utc).AddTicks(2648));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111112"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 13, 10, 12, 41, 336, DateTimeKind.Utc).AddTicks(2673));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111113"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 13, 10, 12, 41, 336, DateTimeKind.Utc).AddTicks(2679));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222221"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 13, 10, 12, 41, 336, DateTimeKind.Utc).AddTicks(2684));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 13, 10, 12, 41, 336, DateTimeKind.Utc).AddTicks(2689));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222223"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 13, 10, 12, 41, 336, DateTimeKind.Utc).AddTicks(2709));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222224"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 13, 10, 12, 41, 336, DateTimeKind.Utc).AddTicks(2715));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333331"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 13, 10, 12, 41, 336, DateTimeKind.Utc).AddTicks(2719));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333332"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 13, 10, 12, 41, 336, DateTimeKind.Utc).AddTicks(2724));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 13, 10, 12, 41, 336, DateTimeKind.Utc).AddTicks(2729));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333334"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 13, 10, 12, 41, 336, DateTimeKind.Utc).AddTicks(2734));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333335"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 13, 10, 12, 41, 336, DateTimeKind.Utc).AddTicks(2739));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444441"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 13, 10, 12, 41, 336, DateTimeKind.Utc).AddTicks(2743));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444442"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 13, 10, 12, 41, 336, DateTimeKind.Utc).AddTicks(2752));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444443"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 13, 10, 12, 41, 336, DateTimeKind.Utc).AddTicks(2757));

            migrationBuilder.InsertData(
                table: "PlanCoverages",
                columns: new[] { "Id", "CoverageAmount", "CoveredGroup", "InsurancePlanId", "IsActive", "Type" },
                values: new object[,]
                {
                    { new Guid("0405b3d3-15fe-4819-868a-3b25288b225f"), 3000000m, 0, new Guid("44444444-4444-4444-4444-444444444442"), true, 1 },
                    { new Guid("0e5c9098-841d-41f3-9d82-3b32d6c44467"), 200000m, 0, new Guid("11111111-1111-1111-1111-111111111111"), true, 0 },
                    { new Guid("13f3d781-f7a3-4cf0-b365-b496369400c0"), 500000m, 0, new Guid("33333333-3333-3333-3333-333333333335"), true, 3 },
                    { new Guid("1a4f27eb-c684-4dbf-93ed-de084cddb3f8"), 700000m, 1, new Guid("22222222-2222-2222-2222-222222222224"), true, 0 },
                    { new Guid("22d22c32-f89b-4e43-bce8-87359077e236"), 300000m, 1, new Guid("11111111-1111-1111-1111-111111111113"), true, 0 },
                    { new Guid("34f50d81-df74-4e4c-ae5d-c4b7abaa31b8"), 300000m, 0, new Guid("22222222-2222-2222-2222-222222222221"), true, 0 },
                    { new Guid("35e675db-2009-4331-a7c2-cada9d135f3a"), 500000m, 0, new Guid("33333333-3333-3333-3333-333333333331"), true, 0 },
                    { new Guid("4ef49a6c-d56a-4e77-b758-cf0a57e9db2d"), 1000000m, 0, new Guid("22222222-2222-2222-2222-222222222223"), true, 1 },
                    { new Guid("55d0a242-c6b2-4fd2-b4c8-065e70af659f"), 3000000m, 0, new Guid("33333333-3333-3333-3333-333333333334"), true, 1 },
                    { new Guid("5972383b-1d94-4e61-8e49-c17867521c78"), 4000000m, 0, new Guid("33333333-3333-3333-3333-333333333335"), true, 1 },
                    { new Guid("5e1d7386-d682-4481-8618-2f40e5ffa451"), 700000m, 1, new Guid("44444444-4444-4444-4444-444444444441"), true, 0 },
                    { new Guid("65b2cc9d-9cf2-49f5-bac7-f5e454278e78"), 1500000m, 0, new Guid("22222222-2222-2222-2222-222222222224"), true, 1 },
                    { new Guid("862f6640-ca07-475b-a7f6-2d0d5002db32"), 2000000m, 2, new Guid("44444444-4444-4444-4444-444444444443"), true, 0 },
                    { new Guid("995d7159-32c4-4933-841e-a88a4e343841"), 1000000m, 1, new Guid("44444444-4444-4444-4444-444444444442"), true, 0 },
                    { new Guid("a5cca799-5ebd-41a1-aecc-3924da0bb46d"), 4000000m, 0, new Guid("44444444-4444-4444-4444-444444444443"), true, 2 },
                    { new Guid("a73f0dce-5419-4a49-af75-dd4574fdd3f0"), 500000m, 0, new Guid("11111111-1111-1111-1111-111111111113"), true, 1 },
                    { new Guid("a9c57f94-3a20-40f3-a040-095eb85ac813"), 2000000m, 0, new Guid("33333333-3333-3333-3333-333333333333"), true, 1 },
                    { new Guid("a9cafc26-420e-4b20-96b8-70d8c2c630d9"), 300000m, 1, new Guid("11111111-1111-1111-1111-111111111112"), true, 0 },
                    { new Guid("aa3e6e1e-4404-4d4c-9ebe-9c2474979309"), 700000m, 1, new Guid("33333333-3333-3333-3333-333333333333"), true, 0 },
                    { new Guid("c91d5737-7931-4f78-9cbe-71f3278b9015"), 500000m, 1, new Guid("22222222-2222-2222-2222-222222222223"), true, 0 },
                    { new Guid("cd807078-59b0-4ca0-af04-6c2769b9d7a1"), 700000m, 1, new Guid("33333333-3333-3333-3333-333333333332"), true, 0 },
                    { new Guid("ec887f3f-659d-4759-9c93-5e4086eb0f33"), 5000000m, 0, new Guid("44444444-4444-4444-4444-444444444443"), true, 1 },
                    { new Guid("ed10653a-079b-4ff1-877e-2e2bb4d586a0"), 1000000m, 1, new Guid("33333333-3333-3333-3333-333333333334"), true, 0 },
                    { new Guid("ee38d982-2dad-4bad-880c-b458b399c6b5"), 1000000m, 0, new Guid("22222222-2222-2222-2222-222222222224"), true, 2 },
                    { new Guid("fa3aac78-817e-491e-a2e6-f34f19ff3d02"), 1500000m, 2, new Guid("33333333-3333-3333-3333-333333333335"), true, 0 },
                    { new Guid("faee1721-1abb-44f2-b4c4-8c000328dcde"), 500000m, 1, new Guid("22222222-2222-2222-2222-222222222222"), true, 0 },
                    { new Guid("fb07e2e8-5ad5-4ce2-acc9-6b176e24f6d9"), 3000000m, 0, new Guid("33333333-3333-3333-3333-333333333335"), true, 2 },
                    { new Guid("ffc65b0c-4497-473f-af0c-1661a85a913f"), 2000000m, 0, new Guid("33333333-3333-3333-3333-333333333334"), true, 2 }
                });
        }
    }
}
