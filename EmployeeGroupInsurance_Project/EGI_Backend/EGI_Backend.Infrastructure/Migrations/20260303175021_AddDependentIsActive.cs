using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EGI_Backend.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddDependentIsActive : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("053599c6-f572-49e8-898a-44f07f7a5f71"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("060bc6b9-03ae-41a8-8af7-6d1b9f2205a1"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("1598f0a9-6162-4340-a038-2e5d1539dc30"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("195ced8d-cec3-4e6a-a3ef-c805f54d2425"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("2aa6e2b7-2d78-4bf5-b4bd-f3e3db9b6267"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("3479d48b-d818-4288-ba08-d5b4af220283"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("38cc0a7c-f9d4-402a-9ee5-061f5ad84612"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("3b26cb3d-3b36-4bae-9f9c-294c0611ea0a"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("43388b96-b854-48be-992c-e4496801bd53"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("59c21b23-3b50-430e-9ac0-935b04e054fd"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("61d69495-6047-4b9b-959c-1d3f9ed70946"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("6440613f-f50d-4d01-be92-8f71809113bd"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("66e15643-9e64-482d-ab51-fdaa3e66d8ba"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("7aa35f0a-aa20-4876-bdfa-38400aac6c1b"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("7cfdf408-e543-4d5a-b51c-18dab76c6b61"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("80fdd570-20ab-41a0-9f5c-bb0a59b88f8f"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("837306c2-13f0-4b21-8b91-7b66ffd7d5d1"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("931bb9e2-8400-4c82-bfd6-1472c15b2811"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("94e9f558-6d4b-4aa5-9ef7-61b00acd670f"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("98510cfc-d567-42aa-bd4a-4cdfaeb8072b"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("a12a15d8-63b4-45e5-82eb-312b0b364739"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("aef2d9f6-424a-427f-b246-01d26e12c3e3"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("c1f809ed-1f9f-437b-915c-25869c7318d4"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("ddeb34b5-5d21-452d-8252-11fcc2aaf365"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("e0c4f00d-f537-4748-b099-9ebb8fd6d58d"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("f3c3d51b-c072-46b2-bdd1-5fc3a0c8ca4b"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("fecc0f63-8ae1-45b8-a95b-702d3a340478"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("ff0f6b24-b570-444b-8f8c-3e1b528bb164"));

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Dependents",
                type: "bit",
                nullable: false,
                defaultValue: false);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Dependents");

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 3, 9, 36, 24, 141, DateTimeKind.Utc).AddTicks(6950));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111112"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 3, 9, 36, 24, 141, DateTimeKind.Utc).AddTicks(6973));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111113"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 3, 9, 36, 24, 141, DateTimeKind.Utc).AddTicks(6979));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222221"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 3, 9, 36, 24, 141, DateTimeKind.Utc).AddTicks(6983));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 3, 9, 36, 24, 141, DateTimeKind.Utc).AddTicks(6987));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222223"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 3, 9, 36, 24, 141, DateTimeKind.Utc).AddTicks(6989));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222224"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 3, 9, 36, 24, 141, DateTimeKind.Utc).AddTicks(6991));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333331"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 3, 9, 36, 24, 141, DateTimeKind.Utc).AddTicks(7000));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333332"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 3, 9, 36, 24, 141, DateTimeKind.Utc).AddTicks(7002));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 3, 9, 36, 24, 141, DateTimeKind.Utc).AddTicks(7004));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333334"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 3, 9, 36, 24, 141, DateTimeKind.Utc).AddTicks(7007));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333335"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 3, 9, 36, 24, 141, DateTimeKind.Utc).AddTicks(7012));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444441"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 3, 9, 36, 24, 141, DateTimeKind.Utc).AddTicks(7014));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444442"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 3, 9, 36, 24, 141, DateTimeKind.Utc).AddTicks(7016));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444443"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 3, 9, 36, 24, 141, DateTimeKind.Utc).AddTicks(7018));

            migrationBuilder.InsertData(
                table: "PlanCoverages",
                columns: new[] { "Id", "CoverageAmount", "CoveredGroup", "InsurancePlanId", "IsActive", "Type" },
                values: new object[,]
                {
                    { new Guid("053599c6-f572-49e8-898a-44f07f7a5f71"), 5000000m, 0, new Guid("44444444-4444-4444-4444-444444444443"), true, 1 },
                    { new Guid("060bc6b9-03ae-41a8-8af7-6d1b9f2205a1"), 1500000m, 2, new Guid("33333333-3333-3333-3333-333333333335"), true, 0 },
                    { new Guid("1598f0a9-6162-4340-a038-2e5d1539dc30"), 300000m, 1, new Guid("11111111-1111-1111-1111-111111111112"), true, 0 },
                    { new Guid("195ced8d-cec3-4e6a-a3ef-c805f54d2425"), 1000000m, 0, new Guid("22222222-2222-2222-2222-222222222224"), true, 2 },
                    { new Guid("2aa6e2b7-2d78-4bf5-b4bd-f3e3db9b6267"), 1000000m, 0, new Guid("22222222-2222-2222-2222-222222222223"), true, 1 },
                    { new Guid("3479d48b-d818-4288-ba08-d5b4af220283"), 1500000m, 0, new Guid("22222222-2222-2222-2222-222222222224"), true, 1 },
                    { new Guid("38cc0a7c-f9d4-402a-9ee5-061f5ad84612"), 3000000m, 0, new Guid("33333333-3333-3333-3333-333333333334"), true, 1 },
                    { new Guid("3b26cb3d-3b36-4bae-9f9c-294c0611ea0a"), 1000000m, 1, new Guid("44444444-4444-4444-4444-444444444442"), true, 0 },
                    { new Guid("43388b96-b854-48be-992c-e4496801bd53"), 500000m, 0, new Guid("33333333-3333-3333-3333-333333333331"), true, 0 },
                    { new Guid("59c21b23-3b50-430e-9ac0-935b04e054fd"), 700000m, 1, new Guid("44444444-4444-4444-4444-444444444441"), true, 0 },
                    { new Guid("61d69495-6047-4b9b-959c-1d3f9ed70946"), 700000m, 1, new Guid("33333333-3333-3333-3333-333333333332"), true, 0 },
                    { new Guid("6440613f-f50d-4d01-be92-8f71809113bd"), 2000000m, 0, new Guid("33333333-3333-3333-3333-333333333333"), true, 1 },
                    { new Guid("66e15643-9e64-482d-ab51-fdaa3e66d8ba"), 2000000m, 0, new Guid("33333333-3333-3333-3333-333333333334"), true, 2 },
                    { new Guid("7aa35f0a-aa20-4876-bdfa-38400aac6c1b"), 500000m, 1, new Guid("22222222-2222-2222-2222-222222222222"), true, 0 },
                    { new Guid("7cfdf408-e543-4d5a-b51c-18dab76c6b61"), 4000000m, 0, new Guid("44444444-4444-4444-4444-444444444443"), true, 2 },
                    { new Guid("80fdd570-20ab-41a0-9f5c-bb0a59b88f8f"), 1000000m, 1, new Guid("33333333-3333-3333-3333-333333333334"), true, 0 },
                    { new Guid("837306c2-13f0-4b21-8b91-7b66ffd7d5d1"), 2000000m, 2, new Guid("44444444-4444-4444-4444-444444444443"), true, 0 },
                    { new Guid("931bb9e2-8400-4c82-bfd6-1472c15b2811"), 3000000m, 0, new Guid("33333333-3333-3333-3333-333333333335"), true, 2 },
                    { new Guid("94e9f558-6d4b-4aa5-9ef7-61b00acd670f"), 500000m, 0, new Guid("11111111-1111-1111-1111-111111111113"), true, 1 },
                    { new Guid("98510cfc-d567-42aa-bd4a-4cdfaeb8072b"), 300000m, 1, new Guid("11111111-1111-1111-1111-111111111113"), true, 0 },
                    { new Guid("a12a15d8-63b4-45e5-82eb-312b0b364739"), 500000m, 0, new Guid("33333333-3333-3333-3333-333333333335"), true, 3 },
                    { new Guid("aef2d9f6-424a-427f-b246-01d26e12c3e3"), 300000m, 0, new Guid("22222222-2222-2222-2222-222222222221"), true, 0 },
                    { new Guid("c1f809ed-1f9f-437b-915c-25869c7318d4"), 200000m, 0, new Guid("11111111-1111-1111-1111-111111111111"), true, 0 },
                    { new Guid("ddeb34b5-5d21-452d-8252-11fcc2aaf365"), 4000000m, 0, new Guid("33333333-3333-3333-3333-333333333335"), true, 1 },
                    { new Guid("e0c4f00d-f537-4748-b099-9ebb8fd6d58d"), 3000000m, 0, new Guid("44444444-4444-4444-4444-444444444442"), true, 1 },
                    { new Guid("f3c3d51b-c072-46b2-bdd1-5fc3a0c8ca4b"), 500000m, 1, new Guid("22222222-2222-2222-2222-222222222223"), true, 0 },
                    { new Guid("fecc0f63-8ae1-45b8-a95b-702d3a340478"), 700000m, 1, new Guid("33333333-3333-3333-3333-333333333333"), true, 0 },
                    { new Guid("ff0f6b24-b570-444b-8f8c-3e1b528bb164"), 700000m, 1, new Guid("22222222-2222-2222-2222-222222222224"), true, 0 }
                });
        }
    }
}
