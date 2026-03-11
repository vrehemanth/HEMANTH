using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EGI_Backend.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ClaimsUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "Members",
                type: "rowversion",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "Dependents",
                type: "rowversion",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "Description" },
                values: new object[] { new DateTime(2026, 3, 10, 3, 24, 14, 499, DateTimeKind.Utc).AddTicks(7483), "Health: ₹2L | Covers: Employee Only | No Life | No Accident" });

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111112"),
                columns: new[] { "CreatedAt", "Description" },
                values: new object[] { new DateTime(2026, 3, 10, 3, 24, 14, 499, DateTimeKind.Utc).AddTicks(7524), "Health: ₹3L | Covers: Employee + Spouse + 2 Children | No Life | No Accident" });

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111113"),
                columns: new[] { "CreatedAt", "Description" },
                values: new object[] { new DateTime(2026, 3, 10, 3, 24, 14, 499, DateTimeKind.Utc).AddTicks(7533), "Health: ₹3L (Family) | Life: ₹5L | No Accident | Parents Not Included" });

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222221"),
                columns: new[] { "CreatedAt", "Description" },
                values: new object[] { new DateTime(2026, 3, 10, 3, 24, 14, 499, DateTimeKind.Utc).AddTicks(7539), "Health: ₹3L | Covers: Employee Only" });

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "Description" },
                values: new object[] { new DateTime(2026, 3, 10, 3, 24, 14, 499, DateTimeKind.Utc).AddTicks(7547), "Health: ₹5L | Covers: Employee + Spouse + 2 Children" });

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222223"),
                columns: new[] { "CreatedAt", "Description" },
                values: new object[] { new DateTime(2026, 3, 10, 3, 24, 14, 499, DateTimeKind.Utc).AddTicks(7552), "Health: ₹5L (Family) | Life: ₹10L | Parents Not Included" });

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222224"),
                columns: new[] { "CreatedAt", "Description" },
                values: new object[] { new DateTime(2026, 3, 10, 3, 24, 14, 499, DateTimeKind.Utc).AddTicks(7577), "Health: ₹7L (Family) | Life: ₹15L | Accident: ₹10L | Parents Optional Add-on" });

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333331"),
                columns: new[] { "CreatedAt", "Description" },
                values: new object[] { new DateTime(2026, 3, 10, 3, 24, 14, 499, DateTimeKind.Utc).AddTicks(7584), "Health: ₹5L | Covers: Employee Only" });

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333332"),
                columns: new[] { "CreatedAt", "Description" },
                values: new object[] { new DateTime(2026, 3, 10, 3, 24, 14, 499, DateTimeKind.Utc).AddTicks(7590), "Health: ₹7L | Covers: Employee + Spouse + 2 Children" });

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                columns: new[] { "CreatedAt", "Description" },
                values: new object[] { new DateTime(2026, 3, 10, 3, 24, 14, 499, DateTimeKind.Utc).AddTicks(7596), "Health: ₹7L (Family) | Life: ₹20L" });

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333334"),
                columns: new[] { "CreatedAt", "Description" },
                values: new object[] { new DateTime(2026, 3, 10, 3, 24, 14, 499, DateTimeKind.Utc).AddTicks(7601), "Health: ₹10L (Family) | Life: ₹30L | Accident: ₹20L" });

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333335"),
                columns: new[] { "CreatedAt", "Description" },
                values: new object[] { new DateTime(2026, 3, 10, 3, 24, 14, 499, DateTimeKind.Utc).AddTicks(7607), "Health: ₹15L (Employee + Spouse + 2 Children + Parents) | Life: ₹40L | Accident: ₹30L | Critical Illness Included" });

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444441"),
                columns: new[] { "CreatedAt", "Description" },
                values: new object[] { new DateTime(2026, 3, 10, 3, 24, 14, 499, DateTimeKind.Utc).AddTicks(7612), "Health: ₹7L | Covers: Employee + Spouse + 2 Children" });

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444442"),
                columns: new[] { "CreatedAt", "Description" },
                values: new object[] { new DateTime(2026, 3, 10, 3, 24, 14, 499, DateTimeKind.Utc).AddTicks(7618), "Health: ₹10L (Employee + Spouse + 2 Children) | Life: ₹30L" });

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444443"),
                columns: new[] { "CreatedAt", "Description" },
                values: new object[] { new DateTime(2026, 3, 10, 3, 24, 14, 499, DateTimeKind.Utc).AddTicks(7630), "Health: ₹20L (Employee + Spouse + 2 Children + Parents) | Life: ₹50L | Accident: ₹40L | Global Coverage Option | Dedicated Claim Officer" });

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

            migrationBuilder.CreateIndex(
                name: "IX_PolicyAssignments_Status",
                table: "PolicyAssignments",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_Claims_ClaimDate",
                table: "Claims",
                column: "ClaimDate");

            migrationBuilder.CreateIndex(
                name: "IX_Claims_Status",
                table: "Claims",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogs_Timestamp",
                table: "AuditLogs",
                column: "Timestamp");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PolicyAssignments_Status",
                table: "PolicyAssignments");

            migrationBuilder.DropIndex(
                name: "IX_Claims_ClaimDate",
                table: "Claims");

            migrationBuilder.DropIndex(
                name: "IX_Claims_Status",
                table: "Claims");

            migrationBuilder.DropIndex(
                name: "IX_AuditLogs_Timestamp",
                table: "AuditLogs");

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

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "Dependents");

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "Description" },
                values: new object[] { new DateTime(2026, 3, 8, 12, 1, 18, 293, DateTimeKind.Utc).AddTicks(9682), "Health: ?2L | Covers: Employee Only | No Life | No Accident" });

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111112"),
                columns: new[] { "CreatedAt", "Description" },
                values: new object[] { new DateTime(2026, 3, 8, 12, 1, 18, 293, DateTimeKind.Utc).AddTicks(9700), "Health: ?3L | Covers: Employee + Spouse + 2 Children | No Life | No Accident" });

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111113"),
                columns: new[] { "CreatedAt", "Description" },
                values: new object[] { new DateTime(2026, 3, 8, 12, 1, 18, 293, DateTimeKind.Utc).AddTicks(9704), "Health: ?3L (Family) | Life: ?5L | No Accident | Parents Not Included" });

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222221"),
                columns: new[] { "CreatedAt", "Description" },
                values: new object[] { new DateTime(2026, 3, 8, 12, 1, 18, 293, DateTimeKind.Utc).AddTicks(9708), "Health: ?3L | Covers: Employee Only" });

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "Description" },
                values: new object[] { new DateTime(2026, 3, 8, 12, 1, 18, 293, DateTimeKind.Utc).AddTicks(9712), "Health: ?5L | Covers: Employee + Spouse + 2 Children" });

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222223"),
                columns: new[] { "CreatedAt", "Description" },
                values: new object[] { new DateTime(2026, 3, 8, 12, 1, 18, 293, DateTimeKind.Utc).AddTicks(9715), "Health: ?5L (Family) | Life: ?10L | Parents Not Included" });

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222224"),
                columns: new[] { "CreatedAt", "Description" },
                values: new object[] { new DateTime(2026, 3, 8, 12, 1, 18, 293, DateTimeKind.Utc).AddTicks(9719), "Health: ?7L (Family) | Life: ?15L | Accident: ?10L | Parents Optional Add-on" });

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333331"),
                columns: new[] { "CreatedAt", "Description" },
                values: new object[] { new DateTime(2026, 3, 8, 12, 1, 18, 293, DateTimeKind.Utc).AddTicks(9723), "Health: ?5L | Covers: Employee Only" });

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333332"),
                columns: new[] { "CreatedAt", "Description" },
                values: new object[] { new DateTime(2026, 3, 8, 12, 1, 18, 293, DateTimeKind.Utc).AddTicks(9729), "Health: ?7L | Covers: Employee + Spouse + 2 Children" });

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                columns: new[] { "CreatedAt", "Description" },
                values: new object[] { new DateTime(2026, 3, 8, 12, 1, 18, 293, DateTimeKind.Utc).AddTicks(9732), "Health: ?7L (Family) | Life: ?20L" });

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333334"),
                columns: new[] { "CreatedAt", "Description" },
                values: new object[] { new DateTime(2026, 3, 8, 12, 1, 18, 293, DateTimeKind.Utc).AddTicks(9736), "Health: ?10L (Family) | Life: ?30L | Accident: ?20L" });

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333335"),
                columns: new[] { "CreatedAt", "Description" },
                values: new object[] { new DateTime(2026, 3, 8, 12, 1, 18, 293, DateTimeKind.Utc).AddTicks(9739), "Health: ?15L (Employee + Spouse + 2 Children + Parents) | Life: ?40L | Accident: ?30L | Critical Illness Included" });

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444441"),
                columns: new[] { "CreatedAt", "Description" },
                values: new object[] { new DateTime(2026, 3, 8, 12, 1, 18, 293, DateTimeKind.Utc).AddTicks(9742), "Health: ?7L | Covers: Employee + Spouse + 2 Children" });

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444442"),
                columns: new[] { "CreatedAt", "Description" },
                values: new object[] { new DateTime(2026, 3, 8, 12, 1, 18, 293, DateTimeKind.Utc).AddTicks(9745), "Health: ?10L (Employee + Spouse + 2 Children) | Life: ?30L" });

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444443"),
                columns: new[] { "CreatedAt", "Description" },
                values: new object[] { new DateTime(2026, 3, 8, 12, 1, 18, 293, DateTimeKind.Utc).AddTicks(9748), "Health: ?20L (Employee + Spouse + 2 Children + Parents) | Life: ?50L | Accident: ?40L | Global Coverage Option | Dedicated Claim Officer" });

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
    }
}
