using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EGI_Backend.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class KYBinit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("01f6fc21-e952-4f7d-ac65-d36a81bc5a35"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("15844250-7a50-4af9-a911-0edfa38fce3a"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("26896ab2-5e75-45b5-92e8-0bff25070acf"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("270a0da7-2a09-40d0-a468-2f521bafcec2"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("28e30166-68b2-41a0-8743-e9f921a34db3"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("36467ba5-6932-427c-b200-6014873431ca"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("3c6c3f8e-0e1a-4b42-adc4-3b60fedba346"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("3d6d46e9-1a4d-4c38-8a89-3bdc0d279bfc"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("3edda525-5221-4349-8de7-a5c45c18bda9"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("4cc8b2c0-a727-46ea-8ff7-4d5240157c2c"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("730f109d-0378-433d-86f8-71849c359733"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("782d7a4b-df65-45c4-8581-1153dd7981c8"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("7d20f588-856c-4b78-8e76-a3ca8c5527e9"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("85175df3-7658-4dec-9245-e690a4bcac87"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("9c3421b6-f40d-44f5-9588-ccab03be5dc8"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("9d83eab7-a1ad-41b1-8e33-5b8a4f5aa9f7"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("a412f143-7eb2-4120-b4e0-4d57b9e8d605"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("a6b07bcc-cd68-4dac-a13a-93b08901a565"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("aad900d6-3118-4c3b-9f2a-d20555084eb0"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("b1971577-f846-4905-a5b3-9630d2160c8a"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("bf96db89-ac3a-4827-937e-9c01fd44fc42"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("c5c5e43b-8f10-4731-9706-a12077a637d8"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("c632b8a7-d60f-4819-bd2a-52fa6e99a3d8"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("c82cafbb-cd5e-4494-a884-8e42017a1339"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("ccb74800-ecd8-4be4-bc64-6b4d88c2a684"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("ed2cc419-ba03-4527-bedb-8eb5bf722118"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("ee959391-0bf2-4090-97c1-9ff6409555fd"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("f1685a57-8678-4b4b-8e50-50a66fa923fa"));

            migrationBuilder.AddColumn<string>(
                name: "KybAiAnalysis",
                table: "CorporateClients",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "KybAiConfidenceScore",
                table: "CorporateClients",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 18, 6, 44, 21, 145, DateTimeKind.Utc).AddTicks(1377));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111112"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 18, 6, 44, 21, 145, DateTimeKind.Utc).AddTicks(1400));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111113"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 18, 6, 44, 21, 145, DateTimeKind.Utc).AddTicks(1405));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222221"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 18, 6, 44, 21, 145, DateTimeKind.Utc).AddTicks(1409));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 18, 6, 44, 21, 145, DateTimeKind.Utc).AddTicks(1413));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222223"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 18, 6, 44, 21, 145, DateTimeKind.Utc).AddTicks(1431));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222224"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 18, 6, 44, 21, 145, DateTimeKind.Utc).AddTicks(1435));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333331"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 18, 6, 44, 21, 145, DateTimeKind.Utc).AddTicks(1439));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333332"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 18, 6, 44, 21, 145, DateTimeKind.Utc).AddTicks(1443));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 18, 6, 44, 21, 145, DateTimeKind.Utc).AddTicks(1446));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333334"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 18, 6, 44, 21, 145, DateTimeKind.Utc).AddTicks(1450));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333335"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 18, 6, 44, 21, 145, DateTimeKind.Utc).AddTicks(1453));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444441"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 18, 6, 44, 21, 145, DateTimeKind.Utc).AddTicks(1456));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444442"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 18, 6, 44, 21, 145, DateTimeKind.Utc).AddTicks(1463));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444443"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 18, 6, 44, 21, 145, DateTimeKind.Utc).AddTicks(1466));

            migrationBuilder.InsertData(
                table: "PlanCoverages",
                columns: new[] { "Id", "CoverageAmount", "CoveredGroup", "InsurancePlanId", "IsActive", "Type" },
                values: new object[,]
                {
                    { new Guid("14a9d6c0-2ed2-4dff-ae73-c922fdc7b21c"), 1500000m, 2, new Guid("33333333-3333-3333-3333-333333333335"), true, 0 },
                    { new Guid("1734cf91-15a9-45bd-8c8c-53fe578dfc28"), 4000000m, 0, new Guid("44444444-4444-4444-4444-444444444443"), true, 2 },
                    { new Guid("217252a7-75e2-41de-b647-3f05704345b4"), 500000m, 0, new Guid("33333333-3333-3333-3333-333333333335"), true, 3 },
                    { new Guid("26ddb03c-6129-483b-9deb-0a84559e12fe"), 1500000m, 0, new Guid("22222222-2222-2222-2222-222222222224"), true, 1 },
                    { new Guid("2c33ba88-2919-4106-a366-f488ad7dfc75"), 300000m, 1, new Guid("11111111-1111-1111-1111-111111111113"), true, 0 },
                    { new Guid("3acefdcb-f6a4-4a14-9f1c-4c0fcff50632"), 700000m, 1, new Guid("33333333-3333-3333-3333-333333333332"), true, 0 },
                    { new Guid("3e9acfd1-6dc8-40d3-b079-0ef1d8ba9afd"), 3000000m, 0, new Guid("33333333-3333-3333-3333-333333333334"), true, 1 },
                    { new Guid("49a285f0-ab4b-4087-94ef-96e32573a2d4"), 700000m, 1, new Guid("33333333-3333-3333-3333-333333333333"), true, 0 },
                    { new Guid("4d2e5e26-5155-4524-8202-174d0717b7ad"), 3000000m, 0, new Guid("44444444-4444-4444-4444-444444444442"), true, 1 },
                    { new Guid("4d2ec7ee-4ce5-4dde-995d-6d68405c1914"), 1000000m, 0, new Guid("22222222-2222-2222-2222-222222222224"), true, 2 },
                    { new Guid("51ba42b3-a31e-44b6-967c-076a690169a4"), 500000m, 1, new Guid("22222222-2222-2222-2222-222222222223"), true, 0 },
                    { new Guid("70d65b2b-b333-49f6-a11c-596fb68730d7"), 500000m, 0, new Guid("11111111-1111-1111-1111-111111111113"), true, 1 },
                    { new Guid("83cf7484-f048-4145-876b-1e72670099d2"), 500000m, 1, new Guid("22222222-2222-2222-2222-222222222222"), true, 0 },
                    { new Guid("86aabe01-37f8-40b2-8fc0-9c3ce7fc6b36"), 4000000m, 0, new Guid("33333333-3333-3333-3333-333333333335"), true, 1 },
                    { new Guid("91af5ecc-b075-470f-8b9f-8822622cbc97"), 2000000m, 2, new Guid("44444444-4444-4444-4444-444444444443"), true, 0 },
                    { new Guid("996e22b5-0469-4dcb-b81c-8f0888f0805c"), 1000000m, 0, new Guid("22222222-2222-2222-2222-222222222223"), true, 1 },
                    { new Guid("9c08cc83-8c6a-4131-8017-ac51c7a94a1a"), 1000000m, 1, new Guid("44444444-4444-4444-4444-444444444442"), true, 0 },
                    { new Guid("a40d7535-672a-4393-8dff-42e24a837337"), 2000000m, 0, new Guid("33333333-3333-3333-3333-333333333334"), true, 2 },
                    { new Guid("a438a33b-101c-47fb-9087-b1656987e459"), 700000m, 1, new Guid("22222222-2222-2222-2222-222222222224"), true, 0 },
                    { new Guid("af51e514-77ad-43af-8bb6-f7c7c9a65659"), 200000m, 0, new Guid("11111111-1111-1111-1111-111111111111"), true, 0 },
                    { new Guid("bd8d4e20-c3e0-499a-9d9b-c2e11f81bb44"), 700000m, 1, new Guid("44444444-4444-4444-4444-444444444441"), true, 0 },
                    { new Guid("c8581b51-c764-49d6-ba05-5259603e33d5"), 300000m, 1, new Guid("11111111-1111-1111-1111-111111111112"), true, 0 },
                    { new Guid("cc72788f-2919-4044-800b-9e5687d08f21"), 3000000m, 0, new Guid("33333333-3333-3333-3333-333333333335"), true, 2 },
                    { new Guid("d03fe70d-b60c-4145-b0e4-f6bb67f51bf7"), 2000000m, 0, new Guid("33333333-3333-3333-3333-333333333333"), true, 1 },
                    { new Guid("d827b4a9-bfb1-4952-bb5d-b003406f046e"), 5000000m, 0, new Guid("44444444-4444-4444-4444-444444444443"), true, 1 },
                    { new Guid("d9a5b914-1118-4d22-ae3b-3bb2d11fa462"), 500000m, 0, new Guid("33333333-3333-3333-3333-333333333331"), true, 0 },
                    { new Guid("de01cd59-afe4-4bf6-9d91-82e3025ba6f2"), 300000m, 0, new Guid("22222222-2222-2222-2222-222222222221"), true, 0 },
                    { new Guid("e294db7b-7fc1-4dad-8738-abe8770deb1c"), 1000000m, 1, new Guid("33333333-3333-3333-3333-333333333334"), true, 0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("14a9d6c0-2ed2-4dff-ae73-c922fdc7b21c"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("1734cf91-15a9-45bd-8c8c-53fe578dfc28"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("217252a7-75e2-41de-b647-3f05704345b4"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("26ddb03c-6129-483b-9deb-0a84559e12fe"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("2c33ba88-2919-4106-a366-f488ad7dfc75"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("3acefdcb-f6a4-4a14-9f1c-4c0fcff50632"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("3e9acfd1-6dc8-40d3-b079-0ef1d8ba9afd"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("49a285f0-ab4b-4087-94ef-96e32573a2d4"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("4d2e5e26-5155-4524-8202-174d0717b7ad"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("4d2ec7ee-4ce5-4dde-995d-6d68405c1914"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("51ba42b3-a31e-44b6-967c-076a690169a4"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("70d65b2b-b333-49f6-a11c-596fb68730d7"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("83cf7484-f048-4145-876b-1e72670099d2"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("86aabe01-37f8-40b2-8fc0-9c3ce7fc6b36"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("91af5ecc-b075-470f-8b9f-8822622cbc97"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("996e22b5-0469-4dcb-b81c-8f0888f0805c"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("9c08cc83-8c6a-4131-8017-ac51c7a94a1a"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("a40d7535-672a-4393-8dff-42e24a837337"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("a438a33b-101c-47fb-9087-b1656987e459"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("af51e514-77ad-43af-8bb6-f7c7c9a65659"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("bd8d4e20-c3e0-499a-9d9b-c2e11f81bb44"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("c8581b51-c764-49d6-ba05-5259603e33d5"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("cc72788f-2919-4044-800b-9e5687d08f21"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("d03fe70d-b60c-4145-b0e4-f6bb67f51bf7"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("d827b4a9-bfb1-4952-bb5d-b003406f046e"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("d9a5b914-1118-4d22-ae3b-3bb2d11fa462"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("de01cd59-afe4-4bf6-9d91-82e3025ba6f2"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("e294db7b-7fc1-4dad-8738-abe8770deb1c"));

            migrationBuilder.DropColumn(
                name: "KybAiAnalysis",
                table: "CorporateClients");

            migrationBuilder.DropColumn(
                name: "KybAiConfidenceScore",
                table: "CorporateClients");

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 17, 16, 20, 32, 810, DateTimeKind.Utc).AddTicks(1964));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111112"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 17, 16, 20, 32, 810, DateTimeKind.Utc).AddTicks(1980));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111113"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 17, 16, 20, 32, 810, DateTimeKind.Utc).AddTicks(1984));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222221"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 17, 16, 20, 32, 810, DateTimeKind.Utc).AddTicks(1988));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 17, 16, 20, 32, 810, DateTimeKind.Utc).AddTicks(1991));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222223"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 17, 16, 20, 32, 810, DateTimeKind.Utc).AddTicks(1994));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222224"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 17, 16, 20, 32, 810, DateTimeKind.Utc).AddTicks(1997));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333331"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 17, 16, 20, 32, 810, DateTimeKind.Utc).AddTicks(2000));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333332"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 17, 16, 20, 32, 810, DateTimeKind.Utc).AddTicks(2005));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 17, 16, 20, 32, 810, DateTimeKind.Utc).AddTicks(2008));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333334"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 17, 16, 20, 32, 810, DateTimeKind.Utc).AddTicks(2012));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333335"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 17, 16, 20, 32, 810, DateTimeKind.Utc).AddTicks(2014));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444441"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 17, 16, 20, 32, 810, DateTimeKind.Utc).AddTicks(2018));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444442"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 17, 16, 20, 32, 810, DateTimeKind.Utc).AddTicks(2062));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444443"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 17, 16, 20, 32, 810, DateTimeKind.Utc).AddTicks(2065));

            migrationBuilder.InsertData(
                table: "PlanCoverages",
                columns: new[] { "Id", "CoverageAmount", "CoveredGroup", "InsurancePlanId", "IsActive", "Type" },
                values: new object[,]
                {
                    { new Guid("01f6fc21-e952-4f7d-ac65-d36a81bc5a35"), 2000000m, 2, new Guid("44444444-4444-4444-4444-444444444443"), true, 0 },
                    { new Guid("15844250-7a50-4af9-a911-0edfa38fce3a"), 4000000m, 0, new Guid("33333333-3333-3333-3333-333333333335"), true, 1 },
                    { new Guid("26896ab2-5e75-45b5-92e8-0bff25070acf"), 700000m, 1, new Guid("22222222-2222-2222-2222-222222222224"), true, 0 },
                    { new Guid("270a0da7-2a09-40d0-a468-2f521bafcec2"), 300000m, 1, new Guid("11111111-1111-1111-1111-111111111113"), true, 0 },
                    { new Guid("28e30166-68b2-41a0-8743-e9f921a34db3"), 3000000m, 0, new Guid("33333333-3333-3333-3333-333333333334"), true, 1 },
                    { new Guid("36467ba5-6932-427c-b200-6014873431ca"), 500000m, 0, new Guid("11111111-1111-1111-1111-111111111113"), true, 1 },
                    { new Guid("3c6c3f8e-0e1a-4b42-adc4-3b60fedba346"), 3000000m, 0, new Guid("33333333-3333-3333-3333-333333333335"), true, 2 },
                    { new Guid("3d6d46e9-1a4d-4c38-8a89-3bdc0d279bfc"), 3000000m, 0, new Guid("44444444-4444-4444-4444-444444444442"), true, 1 },
                    { new Guid("3edda525-5221-4349-8de7-a5c45c18bda9"), 1000000m, 1, new Guid("44444444-4444-4444-4444-444444444442"), true, 0 },
                    { new Guid("4cc8b2c0-a727-46ea-8ff7-4d5240157c2c"), 700000m, 1, new Guid("33333333-3333-3333-3333-333333333333"), true, 0 },
                    { new Guid("730f109d-0378-433d-86f8-71849c359733"), 500000m, 1, new Guid("22222222-2222-2222-2222-222222222222"), true, 0 },
                    { new Guid("782d7a4b-df65-45c4-8581-1153dd7981c8"), 2000000m, 0, new Guid("33333333-3333-3333-3333-333333333333"), true, 1 },
                    { new Guid("7d20f588-856c-4b78-8e76-a3ca8c5527e9"), 200000m, 0, new Guid("11111111-1111-1111-1111-111111111111"), true, 0 },
                    { new Guid("85175df3-7658-4dec-9245-e690a4bcac87"), 5000000m, 0, new Guid("44444444-4444-4444-4444-444444444443"), true, 1 },
                    { new Guid("9c3421b6-f40d-44f5-9588-ccab03be5dc8"), 500000m, 0, new Guid("33333333-3333-3333-3333-333333333331"), true, 0 },
                    { new Guid("9d83eab7-a1ad-41b1-8e33-5b8a4f5aa9f7"), 500000m, 1, new Guid("22222222-2222-2222-2222-222222222223"), true, 0 },
                    { new Guid("a412f143-7eb2-4120-b4e0-4d57b9e8d605"), 1000000m, 1, new Guid("33333333-3333-3333-3333-333333333334"), true, 0 },
                    { new Guid("a6b07bcc-cd68-4dac-a13a-93b08901a565"), 1500000m, 2, new Guid("33333333-3333-3333-3333-333333333335"), true, 0 },
                    { new Guid("aad900d6-3118-4c3b-9f2a-d20555084eb0"), 4000000m, 0, new Guid("44444444-4444-4444-4444-444444444443"), true, 2 },
                    { new Guid("b1971577-f846-4905-a5b3-9630d2160c8a"), 300000m, 1, new Guid("11111111-1111-1111-1111-111111111112"), true, 0 },
                    { new Guid("bf96db89-ac3a-4827-937e-9c01fd44fc42"), 700000m, 1, new Guid("44444444-4444-4444-4444-444444444441"), true, 0 },
                    { new Guid("c5c5e43b-8f10-4731-9706-a12077a637d8"), 2000000m, 0, new Guid("33333333-3333-3333-3333-333333333334"), true, 2 },
                    { new Guid("c632b8a7-d60f-4819-bd2a-52fa6e99a3d8"), 1500000m, 0, new Guid("22222222-2222-2222-2222-222222222224"), true, 1 },
                    { new Guid("c82cafbb-cd5e-4494-a884-8e42017a1339"), 1000000m, 0, new Guid("22222222-2222-2222-2222-222222222224"), true, 2 },
                    { new Guid("ccb74800-ecd8-4be4-bc64-6b4d88c2a684"), 500000m, 0, new Guid("33333333-3333-3333-3333-333333333335"), true, 3 },
                    { new Guid("ed2cc419-ba03-4527-bedb-8eb5bf722118"), 300000m, 0, new Guid("22222222-2222-2222-2222-222222222221"), true, 0 },
                    { new Guid("ee959391-0bf2-4090-97c1-9ff6409555fd"), 700000m, 1, new Guid("33333333-3333-3333-3333-333333333332"), true, 0 },
                    { new Guid("f1685a57-8678-4b4b-8e50-50a66fa923fa"), 1000000m, 0, new Guid("22222222-2222-2222-2222-222222222223"), true, 1 }
                });
        }
    }
}
