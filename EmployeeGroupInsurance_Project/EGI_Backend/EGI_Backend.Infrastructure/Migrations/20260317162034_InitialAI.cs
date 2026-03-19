using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EGI_Backend.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialAI : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("0e6c15a9-25dd-435f-8ee3-edfe99ab3106"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("1f7626c2-b1f9-42ed-b89f-df95fda5af6c"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("26ee17b7-0396-4c05-8ef1-c5537a32d92e"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("2787e866-fd82-4627-88fb-9c6a7989a947"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("2c753880-e534-4348-9a41-f411b28e28ce"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("43954c33-ef00-4f60-b0ed-b15a267f6267"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("45e87c22-f388-4593-8cad-2b3e56b3786c"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("624d61da-7b54-4e55-bb7b-aaf63a5aec68"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("6e9a20ea-aebb-4ee1-bd53-3ac538d8a8cb"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("7d78c524-571d-404e-a27e-53bda24b3873"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("85b3714e-3862-4054-bc0a-d55e4a9ac938"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("8e0e6973-cf18-4098-acb4-f5fe93163bcc"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("9c95a089-c2cd-41b6-9245-ceb683853f8f"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("9eeb100d-6665-491e-a4c8-09b68507797e"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("a92bd51f-2b47-4b98-9311-3ab1ef69ddd5"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("b24cb10a-3f66-437c-9576-b6b25c4d32cd"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("b60a451f-450f-4893-b707-3b5b886b7d25"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("bd69ac1f-3806-4bd0-99c4-a12909010602"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("c65c7d77-7d19-464f-bd27-8bf87a0a4ca9"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("c8e56583-540c-4aef-bd17-4e962cf0d75f"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("cdd11d7d-ee37-462d-ab65-96814f341ce8"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("cf6a81ed-6438-4c59-8156-746023eaac8b"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("d48bd8b3-2d0e-4ba5-bdf3-15271b6bbf95"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("df286152-5fd4-4d19-b38e-5642e87b8b1c"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("e6b7ee3d-c9d9-4f83-ae57-6c79b7aa318d"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("f2d82470-08ae-4bc3-bc82-f9b43b0daaec"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("f369782e-eb4d-4c14-8c40-4e863c50d31c"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("f6acf004-e3c6-4118-9273-d30f2febc72a"));

            migrationBuilder.AddColumn<string>(
                name: "AIAdjudicationReasoning",
                table: "Claims",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AIConfidenceScore",
                table: "Claims",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsAIApproved",
                table: "Claims",
                type: "bit",
                nullable: false,
                defaultValue: false);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "AIAdjudicationReasoning",
                table: "Claims");

            migrationBuilder.DropColumn(
                name: "AIConfidenceScore",
                table: "Claims");

            migrationBuilder.DropColumn(
                name: "IsAIApproved",
                table: "Claims");

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 17, 8, 57, 3, 234, DateTimeKind.Utc).AddTicks(6338));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111112"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 17, 8, 57, 3, 234, DateTimeKind.Utc).AddTicks(6359));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111113"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 17, 8, 57, 3, 234, DateTimeKind.Utc).AddTicks(6365));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222221"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 17, 8, 57, 3, 234, DateTimeKind.Utc).AddTicks(6370));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 17, 8, 57, 3, 234, DateTimeKind.Utc).AddTicks(6374));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222223"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 17, 8, 57, 3, 234, DateTimeKind.Utc).AddTicks(6378));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222224"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 17, 8, 57, 3, 234, DateTimeKind.Utc).AddTicks(6394));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333331"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 17, 8, 57, 3, 234, DateTimeKind.Utc).AddTicks(6398));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333332"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 17, 8, 57, 3, 234, DateTimeKind.Utc).AddTicks(6403));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 17, 8, 57, 3, 234, DateTimeKind.Utc).AddTicks(6407));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333334"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 17, 8, 57, 3, 234, DateTimeKind.Utc).AddTicks(6411));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333335"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 17, 8, 57, 3, 234, DateTimeKind.Utc).AddTicks(6415));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444441"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 17, 8, 57, 3, 234, DateTimeKind.Utc).AddTicks(6418));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444442"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 17, 8, 57, 3, 234, DateTimeKind.Utc).AddTicks(6422));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444443"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 17, 8, 57, 3, 234, DateTimeKind.Utc).AddTicks(6428));

            migrationBuilder.InsertData(
                table: "PlanCoverages",
                columns: new[] { "Id", "CoverageAmount", "CoveredGroup", "InsurancePlanId", "IsActive", "Type" },
                values: new object[,]
                {
                    { new Guid("0e6c15a9-25dd-435f-8ee3-edfe99ab3106"), 500000m, 1, new Guid("22222222-2222-2222-2222-222222222222"), true, 0 },
                    { new Guid("1f7626c2-b1f9-42ed-b89f-df95fda5af6c"), 700000m, 1, new Guid("22222222-2222-2222-2222-222222222224"), true, 0 },
                    { new Guid("26ee17b7-0396-4c05-8ef1-c5537a32d92e"), 1000000m, 0, new Guid("22222222-2222-2222-2222-222222222223"), true, 1 },
                    { new Guid("2787e866-fd82-4627-88fb-9c6a7989a947"), 1500000m, 0, new Guid("22222222-2222-2222-2222-222222222224"), true, 1 },
                    { new Guid("2c753880-e534-4348-9a41-f411b28e28ce"), 3000000m, 0, new Guid("44444444-4444-4444-4444-444444444442"), true, 1 },
                    { new Guid("43954c33-ef00-4f60-b0ed-b15a267f6267"), 2000000m, 0, new Guid("33333333-3333-3333-3333-333333333333"), true, 1 },
                    { new Guid("45e87c22-f388-4593-8cad-2b3e56b3786c"), 4000000m, 0, new Guid("44444444-4444-4444-4444-444444444443"), true, 2 },
                    { new Guid("624d61da-7b54-4e55-bb7b-aaf63a5aec68"), 1000000m, 1, new Guid("33333333-3333-3333-3333-333333333334"), true, 0 },
                    { new Guid("6e9a20ea-aebb-4ee1-bd53-3ac538d8a8cb"), 1000000m, 0, new Guid("22222222-2222-2222-2222-222222222224"), true, 2 },
                    { new Guid("7d78c524-571d-404e-a27e-53bda24b3873"), 700000m, 1, new Guid("44444444-4444-4444-4444-444444444441"), true, 0 },
                    { new Guid("85b3714e-3862-4054-bc0a-d55e4a9ac938"), 300000m, 1, new Guid("11111111-1111-1111-1111-111111111113"), true, 0 },
                    { new Guid("8e0e6973-cf18-4098-acb4-f5fe93163bcc"), 3000000m, 0, new Guid("33333333-3333-3333-3333-333333333335"), true, 2 },
                    { new Guid("9c95a089-c2cd-41b6-9245-ceb683853f8f"), 500000m, 0, new Guid("11111111-1111-1111-1111-111111111113"), true, 1 },
                    { new Guid("9eeb100d-6665-491e-a4c8-09b68507797e"), 700000m, 1, new Guid("33333333-3333-3333-3333-333333333332"), true, 0 },
                    { new Guid("a92bd51f-2b47-4b98-9311-3ab1ef69ddd5"), 1500000m, 2, new Guid("33333333-3333-3333-3333-333333333335"), true, 0 },
                    { new Guid("b24cb10a-3f66-437c-9576-b6b25c4d32cd"), 3000000m, 0, new Guid("33333333-3333-3333-3333-333333333334"), true, 1 },
                    { new Guid("b60a451f-450f-4893-b707-3b5b886b7d25"), 1000000m, 1, new Guid("44444444-4444-4444-4444-444444444442"), true, 0 },
                    { new Guid("bd69ac1f-3806-4bd0-99c4-a12909010602"), 300000m, 1, new Guid("11111111-1111-1111-1111-111111111112"), true, 0 },
                    { new Guid("c65c7d77-7d19-464f-bd27-8bf87a0a4ca9"), 300000m, 0, new Guid("22222222-2222-2222-2222-222222222221"), true, 0 },
                    { new Guid("c8e56583-540c-4aef-bd17-4e962cf0d75f"), 200000m, 0, new Guid("11111111-1111-1111-1111-111111111111"), true, 0 },
                    { new Guid("cdd11d7d-ee37-462d-ab65-96814f341ce8"), 2000000m, 0, new Guid("33333333-3333-3333-3333-333333333334"), true, 2 },
                    { new Guid("cf6a81ed-6438-4c59-8156-746023eaac8b"), 700000m, 1, new Guid("33333333-3333-3333-3333-333333333333"), true, 0 },
                    { new Guid("d48bd8b3-2d0e-4ba5-bdf3-15271b6bbf95"), 4000000m, 0, new Guid("33333333-3333-3333-3333-333333333335"), true, 1 },
                    { new Guid("df286152-5fd4-4d19-b38e-5642e87b8b1c"), 500000m, 0, new Guid("33333333-3333-3333-3333-333333333331"), true, 0 },
                    { new Guid("e6b7ee3d-c9d9-4f83-ae57-6c79b7aa318d"), 5000000m, 0, new Guid("44444444-4444-4444-4444-444444444443"), true, 1 },
                    { new Guid("f2d82470-08ae-4bc3-bc82-f9b43b0daaec"), 500000m, 1, new Guid("22222222-2222-2222-2222-222222222223"), true, 0 },
                    { new Guid("f369782e-eb4d-4c14-8c40-4e863c50d31c"), 2000000m, 2, new Guid("44444444-4444-4444-4444-444444444443"), true, 0 },
                    { new Guid("f6acf004-e3c6-4118-9273-d30f2febc72a"), 500000m, 0, new Guid("33333333-3333-3333-3333-333333333335"), true, 3 }
                });
        }
    }
}
