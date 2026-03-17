using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EGI_Backend.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ClaimLogics : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("06466d51-ebc9-4bd2-9737-f369121f449d"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("0cacc84d-d001-460f-b2c9-1365299e607e"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("10b7b0ea-91e8-4c60-8d62-46e9f6aadb34"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("22585908-0381-44d0-830c-fcb60022b8fa"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("2c1b4fad-f92b-4de5-8edd-8bd669a47fd7"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("3514f4d3-8011-4f3e-9abd-0c06fba17c26"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("36e77efb-a76a-400c-86da-5671bd34ec0b"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("44383c65-bc40-4197-aa66-77e2498e5a1a"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("4fc1586c-9b55-4d51-a908-7ba468091b2d"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("537215a8-c4c5-40cd-83d8-742d71453fb5"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("5c337dae-6195-4e63-80e3-bd354bb53986"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("5f400cf9-7e80-4583-a975-e258e2fce458"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("759a3d7e-bc54-42e7-bbde-4065b00d6c99"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("8dce466f-d41c-4b1b-b124-08eb890d16e4"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("968ba075-4659-4a73-a0a5-7c7106360685"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("9cc13114-a9c1-484d-8efa-fe41254d4911"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("a1732dd8-2c94-4a93-bcbc-d239370811b4"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("a31e56be-d462-426f-83e8-b05cb100bce4"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("b377d73c-7e97-4f0c-8c9d-effa276ddad6"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("bab14c76-8f29-4ab4-9826-0d567a067bad"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("bdc9b72e-0e42-40fa-bd5b-e2688b7628a8"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("c0cb39e2-a543-4744-b438-1071159e8c5a"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("d4ad0434-5163-4d9e-b0fe-f8c461d3620e"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("d6f4a399-4273-41f4-b0eb-19e50c624d6b"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("e0bd3383-97c4-4a04-85c1-33bf2d98fdcf"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("e6e436d0-3f4d-4c63-bf3c-cdc941444295"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("ee8d4cce-2a43-49ab-81c6-bd4420c25662"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("f16a453f-51ca-4cad-b27a-c86e34d05c9e"));

            migrationBuilder.AddColumn<string>(
                name: "ExtractedCauseOfDeath",
                table: "Claims",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ExtractedDateOfDeath",
                table: "Claims",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExtractedDiagnosis",
                table: "Claims",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExtractedFirNumber",
                table: "Claims",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ExtractedIncidentDate",
                table: "Claims",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExtractedPoliceStation",
                table: "Claims",
                type: "nvarchar(max)",
                nullable: true);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "ExtractedCauseOfDeath",
                table: "Claims");

            migrationBuilder.DropColumn(
                name: "ExtractedDateOfDeath",
                table: "Claims");

            migrationBuilder.DropColumn(
                name: "ExtractedDiagnosis",
                table: "Claims");

            migrationBuilder.DropColumn(
                name: "ExtractedFirNumber",
                table: "Claims");

            migrationBuilder.DropColumn(
                name: "ExtractedIncidentDate",
                table: "Claims");

            migrationBuilder.DropColumn(
                name: "ExtractedPoliceStation",
                table: "Claims");

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 13, 7, 2, 35, 667, DateTimeKind.Utc).AddTicks(3441));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111112"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 13, 7, 2, 35, 667, DateTimeKind.Utc).AddTicks(3467));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111113"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 13, 7, 2, 35, 667, DateTimeKind.Utc).AddTicks(3476));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222221"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 13, 7, 2, 35, 667, DateTimeKind.Utc).AddTicks(3481));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 13, 7, 2, 35, 667, DateTimeKind.Utc).AddTicks(3486));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222223"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 13, 7, 2, 35, 667, DateTimeKind.Utc).AddTicks(3490));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222224"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 13, 7, 2, 35, 667, DateTimeKind.Utc).AddTicks(3495));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333331"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 13, 7, 2, 35, 667, DateTimeKind.Utc).AddTicks(3499));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333332"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 13, 7, 2, 35, 667, DateTimeKind.Utc).AddTicks(3506));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 13, 7, 2, 35, 667, DateTimeKind.Utc).AddTicks(3511));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333334"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 13, 7, 2, 35, 667, DateTimeKind.Utc).AddTicks(3516));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333335"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 13, 7, 2, 35, 667, DateTimeKind.Utc).AddTicks(3522));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444441"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 13, 7, 2, 35, 667, DateTimeKind.Utc).AddTicks(3526));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444442"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 13, 7, 2, 35, 667, DateTimeKind.Utc).AddTicks(3531));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444443"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 13, 7, 2, 35, 667, DateTimeKind.Utc).AddTicks(3536));

            migrationBuilder.InsertData(
                table: "PlanCoverages",
                columns: new[] { "Id", "CoverageAmount", "CoveredGroup", "InsurancePlanId", "IsActive", "Type" },
                values: new object[,]
                {
                    { new Guid("06466d51-ebc9-4bd2-9737-f369121f449d"), 1500000m, 0, new Guid("22222222-2222-2222-2222-222222222224"), true, 1 },
                    { new Guid("0cacc84d-d001-460f-b2c9-1365299e607e"), 3000000m, 0, new Guid("44444444-4444-4444-4444-444444444442"), true, 1 },
                    { new Guid("10b7b0ea-91e8-4c60-8d62-46e9f6aadb34"), 200000m, 0, new Guid("11111111-1111-1111-1111-111111111111"), true, 0 },
                    { new Guid("22585908-0381-44d0-830c-fcb60022b8fa"), 700000m, 1, new Guid("44444444-4444-4444-4444-444444444441"), true, 0 },
                    { new Guid("2c1b4fad-f92b-4de5-8edd-8bd669a47fd7"), 500000m, 1, new Guid("22222222-2222-2222-2222-222222222222"), true, 0 },
                    { new Guid("3514f4d3-8011-4f3e-9abd-0c06fba17c26"), 4000000m, 0, new Guid("33333333-3333-3333-3333-333333333335"), true, 1 },
                    { new Guid("36e77efb-a76a-400c-86da-5671bd34ec0b"), 2000000m, 2, new Guid("44444444-4444-4444-4444-444444444443"), true, 0 },
                    { new Guid("44383c65-bc40-4197-aa66-77e2498e5a1a"), 500000m, 0, new Guid("33333333-3333-3333-3333-333333333331"), true, 0 },
                    { new Guid("4fc1586c-9b55-4d51-a908-7ba468091b2d"), 300000m, 0, new Guid("22222222-2222-2222-2222-222222222221"), true, 0 },
                    { new Guid("537215a8-c4c5-40cd-83d8-742d71453fb5"), 700000m, 1, new Guid("22222222-2222-2222-2222-222222222224"), true, 0 },
                    { new Guid("5c337dae-6195-4e63-80e3-bd354bb53986"), 1000000m, 1, new Guid("33333333-3333-3333-3333-333333333334"), true, 0 },
                    { new Guid("5f400cf9-7e80-4583-a975-e258e2fce458"), 700000m, 1, new Guid("33333333-3333-3333-3333-333333333333"), true, 0 },
                    { new Guid("759a3d7e-bc54-42e7-bbde-4065b00d6c99"), 2000000m, 0, new Guid("33333333-3333-3333-3333-333333333334"), true, 2 },
                    { new Guid("8dce466f-d41c-4b1b-b124-08eb890d16e4"), 3000000m, 0, new Guid("33333333-3333-3333-3333-333333333334"), true, 1 },
                    { new Guid("968ba075-4659-4a73-a0a5-7c7106360685"), 500000m, 0, new Guid("11111111-1111-1111-1111-111111111113"), true, 1 },
                    { new Guid("9cc13114-a9c1-484d-8efa-fe41254d4911"), 1000000m, 0, new Guid("22222222-2222-2222-2222-222222222223"), true, 1 },
                    { new Guid("a1732dd8-2c94-4a93-bcbc-d239370811b4"), 1500000m, 2, new Guid("33333333-3333-3333-3333-333333333335"), true, 0 },
                    { new Guid("a31e56be-d462-426f-83e8-b05cb100bce4"), 1000000m, 0, new Guid("22222222-2222-2222-2222-222222222224"), true, 2 },
                    { new Guid("b377d73c-7e97-4f0c-8c9d-effa276ddad6"), 3000000m, 0, new Guid("33333333-3333-3333-3333-333333333335"), true, 2 },
                    { new Guid("bab14c76-8f29-4ab4-9826-0d567a067bad"), 300000m, 1, new Guid("11111111-1111-1111-1111-111111111113"), true, 0 },
                    { new Guid("bdc9b72e-0e42-40fa-bd5b-e2688b7628a8"), 500000m, 0, new Guid("33333333-3333-3333-3333-333333333335"), true, 3 },
                    { new Guid("c0cb39e2-a543-4744-b438-1071159e8c5a"), 300000m, 1, new Guid("11111111-1111-1111-1111-111111111112"), true, 0 },
                    { new Guid("d4ad0434-5163-4d9e-b0fe-f8c461d3620e"), 5000000m, 0, new Guid("44444444-4444-4444-4444-444444444443"), true, 1 },
                    { new Guid("d6f4a399-4273-41f4-b0eb-19e50c624d6b"), 500000m, 1, new Guid("22222222-2222-2222-2222-222222222223"), true, 0 },
                    { new Guid("e0bd3383-97c4-4a04-85c1-33bf2d98fdcf"), 2000000m, 0, new Guid("33333333-3333-3333-3333-333333333333"), true, 1 },
                    { new Guid("e6e436d0-3f4d-4c63-bf3c-cdc941444295"), 700000m, 1, new Guid("33333333-3333-3333-3333-333333333332"), true, 0 },
                    { new Guid("ee8d4cce-2a43-49ab-81c6-bd4420c25662"), 1000000m, 1, new Guid("44444444-4444-4444-4444-444444444442"), true, 0 },
                    { new Guid("f16a453f-51ca-4cad-b27a-c86e34d05c9e"), 4000000m, 0, new Guid("44444444-4444-4444-4444-444444444443"), true, 2 }
                });
        }
    }
}
