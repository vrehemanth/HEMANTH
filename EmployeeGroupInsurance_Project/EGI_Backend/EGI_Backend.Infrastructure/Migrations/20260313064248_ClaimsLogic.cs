using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EGI_Backend.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ClaimsLogic : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("0dcf4345-a5e7-42a3-88a7-e115420420e7"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("15c49118-44c1-43c6-ab1a-095a0ffdcf5a"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("213cf098-a526-4cd7-a268-a79bb8773d94"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("2375898f-6802-4ea5-bd8a-0e77a41d0dc7"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("237b0ea7-50a7-4e45-9e70-fbce45df82e3"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("2b9ce218-58af-4d74-89bc-2640b36ad8fa"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("3c709b06-0fa9-47cb-93f9-a3e5b5d9bc6a"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("41a6781d-59f2-476e-afb1-103e2edf2ee9"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("4b75df30-7ff1-4580-9b4a-7bb6f67b445a"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("57689e5f-fbd5-4e7f-8ca0-f45b1fe894e1"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("589bb989-6ba3-48d0-a95b-d710cf9ada54"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("6f2e09e8-27b8-4c7a-8a2f-0385c9b55c7a"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("7193b5bd-c02a-46c1-b5c7-5de07f44e74b"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("79f75d05-cd4f-418a-beb4-ae6ecd3430a5"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("8a55205c-699b-4a47-afc9-533bb4965065"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("8d6c711c-8b8e-4c81-9717-2f03dbe17cdf"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("9660f92b-0950-44f1-8f72-0ac08f5bdd19"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("9a1da4f1-720b-4cae-adda-ed64dc341479"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("c1596dae-484e-44a7-b17a-55e634b48490"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("c2f13c20-5b95-469d-9f89-787cf1b60138"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("c68032ad-c3aa-4f4e-83f5-60de1c97e120"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("c861a741-9eba-4cca-8cb4-81484b88ded0"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("dc850a98-6ca3-47ca-bb43-b802de04e2bb"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("deab626d-1990-4c43-bc61-ddba294bd276"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("e98660b3-2f86-4526-9be6-1fa553d38358"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("f55ae5b8-f7ff-4e1f-be2b-301e3e1b99a1"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("f80713f0-a17d-4ba1-95cb-609bb9c57b8e"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("fb955004-cb5a-4a0b-9630-33121ec942d0"));

            migrationBuilder.AddColumn<decimal>(
                name: "ExtractedBillAmount",
                table: "Claims",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ExtractedBillDate",
                table: "Claims",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExtractedHospitalName",
                table: "Claims",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsAutoApproved",
                table: "Claims",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "RequiresSeniorApproval",
                table: "Claims",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "SeniorApprovedAt",
                table: "Claims",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SeniorApprovedBy",
                table: "Claims",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SeniorApprovedByUserId",
                table: "Claims",
                type: "uniqueidentifier",
                nullable: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_IsRead",
                table: "Notifications",
                column: "IsRead");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_DueDate",
                table: "Invoices",
                column: "DueDate");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_Status",
                table: "Invoices",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_CorporateClients_CompanyName",
                table: "CorporateClients",
                column: "CompanyName");

            migrationBuilder.CreateIndex(
                name: "IX_Claims_SeniorApprovedByUserId",
                table: "Claims",
                column: "SeniorApprovedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Claims_Users_SeniorApprovedByUserId",
                table: "Claims",
                column: "SeniorApprovedByUserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Claims_Users_SeniorApprovedByUserId",
                table: "Claims");

            migrationBuilder.DropIndex(
                name: "IX_Notifications_IsRead",
                table: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_DueDate",
                table: "Invoices");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_Status",
                table: "Invoices");

            migrationBuilder.DropIndex(
                name: "IX_CorporateClients_CompanyName",
                table: "CorporateClients");

            migrationBuilder.DropIndex(
                name: "IX_Claims_SeniorApprovedByUserId",
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

            migrationBuilder.DropColumn(
                name: "ExtractedBillAmount",
                table: "Claims");

            migrationBuilder.DropColumn(
                name: "ExtractedBillDate",
                table: "Claims");

            migrationBuilder.DropColumn(
                name: "ExtractedHospitalName",
                table: "Claims");

            migrationBuilder.DropColumn(
                name: "IsAutoApproved",
                table: "Claims");

            migrationBuilder.DropColumn(
                name: "RequiresSeniorApproval",
                table: "Claims");

            migrationBuilder.DropColumn(
                name: "SeniorApprovedAt",
                table: "Claims");

            migrationBuilder.DropColumn(
                name: "SeniorApprovedBy",
                table: "Claims");

            migrationBuilder.DropColumn(
                name: "SeniorApprovedByUserId",
                table: "Claims");

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 10, 7, 11, 42, 868, DateTimeKind.Utc).AddTicks(1070));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111112"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 10, 7, 11, 42, 868, DateTimeKind.Utc).AddTicks(1094));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111113"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 10, 7, 11, 42, 868, DateTimeKind.Utc).AddTicks(1100));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222221"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 10, 7, 11, 42, 868, DateTimeKind.Utc).AddTicks(1107));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 10, 7, 11, 42, 868, DateTimeKind.Utc).AddTicks(1192));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222223"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 10, 7, 11, 42, 868, DateTimeKind.Utc).AddTicks(1198));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222224"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 10, 7, 11, 42, 868, DateTimeKind.Utc).AddTicks(1203));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333331"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 10, 7, 11, 42, 868, DateTimeKind.Utc).AddTicks(1207));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333332"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 10, 7, 11, 42, 868, DateTimeKind.Utc).AddTicks(1212));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 10, 7, 11, 42, 868, DateTimeKind.Utc).AddTicks(1217));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333334"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 10, 7, 11, 42, 868, DateTimeKind.Utc).AddTicks(1221));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333335"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 10, 7, 11, 42, 868, DateTimeKind.Utc).AddTicks(1225));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444441"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 10, 7, 11, 42, 868, DateTimeKind.Utc).AddTicks(1234));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444442"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 10, 7, 11, 42, 868, DateTimeKind.Utc).AddTicks(1239));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444443"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 10, 7, 11, 42, 868, DateTimeKind.Utc).AddTicks(1244));

            migrationBuilder.InsertData(
                table: "PlanCoverages",
                columns: new[] { "Id", "CoverageAmount", "CoveredGroup", "InsurancePlanId", "IsActive", "Type" },
                values: new object[,]
                {
                    { new Guid("0dcf4345-a5e7-42a3-88a7-e115420420e7"), 700000m, 1, new Guid("44444444-4444-4444-4444-444444444441"), true, 0 },
                    { new Guid("15c49118-44c1-43c6-ab1a-095a0ffdcf5a"), 500000m, 0, new Guid("33333333-3333-3333-3333-333333333335"), true, 3 },
                    { new Guid("213cf098-a526-4cd7-a268-a79bb8773d94"), 3000000m, 0, new Guid("44444444-4444-4444-4444-444444444442"), true, 1 },
                    { new Guid("2375898f-6802-4ea5-bd8a-0e77a41d0dc7"), 5000000m, 0, new Guid("44444444-4444-4444-4444-444444444443"), true, 1 },
                    { new Guid("237b0ea7-50a7-4e45-9e70-fbce45df82e3"), 1500000m, 0, new Guid("22222222-2222-2222-2222-222222222224"), true, 1 },
                    { new Guid("2b9ce218-58af-4d74-89bc-2640b36ad8fa"), 2000000m, 2, new Guid("44444444-4444-4444-4444-444444444443"), true, 0 },
                    { new Guid("3c709b06-0fa9-47cb-93f9-a3e5b5d9bc6a"), 1000000m, 1, new Guid("44444444-4444-4444-4444-444444444442"), true, 0 },
                    { new Guid("41a6781d-59f2-476e-afb1-103e2edf2ee9"), 300000m, 1, new Guid("11111111-1111-1111-1111-111111111112"), true, 0 },
                    { new Guid("4b75df30-7ff1-4580-9b4a-7bb6f67b445a"), 2000000m, 0, new Guid("33333333-3333-3333-3333-333333333334"), true, 2 },
                    { new Guid("57689e5f-fbd5-4e7f-8ca0-f45b1fe894e1"), 4000000m, 0, new Guid("33333333-3333-3333-3333-333333333335"), true, 1 },
                    { new Guid("589bb989-6ba3-48d0-a95b-d710cf9ada54"), 300000m, 1, new Guid("11111111-1111-1111-1111-111111111113"), true, 0 },
                    { new Guid("6f2e09e8-27b8-4c7a-8a2f-0385c9b55c7a"), 500000m, 1, new Guid("22222222-2222-2222-2222-222222222222"), true, 0 },
                    { new Guid("7193b5bd-c02a-46c1-b5c7-5de07f44e74b"), 1000000m, 1, new Guid("33333333-3333-3333-3333-333333333334"), true, 0 },
                    { new Guid("79f75d05-cd4f-418a-beb4-ae6ecd3430a5"), 1000000m, 0, new Guid("22222222-2222-2222-2222-222222222224"), true, 2 },
                    { new Guid("8a55205c-699b-4a47-afc9-533bb4965065"), 500000m, 1, new Guid("22222222-2222-2222-2222-222222222223"), true, 0 },
                    { new Guid("8d6c711c-8b8e-4c81-9717-2f03dbe17cdf"), 500000m, 0, new Guid("33333333-3333-3333-3333-333333333331"), true, 0 },
                    { new Guid("9660f92b-0950-44f1-8f72-0ac08f5bdd19"), 2000000m, 0, new Guid("33333333-3333-3333-3333-333333333333"), true, 1 },
                    { new Guid("9a1da4f1-720b-4cae-adda-ed64dc341479"), 1000000m, 0, new Guid("22222222-2222-2222-2222-222222222223"), true, 1 },
                    { new Guid("c1596dae-484e-44a7-b17a-55e634b48490"), 500000m, 0, new Guid("11111111-1111-1111-1111-111111111113"), true, 1 },
                    { new Guid("c2f13c20-5b95-469d-9f89-787cf1b60138"), 700000m, 1, new Guid("22222222-2222-2222-2222-222222222224"), true, 0 },
                    { new Guid("c68032ad-c3aa-4f4e-83f5-60de1c97e120"), 700000m, 1, new Guid("33333333-3333-3333-3333-333333333332"), true, 0 },
                    { new Guid("c861a741-9eba-4cca-8cb4-81484b88ded0"), 300000m, 0, new Guid("22222222-2222-2222-2222-222222222221"), true, 0 },
                    { new Guid("dc850a98-6ca3-47ca-bb43-b802de04e2bb"), 700000m, 1, new Guid("33333333-3333-3333-3333-333333333333"), true, 0 },
                    { new Guid("deab626d-1990-4c43-bc61-ddba294bd276"), 4000000m, 0, new Guid("44444444-4444-4444-4444-444444444443"), true, 2 },
                    { new Guid("e98660b3-2f86-4526-9be6-1fa553d38358"), 3000000m, 0, new Guid("33333333-3333-3333-3333-333333333335"), true, 2 },
                    { new Guid("f55ae5b8-f7ff-4e1f-be2b-301e3e1b99a1"), 3000000m, 0, new Guid("33333333-3333-3333-3333-333333333334"), true, 1 },
                    { new Guid("f80713f0-a17d-4ba1-95cb-609bb9c57b8e"), 200000m, 0, new Guid("11111111-1111-1111-1111-111111111111"), true, 0 },
                    { new Guid("fb955004-cb5a-4a0b-9630-33121ec942d0"), 1500000m, 2, new Guid("33333333-3333-3333-3333-333333333335"), true, 0 }
                });
        }
    }
}
