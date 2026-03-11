using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EGI_Backend.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InvoiceUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("01ab187c-26bf-4092-8d8c-908b34d40592"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("02a77076-1070-45da-8e9e-7f3263ba3290"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("0f0e8582-5b5f-4f46-8d2d-e1c728bd1115"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("106f5efe-0764-40bc-b04c-801c22dc3ff5"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("20c14505-c4fe-453b-9cf1-990a9789efff"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("22dde7a6-a93d-44ef-b062-532afb45e5a4"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("26bb6ce5-20f7-4847-b874-57a7536489dc"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("26d1e061-428a-46b6-ad86-0cd52d656459"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("3bdfd398-1afd-4833-a8e0-23337336714c"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("3beb0e41-6333-4b73-9a3d-5e59298819c0"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("3f59dd60-bd62-4804-abfa-e32e775a6f88"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("4fe8b37b-915b-48c5-b983-413275893b73"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("508e91d5-ead8-41aa-9912-eeca1d0ffb08"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("6212ab84-23e7-4ba1-870e-85c5fa489735"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("63047b01-7aad-4182-9696-ab13e6585d5d"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("76100d29-693f-462c-a1b2-5abb01c13179"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("878b2c6f-fe23-41ef-a6e2-a83d8c398556"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("abb680c1-8b1f-46de-98d2-1b6b0c518714"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("af79b9d2-e7d9-412f-a039-4040f86480b1"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("b0cef252-512b-4b2c-b3a8-bf8f64f13d8b"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("bbb4be48-4bce-4a98-a2a0-6fc6e1bd488c"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("ce6c5e6e-4689-48f7-8d2a-fa4343136157"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("cee44b76-923f-4932-a16d-2047af769983"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("d1d4b2b8-c587-466b-a393-c56fe9c25c0e"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("e533c8ea-43b1-4772-960a-22310a902f1f"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("eb0ed0b9-8bca-4b9e-9d9f-4218cf098cdc"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("f268560e-49b8-4b42-8cb8-e8c0ce9bba12"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("f5e925a3-dacb-4aed-a3cc-5c96bca47ac6"));

            migrationBuilder.AddColumn<bool>(
                name: "IsPenaltyApplied",
                table: "Invoices",
                type: "bit",
                nullable: false,
                defaultValue: false);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "IsPenaltyApplied",
                table: "Invoices");

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 10, 3, 24, 14, 499, DateTimeKind.Utc).AddTicks(7483));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111112"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 10, 3, 24, 14, 499, DateTimeKind.Utc).AddTicks(7524));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111113"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 10, 3, 24, 14, 499, DateTimeKind.Utc).AddTicks(7533));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222221"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 10, 3, 24, 14, 499, DateTimeKind.Utc).AddTicks(7539));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 10, 3, 24, 14, 499, DateTimeKind.Utc).AddTicks(7547));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222223"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 10, 3, 24, 14, 499, DateTimeKind.Utc).AddTicks(7552));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222224"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 10, 3, 24, 14, 499, DateTimeKind.Utc).AddTicks(7577));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333331"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 10, 3, 24, 14, 499, DateTimeKind.Utc).AddTicks(7584));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333332"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 10, 3, 24, 14, 499, DateTimeKind.Utc).AddTicks(7590));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 10, 3, 24, 14, 499, DateTimeKind.Utc).AddTicks(7596));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333334"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 10, 3, 24, 14, 499, DateTimeKind.Utc).AddTicks(7601));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333335"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 10, 3, 24, 14, 499, DateTimeKind.Utc).AddTicks(7607));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444441"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 10, 3, 24, 14, 499, DateTimeKind.Utc).AddTicks(7612));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444442"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 10, 3, 24, 14, 499, DateTimeKind.Utc).AddTicks(7618));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444443"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 10, 3, 24, 14, 499, DateTimeKind.Utc).AddTicks(7630));

            migrationBuilder.InsertData(
                table: "PlanCoverages",
                columns: new[] { "Id", "CoverageAmount", "CoveredGroup", "InsurancePlanId", "IsActive", "Type" },
                values: new object[,]
                {
                    { new Guid("01ab187c-26bf-4092-8d8c-908b34d40592"), 500000m, 0, new Guid("11111111-1111-1111-1111-111111111113"), true, 1 },
                    { new Guid("02a77076-1070-45da-8e9e-7f3263ba3290"), 300000m, 0, new Guid("22222222-2222-2222-2222-222222222221"), true, 0 },
                    { new Guid("0f0e8582-5b5f-4f46-8d2d-e1c728bd1115"), 700000m, 1, new Guid("33333333-3333-3333-3333-333333333333"), true, 0 },
                    { new Guid("106f5efe-0764-40bc-b04c-801c22dc3ff5"), 2000000m, 0, new Guid("33333333-3333-3333-3333-333333333333"), true, 1 },
                    { new Guid("20c14505-c4fe-453b-9cf1-990a9789efff"), 1000000m, 1, new Guid("44444444-4444-4444-4444-444444444442"), true, 0 },
                    { new Guid("22dde7a6-a93d-44ef-b062-532afb45e5a4"), 1000000m, 0, new Guid("22222222-2222-2222-2222-222222222224"), true, 2 },
                    { new Guid("26bb6ce5-20f7-4847-b874-57a7536489dc"), 500000m, 0, new Guid("33333333-3333-3333-3333-333333333331"), true, 0 },
                    { new Guid("26d1e061-428a-46b6-ad86-0cd52d656459"), 200000m, 0, new Guid("11111111-1111-1111-1111-111111111111"), true, 0 },
                    { new Guid("3bdfd398-1afd-4833-a8e0-23337336714c"), 4000000m, 0, new Guid("44444444-4444-4444-4444-444444444443"), true, 2 },
                    { new Guid("3beb0e41-6333-4b73-9a3d-5e59298819c0"), 300000m, 1, new Guid("11111111-1111-1111-1111-111111111113"), true, 0 },
                    { new Guid("3f59dd60-bd62-4804-abfa-e32e775a6f88"), 1000000m, 1, new Guid("33333333-3333-3333-3333-333333333334"), true, 0 },
                    { new Guid("4fe8b37b-915b-48c5-b983-413275893b73"), 700000m, 1, new Guid("22222222-2222-2222-2222-222222222224"), true, 0 },
                    { new Guid("508e91d5-ead8-41aa-9912-eeca1d0ffb08"), 1500000m, 2, new Guid("33333333-3333-3333-3333-333333333335"), true, 0 },
                    { new Guid("6212ab84-23e7-4ba1-870e-85c5fa489735"), 2000000m, 0, new Guid("33333333-3333-3333-3333-333333333334"), true, 2 },
                    { new Guid("63047b01-7aad-4182-9696-ab13e6585d5d"), 500000m, 1, new Guid("22222222-2222-2222-2222-222222222222"), true, 0 },
                    { new Guid("76100d29-693f-462c-a1b2-5abb01c13179"), 700000m, 1, new Guid("44444444-4444-4444-4444-444444444441"), true, 0 },
                    { new Guid("878b2c6f-fe23-41ef-a6e2-a83d8c398556"), 300000m, 1, new Guid("11111111-1111-1111-1111-111111111112"), true, 0 },
                    { new Guid("abb680c1-8b1f-46de-98d2-1b6b0c518714"), 3000000m, 0, new Guid("33333333-3333-3333-3333-333333333334"), true, 1 },
                    { new Guid("af79b9d2-e7d9-412f-a039-4040f86480b1"), 1500000m, 0, new Guid("22222222-2222-2222-2222-222222222224"), true, 1 },
                    { new Guid("b0cef252-512b-4b2c-b3a8-bf8f64f13d8b"), 4000000m, 0, new Guid("33333333-3333-3333-3333-333333333335"), true, 1 },
                    { new Guid("bbb4be48-4bce-4a98-a2a0-6fc6e1bd488c"), 500000m, 1, new Guid("22222222-2222-2222-2222-222222222223"), true, 0 },
                    { new Guid("ce6c5e6e-4689-48f7-8d2a-fa4343136157"), 1000000m, 0, new Guid("22222222-2222-2222-2222-222222222223"), true, 1 },
                    { new Guid("cee44b76-923f-4932-a16d-2047af769983"), 500000m, 0, new Guid("33333333-3333-3333-3333-333333333335"), true, 3 },
                    { new Guid("d1d4b2b8-c587-466b-a393-c56fe9c25c0e"), 700000m, 1, new Guid("33333333-3333-3333-3333-333333333332"), true, 0 },
                    { new Guid("e533c8ea-43b1-4772-960a-22310a902f1f"), 3000000m, 0, new Guid("44444444-4444-4444-4444-444444444442"), true, 1 },
                    { new Guid("eb0ed0b9-8bca-4b9e-9d9f-4218cf098cdc"), 3000000m, 0, new Guid("33333333-3333-3333-3333-333333333335"), true, 2 },
                    { new Guid("f268560e-49b8-4b42-8cb8-e8c0ce9bba12"), 5000000m, 0, new Guid("44444444-4444-4444-4444-444444444443"), true, 1 },
                    { new Guid("f5e925a3-dacb-4aed-a3cc-5c96bca47ac6"), 2000000m, 2, new Guid("44444444-4444-4444-4444-444444444443"), true, 0 }
                });
        }
    }
}
