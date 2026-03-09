using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EGI_Backend.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddUserLastLogin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("024e9254-1d89-4338-81f4-dce998fc70b9"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("032c3454-1579-4244-b1f8-8c74d4e74a11"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("058edeac-dc78-467b-b8ac-a407b2fc94f8"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("22d35e77-aa60-46af-8e3b-1cac4df9d61d"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("249c5781-c4f9-43ee-86c2-1adfe056eaac"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("260e48ae-98b3-4180-adcf-d784adb82dd3"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("283e6cac-baef-46cc-ab0c-08e457b41ae5"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("2dfc2a64-4028-4a85-878a-fc4dc195a146"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("30bb04e9-f7e3-4033-a6a6-f21c92c857d1"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("334b3174-b5ea-4ac9-bf07-fb10653181e1"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("38e25c56-ce68-40aa-be60-511adc95f3ff"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("4e8b8bb4-bd1e-416a-9bf4-e1cf982bc7e1"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("4ef5b68d-bc5f-4075-bb3e-d40e39edeaa4"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("5a800a01-17ba-40b1-8511-e9494c73b17c"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("72b0c415-9a77-473e-9245-40b417feb8a6"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("7eb88fae-1c65-4c0c-a9c2-38d0d4ae9abc"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("8b08b5c5-ed16-4244-960d-687031376093"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("a2ce23d2-48f0-435f-9346-b79c4e23d6d3"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("a8ea4547-c0f9-4547-84ef-c4f13ef92c65"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("ae684ba1-448f-4008-acb1-de576a568df0"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("b79e2417-fbdb-4066-9e8e-87af46a9f34b"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("d2662cc9-e908-41a3-946d-dae7711c24bb"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("daa1608c-e2f8-47c9-b6c5-03f308f65b1c"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("e2b68a9f-092c-4466-bb4c-d1710151e9c8"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("e5ae509a-f4ad-48b7-bae6-74cabee9c908"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("f030967e-b1a4-4739-9454-2d53979a4f12"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("f66e6fa3-cc05-4748-bc4f-f2d481539772"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("ff6e58f9-3393-46b3-8c6a-96c0dbbabe76"));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastLogin",
                table: "Users",
                type: "datetime2",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 28, 12, 58, 2, 873, DateTimeKind.Utc).AddTicks(4904));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111112"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 28, 12, 58, 2, 873, DateTimeKind.Utc).AddTicks(4924));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111113"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 28, 12, 58, 2, 873, DateTimeKind.Utc).AddTicks(4929));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222221"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 28, 12, 58, 2, 873, DateTimeKind.Utc).AddTicks(4934));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 28, 12, 58, 2, 873, DateTimeKind.Utc).AddTicks(4937));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222223"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 28, 12, 58, 2, 873, DateTimeKind.Utc).AddTicks(4940));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222224"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 28, 12, 58, 2, 873, DateTimeKind.Utc).AddTicks(4943));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333331"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 28, 12, 58, 2, 873, DateTimeKind.Utc).AddTicks(4947));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333332"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 28, 12, 58, 2, 873, DateTimeKind.Utc).AddTicks(4955));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 28, 12, 58, 2, 873, DateTimeKind.Utc).AddTicks(4999));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333334"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 28, 12, 58, 2, 873, DateTimeKind.Utc).AddTicks(5003));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333335"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 28, 12, 58, 2, 873, DateTimeKind.Utc).AddTicks(5006));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444441"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 28, 12, 58, 2, 873, DateTimeKind.Utc).AddTicks(5009));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444442"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 28, 12, 58, 2, 873, DateTimeKind.Utc).AddTicks(5013));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444443"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 28, 12, 58, 2, 873, DateTimeKind.Utc).AddTicks(5016));

            migrationBuilder.InsertData(
                table: "PlanCoverages",
                columns: new[] { "Id", "CoverageAmount", "CoveredGroup", "InsurancePlanId", "IsActive", "Type" },
                values: new object[,]
                {
                    { new Guid("0c571f62-c026-4352-b68c-dd811efd6956"), 300000m, 0, new Guid("22222222-2222-2222-2222-222222222221"), true, 0 },
                    { new Guid("2e2a9b2d-9cfa-4f6b-86c5-0be31cb1444d"), 1000000m, 0, new Guid("22222222-2222-2222-2222-222222222223"), true, 1 },
                    { new Guid("3d707bfd-719b-40bc-83eb-37f0a89e689a"), 2000000m, 2, new Guid("44444444-4444-4444-4444-444444444443"), true, 0 },
                    { new Guid("3ee8de3b-b633-40c1-9dc4-e83ce8d2e86b"), 500000m, 0, new Guid("11111111-1111-1111-1111-111111111113"), true, 1 },
                    { new Guid("4788d41d-e6ce-45ce-994b-8336f6dab98f"), 1000000m, 0, new Guid("22222222-2222-2222-2222-222222222224"), true, 2 },
                    { new Guid("53a2f91e-c091-4107-8539-cc4ecd952f41"), 500000m, 1, new Guid("22222222-2222-2222-2222-222222222222"), true, 0 },
                    { new Guid("5b722ad8-b674-4d8a-bb72-280c84935c52"), 700000m, 1, new Guid("44444444-4444-4444-4444-444444444441"), true, 0 },
                    { new Guid("5e92d46a-28b3-4d52-813e-24fbca61ab8b"), 1500000m, 2, new Guid("33333333-3333-3333-3333-333333333335"), true, 0 },
                    { new Guid("63659541-5228-45ca-b256-23bf5e58300b"), 500000m, 1, new Guid("22222222-2222-2222-2222-222222222223"), true, 0 },
                    { new Guid("71a98682-7ceb-46ea-99c4-ecc252a04a66"), 500000m, 0, new Guid("33333333-3333-3333-3333-333333333331"), true, 0 },
                    { new Guid("72930849-de3b-48b3-82f0-85ab49caa524"), 5000000m, 0, new Guid("44444444-4444-4444-4444-444444444443"), true, 1 },
                    { new Guid("7cf06f7c-8c4a-4f5f-bfa3-08002a114187"), 700000m, 1, new Guid("33333333-3333-3333-3333-333333333332"), true, 0 },
                    { new Guid("7e2ed4d3-49cb-47c4-a278-23a324499992"), 700000m, 1, new Guid("22222222-2222-2222-2222-222222222224"), true, 0 },
                    { new Guid("842a63a1-7183-48ef-93dd-2e86bdd7c079"), 1500000m, 0, new Guid("22222222-2222-2222-2222-222222222224"), true, 1 },
                    { new Guid("8ab28186-6b67-43e6-a0ac-aad5f4920ea2"), 700000m, 1, new Guid("33333333-3333-3333-3333-333333333333"), true, 0 },
                    { new Guid("8d4a0817-60e0-4b8a-afaf-3703f3516cc4"), 1000000m, 1, new Guid("33333333-3333-3333-3333-333333333334"), true, 0 },
                    { new Guid("989ac7df-2f91-4870-a950-2cdd3894b905"), 4000000m, 0, new Guid("33333333-3333-3333-3333-333333333335"), true, 1 },
                    { new Guid("a286c0a4-f582-47ce-a629-0694d7270daa"), 300000m, 1, new Guid("11111111-1111-1111-1111-111111111112"), true, 0 },
                    { new Guid("b79f6443-47ac-4f23-bff5-6e5b507c1f5f"), 500000m, 0, new Guid("33333333-3333-3333-3333-333333333335"), true, 3 },
                    { new Guid("c385a716-3bd8-4eb0-87a5-7f6446075956"), 3000000m, 0, new Guid("33333333-3333-3333-3333-333333333335"), true, 2 },
                    { new Guid("c4bc94a4-529a-4baf-9dd2-2c178e4bdbfc"), 200000m, 0, new Guid("11111111-1111-1111-1111-111111111111"), true, 0 },
                    { new Guid("cb3c5f22-7945-4ed6-9f38-715f051d4fb2"), 300000m, 1, new Guid("11111111-1111-1111-1111-111111111113"), true, 0 },
                    { new Guid("cc1d7ad2-543e-4933-b046-23b392f10a82"), 1000000m, 1, new Guid("44444444-4444-4444-4444-444444444442"), true, 0 },
                    { new Guid("d12e271b-2499-43ca-b3cf-84db2a1b5804"), 3000000m, 0, new Guid("33333333-3333-3333-3333-333333333334"), true, 1 },
                    { new Guid("d7d89bbc-d360-4d9c-ada3-d840517284e0"), 3000000m, 0, new Guid("44444444-4444-4444-4444-444444444442"), true, 1 },
                    { new Guid("de0175b3-64f7-49bf-99fb-870a612cc2cd"), 2000000m, 0, new Guid("33333333-3333-3333-3333-333333333334"), true, 2 },
                    { new Guid("eae5970d-7177-4ff5-b109-0c305a1dd738"), 4000000m, 0, new Guid("44444444-4444-4444-4444-444444444443"), true, 2 },
                    { new Guid("f58951b1-175d-4d49-ae7a-b249ebbab949"), 2000000m, 0, new Guid("33333333-3333-3333-3333-333333333333"), true, 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("0c571f62-c026-4352-b68c-dd811efd6956"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("2e2a9b2d-9cfa-4f6b-86c5-0be31cb1444d"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("3d707bfd-719b-40bc-83eb-37f0a89e689a"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("3ee8de3b-b633-40c1-9dc4-e83ce8d2e86b"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("4788d41d-e6ce-45ce-994b-8336f6dab98f"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("53a2f91e-c091-4107-8539-cc4ecd952f41"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("5b722ad8-b674-4d8a-bb72-280c84935c52"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("5e92d46a-28b3-4d52-813e-24fbca61ab8b"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("63659541-5228-45ca-b256-23bf5e58300b"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("71a98682-7ceb-46ea-99c4-ecc252a04a66"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("72930849-de3b-48b3-82f0-85ab49caa524"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("7cf06f7c-8c4a-4f5f-bfa3-08002a114187"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("7e2ed4d3-49cb-47c4-a278-23a324499992"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("842a63a1-7183-48ef-93dd-2e86bdd7c079"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("8ab28186-6b67-43e6-a0ac-aad5f4920ea2"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("8d4a0817-60e0-4b8a-afaf-3703f3516cc4"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("989ac7df-2f91-4870-a950-2cdd3894b905"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("a286c0a4-f582-47ce-a629-0694d7270daa"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("b79f6443-47ac-4f23-bff5-6e5b507c1f5f"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("c385a716-3bd8-4eb0-87a5-7f6446075956"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("c4bc94a4-529a-4baf-9dd2-2c178e4bdbfc"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("cb3c5f22-7945-4ed6-9f38-715f051d4fb2"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("cc1d7ad2-543e-4933-b046-23b392f10a82"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("d12e271b-2499-43ca-b3cf-84db2a1b5804"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("d7d89bbc-d360-4d9c-ada3-d840517284e0"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("de0175b3-64f7-49bf-99fb-870a612cc2cd"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("eae5970d-7177-4ff5-b109-0c305a1dd738"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("f58951b1-175d-4d49-ae7a-b249ebbab949"));

            migrationBuilder.DropColumn(
                name: "LastLogin",
                table: "Users");

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 28, 12, 53, 36, 879, DateTimeKind.Utc).AddTicks(4425));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111112"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 28, 12, 53, 36, 879, DateTimeKind.Utc).AddTicks(4445));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111113"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 28, 12, 53, 36, 879, DateTimeKind.Utc).AddTicks(4451));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222221"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 28, 12, 53, 36, 879, DateTimeKind.Utc).AddTicks(4455));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 28, 12, 53, 36, 879, DateTimeKind.Utc).AddTicks(4460));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222223"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 28, 12, 53, 36, 879, DateTimeKind.Utc).AddTicks(4463));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222224"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 28, 12, 53, 36, 879, DateTimeKind.Utc).AddTicks(4467));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333331"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 28, 12, 53, 36, 879, DateTimeKind.Utc).AddTicks(4471));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333332"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 28, 12, 53, 36, 879, DateTimeKind.Utc).AddTicks(4477));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 28, 12, 53, 36, 879, DateTimeKind.Utc).AddTicks(4480));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333334"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 28, 12, 53, 36, 879, DateTimeKind.Utc).AddTicks(4484));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333335"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 28, 12, 53, 36, 879, DateTimeKind.Utc).AddTicks(4487));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444441"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 28, 12, 53, 36, 879, DateTimeKind.Utc).AddTicks(4490));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444442"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 28, 12, 53, 36, 879, DateTimeKind.Utc).AddTicks(4493));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444443"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 28, 12, 53, 36, 879, DateTimeKind.Utc).AddTicks(4496));

            migrationBuilder.InsertData(
                table: "PlanCoverages",
                columns: new[] { "Id", "CoverageAmount", "CoveredGroup", "InsurancePlanId", "IsActive", "Type" },
                values: new object[,]
                {
                    { new Guid("024e9254-1d89-4338-81f4-dce998fc70b9"), 300000m, 1, new Guid("11111111-1111-1111-1111-111111111112"), true, 0 },
                    { new Guid("032c3454-1579-4244-b1f8-8c74d4e74a11"), 2000000m, 2, new Guid("44444444-4444-4444-4444-444444444443"), true, 0 },
                    { new Guid("058edeac-dc78-467b-b8ac-a407b2fc94f8"), 1000000m, 1, new Guid("44444444-4444-4444-4444-444444444442"), true, 0 },
                    { new Guid("22d35e77-aa60-46af-8e3b-1cac4df9d61d"), 700000m, 1, new Guid("33333333-3333-3333-3333-333333333332"), true, 0 },
                    { new Guid("249c5781-c4f9-43ee-86c2-1adfe056eaac"), 700000m, 1, new Guid("44444444-4444-4444-4444-444444444441"), true, 0 },
                    { new Guid("260e48ae-98b3-4180-adcf-d784adb82dd3"), 700000m, 1, new Guid("33333333-3333-3333-3333-333333333333"), true, 0 },
                    { new Guid("283e6cac-baef-46cc-ab0c-08e457b41ae5"), 500000m, 1, new Guid("22222222-2222-2222-2222-222222222222"), true, 0 },
                    { new Guid("2dfc2a64-4028-4a85-878a-fc4dc195a146"), 1000000m, 0, new Guid("22222222-2222-2222-2222-222222222224"), true, 2 },
                    { new Guid("30bb04e9-f7e3-4033-a6a6-f21c92c857d1"), 700000m, 1, new Guid("22222222-2222-2222-2222-222222222224"), true, 0 },
                    { new Guid("334b3174-b5ea-4ac9-bf07-fb10653181e1"), 2000000m, 0, new Guid("33333333-3333-3333-3333-333333333333"), true, 1 },
                    { new Guid("38e25c56-ce68-40aa-be60-511adc95f3ff"), 4000000m, 0, new Guid("44444444-4444-4444-4444-444444444443"), true, 2 },
                    { new Guid("4e8b8bb4-bd1e-416a-9bf4-e1cf982bc7e1"), 2000000m, 0, new Guid("33333333-3333-3333-3333-333333333334"), true, 2 },
                    { new Guid("4ef5b68d-bc5f-4075-bb3e-d40e39edeaa4"), 1000000m, 0, new Guid("22222222-2222-2222-2222-222222222223"), true, 1 },
                    { new Guid("5a800a01-17ba-40b1-8511-e9494c73b17c"), 500000m, 0, new Guid("11111111-1111-1111-1111-111111111113"), true, 1 },
                    { new Guid("72b0c415-9a77-473e-9245-40b417feb8a6"), 300000m, 1, new Guid("11111111-1111-1111-1111-111111111113"), true, 0 },
                    { new Guid("7eb88fae-1c65-4c0c-a9c2-38d0d4ae9abc"), 1500000m, 0, new Guid("22222222-2222-2222-2222-222222222224"), true, 1 },
                    { new Guid("8b08b5c5-ed16-4244-960d-687031376093"), 1000000m, 1, new Guid("33333333-3333-3333-3333-333333333334"), true, 0 },
                    { new Guid("a2ce23d2-48f0-435f-9346-b79c4e23d6d3"), 1500000m, 2, new Guid("33333333-3333-3333-3333-333333333335"), true, 0 },
                    { new Guid("a8ea4547-c0f9-4547-84ef-c4f13ef92c65"), 500000m, 0, new Guid("33333333-3333-3333-3333-333333333331"), true, 0 },
                    { new Guid("ae684ba1-448f-4008-acb1-de576a568df0"), 3000000m, 0, new Guid("33333333-3333-3333-3333-333333333334"), true, 1 },
                    { new Guid("b79e2417-fbdb-4066-9e8e-87af46a9f34b"), 200000m, 0, new Guid("11111111-1111-1111-1111-111111111111"), true, 0 },
                    { new Guid("d2662cc9-e908-41a3-946d-dae7711c24bb"), 4000000m, 0, new Guid("33333333-3333-3333-3333-333333333335"), true, 1 },
                    { new Guid("daa1608c-e2f8-47c9-b6c5-03f308f65b1c"), 500000m, 1, new Guid("22222222-2222-2222-2222-222222222223"), true, 0 },
                    { new Guid("e2b68a9f-092c-4466-bb4c-d1710151e9c8"), 5000000m, 0, new Guid("44444444-4444-4444-4444-444444444443"), true, 1 },
                    { new Guid("e5ae509a-f4ad-48b7-bae6-74cabee9c908"), 500000m, 0, new Guid("33333333-3333-3333-3333-333333333335"), true, 3 },
                    { new Guid("f030967e-b1a4-4739-9454-2d53979a4f12"), 3000000m, 0, new Guid("33333333-3333-3333-3333-333333333335"), true, 2 },
                    { new Guid("f66e6fa3-cc05-4748-bc4f-f2d481539772"), 300000m, 0, new Guid("22222222-2222-2222-2222-222222222221"), true, 0 },
                    { new Guid("ff6e58f9-3393-46b3-8c6a-96c0dbbabe76"), 3000000m, 0, new Guid("44444444-4444-4444-4444-444444444442"), true, 1 }
                });
        }
    }
}
