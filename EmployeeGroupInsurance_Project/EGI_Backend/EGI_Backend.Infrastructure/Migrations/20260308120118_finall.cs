using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EGI_Backend.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class finall : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("00778796-9dbb-41b1-89cb-dcda30683f22"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("1a02d236-8615-4cf9-aa10-f01496e8fb8b"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("1cad7e58-1fb4-4fc4-8af6-c8ac7b022a65"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("1f9eebe2-88cb-49d4-83d5-bfd2d250e38b"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("2a3d0f0a-c90c-4e7f-b768-5f0928ce3681"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("31512304-2a75-49d1-baae-9f837a5835f6"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("4e89b12a-bd9a-4248-b354-6055a3f36d20"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("5cd758ae-a760-454e-9056-42bf416c8197"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("787e2b69-d0c5-4d0b-b52b-4840a884feb7"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("887a0c9d-44f6-494d-8bc6-b318a10817e2"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("8a6d204f-a598-4d5c-ba9a-dd4b2e10667f"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("8a8fb0d2-49ee-4953-acba-ed87d72029fb"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("8b654bce-11c2-4c1c-bbe2-3b52cd3845c7"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("95cc149b-4690-45d7-986d-b241d8cf40d9"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("987110b9-6772-44aa-9654-7aa1ad457df6"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("b0250418-4ddf-42f3-a029-0e21f8c0d2cd"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("b0e64624-08a7-47e3-b216-cac24779cc54"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("b176eaf2-b37a-452f-aa26-c81e685c874a"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("bbedd8fc-9abc-4aeb-8181-cdf7ebaac0e2"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("c5c3671a-9b1f-456e-80c4-889e427549cc"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("c68af59e-eb2e-4360-862b-26635118e968"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("cbbd9281-8e88-4739-8fcd-4ab8a5df6323"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("cce5b192-172d-4784-b410-f794fd2e3c14"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("d6318af2-ffb1-4463-855b-6779f7d47c63"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("e5983d4c-32c5-477a-b1fc-a60893ed221a"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("e6a65b32-3dbd-4563-b35c-fe50320bc5a3"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("f7625738-f897-410f-b98c-aea0a92b8520"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("fbeb1868-2845-4e8f-b3f5-4cc8f054ad25"));

            migrationBuilder.AddColumn<int>(
                name: "BusinessCategory",
                table: "PolicyAssignments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 8, 12, 1, 18, 293, DateTimeKind.Utc).AddTicks(9682));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111112"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 8, 12, 1, 18, 293, DateTimeKind.Utc).AddTicks(9700));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111113"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 8, 12, 1, 18, 293, DateTimeKind.Utc).AddTicks(9704));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222221"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 8, 12, 1, 18, 293, DateTimeKind.Utc).AddTicks(9708));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 8, 12, 1, 18, 293, DateTimeKind.Utc).AddTicks(9712));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222223"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 8, 12, 1, 18, 293, DateTimeKind.Utc).AddTicks(9715));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222224"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 8, 12, 1, 18, 293, DateTimeKind.Utc).AddTicks(9719));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333331"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 8, 12, 1, 18, 293, DateTimeKind.Utc).AddTicks(9723));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333332"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 8, 12, 1, 18, 293, DateTimeKind.Utc).AddTicks(9729));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 8, 12, 1, 18, 293, DateTimeKind.Utc).AddTicks(9732));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333334"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 8, 12, 1, 18, 293, DateTimeKind.Utc).AddTicks(9736));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333335"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 8, 12, 1, 18, 293, DateTimeKind.Utc).AddTicks(9739));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444441"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 8, 12, 1, 18, 293, DateTimeKind.Utc).AddTicks(9742));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444442"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 8, 12, 1, 18, 293, DateTimeKind.Utc).AddTicks(9745));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444443"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 8, 12, 1, 18, 293, DateTimeKind.Utc).AddTicks(9748));

            migrationBuilder.InsertData(
                table: "PlanCoverages",
                columns: new[] { "Id", "CoverageAmount", "CoveredGroup", "InsurancePlanId", "IsActive", "Type" },
                values: new object[,]
                {
                    { new Guid("002934e6-1a57-40b4-bcd4-7f8f482105b7"), 500000m, 0, new Guid("33333333-3333-3333-3333-333333333331"), true, 0 },
                    { new Guid("0836ec55-f52c-499c-a4a9-87b4a03e951e"), 300000m, 1, new Guid("11111111-1111-1111-1111-111111111113"), true, 0 },
                    { new Guid("0dca4a45-79c1-4537-9e1a-2089f6474962"), 700000m, 1, new Guid("33333333-3333-3333-3333-333333333333"), true, 0 },
                    { new Guid("139f3115-f99a-4286-9985-bf52f52a331a"), 700000m, 1, new Guid("44444444-4444-4444-4444-444444444441"), true, 0 },
                    { new Guid("20746617-614e-4b0c-8b8d-a3cc4fe33b3f"), 2000000m, 0, new Guid("33333333-3333-3333-3333-333333333333"), true, 1 },
                    { new Guid("2c36ea46-cedf-4e3d-beb9-9615ae85f657"), 4000000m, 0, new Guid("33333333-3333-3333-3333-333333333335"), true, 1 },
                    { new Guid("3b5b337b-3c6e-4851-922b-ac657b3648fe"), 1500000m, 2, new Guid("33333333-3333-3333-3333-333333333335"), true, 0 },
                    { new Guid("4b106613-f822-4af4-8db3-a083a29b7a4e"), 1000000m, 1, new Guid("33333333-3333-3333-3333-333333333334"), true, 0 },
                    { new Guid("51799834-3501-491e-8e7a-cb9ff52493b8"), 3000000m, 0, new Guid("33333333-3333-3333-3333-333333333334"), true, 1 },
                    { new Guid("72c9f89b-4919-4255-84a9-8c3435968181"), 1000000m, 0, new Guid("22222222-2222-2222-2222-222222222224"), true, 2 },
                    { new Guid("75e484b2-0517-4902-858a-924b0dd95d0d"), 3000000m, 0, new Guid("44444444-4444-4444-4444-444444444442"), true, 1 },
                    { new Guid("850fef35-8d83-4ab3-8fbc-4975105ecd57"), 300000m, 1, new Guid("11111111-1111-1111-1111-111111111112"), true, 0 },
                    { new Guid("95f4b03f-0abc-4e5b-b5ba-8bc6d3ade441"), 1000000m, 1, new Guid("44444444-4444-4444-4444-444444444442"), true, 0 },
                    { new Guid("99966160-117e-4567-a987-3de4e9e6e1db"), 5000000m, 0, new Guid("44444444-4444-4444-4444-444444444443"), true, 1 },
                    { new Guid("9a4099da-5ee9-43b8-b779-e8ce4f219f49"), 200000m, 0, new Guid("11111111-1111-1111-1111-111111111111"), true, 0 },
                    { new Guid("9c7e6f7c-d951-4c0c-96d4-0674fb6af627"), 1500000m, 0, new Guid("22222222-2222-2222-2222-222222222224"), true, 1 },
                    { new Guid("9f4cbee1-fb91-4f73-98e0-380efb4121c9"), 500000m, 1, new Guid("22222222-2222-2222-2222-222222222222"), true, 0 },
                    { new Guid("a938e603-e52c-44c4-af08-ae5f88638c14"), 500000m, 0, new Guid("11111111-1111-1111-1111-111111111113"), true, 1 },
                    { new Guid("c2a96788-b74a-4377-86fe-c6fb590c8955"), 4000000m, 0, new Guid("44444444-4444-4444-4444-444444444443"), true, 2 },
                    { new Guid("c3727e98-818c-4072-b2ea-7c6fff99e13d"), 500000m, 0, new Guid("33333333-3333-3333-3333-333333333335"), true, 3 },
                    { new Guid("d0d722ee-d304-49a9-8de5-8f7e9fb15c3b"), 700000m, 1, new Guid("33333333-3333-3333-3333-333333333332"), true, 0 },
                    { new Guid("e0169f00-81cd-4f3f-a9eb-d774b0202ec2"), 500000m, 1, new Guid("22222222-2222-2222-2222-222222222223"), true, 0 },
                    { new Guid("e1e5eddf-cab5-4197-9aed-025e5fb40251"), 2000000m, 2, new Guid("44444444-4444-4444-4444-444444444443"), true, 0 },
                    { new Guid("f06d7e2d-57d7-4801-b23d-da8091fac12f"), 300000m, 0, new Guid("22222222-2222-2222-2222-222222222221"), true, 0 },
                    { new Guid("f85e9705-5435-4076-b426-ba2969e76ffb"), 1000000m, 0, new Guid("22222222-2222-2222-2222-222222222223"), true, 1 },
                    { new Guid("fa197142-6725-4ad3-8cdf-c59c67238dd2"), 3000000m, 0, new Guid("33333333-3333-3333-3333-333333333335"), true, 2 },
                    { new Guid("fa22bcdb-63f1-43aa-a1e0-231361db8bfa"), 700000m, 1, new Guid("22222222-2222-2222-2222-222222222224"), true, 0 },
                    { new Guid("ff5c8a7d-fb01-4621-8c7a-23b673ddc704"), 2000000m, 0, new Guid("33333333-3333-3333-3333-333333333334"), true, 2 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("002934e6-1a57-40b4-bcd4-7f8f482105b7"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("0836ec55-f52c-499c-a4a9-87b4a03e951e"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("0dca4a45-79c1-4537-9e1a-2089f6474962"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("139f3115-f99a-4286-9985-bf52f52a331a"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("20746617-614e-4b0c-8b8d-a3cc4fe33b3f"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("2c36ea46-cedf-4e3d-beb9-9615ae85f657"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("3b5b337b-3c6e-4851-922b-ac657b3648fe"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("4b106613-f822-4af4-8db3-a083a29b7a4e"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("51799834-3501-491e-8e7a-cb9ff52493b8"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("72c9f89b-4919-4255-84a9-8c3435968181"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("75e484b2-0517-4902-858a-924b0dd95d0d"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("850fef35-8d83-4ab3-8fbc-4975105ecd57"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("95f4b03f-0abc-4e5b-b5ba-8bc6d3ade441"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("99966160-117e-4567-a987-3de4e9e6e1db"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("9a4099da-5ee9-43b8-b779-e8ce4f219f49"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("9c7e6f7c-d951-4c0c-96d4-0674fb6af627"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("9f4cbee1-fb91-4f73-98e0-380efb4121c9"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("a938e603-e52c-44c4-af08-ae5f88638c14"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("c2a96788-b74a-4377-86fe-c6fb590c8955"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("c3727e98-818c-4072-b2ea-7c6fff99e13d"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("d0d722ee-d304-49a9-8de5-8f7e9fb15c3b"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("e0169f00-81cd-4f3f-a9eb-d774b0202ec2"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("e1e5eddf-cab5-4197-9aed-025e5fb40251"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("f06d7e2d-57d7-4801-b23d-da8091fac12f"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("f85e9705-5435-4076-b426-ba2969e76ffb"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("fa197142-6725-4ad3-8cdf-c59c67238dd2"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("fa22bcdb-63f1-43aa-a1e0-231361db8bfa"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("ff5c8a7d-fb01-4621-8c7a-23b673ddc704"));

            migrationBuilder.DropColumn(
                name: "BusinessCategory",
                table: "PolicyAssignments");

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 7, 18, 40, 51, 30, DateTimeKind.Utc).AddTicks(7944));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111112"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 7, 18, 40, 51, 30, DateTimeKind.Utc).AddTicks(7980));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111113"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 7, 18, 40, 51, 30, DateTimeKind.Utc).AddTicks(7988));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222221"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 7, 18, 40, 51, 30, DateTimeKind.Utc).AddTicks(7994));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 7, 18, 40, 51, 30, DateTimeKind.Utc).AddTicks(8001));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222223"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 7, 18, 40, 51, 30, DateTimeKind.Utc).AddTicks(8007));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222224"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 7, 18, 40, 51, 30, DateTimeKind.Utc).AddTicks(8013));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333331"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 7, 18, 40, 51, 30, DateTimeKind.Utc).AddTicks(8018));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333332"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 7, 18, 40, 51, 30, DateTimeKind.Utc).AddTicks(8029));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 7, 18, 40, 51, 30, DateTimeKind.Utc).AddTicks(8034));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333334"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 7, 18, 40, 51, 30, DateTimeKind.Utc).AddTicks(8040));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333335"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 7, 18, 40, 51, 30, DateTimeKind.Utc).AddTicks(8046));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444441"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 7, 18, 40, 51, 30, DateTimeKind.Utc).AddTicks(8051));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444442"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 7, 18, 40, 51, 30, DateTimeKind.Utc).AddTicks(8057));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444443"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 7, 18, 40, 51, 30, DateTimeKind.Utc).AddTicks(8062));

            migrationBuilder.InsertData(
                table: "PlanCoverages",
                columns: new[] { "Id", "CoverageAmount", "CoveredGroup", "InsurancePlanId", "IsActive", "Type" },
                values: new object[,]
                {
                    { new Guid("00778796-9dbb-41b1-89cb-dcda30683f22"), 1500000m, 0, new Guid("22222222-2222-2222-2222-222222222224"), true, 1 },
                    { new Guid("1a02d236-8615-4cf9-aa10-f01496e8fb8b"), 1000000m, 0, new Guid("22222222-2222-2222-2222-222222222223"), true, 1 },
                    { new Guid("1cad7e58-1fb4-4fc4-8af6-c8ac7b022a65"), 300000m, 0, new Guid("22222222-2222-2222-2222-222222222221"), true, 0 },
                    { new Guid("1f9eebe2-88cb-49d4-83d5-bfd2d250e38b"), 2000000m, 0, new Guid("33333333-3333-3333-3333-333333333333"), true, 1 },
                    { new Guid("2a3d0f0a-c90c-4e7f-b768-5f0928ce3681"), 1500000m, 2, new Guid("33333333-3333-3333-3333-333333333335"), true, 0 },
                    { new Guid("31512304-2a75-49d1-baae-9f837a5835f6"), 200000m, 0, new Guid("11111111-1111-1111-1111-111111111111"), true, 0 },
                    { new Guid("4e89b12a-bd9a-4248-b354-6055a3f36d20"), 2000000m, 2, new Guid("44444444-4444-4444-4444-444444444443"), true, 0 },
                    { new Guid("5cd758ae-a760-454e-9056-42bf416c8197"), 700000m, 1, new Guid("33333333-3333-3333-3333-333333333332"), true, 0 },
                    { new Guid("787e2b69-d0c5-4d0b-b52b-4840a884feb7"), 300000m, 1, new Guid("11111111-1111-1111-1111-111111111112"), true, 0 },
                    { new Guid("887a0c9d-44f6-494d-8bc6-b318a10817e2"), 3000000m, 0, new Guid("44444444-4444-4444-4444-444444444442"), true, 1 },
                    { new Guid("8a6d204f-a598-4d5c-ba9a-dd4b2e10667f"), 3000000m, 0, new Guid("33333333-3333-3333-3333-333333333334"), true, 1 },
                    { new Guid("8a8fb0d2-49ee-4953-acba-ed87d72029fb"), 500000m, 0, new Guid("33333333-3333-3333-3333-333333333335"), true, 3 },
                    { new Guid("8b654bce-11c2-4c1c-bbe2-3b52cd3845c7"), 700000m, 1, new Guid("44444444-4444-4444-4444-444444444441"), true, 0 },
                    { new Guid("95cc149b-4690-45d7-986d-b241d8cf40d9"), 2000000m, 0, new Guid("33333333-3333-3333-3333-333333333334"), true, 2 },
                    { new Guid("987110b9-6772-44aa-9654-7aa1ad457df6"), 5000000m, 0, new Guid("44444444-4444-4444-4444-444444444443"), true, 1 },
                    { new Guid("b0250418-4ddf-42f3-a029-0e21f8c0d2cd"), 500000m, 0, new Guid("11111111-1111-1111-1111-111111111113"), true, 1 },
                    { new Guid("b0e64624-08a7-47e3-b216-cac24779cc54"), 500000m, 1, new Guid("22222222-2222-2222-2222-222222222222"), true, 0 },
                    { new Guid("b176eaf2-b37a-452f-aa26-c81e685c874a"), 3000000m, 0, new Guid("33333333-3333-3333-3333-333333333335"), true, 2 },
                    { new Guid("bbedd8fc-9abc-4aeb-8181-cdf7ebaac0e2"), 700000m, 1, new Guid("33333333-3333-3333-3333-333333333333"), true, 0 },
                    { new Guid("c5c3671a-9b1f-456e-80c4-889e427549cc"), 4000000m, 0, new Guid("44444444-4444-4444-4444-444444444443"), true, 2 },
                    { new Guid("c68af59e-eb2e-4360-862b-26635118e968"), 1000000m, 0, new Guid("22222222-2222-2222-2222-222222222224"), true, 2 },
                    { new Guid("cbbd9281-8e88-4739-8fcd-4ab8a5df6323"), 1000000m, 1, new Guid("44444444-4444-4444-4444-444444444442"), true, 0 },
                    { new Guid("cce5b192-172d-4784-b410-f794fd2e3c14"), 700000m, 1, new Guid("22222222-2222-2222-2222-222222222224"), true, 0 },
                    { new Guid("d6318af2-ffb1-4463-855b-6779f7d47c63"), 1000000m, 1, new Guid("33333333-3333-3333-3333-333333333334"), true, 0 },
                    { new Guid("e5983d4c-32c5-477a-b1fc-a60893ed221a"), 500000m, 0, new Guid("33333333-3333-3333-3333-333333333331"), true, 0 },
                    { new Guid("e6a65b32-3dbd-4563-b35c-fe50320bc5a3"), 300000m, 1, new Guid("11111111-1111-1111-1111-111111111113"), true, 0 },
                    { new Guid("f7625738-f897-410f-b98c-aea0a92b8520"), 500000m, 1, new Guid("22222222-2222-2222-2222-222222222223"), true, 0 },
                    { new Guid("fbeb1868-2845-4e8f-b3f5-4cc8f054ad25"), 4000000m, 0, new Guid("33333333-3333-3333-3333-333333333335"), true, 1 }
                });
        }
    }
}
