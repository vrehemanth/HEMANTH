using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EGI_Backend.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPhoneToCorporateClient : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("05d222ba-61eb-4032-9dc8-9c3ac481170d"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("1f4c41a7-77ed-4ef9-800a-c9a1a7ac396a"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("45af84ca-3899-40dd-9188-b99f38ae8eed"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("45dc6427-c65a-41ce-83fe-54b2862f0ee2"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("498c5d86-e9ba-428b-9351-f5137c29ba15"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("52bdd4e5-838c-4dfa-afe7-8e2d0cb1cbb2"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("52ce1ec5-c191-4e23-a624-bfb369c92677"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("5328fe38-c6b7-4c13-997e-f8c57ac00d41"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("53c58970-864a-49ce-83a1-fa6ea7b0d74e"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("64445448-5642-43c2-be80-d34b96732d49"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("7480f65f-4b3a-47a7-a57e-8e4c4f0ec65b"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("75b86368-6984-45da-8c17-ab20c763f032"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("78655cc0-0d11-4be8-b553-357cb0ce2f92"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("89eb951d-6962-47a1-aed6-6b66d90df4a3"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("8af58073-7df3-4b14-b29b-9b31915c9380"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("90ee139c-3e36-4b85-be77-5dba0d0eb60d"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("9e5b69d0-8100-4364-9229-7b3610882206"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("a16a4170-0e60-4477-becf-9f9981410c9b"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("ab6fa53a-cea8-464d-b2a4-50d1987a99b2"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("ab71795a-4643-43b1-a957-41744934bea7"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("afe93881-0fc8-43a6-a0fa-0f9087d6c664"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("b685ccaf-b41d-4ae2-9738-5b74548d068b"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("c9f78a9d-43c3-4f4d-a7fd-7377a92231a3"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("dd1eb353-363b-4af4-a593-550af53fc390"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("f2a63d37-40c1-4965-a6aa-5b65609376e6"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("f2c70143-fc80-480d-a51a-b4b5cc639308"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("f4cdc9d7-4220-4c77-9387-f72bd449fdc7"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("fbd746c8-3ef8-48ba-99f8-70ab15321286"));

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "CorporateClients",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 4, 15, 26, 24, 686, DateTimeKind.Utc).AddTicks(6521));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111112"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 4, 15, 26, 24, 686, DateTimeKind.Utc).AddTicks(6536));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111113"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 4, 15, 26, 24, 686, DateTimeKind.Utc).AddTicks(6541));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222221"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 4, 15, 26, 24, 686, DateTimeKind.Utc).AddTicks(6544));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 4, 15, 26, 24, 686, DateTimeKind.Utc).AddTicks(6548));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222223"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 4, 15, 26, 24, 686, DateTimeKind.Utc).AddTicks(6551));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222224"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 4, 15, 26, 24, 686, DateTimeKind.Utc).AddTicks(6553));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333331"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 4, 15, 26, 24, 686, DateTimeKind.Utc).AddTicks(6563));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333332"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 4, 15, 26, 24, 686, DateTimeKind.Utc).AddTicks(6566));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 4, 15, 26, 24, 686, DateTimeKind.Utc).AddTicks(6568));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333334"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 4, 15, 26, 24, 686, DateTimeKind.Utc).AddTicks(6571));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333335"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 4, 15, 26, 24, 686, DateTimeKind.Utc).AddTicks(6573));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444441"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 4, 15, 26, 24, 686, DateTimeKind.Utc).AddTicks(6576));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444442"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 4, 15, 26, 24, 686, DateTimeKind.Utc).AddTicks(6578));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444443"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 4, 15, 26, 24, 686, DateTimeKind.Utc).AddTicks(6581));

            migrationBuilder.InsertData(
                table: "PlanCoverages",
                columns: new[] { "Id", "CoverageAmount", "CoveredGroup", "InsurancePlanId", "IsActive", "Type" },
                values: new object[,]
                {
                    { new Guid("058fd95a-5ae9-4bdb-aea9-8f9ce53d0994"), 1000000m, 1, new Guid("33333333-3333-3333-3333-333333333334"), true, 0 },
                    { new Guid("1afbf498-2946-4b84-9333-8e802ac4049e"), 300000m, 1, new Guid("11111111-1111-1111-1111-111111111112"), true, 0 },
                    { new Guid("259d8b93-b5e3-4a47-b0b8-0c838cec1d9a"), 500000m, 0, new Guid("33333333-3333-3333-3333-333333333331"), true, 0 },
                    { new Guid("29267671-bf6e-40fe-9ec5-7f6cb0fd56bd"), 500000m, 0, new Guid("33333333-3333-3333-3333-333333333335"), true, 3 },
                    { new Guid("3031ffc8-5683-4fb3-bc29-1bb78de5b0ef"), 3000000m, 0, new Guid("44444444-4444-4444-4444-444444444442"), true, 1 },
                    { new Guid("344d08be-61f6-44c7-b833-f4657df58f45"), 1000000m, 1, new Guid("44444444-4444-4444-4444-444444444442"), true, 0 },
                    { new Guid("384e8807-e81c-495f-9e05-f902e9ec5401"), 700000m, 1, new Guid("44444444-4444-4444-4444-444444444441"), true, 0 },
                    { new Guid("38f7f72a-2d5a-4b6d-ad32-adcd94540258"), 300000m, 0, new Guid("22222222-2222-2222-2222-222222222221"), true, 0 },
                    { new Guid("4d685d7b-40b9-4cdf-95df-37d8973253e7"), 700000m, 1, new Guid("22222222-2222-2222-2222-222222222224"), true, 0 },
                    { new Guid("53f13a5d-7cb7-4b44-9820-aa66f59e98cc"), 2000000m, 0, new Guid("33333333-3333-3333-3333-333333333333"), true, 1 },
                    { new Guid("589a6d53-9a38-43ee-b800-66f0da7ee8d7"), 3000000m, 0, new Guid("33333333-3333-3333-3333-333333333334"), true, 1 },
                    { new Guid("5cbea108-d13f-4384-9cd0-989c9399f666"), 700000m, 1, new Guid("33333333-3333-3333-3333-333333333332"), true, 0 },
                    { new Guid("5f258b54-2930-4a1b-ad35-5655b96bb310"), 1500000m, 2, new Guid("33333333-3333-3333-3333-333333333335"), true, 0 },
                    { new Guid("6d0daaaa-4aa7-4f56-8281-3fa6a4f422dc"), 500000m, 1, new Guid("22222222-2222-2222-2222-222222222222"), true, 0 },
                    { new Guid("75b65240-507c-4ffc-9368-493733303332"), 500000m, 0, new Guid("11111111-1111-1111-1111-111111111113"), true, 1 },
                    { new Guid("91cc444c-3114-467b-a2fa-209bb1c4fd6d"), 5000000m, 0, new Guid("44444444-4444-4444-4444-444444444443"), true, 1 },
                    { new Guid("95d48669-a000-4dfc-8eb9-c855326a8384"), 200000m, 0, new Guid("11111111-1111-1111-1111-111111111111"), true, 0 },
                    { new Guid("9e27f78c-2ea5-4515-b70d-d1cdb4c3ce75"), 2000000m, 2, new Guid("44444444-4444-4444-4444-444444444443"), true, 0 },
                    { new Guid("a68fdbbd-b290-4ea2-93e5-449086eba658"), 4000000m, 0, new Guid("33333333-3333-3333-3333-333333333335"), true, 1 },
                    { new Guid("a7b2c5bf-1998-4309-85f9-4e2fe32decaf"), 4000000m, 0, new Guid("44444444-4444-4444-4444-444444444443"), true, 2 },
                    { new Guid("aa3cb881-3b72-4d13-93e3-10ea6df91c93"), 3000000m, 0, new Guid("33333333-3333-3333-3333-333333333335"), true, 2 },
                    { new Guid("ad57ca27-2d8b-4e1b-b8ca-907be6e32698"), 1500000m, 0, new Guid("22222222-2222-2222-2222-222222222224"), true, 1 },
                    { new Guid("be55ed67-5830-4a0c-ac57-4e22d79e4121"), 500000m, 1, new Guid("22222222-2222-2222-2222-222222222223"), true, 0 },
                    { new Guid("cd9d27de-b331-4612-ad20-7c9c000bd7d8"), 2000000m, 0, new Guid("33333333-3333-3333-3333-333333333334"), true, 2 },
                    { new Guid("e43ad514-f1f7-4b15-8484-9e3cacfd99a4"), 700000m, 1, new Guid("33333333-3333-3333-3333-333333333333"), true, 0 },
                    { new Guid("f4dbbf6e-31a2-4ffc-b56c-7722cf43ee33"), 300000m, 1, new Guid("11111111-1111-1111-1111-111111111113"), true, 0 },
                    { new Guid("f586aac0-75f1-495a-b650-fae8f262e225"), 1000000m, 0, new Guid("22222222-2222-2222-2222-222222222223"), true, 1 },
                    { new Guid("fabe36ba-b585-4250-b094-856fd4853939"), 1000000m, 0, new Guid("22222222-2222-2222-2222-222222222224"), true, 2 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("058fd95a-5ae9-4bdb-aea9-8f9ce53d0994"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("1afbf498-2946-4b84-9333-8e802ac4049e"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("259d8b93-b5e3-4a47-b0b8-0c838cec1d9a"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("29267671-bf6e-40fe-9ec5-7f6cb0fd56bd"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("3031ffc8-5683-4fb3-bc29-1bb78de5b0ef"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("344d08be-61f6-44c7-b833-f4657df58f45"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("384e8807-e81c-495f-9e05-f902e9ec5401"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("38f7f72a-2d5a-4b6d-ad32-adcd94540258"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("4d685d7b-40b9-4cdf-95df-37d8973253e7"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("53f13a5d-7cb7-4b44-9820-aa66f59e98cc"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("589a6d53-9a38-43ee-b800-66f0da7ee8d7"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("5cbea108-d13f-4384-9cd0-989c9399f666"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("5f258b54-2930-4a1b-ad35-5655b96bb310"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("6d0daaaa-4aa7-4f56-8281-3fa6a4f422dc"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("75b65240-507c-4ffc-9368-493733303332"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("91cc444c-3114-467b-a2fa-209bb1c4fd6d"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("95d48669-a000-4dfc-8eb9-c855326a8384"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("9e27f78c-2ea5-4515-b70d-d1cdb4c3ce75"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("a68fdbbd-b290-4ea2-93e5-449086eba658"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("a7b2c5bf-1998-4309-85f9-4e2fe32decaf"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("aa3cb881-3b72-4d13-93e3-10ea6df91c93"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("ad57ca27-2d8b-4e1b-b8ca-907be6e32698"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("be55ed67-5830-4a0c-ac57-4e22d79e4121"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("cd9d27de-b331-4612-ad20-7c9c000bd7d8"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("e43ad514-f1f7-4b15-8484-9e3cacfd99a4"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("f4dbbf6e-31a2-4ffc-b56c-7722cf43ee33"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("f586aac0-75f1-495a-b650-fae8f262e225"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("fabe36ba-b585-4250-b094-856fd4853939"));

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "CorporateClients");

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 3, 17, 50, 20, 442, DateTimeKind.Utc).AddTicks(8743));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111112"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 3, 17, 50, 20, 442, DateTimeKind.Utc).AddTicks(8766));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111113"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 3, 17, 50, 20, 442, DateTimeKind.Utc).AddTicks(8772));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222221"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 3, 17, 50, 20, 442, DateTimeKind.Utc).AddTicks(8779));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 3, 17, 50, 20, 442, DateTimeKind.Utc).AddTicks(8784));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222223"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 3, 17, 50, 20, 442, DateTimeKind.Utc).AddTicks(8806));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222224"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 3, 17, 50, 20, 442, DateTimeKind.Utc).AddTicks(8811));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333331"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 3, 17, 50, 20, 442, DateTimeKind.Utc).AddTicks(8816));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333332"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 3, 17, 50, 20, 442, DateTimeKind.Utc).AddTicks(8821));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 3, 17, 50, 20, 442, DateTimeKind.Utc).AddTicks(8825));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333334"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 3, 17, 50, 20, 442, DateTimeKind.Utc).AddTicks(8829));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333335"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 3, 17, 50, 20, 442, DateTimeKind.Utc).AddTicks(8834));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444441"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 3, 17, 50, 20, 442, DateTimeKind.Utc).AddTicks(8838));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444442"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 3, 17, 50, 20, 442, DateTimeKind.Utc).AddTicks(8847));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444443"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 3, 17, 50, 20, 442, DateTimeKind.Utc).AddTicks(8918));

            migrationBuilder.InsertData(
                table: "PlanCoverages",
                columns: new[] { "Id", "CoverageAmount", "CoveredGroup", "InsurancePlanId", "IsActive", "Type" },
                values: new object[,]
                {
                    { new Guid("05d222ba-61eb-4032-9dc8-9c3ac481170d"), 2000000m, 0, new Guid("33333333-3333-3333-3333-333333333333"), true, 1 },
                    { new Guid("1f4c41a7-77ed-4ef9-800a-c9a1a7ac396a"), 500000m, 1, new Guid("22222222-2222-2222-2222-222222222223"), true, 0 },
                    { new Guid("45af84ca-3899-40dd-9188-b99f38ae8eed"), 2000000m, 0, new Guid("33333333-3333-3333-3333-333333333334"), true, 2 },
                    { new Guid("45dc6427-c65a-41ce-83fe-54b2862f0ee2"), 700000m, 1, new Guid("33333333-3333-3333-3333-333333333332"), true, 0 },
                    { new Guid("498c5d86-e9ba-428b-9351-f5137c29ba15"), 500000m, 1, new Guid("22222222-2222-2222-2222-222222222222"), true, 0 },
                    { new Guid("52bdd4e5-838c-4dfa-afe7-8e2d0cb1cbb2"), 300000m, 1, new Guid("11111111-1111-1111-1111-111111111112"), true, 0 },
                    { new Guid("52ce1ec5-c191-4e23-a624-bfb369c92677"), 500000m, 0, new Guid("33333333-3333-3333-3333-333333333331"), true, 0 },
                    { new Guid("5328fe38-c6b7-4c13-997e-f8c57ac00d41"), 1000000m, 0, new Guid("22222222-2222-2222-2222-222222222223"), true, 1 },
                    { new Guid("53c58970-864a-49ce-83a1-fa6ea7b0d74e"), 500000m, 0, new Guid("11111111-1111-1111-1111-111111111113"), true, 1 },
                    { new Guid("64445448-5642-43c2-be80-d34b96732d49"), 4000000m, 0, new Guid("33333333-3333-3333-3333-333333333335"), true, 1 },
                    { new Guid("7480f65f-4b3a-47a7-a57e-8e4c4f0ec65b"), 1000000m, 0, new Guid("22222222-2222-2222-2222-222222222224"), true, 2 },
                    { new Guid("75b86368-6984-45da-8c17-ab20c763f032"), 700000m, 1, new Guid("22222222-2222-2222-2222-222222222224"), true, 0 },
                    { new Guid("78655cc0-0d11-4be8-b553-357cb0ce2f92"), 500000m, 0, new Guid("33333333-3333-3333-3333-333333333335"), true, 3 },
                    { new Guid("89eb951d-6962-47a1-aed6-6b66d90df4a3"), 700000m, 1, new Guid("44444444-4444-4444-4444-444444444441"), true, 0 },
                    { new Guid("8af58073-7df3-4b14-b29b-9b31915c9380"), 3000000m, 0, new Guid("33333333-3333-3333-3333-333333333334"), true, 1 },
                    { new Guid("90ee139c-3e36-4b85-be77-5dba0d0eb60d"), 1500000m, 2, new Guid("33333333-3333-3333-3333-333333333335"), true, 0 },
                    { new Guid("9e5b69d0-8100-4364-9229-7b3610882206"), 1000000m, 1, new Guid("33333333-3333-3333-3333-333333333334"), true, 0 },
                    { new Guid("a16a4170-0e60-4477-becf-9f9981410c9b"), 4000000m, 0, new Guid("44444444-4444-4444-4444-444444444443"), true, 2 },
                    { new Guid("ab6fa53a-cea8-464d-b2a4-50d1987a99b2"), 5000000m, 0, new Guid("44444444-4444-4444-4444-444444444443"), true, 1 },
                    { new Guid("ab71795a-4643-43b1-a957-41744934bea7"), 200000m, 0, new Guid("11111111-1111-1111-1111-111111111111"), true, 0 },
                    { new Guid("afe93881-0fc8-43a6-a0fa-0f9087d6c664"), 3000000m, 0, new Guid("44444444-4444-4444-4444-444444444442"), true, 1 },
                    { new Guid("b685ccaf-b41d-4ae2-9738-5b74548d068b"), 300000m, 1, new Guid("11111111-1111-1111-1111-111111111113"), true, 0 },
                    { new Guid("c9f78a9d-43c3-4f4d-a7fd-7377a92231a3"), 3000000m, 0, new Guid("33333333-3333-3333-3333-333333333335"), true, 2 },
                    { new Guid("dd1eb353-363b-4af4-a593-550af53fc390"), 1500000m, 0, new Guid("22222222-2222-2222-2222-222222222224"), true, 1 },
                    { new Guid("f2a63d37-40c1-4965-a6aa-5b65609376e6"), 2000000m, 2, new Guid("44444444-4444-4444-4444-444444444443"), true, 0 },
                    { new Guid("f2c70143-fc80-480d-a51a-b4b5cc639308"), 300000m, 0, new Guid("22222222-2222-2222-2222-222222222221"), true, 0 },
                    { new Guid("f4cdc9d7-4220-4c77-9387-f72bd449fdc7"), 700000m, 1, new Guid("33333333-3333-3333-3333-333333333333"), true, 0 },
                    { new Guid("fbd746c8-3ef8-48ba-99f8-70ab15321286"), 1000000m, 1, new Guid("44444444-4444-4444-4444-444444444442"), true, 0 }
                });
        }
    }
}
