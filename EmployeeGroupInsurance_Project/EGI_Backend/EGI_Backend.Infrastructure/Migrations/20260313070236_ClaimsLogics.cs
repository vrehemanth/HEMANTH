using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EGI_Backend.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ClaimsLogics : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Claims_Users_SeniorApprovedByUserId",
                table: "Claims");

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("00ec1547-8f78-4d3f-b24c-08fe73102a23"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("01fdbfcc-c11f-4060-828b-b8fe708086dc"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("04ae71a0-7b72-4d1d-92e7-4525ef31aa68"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("09294ecb-a396-4cdb-a784-6663ca73d259"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("0b9efdc1-6169-49ff-92ca-1d1c866dae17"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("0e89966c-c531-4976-a44b-4a552d532912"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("35b951ac-ded6-4c18-8a30-cd71e7ba7890"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("459e105e-2cec-4f5b-acdb-e97067619afa"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("4b35b8b5-eac1-4668-877d-846829ca74cd"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("4d68836d-076e-4545-879c-4410d9785667"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("5f4096b0-4df5-4f5f-90a2-d4f23c69271f"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("664c6f74-5522-4a9c-8092-94e027095108"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("75afc5fc-42e9-4851-ad71-d05041c15c40"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("7f5c7698-9e3c-4461-9214-4b97303c8cb4"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("9d57502b-8a60-465d-8b24-5b61ab319da6"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("af8ecd8b-7b59-4ea1-a953-4f790d1c6b68"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("b0a026d1-1123-4519-8193-294cbb316c41"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("b33349e1-91e7-438c-a3a7-679e59cda30a"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("b4d0cf32-045a-4a4d-b9c0-9f2079f74845"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("c10f91db-3d12-4e83-8f8d-3bbfd14975b9"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("cff4c789-4b7a-4238-99b6-09e06f375086"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("d9bca59d-a4de-48e5-9fa3-2cedbafc9e50"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("d9da7699-2219-487c-b78f-82581f4a0480"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("dabf1532-1c1e-444b-b9d5-d12634cbf55f"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("e593c1c3-0a40-4788-b093-b42aae67b355"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("ecfcf10c-bfc8-4ada-8dfe-97132d9cd3e9"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("ef174cb9-ce86-4081-ab0d-c636ba7126c3"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("ff86a6e3-6fbb-49e0-8054-793b2c55e201"));

            migrationBuilder.RenameColumn(
                name: "SeniorApprovedByUserId",
                table: "Claims",
                newName: "AdminApprovedByUserId");

            migrationBuilder.RenameColumn(
                name: "SeniorApprovedBy",
                table: "Claims",
                newName: "AdminApprovedBy");

            migrationBuilder.RenameColumn(
                name: "SeniorApprovedAt",
                table: "Claims",
                newName: "AdminApprovedAt");

            migrationBuilder.RenameColumn(
                name: "RequiresSeniorApproval",
                table: "Claims",
                newName: "RequiresAdminApproval");

            migrationBuilder.RenameIndex(
                name: "IX_Claims_SeniorApprovedByUserId",
                table: "Claims",
                newName: "IX_Claims_AdminApprovedByUserId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Claims_Users_AdminApprovedByUserId",
                table: "Claims",
                column: "AdminApprovedByUserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Claims_Users_AdminApprovedByUserId",
                table: "Claims");

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

            migrationBuilder.RenameColumn(
                name: "RequiresAdminApproval",
                table: "Claims",
                newName: "RequiresSeniorApproval");

            migrationBuilder.RenameColumn(
                name: "AdminApprovedByUserId",
                table: "Claims",
                newName: "SeniorApprovedByUserId");

            migrationBuilder.RenameColumn(
                name: "AdminApprovedBy",
                table: "Claims",
                newName: "SeniorApprovedBy");

            migrationBuilder.RenameColumn(
                name: "AdminApprovedAt",
                table: "Claims",
                newName: "SeniorApprovedAt");

            migrationBuilder.RenameIndex(
                name: "IX_Claims_AdminApprovedByUserId",
                table: "Claims",
                newName: "IX_Claims_SeniorApprovedByUserId");

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 13, 6, 42, 46, 102, DateTimeKind.Utc).AddTicks(3138));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111112"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 13, 6, 42, 46, 102, DateTimeKind.Utc).AddTicks(3163));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111113"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 13, 6, 42, 46, 102, DateTimeKind.Utc).AddTicks(3169));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222221"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 13, 6, 42, 46, 102, DateTimeKind.Utc).AddTicks(3174));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 13, 6, 42, 46, 102, DateTimeKind.Utc).AddTicks(3180));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222223"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 13, 6, 42, 46, 102, DateTimeKind.Utc).AddTicks(3197));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222224"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 13, 6, 42, 46, 102, DateTimeKind.Utc).AddTicks(3202));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333331"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 13, 6, 42, 46, 102, DateTimeKind.Utc).AddTicks(3206));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333332"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 13, 6, 42, 46, 102, DateTimeKind.Utc).AddTicks(3211));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 13, 6, 42, 46, 102, DateTimeKind.Utc).AddTicks(3215));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333334"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 13, 6, 42, 46, 102, DateTimeKind.Utc).AddTicks(3219));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333335"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 13, 6, 42, 46, 102, DateTimeKind.Utc).AddTicks(3223));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444441"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 13, 6, 42, 46, 102, DateTimeKind.Utc).AddTicks(3228));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444442"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 13, 6, 42, 46, 102, DateTimeKind.Utc).AddTicks(3238));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444443"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 13, 6, 42, 46, 102, DateTimeKind.Utc).AddTicks(3242));

            migrationBuilder.InsertData(
                table: "PlanCoverages",
                columns: new[] { "Id", "CoverageAmount", "CoveredGroup", "InsurancePlanId", "IsActive", "Type" },
                values: new object[,]
                {
                    { new Guid("00ec1547-8f78-4d3f-b24c-08fe73102a23"), 4000000m, 0, new Guid("44444444-4444-4444-4444-444444444443"), true, 2 },
                    { new Guid("01fdbfcc-c11f-4060-828b-b8fe708086dc"), 1500000m, 0, new Guid("22222222-2222-2222-2222-222222222224"), true, 1 },
                    { new Guid("04ae71a0-7b72-4d1d-92e7-4525ef31aa68"), 2000000m, 2, new Guid("44444444-4444-4444-4444-444444444443"), true, 0 },
                    { new Guid("09294ecb-a396-4cdb-a784-6663ca73d259"), 2000000m, 0, new Guid("33333333-3333-3333-3333-333333333334"), true, 2 },
                    { new Guid("0b9efdc1-6169-49ff-92ca-1d1c866dae17"), 700000m, 1, new Guid("33333333-3333-3333-3333-333333333333"), true, 0 },
                    { new Guid("0e89966c-c531-4976-a44b-4a552d532912"), 500000m, 0, new Guid("33333333-3333-3333-3333-333333333335"), true, 3 },
                    { new Guid("35b951ac-ded6-4c18-8a30-cd71e7ba7890"), 300000m, 1, new Guid("11111111-1111-1111-1111-111111111113"), true, 0 },
                    { new Guid("459e105e-2cec-4f5b-acdb-e97067619afa"), 200000m, 0, new Guid("11111111-1111-1111-1111-111111111111"), true, 0 },
                    { new Guid("4b35b8b5-eac1-4668-877d-846829ca74cd"), 500000m, 1, new Guid("22222222-2222-2222-2222-222222222222"), true, 0 },
                    { new Guid("4d68836d-076e-4545-879c-4410d9785667"), 5000000m, 0, new Guid("44444444-4444-4444-4444-444444444443"), true, 1 },
                    { new Guid("5f4096b0-4df5-4f5f-90a2-d4f23c69271f"), 1000000m, 0, new Guid("22222222-2222-2222-2222-222222222224"), true, 2 },
                    { new Guid("664c6f74-5522-4a9c-8092-94e027095108"), 500000m, 0, new Guid("11111111-1111-1111-1111-111111111113"), true, 1 },
                    { new Guid("75afc5fc-42e9-4851-ad71-d05041c15c40"), 700000m, 1, new Guid("33333333-3333-3333-3333-333333333332"), true, 0 },
                    { new Guid("7f5c7698-9e3c-4461-9214-4b97303c8cb4"), 300000m, 0, new Guid("22222222-2222-2222-2222-222222222221"), true, 0 },
                    { new Guid("9d57502b-8a60-465d-8b24-5b61ab319da6"), 3000000m, 0, new Guid("33333333-3333-3333-3333-333333333334"), true, 1 },
                    { new Guid("af8ecd8b-7b59-4ea1-a953-4f790d1c6b68"), 3000000m, 0, new Guid("44444444-4444-4444-4444-444444444442"), true, 1 },
                    { new Guid("b0a026d1-1123-4519-8193-294cbb316c41"), 1000000m, 1, new Guid("33333333-3333-3333-3333-333333333334"), true, 0 },
                    { new Guid("b33349e1-91e7-438c-a3a7-679e59cda30a"), 700000m, 1, new Guid("22222222-2222-2222-2222-222222222224"), true, 0 },
                    { new Guid("b4d0cf32-045a-4a4d-b9c0-9f2079f74845"), 2000000m, 0, new Guid("33333333-3333-3333-3333-333333333333"), true, 1 },
                    { new Guid("c10f91db-3d12-4e83-8f8d-3bbfd14975b9"), 500000m, 1, new Guid("22222222-2222-2222-2222-222222222223"), true, 0 },
                    { new Guid("cff4c789-4b7a-4238-99b6-09e06f375086"), 4000000m, 0, new Guid("33333333-3333-3333-3333-333333333335"), true, 1 },
                    { new Guid("d9bca59d-a4de-48e5-9fa3-2cedbafc9e50"), 700000m, 1, new Guid("44444444-4444-4444-4444-444444444441"), true, 0 },
                    { new Guid("d9da7699-2219-487c-b78f-82581f4a0480"), 500000m, 0, new Guid("33333333-3333-3333-3333-333333333331"), true, 0 },
                    { new Guid("dabf1532-1c1e-444b-b9d5-d12634cbf55f"), 3000000m, 0, new Guid("33333333-3333-3333-3333-333333333335"), true, 2 },
                    { new Guid("e593c1c3-0a40-4788-b093-b42aae67b355"), 1500000m, 2, new Guid("33333333-3333-3333-3333-333333333335"), true, 0 },
                    { new Guid("ecfcf10c-bfc8-4ada-8dfe-97132d9cd3e9"), 300000m, 1, new Guid("11111111-1111-1111-1111-111111111112"), true, 0 },
                    { new Guid("ef174cb9-ce86-4081-ab0d-c636ba7126c3"), 1000000m, 1, new Guid("44444444-4444-4444-4444-444444444442"), true, 0 },
                    { new Guid("ff86a6e3-6fbb-49e0-8054-793b2c55e201"), 1000000m, 0, new Guid("22222222-2222-2222-2222-222222222223"), true, 1 }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Claims_Users_SeniorApprovedByUserId",
                table: "Claims",
                column: "SeniorApprovedByUserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
