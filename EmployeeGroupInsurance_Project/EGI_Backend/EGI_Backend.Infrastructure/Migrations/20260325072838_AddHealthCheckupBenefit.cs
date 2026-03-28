using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EGI_Backend.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddHealthCheckupBenefit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("0cab22ea-3c6a-4337-8faf-929a7fbc0e7c"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("0f2287cc-0aa3-49f2-b490-4f9ecdf6a0e2"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("1257a537-e2b9-485c-bd2f-16d0bbc99c67"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("22eed64e-c84d-485d-8cc2-0ef8f0319873"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("31366672-12d2-4b4d-82b7-db421f7927a1"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("316b41cd-182c-44b8-9006-5919fc575596"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("3172ca3f-fd1c-4ac2-9b81-3deb7182f7f1"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("31a83a4d-0047-435e-8cfa-cecd3090e0b5"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("3976e5c8-66d5-4236-8967-ffdc5a6b3dec"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("42b457d9-8d49-4d54-b1b8-93b21b878d4d"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("59ef49d8-be66-44c3-85de-f55437555b1b"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("623d5230-8178-4e64-bfd2-d4a9a759e0c8"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("65332b66-9c95-461a-8367-b46fce295f74"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("65e0d02f-2c73-4e7e-b13e-539ce2735957"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("6ef2221c-200a-406f-ad7c-fe8a5d8a7e13"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("7da248d1-ed0a-4169-a794-2c2ef216fc46"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("8371af0a-b42c-4242-abfd-bd342f7cbb2e"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("852f7fcf-a6bb-4fb2-a773-0070413707c7"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("a387be4d-838e-451d-925b-87db8cc7c738"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("af5c69a6-ee94-4dfd-adb7-68809717eaba"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("aff10fee-bec0-4007-bb56-ec7a9a543109"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("c6545531-076a-486b-b4d6-a2cf8aaa5804"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("cbddd181-bb70-460f-8959-5447b503c2f7"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("cd0fbdb6-6ac6-4b9e-a9dd-45001060fb63"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("e8493f1c-b473-49b7-aebe-e0321eb4dd86"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("edc783c2-2258-4d39-8cfa-702966f0150c"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("f41f9662-bd8e-4193-9a89-116401e4df62"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("f68bd1e7-2aec-4d6d-9f9a-fe28485cb9af"));

            migrationBuilder.AddColumn<bool>(
                name: "HasHealthCheckup",
                table: "InsurancePlans",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "HasHealthCheckup" },
                values: new object[] { new DateTime(2026, 3, 25, 7, 28, 37, 699, DateTimeKind.Utc).AddTicks(6472), false });

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111112"),
                columns: new[] { "CreatedAt", "HasHealthCheckup" },
                values: new object[] { new DateTime(2026, 3, 25, 7, 28, 37, 699, DateTimeKind.Utc).AddTicks(6500), false });

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111113"),
                columns: new[] { "CreatedAt", "HasHealthCheckup" },
                values: new object[] { new DateTime(2026, 3, 25, 7, 28, 37, 699, DateTimeKind.Utc).AddTicks(6506), false });

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222221"),
                columns: new[] { "CreatedAt", "HasHealthCheckup" },
                values: new object[] { new DateTime(2026, 3, 25, 7, 28, 37, 699, DateTimeKind.Utc).AddTicks(6510), false });

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "HasHealthCheckup" },
                values: new object[] { new DateTime(2026, 3, 25, 7, 28, 37, 699, DateTimeKind.Utc).AddTicks(6514), false });

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222223"),
                columns: new[] { "CreatedAt", "HasHealthCheckup" },
                values: new object[] { new DateTime(2026, 3, 25, 7, 28, 37, 699, DateTimeKind.Utc).AddTicks(6518), false });

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222224"),
                columns: new[] { "CreatedAt", "HasHealthCheckup" },
                values: new object[] { new DateTime(2026, 3, 25, 7, 28, 37, 699, DateTimeKind.Utc).AddTicks(6521), false });

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333331"),
                columns: new[] { "CreatedAt", "HasHealthCheckup" },
                values: new object[] { new DateTime(2026, 3, 25, 7, 28, 37, 699, DateTimeKind.Utc).AddTicks(6536), false });

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333332"),
                columns: new[] { "CreatedAt", "HasHealthCheckup" },
                values: new object[] { new DateTime(2026, 3, 25, 7, 28, 37, 699, DateTimeKind.Utc).AddTicks(6540), false });

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                columns: new[] { "CreatedAt", "HasHealthCheckup" },
                values: new object[] { new DateTime(2026, 3, 25, 7, 28, 37, 699, DateTimeKind.Utc).AddTicks(6543), false });

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333334"),
                columns: new[] { "CreatedAt", "HasHealthCheckup" },
                values: new object[] { new DateTime(2026, 3, 25, 7, 28, 37, 699, DateTimeKind.Utc).AddTicks(6546), false });

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333335"),
                columns: new[] { "CreatedAt", "HasHealthCheckup" },
                values: new object[] { new DateTime(2026, 3, 25, 7, 28, 37, 699, DateTimeKind.Utc).AddTicks(6549), false });

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444441"),
                columns: new[] { "CreatedAt", "HasHealthCheckup" },
                values: new object[] { new DateTime(2026, 3, 25, 7, 28, 37, 699, DateTimeKind.Utc).AddTicks(6553), false });

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444442"),
                columns: new[] { "CreatedAt", "HasHealthCheckup" },
                values: new object[] { new DateTime(2026, 3, 25, 7, 28, 37, 699, DateTimeKind.Utc).AddTicks(6609), false });

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444443"),
                columns: new[] { "CreatedAt", "HasHealthCheckup" },
                values: new object[] { new DateTime(2026, 3, 25, 7, 28, 37, 699, DateTimeKind.Utc).AddTicks(6614), false });

            migrationBuilder.InsertData(
                table: "PlanCoverages",
                columns: new[] { "Id", "CoverageAmount", "CoveredGroup", "InsurancePlanId", "IsActive", "Type" },
                values: new object[,]
                {
                    { new Guid("2da365e5-cf7c-48f1-a4e7-53fc88468660"), 700000m, 1, new Guid("33333333-3333-3333-3333-333333333333"), true, 0 },
                    { new Guid("3d8538d2-cdbb-4e44-ae8b-14fdf59342c6"), 1000000m, 0, new Guid("22222222-2222-2222-2222-222222222224"), true, 2 },
                    { new Guid("607c878d-3d47-4519-a00d-38fffdc4ed6e"), 700000m, 1, new Guid("22222222-2222-2222-2222-222222222224"), true, 0 },
                    { new Guid("6f522c44-ffdc-4383-830b-0ee284c78f3d"), 5000000m, 0, new Guid("44444444-4444-4444-4444-444444444443"), true, 1 },
                    { new Guid("72eef32d-d74a-48bd-807e-9d0303ff85a4"), 500000m, 0, new Guid("11111111-1111-1111-1111-111111111113"), true, 1 },
                    { new Guid("7ba230ac-f48f-43c6-ba6f-27efc647fd65"), 500000m, 1, new Guid("22222222-2222-2222-2222-222222222223"), true, 0 },
                    { new Guid("87a88a28-531c-4986-8000-e619d8221509"), 2000000m, 2, new Guid("44444444-4444-4444-4444-444444444443"), true, 0 },
                    { new Guid("891c1b30-de99-4034-959a-d259926eba81"), 1000000m, 1, new Guid("44444444-4444-4444-4444-444444444442"), true, 0 },
                    { new Guid("951eeb73-1ed4-44b7-859e-336067309ad1"), 2000000m, 0, new Guid("33333333-3333-3333-3333-333333333334"), true, 2 },
                    { new Guid("a3348cd3-9f04-431a-92b8-5f80edbd4b62"), 300000m, 0, new Guid("22222222-2222-2222-2222-222222222221"), true, 0 },
                    { new Guid("a3727241-c2ba-4a17-aaa2-7d1172e5072f"), 500000m, 0, new Guid("33333333-3333-3333-3333-333333333335"), true, 3 },
                    { new Guid("a7a05710-ffc8-4400-8677-8a8aa2e376f8"), 1500000m, 0, new Guid("22222222-2222-2222-2222-222222222224"), true, 1 },
                    { new Guid("a9968c32-8731-489a-9d28-fad6a4a4edb4"), 3000000m, 0, new Guid("33333333-3333-3333-3333-333333333335"), true, 2 },
                    { new Guid("aaeff4ed-0115-49e3-89bb-c18cfd99d3a9"), 200000m, 0, new Guid("11111111-1111-1111-1111-111111111111"), true, 0 },
                    { new Guid("af0510e2-afef-49fc-b766-fde17bec6508"), 3000000m, 0, new Guid("44444444-4444-4444-4444-444444444442"), true, 1 },
                    { new Guid("bb7171bd-2cd6-436e-a13e-7e12ed161583"), 300000m, 1, new Guid("11111111-1111-1111-1111-111111111113"), true, 0 },
                    { new Guid("bd01b68c-726f-4aa3-9f8e-7c50bd5f0973"), 1000000m, 0, new Guid("22222222-2222-2222-2222-222222222223"), true, 1 },
                    { new Guid("bfd8b616-cd2d-4a60-b18b-2ea76bd03647"), 300000m, 1, new Guid("11111111-1111-1111-1111-111111111112"), true, 0 },
                    { new Guid("c23bfb51-b57f-4d9a-9a6b-b320c0fad4f2"), 4000000m, 0, new Guid("33333333-3333-3333-3333-333333333335"), true, 1 },
                    { new Guid("c2b86ddb-85bf-4d50-8936-bf111bd419d1"), 1500000m, 2, new Guid("33333333-3333-3333-3333-333333333335"), true, 0 },
                    { new Guid("c4211525-250c-4c7b-a7bd-767576673de5"), 500000m, 0, new Guid("33333333-3333-3333-3333-333333333331"), true, 0 },
                    { new Guid("d326e8e6-cb18-45b3-b513-01148f059b32"), 700000m, 1, new Guid("44444444-4444-4444-4444-444444444441"), true, 0 },
                    { new Guid("da4231df-feef-4e5d-ba3f-7e2febe4fca3"), 4000000m, 0, new Guid("44444444-4444-4444-4444-444444444443"), true, 2 },
                    { new Guid("df724ac8-93e8-486a-83e4-0130d1ffeea0"), 1000000m, 1, new Guid("33333333-3333-3333-3333-333333333334"), true, 0 },
                    { new Guid("ef13416e-014e-4ce7-9830-c80b1cfae72d"), 500000m, 1, new Guid("22222222-2222-2222-2222-222222222222"), true, 0 },
                    { new Guid("f2ea174e-aff1-4cc0-a547-d641bb3e99f2"), 3000000m, 0, new Guid("33333333-3333-3333-3333-333333333334"), true, 1 },
                    { new Guid("fa95a611-430d-44b0-94d1-1b9adac42d1b"), 700000m, 1, new Guid("33333333-3333-3333-3333-333333333332"), true, 0 },
                    { new Guid("fd401ee3-fbf0-46aa-a38d-0de032e1eff8"), 2000000m, 0, new Guid("33333333-3333-3333-3333-333333333333"), true, 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("2da365e5-cf7c-48f1-a4e7-53fc88468660"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("3d8538d2-cdbb-4e44-ae8b-14fdf59342c6"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("607c878d-3d47-4519-a00d-38fffdc4ed6e"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("6f522c44-ffdc-4383-830b-0ee284c78f3d"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("72eef32d-d74a-48bd-807e-9d0303ff85a4"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("7ba230ac-f48f-43c6-ba6f-27efc647fd65"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("87a88a28-531c-4986-8000-e619d8221509"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("891c1b30-de99-4034-959a-d259926eba81"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("951eeb73-1ed4-44b7-859e-336067309ad1"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("a3348cd3-9f04-431a-92b8-5f80edbd4b62"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("a3727241-c2ba-4a17-aaa2-7d1172e5072f"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("a7a05710-ffc8-4400-8677-8a8aa2e376f8"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("a9968c32-8731-489a-9d28-fad6a4a4edb4"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("aaeff4ed-0115-49e3-89bb-c18cfd99d3a9"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("af0510e2-afef-49fc-b766-fde17bec6508"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("bb7171bd-2cd6-436e-a13e-7e12ed161583"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("bd01b68c-726f-4aa3-9f8e-7c50bd5f0973"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("bfd8b616-cd2d-4a60-b18b-2ea76bd03647"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("c23bfb51-b57f-4d9a-9a6b-b320c0fad4f2"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("c2b86ddb-85bf-4d50-8936-bf111bd419d1"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("c4211525-250c-4c7b-a7bd-767576673de5"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("d326e8e6-cb18-45b3-b513-01148f059b32"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("da4231df-feef-4e5d-ba3f-7e2febe4fca3"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("df724ac8-93e8-486a-83e4-0130d1ffeea0"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("ef13416e-014e-4ce7-9830-c80b1cfae72d"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("f2ea174e-aff1-4cc0-a547-d641bb3e99f2"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("fa95a611-430d-44b0-94d1-1b9adac42d1b"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("fd401ee3-fbf0-46aa-a38d-0de032e1eff8"));

            migrationBuilder.DropColumn(
                name: "HasHealthCheckup",
                table: "InsurancePlans");

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 21, 19, 54, 40, 278, DateTimeKind.Utc).AddTicks(260));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111112"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 21, 19, 54, 40, 278, DateTimeKind.Utc).AddTicks(288));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111113"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 21, 19, 54, 40, 278, DateTimeKind.Utc).AddTicks(304));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222221"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 21, 19, 54, 40, 278, DateTimeKind.Utc).AddTicks(310));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 21, 19, 54, 40, 278, DateTimeKind.Utc).AddTicks(314));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222223"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 21, 19, 54, 40, 278, DateTimeKind.Utc).AddTicks(317));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222224"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 21, 19, 54, 40, 278, DateTimeKind.Utc).AddTicks(324));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333331"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 21, 19, 54, 40, 278, DateTimeKind.Utc).AddTicks(327));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333332"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 21, 19, 54, 40, 278, DateTimeKind.Utc).AddTicks(332));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 21, 19, 54, 40, 278, DateTimeKind.Utc).AddTicks(335));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333334"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 21, 19, 54, 40, 278, DateTimeKind.Utc).AddTicks(343));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333335"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 21, 19, 54, 40, 278, DateTimeKind.Utc).AddTicks(357));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444441"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 21, 19, 54, 40, 278, DateTimeKind.Utc).AddTicks(360));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444442"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 21, 19, 54, 40, 278, DateTimeKind.Utc).AddTicks(364));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444443"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 21, 19, 54, 40, 278, DateTimeKind.Utc).AddTicks(367));

            migrationBuilder.InsertData(
                table: "PlanCoverages",
                columns: new[] { "Id", "CoverageAmount", "CoveredGroup", "InsurancePlanId", "IsActive", "Type" },
                values: new object[,]
                {
                    { new Guid("0cab22ea-3c6a-4337-8faf-929a7fbc0e7c"), 2000000m, 0, new Guid("33333333-3333-3333-3333-333333333333"), true, 1 },
                    { new Guid("0f2287cc-0aa3-49f2-b490-4f9ecdf6a0e2"), 700000m, 1, new Guid("44444444-4444-4444-4444-444444444441"), true, 0 },
                    { new Guid("1257a537-e2b9-485c-bd2f-16d0bbc99c67"), 1000000m, 1, new Guid("33333333-3333-3333-3333-333333333334"), true, 0 },
                    { new Guid("22eed64e-c84d-485d-8cc2-0ef8f0319873"), 300000m, 1, new Guid("11111111-1111-1111-1111-111111111113"), true, 0 },
                    { new Guid("31366672-12d2-4b4d-82b7-db421f7927a1"), 5000000m, 0, new Guid("44444444-4444-4444-4444-444444444443"), true, 1 },
                    { new Guid("316b41cd-182c-44b8-9006-5919fc575596"), 3000000m, 0, new Guid("33333333-3333-3333-3333-333333333334"), true, 1 },
                    { new Guid("3172ca3f-fd1c-4ac2-9b81-3deb7182f7f1"), 700000m, 1, new Guid("33333333-3333-3333-3333-333333333333"), true, 0 },
                    { new Guid("31a83a4d-0047-435e-8cfa-cecd3090e0b5"), 500000m, 1, new Guid("22222222-2222-2222-2222-222222222222"), true, 0 },
                    { new Guid("3976e5c8-66d5-4236-8967-ffdc5a6b3dec"), 3000000m, 0, new Guid("33333333-3333-3333-3333-333333333335"), true, 2 },
                    { new Guid("42b457d9-8d49-4d54-b1b8-93b21b878d4d"), 300000m, 1, new Guid("11111111-1111-1111-1111-111111111112"), true, 0 },
                    { new Guid("59ef49d8-be66-44c3-85de-f55437555b1b"), 200000m, 0, new Guid("11111111-1111-1111-1111-111111111111"), true, 0 },
                    { new Guid("623d5230-8178-4e64-bfd2-d4a9a759e0c8"), 4000000m, 0, new Guid("33333333-3333-3333-3333-333333333335"), true, 1 },
                    { new Guid("65332b66-9c95-461a-8367-b46fce295f74"), 1000000m, 0, new Guid("22222222-2222-2222-2222-222222222224"), true, 2 },
                    { new Guid("65e0d02f-2c73-4e7e-b13e-539ce2735957"), 700000m, 1, new Guid("33333333-3333-3333-3333-333333333332"), true, 0 },
                    { new Guid("6ef2221c-200a-406f-ad7c-fe8a5d8a7e13"), 1000000m, 1, new Guid("44444444-4444-4444-4444-444444444442"), true, 0 },
                    { new Guid("7da248d1-ed0a-4169-a794-2c2ef216fc46"), 300000m, 0, new Guid("22222222-2222-2222-2222-222222222221"), true, 0 },
                    { new Guid("8371af0a-b42c-4242-abfd-bd342f7cbb2e"), 500000m, 0, new Guid("33333333-3333-3333-3333-333333333335"), true, 3 },
                    { new Guid("852f7fcf-a6bb-4fb2-a773-0070413707c7"), 700000m, 1, new Guid("22222222-2222-2222-2222-222222222224"), true, 0 },
                    { new Guid("a387be4d-838e-451d-925b-87db8cc7c738"), 500000m, 1, new Guid("22222222-2222-2222-2222-222222222223"), true, 0 },
                    { new Guid("af5c69a6-ee94-4dfd-adb7-68809717eaba"), 2000000m, 2, new Guid("44444444-4444-4444-4444-444444444443"), true, 0 },
                    { new Guid("aff10fee-bec0-4007-bb56-ec7a9a543109"), 1000000m, 0, new Guid("22222222-2222-2222-2222-222222222223"), true, 1 },
                    { new Guid("c6545531-076a-486b-b4d6-a2cf8aaa5804"), 4000000m, 0, new Guid("44444444-4444-4444-4444-444444444443"), true, 2 },
                    { new Guid("cbddd181-bb70-460f-8959-5447b503c2f7"), 500000m, 0, new Guid("33333333-3333-3333-3333-333333333331"), true, 0 },
                    { new Guid("cd0fbdb6-6ac6-4b9e-a9dd-45001060fb63"), 3000000m, 0, new Guid("44444444-4444-4444-4444-444444444442"), true, 1 },
                    { new Guid("e8493f1c-b473-49b7-aebe-e0321eb4dd86"), 1500000m, 0, new Guid("22222222-2222-2222-2222-222222222224"), true, 1 },
                    { new Guid("edc783c2-2258-4d39-8cfa-702966f0150c"), 1500000m, 2, new Guid("33333333-3333-3333-3333-333333333335"), true, 0 },
                    { new Guid("f41f9662-bd8e-4193-9a89-116401e4df62"), 500000m, 0, new Guid("11111111-1111-1111-1111-111111111113"), true, 1 },
                    { new Guid("f68bd1e7-2aec-4d6d-9f9a-fe28485cb9af"), 2000000m, 0, new Guid("33333333-3333-3333-3333-333333333334"), true, 2 }
                });
        }
    }
}
