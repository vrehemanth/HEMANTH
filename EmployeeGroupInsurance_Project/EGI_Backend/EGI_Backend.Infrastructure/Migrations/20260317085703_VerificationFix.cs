using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EGI_Backend.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class VerificationFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("0380062c-1bcd-43af-ab94-78f9ac74267d"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("058fa2e8-9e99-41a0-ae68-7869269e1809"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("19bba77e-7725-4a63-bb1c-fdd275d4459d"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("2359d400-c3e6-4f1c-b24f-caaf70fd87d3"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("2e4fd81b-8418-4c9c-97b5-bc58d5daa396"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("43de9368-8537-4986-91c5-8a4cd4052743"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("4491b3e1-f126-4342-b918-49f4c3a2ba13"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("52d3f21e-7c3a-4f11-ab33-438ef3cc95ff"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("5c402e9b-8e4c-4682-abdc-c5c5f7cbb738"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("5d1e9762-32a9-4cd1-bd42-4df9b11bf543"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("5f9909fe-68ab-443b-88c4-3e91b2b57dde"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("682214e0-e680-4b2d-a4ab-4dc720dbd61a"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("696d8a6f-2a7c-4f8d-aef7-3fde9e830de9"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("6e87058e-92ae-434d-97d2-c1722a0b22f2"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("6ed76d88-d719-4d5a-a247-257e63943ff6"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("6f48b968-fd59-47bd-9432-aeb16bbadf9f"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("7dc59e96-ccef-4ed2-81b1-2790fdd17dde"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("8248c417-5840-4503-b301-91a68176f07a"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("9a24543d-58b9-4d74-844f-aeb468229ee2"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("a41f2f88-0532-4ead-939d-279ddac37a48"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("aa4eb609-9711-4cd9-8d11-c7565eebb4f7"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("ad29bf95-5f2d-408d-8e3a-b0d7e0cd3543"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("ad2f0a40-4ec1-45d2-babe-fdabd014ed82"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("b3ea8c2d-15fb-479c-b737-8b1db325636c"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("bc742297-6f95-40ac-b6bc-998dabb6a026"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("e7a1e06c-f936-4398-90f6-6007f39e8242"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("ef47d690-fbd0-4b1e-b036-2ceda1426f0b"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("f2edeb57-9cc0-4cb4-b9ce-13faaa53e869"));

            migrationBuilder.AlterColumn<string>(
                name: "PolicyNo",
                table: "PolicyAssignments",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "EmployeeCode",
                table: "Members",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "InvoiceNo",
                table: "Invoices",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "IndustryType",
                table: "CorporateClients",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "ClaimNumber",
                table: "Claims",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

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

            migrationBuilder.CreateIndex(
                name: "IX_PolicyAssignments_PolicyNo",
                table: "PolicyAssignments",
                column: "PolicyNo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Members_EmployeeCode_CorporateClientId",
                table: "Members",
                columns: new[] { "EmployeeCode", "CorporateClientId" });

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_InvoiceNo",
                table: "Invoices",
                column: "InvoiceNo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Claims_ClaimNumber",
                table: "Claims",
                column: "ClaimNumber",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PolicyAssignments_PolicyNo",
                table: "PolicyAssignments");

            migrationBuilder.DropIndex(
                name: "IX_Members_EmployeeCode_CorporateClientId",
                table: "Members");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_InvoiceNo",
                table: "Invoices");

            migrationBuilder.DropIndex(
                name: "IX_Claims_ClaimNumber",
                table: "Claims");

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

            migrationBuilder.DropColumn(
                name: "IndustryType",
                table: "CorporateClients");

            migrationBuilder.AlterColumn<string>(
                name: "PolicyNo",
                table: "PolicyAssignments",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "EmployeeCode",
                table: "Members",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "InvoiceNo",
                table: "Invoices",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "ClaimNumber",
                table: "Claims",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 16, 11, 23, 42, 104, DateTimeKind.Utc).AddTicks(8640));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111112"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 16, 11, 23, 42, 104, DateTimeKind.Utc).AddTicks(9056));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111113"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 16, 11, 23, 42, 104, DateTimeKind.Utc).AddTicks(9074));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222221"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 16, 11, 23, 42, 104, DateTimeKind.Utc).AddTicks(9082));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 16, 11, 23, 42, 104, DateTimeKind.Utc).AddTicks(9092));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222223"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 16, 11, 23, 42, 104, DateTimeKind.Utc).AddTicks(9098));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222224"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 16, 11, 23, 42, 104, DateTimeKind.Utc).AddTicks(9104));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333331"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 16, 11, 23, 42, 104, DateTimeKind.Utc).AddTicks(9110));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333332"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 16, 11, 23, 42, 104, DateTimeKind.Utc).AddTicks(9126));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 16, 11, 23, 42, 104, DateTimeKind.Utc).AddTicks(9131));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333334"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 16, 11, 23, 42, 104, DateTimeKind.Utc).AddTicks(9138));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333335"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 16, 11, 23, 42, 104, DateTimeKind.Utc).AddTicks(9144));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444441"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 16, 11, 23, 42, 104, DateTimeKind.Utc).AddTicks(9150));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444442"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 16, 11, 23, 42, 104, DateTimeKind.Utc).AddTicks(9156));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444443"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 16, 11, 23, 42, 104, DateTimeKind.Utc).AddTicks(9165));

            migrationBuilder.InsertData(
                table: "PlanCoverages",
                columns: new[] { "Id", "CoverageAmount", "CoveredGroup", "InsurancePlanId", "IsActive", "Type" },
                values: new object[,]
                {
                    { new Guid("0380062c-1bcd-43af-ab94-78f9ac74267d"), 500000m, 1, new Guid("22222222-2222-2222-2222-222222222222"), true, 0 },
                    { new Guid("058fa2e8-9e99-41a0-ae68-7869269e1809"), 1000000m, 0, new Guid("22222222-2222-2222-2222-222222222223"), true, 1 },
                    { new Guid("19bba77e-7725-4a63-bb1c-fdd275d4459d"), 300000m, 0, new Guid("22222222-2222-2222-2222-222222222221"), true, 0 },
                    { new Guid("2359d400-c3e6-4f1c-b24f-caaf70fd87d3"), 1500000m, 2, new Guid("33333333-3333-3333-3333-333333333335"), true, 0 },
                    { new Guid("2e4fd81b-8418-4c9c-97b5-bc58d5daa396"), 500000m, 0, new Guid("11111111-1111-1111-1111-111111111113"), true, 1 },
                    { new Guid("43de9368-8537-4986-91c5-8a4cd4052743"), 300000m, 1, new Guid("11111111-1111-1111-1111-111111111113"), true, 0 },
                    { new Guid("4491b3e1-f126-4342-b918-49f4c3a2ba13"), 5000000m, 0, new Guid("44444444-4444-4444-4444-444444444443"), true, 1 },
                    { new Guid("52d3f21e-7c3a-4f11-ab33-438ef3cc95ff"), 300000m, 1, new Guid("11111111-1111-1111-1111-111111111112"), true, 0 },
                    { new Guid("5c402e9b-8e4c-4682-abdc-c5c5f7cbb738"), 4000000m, 0, new Guid("44444444-4444-4444-4444-444444444443"), true, 2 },
                    { new Guid("5d1e9762-32a9-4cd1-bd42-4df9b11bf543"), 4000000m, 0, new Guid("33333333-3333-3333-3333-333333333335"), true, 1 },
                    { new Guid("5f9909fe-68ab-443b-88c4-3e91b2b57dde"), 3000000m, 0, new Guid("44444444-4444-4444-4444-444444444442"), true, 1 },
                    { new Guid("682214e0-e680-4b2d-a4ab-4dc720dbd61a"), 2000000m, 2, new Guid("44444444-4444-4444-4444-444444444443"), true, 0 },
                    { new Guid("696d8a6f-2a7c-4f8d-aef7-3fde9e830de9"), 700000m, 1, new Guid("22222222-2222-2222-2222-222222222224"), true, 0 },
                    { new Guid("6e87058e-92ae-434d-97d2-c1722a0b22f2"), 500000m, 0, new Guid("33333333-3333-3333-3333-333333333335"), true, 3 },
                    { new Guid("6ed76d88-d719-4d5a-a247-257e63943ff6"), 1000000m, 0, new Guid("22222222-2222-2222-2222-222222222224"), true, 2 },
                    { new Guid("6f48b968-fd59-47bd-9432-aeb16bbadf9f"), 1000000m, 1, new Guid("44444444-4444-4444-4444-444444444442"), true, 0 },
                    { new Guid("7dc59e96-ccef-4ed2-81b1-2790fdd17dde"), 500000m, 1, new Guid("22222222-2222-2222-2222-222222222223"), true, 0 },
                    { new Guid("8248c417-5840-4503-b301-91a68176f07a"), 200000m, 0, new Guid("11111111-1111-1111-1111-111111111111"), true, 0 },
                    { new Guid("9a24543d-58b9-4d74-844f-aeb468229ee2"), 1500000m, 0, new Guid("22222222-2222-2222-2222-222222222224"), true, 1 },
                    { new Guid("a41f2f88-0532-4ead-939d-279ddac37a48"), 700000m, 1, new Guid("33333333-3333-3333-3333-333333333333"), true, 0 },
                    { new Guid("aa4eb609-9711-4cd9-8d11-c7565eebb4f7"), 500000m, 0, new Guid("33333333-3333-3333-3333-333333333331"), true, 0 },
                    { new Guid("ad29bf95-5f2d-408d-8e3a-b0d7e0cd3543"), 3000000m, 0, new Guid("33333333-3333-3333-3333-333333333334"), true, 1 },
                    { new Guid("ad2f0a40-4ec1-45d2-babe-fdabd014ed82"), 2000000m, 0, new Guid("33333333-3333-3333-3333-333333333334"), true, 2 },
                    { new Guid("b3ea8c2d-15fb-479c-b737-8b1db325636c"), 3000000m, 0, new Guid("33333333-3333-3333-3333-333333333335"), true, 2 },
                    { new Guid("bc742297-6f95-40ac-b6bc-998dabb6a026"), 700000m, 1, new Guid("44444444-4444-4444-4444-444444444441"), true, 0 },
                    { new Guid("e7a1e06c-f936-4398-90f6-6007f39e8242"), 1000000m, 1, new Guid("33333333-3333-3333-3333-333333333334"), true, 0 },
                    { new Guid("ef47d690-fbd0-4b1e-b036-2ceda1426f0b"), 2000000m, 0, new Guid("33333333-3333-3333-3333-333333333333"), true, 1 },
                    { new Guid("f2edeb57-9cc0-4cb4-b9ce-13faaa53e869"), 700000m, 1, new Guid("33333333-3333-3333-3333-333333333332"), true, 0 }
                });
        }
    }
}
