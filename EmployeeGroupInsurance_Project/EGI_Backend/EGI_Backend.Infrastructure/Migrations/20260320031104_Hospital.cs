using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EGI_Backend.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Hospital : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("04665df4-41af-4aeb-ab6c-7160dc480de6"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("049eb84c-0092-40f5-ada9-106f08eb7b11"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("0ce1f1c1-f466-43d8-b149-adf84d05d8b7"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("0ecf1cb4-fa48-4509-b538-4970d4667023"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("15234747-159d-4701-b482-631e5a38374d"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("15cab820-a954-4e54-81ce-5d8ce55ab212"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("16dde89d-9b4d-41dc-b48f-d71d2e3725b3"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("3b75a9d3-1b06-404f-95cc-9f6b2d322d16"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("517ee7eb-3446-4f52-a32c-3e8cd3dee296"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("734c3ce2-2852-40ba-9a04-d66007ea14e9"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("7747e54d-f4c8-4c0d-8d84-ec903a48e961"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("8905b2bd-fdcb-4027-9ad2-efd3f8544f4a"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("9000f8e4-4560-473d-bace-1ce14f321316"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("9aab3169-d331-4052-98f9-4879e60703f5"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("9d2a8ac3-d065-4773-af80-a6dc2c860053"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("a221664a-5396-457e-9435-3cd6dd860128"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("a7118dbe-a072-4f58-820a-bd8f170da9a7"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("ab42482f-47ad-41c0-95ed-cc8f571d8343"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("af240586-b2a4-4984-aea8-953c7fa60988"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("b9776d20-397f-4631-bca0-a868e9c1bc39"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("c98955c4-0fe0-4722-876e-745577a9e217"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("cb877d04-f90f-4329-b7ca-08f57ab5a30f"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("d58184df-7c49-4631-b9c2-fe106322c5fc"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("e5bdd39a-697c-452f-b38d-3bbeae137cd9"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("e7840206-3add-49f2-8035-839e6e1760fe"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("f7517ca2-7294-4aa1-9117-3ba5cb58a632"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("fd6cf5bc-8ec0-42a9-a725-41394d004e04"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("fdc1bfa8-a236-4acc-9c10-87bd5bed1ced"));

            migrationBuilder.AddColumn<bool>(
                name: "IsCashless",
                table: "Claims",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsSettledWithHospital",
                table: "Claims",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "NetworkHospitalId",
                table: "Claims",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "SettledAt",
                table: "Claims",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Hospitals",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    State = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ZipCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Latitude = table.Column<double>(type: "float", nullable: false),
                    Longitude = table.Column<double>(type: "float", nullable: false),
                    ContactNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Specialties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsNetworkHospital = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hospitals", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 20, 3, 11, 1, 175, DateTimeKind.Utc).AddTicks(178));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111112"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 20, 3, 11, 1, 175, DateTimeKind.Utc).AddTicks(234));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111113"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 20, 3, 11, 1, 175, DateTimeKind.Utc).AddTicks(241));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222221"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 20, 3, 11, 1, 175, DateTimeKind.Utc).AddTicks(247));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 20, 3, 11, 1, 175, DateTimeKind.Utc).AddTicks(253));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222223"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 20, 3, 11, 1, 175, DateTimeKind.Utc).AddTicks(364));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222224"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 20, 3, 11, 1, 175, DateTimeKind.Utc).AddTicks(370));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333331"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 20, 3, 11, 1, 175, DateTimeKind.Utc).AddTicks(376));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333332"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 20, 3, 11, 1, 175, DateTimeKind.Utc).AddTicks(381));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 20, 3, 11, 1, 175, DateTimeKind.Utc).AddTicks(390));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333334"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 20, 3, 11, 1, 175, DateTimeKind.Utc).AddTicks(395));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333335"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 20, 3, 11, 1, 175, DateTimeKind.Utc).AddTicks(400));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444441"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 20, 3, 11, 1, 175, DateTimeKind.Utc).AddTicks(406));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444442"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 20, 3, 11, 1, 175, DateTimeKind.Utc).AddTicks(412));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444443"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 20, 3, 11, 1, 175, DateTimeKind.Utc).AddTicks(417));

            migrationBuilder.InsertData(
                table: "PlanCoverages",
                columns: new[] { "Id", "CoverageAmount", "CoveredGroup", "InsurancePlanId", "IsActive", "Type" },
                values: new object[,]
                {
                    { new Guid("09aedc40-6905-4757-b98e-0d67819e2b9c"), 500000m, 0, new Guid("33333333-3333-3333-3333-333333333335"), true, 3 },
                    { new Guid("0a9e982f-b895-45c6-9881-95baa54d5c96"), 3000000m, 0, new Guid("33333333-3333-3333-3333-333333333334"), true, 1 },
                    { new Guid("173d236d-88d1-46bf-ab90-41eaa9f3fbf1"), 1500000m, 2, new Guid("33333333-3333-3333-3333-333333333335"), true, 0 },
                    { new Guid("1e476253-c632-48ed-9cf9-d0befdef904d"), 500000m, 1, new Guid("22222222-2222-2222-2222-222222222223"), true, 0 },
                    { new Guid("26991dd3-7628-4de9-91cc-532baad38920"), 2000000m, 0, new Guid("33333333-3333-3333-3333-333333333333"), true, 1 },
                    { new Guid("2be333e0-3f6a-42d8-9645-8e11b49db0e1"), 1500000m, 0, new Guid("22222222-2222-2222-2222-222222222224"), true, 1 },
                    { new Guid("2d2023eb-2763-41ee-b2b4-4ba3b18107e5"), 700000m, 1, new Guid("33333333-3333-3333-3333-333333333332"), true, 0 },
                    { new Guid("37e68c1e-2c63-457e-8f1a-8611466946ea"), 300000m, 1, new Guid("11111111-1111-1111-1111-111111111112"), true, 0 },
                    { new Guid("3905720e-37e2-444e-bf11-6d0283211f3e"), 5000000m, 0, new Guid("44444444-4444-4444-4444-444444444443"), true, 1 },
                    { new Guid("45511029-b19d-4e14-afe4-48e150f1fb57"), 700000m, 1, new Guid("44444444-4444-4444-4444-444444444441"), true, 0 },
                    { new Guid("464a58c2-076b-4a8c-b3ee-210ba71cd416"), 500000m, 0, new Guid("33333333-3333-3333-3333-333333333331"), true, 0 },
                    { new Guid("4fa0b56a-1e81-4298-be3e-34d323aad732"), 4000000m, 0, new Guid("33333333-3333-3333-3333-333333333335"), true, 1 },
                    { new Guid("613a6773-0f63-4a55-892f-3b0e0785f0ed"), 700000m, 1, new Guid("22222222-2222-2222-2222-222222222224"), true, 0 },
                    { new Guid("6196f18b-f9ea-47c0-83a9-24a640ef5370"), 1000000m, 1, new Guid("33333333-3333-3333-3333-333333333334"), true, 0 },
                    { new Guid("6bb91c88-159b-44a4-9c09-bed7fd062d8d"), 2000000m, 2, new Guid("44444444-4444-4444-4444-444444444443"), true, 0 },
                    { new Guid("7560de8b-d19a-4b6b-82b1-c161f9b18062"), 200000m, 0, new Guid("11111111-1111-1111-1111-111111111111"), true, 0 },
                    { new Guid("8ffbf434-f294-481d-bf05-b9d7f838c74c"), 2000000m, 0, new Guid("33333333-3333-3333-3333-333333333334"), true, 2 },
                    { new Guid("909115a7-23fe-49a4-9b1d-3c23aa6efae2"), 4000000m, 0, new Guid("44444444-4444-4444-4444-444444444443"), true, 2 },
                    { new Guid("c96f2833-1450-473d-88d5-cfd02bc421b0"), 3000000m, 0, new Guid("33333333-3333-3333-3333-333333333335"), true, 2 },
                    { new Guid("cc42521e-610e-42f4-9252-76b17770c9d8"), 1000000m, 0, new Guid("22222222-2222-2222-2222-222222222223"), true, 1 },
                    { new Guid("d0f9c4e3-d51e-46ab-bbdb-cdc948d57b95"), 1000000m, 0, new Guid("22222222-2222-2222-2222-222222222224"), true, 2 },
                    { new Guid("d4a5cb0c-def5-4eaa-937e-e0647c4cf3f1"), 1000000m, 1, new Guid("44444444-4444-4444-4444-444444444442"), true, 0 },
                    { new Guid("de88cef3-21a8-44b9-bc97-6a5268339b07"), 3000000m, 0, new Guid("44444444-4444-4444-4444-444444444442"), true, 1 },
                    { new Guid("e001b7dc-a251-4068-a2f5-75e9514d900c"), 300000m, 1, new Guid("11111111-1111-1111-1111-111111111113"), true, 0 },
                    { new Guid("e39a3beb-e2ba-41d0-ada3-06e4546067d5"), 700000m, 1, new Guid("33333333-3333-3333-3333-333333333333"), true, 0 },
                    { new Guid("e84580d0-333f-44b6-b50c-d354d80a6a76"), 500000m, 1, new Guid("22222222-2222-2222-2222-222222222222"), true, 0 },
                    { new Guid("e845cc68-3257-45f1-9ccf-c9901fb2b46c"), 500000m, 0, new Guid("11111111-1111-1111-1111-111111111113"), true, 1 },
                    { new Guid("ef9080b3-b918-4aaa-91d5-f8691f9f6ddc"), 300000m, 0, new Guid("22222222-2222-2222-2222-222222222221"), true, 0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Claims_NetworkHospitalId",
                table: "Claims",
                column: "NetworkHospitalId");

            migrationBuilder.CreateIndex(
                name: "IX_Hospitals_City",
                table: "Hospitals",
                column: "City");

            migrationBuilder.CreateIndex(
                name: "IX_Hospitals_IsNetworkHospital",
                table: "Hospitals",
                column: "IsNetworkHospital");

            migrationBuilder.AddForeignKey(
                name: "FK_Claims_Hospitals_NetworkHospitalId",
                table: "Claims",
                column: "NetworkHospitalId",
                principalTable: "Hospitals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Claims_Hospitals_NetworkHospitalId",
                table: "Claims");

            migrationBuilder.DropTable(
                name: "Hospitals");

            migrationBuilder.DropIndex(
                name: "IX_Claims_NetworkHospitalId",
                table: "Claims");

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("09aedc40-6905-4757-b98e-0d67819e2b9c"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("0a9e982f-b895-45c6-9881-95baa54d5c96"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("173d236d-88d1-46bf-ab90-41eaa9f3fbf1"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("1e476253-c632-48ed-9cf9-d0befdef904d"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("26991dd3-7628-4de9-91cc-532baad38920"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("2be333e0-3f6a-42d8-9645-8e11b49db0e1"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("2d2023eb-2763-41ee-b2b4-4ba3b18107e5"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("37e68c1e-2c63-457e-8f1a-8611466946ea"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("3905720e-37e2-444e-bf11-6d0283211f3e"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("45511029-b19d-4e14-afe4-48e150f1fb57"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("464a58c2-076b-4a8c-b3ee-210ba71cd416"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("4fa0b56a-1e81-4298-be3e-34d323aad732"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("613a6773-0f63-4a55-892f-3b0e0785f0ed"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("6196f18b-f9ea-47c0-83a9-24a640ef5370"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("6bb91c88-159b-44a4-9c09-bed7fd062d8d"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("7560de8b-d19a-4b6b-82b1-c161f9b18062"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("8ffbf434-f294-481d-bf05-b9d7f838c74c"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("909115a7-23fe-49a4-9b1d-3c23aa6efae2"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("c96f2833-1450-473d-88d5-cfd02bc421b0"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("cc42521e-610e-42f4-9252-76b17770c9d8"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("d0f9c4e3-d51e-46ab-bbdb-cdc948d57b95"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("d4a5cb0c-def5-4eaa-937e-e0647c4cf3f1"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("de88cef3-21a8-44b9-bc97-6a5268339b07"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("e001b7dc-a251-4068-a2f5-75e9514d900c"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("e39a3beb-e2ba-41d0-ada3-06e4546067d5"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("e84580d0-333f-44b6-b50c-d354d80a6a76"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("e845cc68-3257-45f1-9ccf-c9901fb2b46c"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("ef9080b3-b918-4aaa-91d5-f8691f9f6ddc"));

            migrationBuilder.DropColumn(
                name: "IsCashless",
                table: "Claims");

            migrationBuilder.DropColumn(
                name: "IsSettledWithHospital",
                table: "Claims");

            migrationBuilder.DropColumn(
                name: "NetworkHospitalId",
                table: "Claims");

            migrationBuilder.DropColumn(
                name: "SettledAt",
                table: "Claims");

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 18, 10, 9, 32, 742, DateTimeKind.Utc).AddTicks(1795));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111112"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 18, 10, 9, 32, 742, DateTimeKind.Utc).AddTicks(1814));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111113"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 18, 10, 9, 32, 742, DateTimeKind.Utc).AddTicks(1819));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222221"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 18, 10, 9, 32, 742, DateTimeKind.Utc).AddTicks(1823));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 18, 10, 9, 32, 742, DateTimeKind.Utc).AddTicks(1827));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222223"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 18, 10, 9, 32, 742, DateTimeKind.Utc).AddTicks(1839));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222224"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 18, 10, 9, 32, 742, DateTimeKind.Utc).AddTicks(1843));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333331"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 18, 10, 9, 32, 742, DateTimeKind.Utc).AddTicks(1847));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333332"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 18, 10, 9, 32, 742, DateTimeKind.Utc).AddTicks(1851));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 18, 10, 9, 32, 742, DateTimeKind.Utc).AddTicks(1854));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333334"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 18, 10, 9, 32, 742, DateTimeKind.Utc).AddTicks(1857));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333335"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 18, 10, 9, 32, 742, DateTimeKind.Utc).AddTicks(1893));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444441"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 18, 10, 9, 32, 742, DateTimeKind.Utc).AddTicks(1897));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444442"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 18, 10, 9, 32, 742, DateTimeKind.Utc).AddTicks(1904));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444443"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 18, 10, 9, 32, 742, DateTimeKind.Utc).AddTicks(1907));

            migrationBuilder.InsertData(
                table: "PlanCoverages",
                columns: new[] { "Id", "CoverageAmount", "CoveredGroup", "InsurancePlanId", "IsActive", "Type" },
                values: new object[,]
                {
                    { new Guid("04665df4-41af-4aeb-ab6c-7160dc480de6"), 300000m, 1, new Guid("11111111-1111-1111-1111-111111111112"), true, 0 },
                    { new Guid("049eb84c-0092-40f5-ada9-106f08eb7b11"), 700000m, 1, new Guid("22222222-2222-2222-2222-222222222224"), true, 0 },
                    { new Guid("0ce1f1c1-f466-43d8-b149-adf84d05d8b7"), 1500000m, 2, new Guid("33333333-3333-3333-3333-333333333335"), true, 0 },
                    { new Guid("0ecf1cb4-fa48-4509-b538-4970d4667023"), 3000000m, 0, new Guid("33333333-3333-3333-3333-333333333335"), true, 2 },
                    { new Guid("15234747-159d-4701-b482-631e5a38374d"), 1000000m, 0, new Guid("22222222-2222-2222-2222-222222222224"), true, 2 },
                    { new Guid("15cab820-a954-4e54-81ce-5d8ce55ab212"), 4000000m, 0, new Guid("44444444-4444-4444-4444-444444444443"), true, 2 },
                    { new Guid("16dde89d-9b4d-41dc-b48f-d71d2e3725b3"), 500000m, 1, new Guid("22222222-2222-2222-2222-222222222223"), true, 0 },
                    { new Guid("3b75a9d3-1b06-404f-95cc-9f6b2d322d16"), 1000000m, 1, new Guid("33333333-3333-3333-3333-333333333334"), true, 0 },
                    { new Guid("517ee7eb-3446-4f52-a32c-3e8cd3dee296"), 500000m, 0, new Guid("11111111-1111-1111-1111-111111111113"), true, 1 },
                    { new Guid("734c3ce2-2852-40ba-9a04-d66007ea14e9"), 500000m, 1, new Guid("22222222-2222-2222-2222-222222222222"), true, 0 },
                    { new Guid("7747e54d-f4c8-4c0d-8d84-ec903a48e961"), 2000000m, 0, new Guid("33333333-3333-3333-3333-333333333333"), true, 1 },
                    { new Guid("8905b2bd-fdcb-4027-9ad2-efd3f8544f4a"), 1000000m, 0, new Guid("22222222-2222-2222-2222-222222222223"), true, 1 },
                    { new Guid("9000f8e4-4560-473d-bace-1ce14f321316"), 300000m, 0, new Guid("22222222-2222-2222-2222-222222222221"), true, 0 },
                    { new Guid("9aab3169-d331-4052-98f9-4879e60703f5"), 200000m, 0, new Guid("11111111-1111-1111-1111-111111111111"), true, 0 },
                    { new Guid("9d2a8ac3-d065-4773-af80-a6dc2c860053"), 500000m, 0, new Guid("33333333-3333-3333-3333-333333333331"), true, 0 },
                    { new Guid("a221664a-5396-457e-9435-3cd6dd860128"), 300000m, 1, new Guid("11111111-1111-1111-1111-111111111113"), true, 0 },
                    { new Guid("a7118dbe-a072-4f58-820a-bd8f170da9a7"), 2000000m, 0, new Guid("33333333-3333-3333-3333-333333333334"), true, 2 },
                    { new Guid("ab42482f-47ad-41c0-95ed-cc8f571d8343"), 700000m, 1, new Guid("44444444-4444-4444-4444-444444444441"), true, 0 },
                    { new Guid("af240586-b2a4-4984-aea8-953c7fa60988"), 5000000m, 0, new Guid("44444444-4444-4444-4444-444444444443"), true, 1 },
                    { new Guid("b9776d20-397f-4631-bca0-a868e9c1bc39"), 1500000m, 0, new Guid("22222222-2222-2222-2222-222222222224"), true, 1 },
                    { new Guid("c98955c4-0fe0-4722-876e-745577a9e217"), 2000000m, 2, new Guid("44444444-4444-4444-4444-444444444443"), true, 0 },
                    { new Guid("cb877d04-f90f-4329-b7ca-08f57ab5a30f"), 3000000m, 0, new Guid("33333333-3333-3333-3333-333333333334"), true, 1 },
                    { new Guid("d58184df-7c49-4631-b9c2-fe106322c5fc"), 700000m, 1, new Guid("33333333-3333-3333-3333-333333333332"), true, 0 },
                    { new Guid("e5bdd39a-697c-452f-b38d-3bbeae137cd9"), 3000000m, 0, new Guid("44444444-4444-4444-4444-444444444442"), true, 1 },
                    { new Guid("e7840206-3add-49f2-8035-839e6e1760fe"), 4000000m, 0, new Guid("33333333-3333-3333-3333-333333333335"), true, 1 },
                    { new Guid("f7517ca2-7294-4aa1-9117-3ba5cb58a632"), 700000m, 1, new Guid("33333333-3333-3333-3333-333333333333"), true, 0 },
                    { new Guid("fd6cf5bc-8ec0-42a9-a725-41394d004e04"), 500000m, 0, new Guid("33333333-3333-3333-3333-333333333335"), true, 3 },
                    { new Guid("fdc1bfa8-a236-4acc-9c10-87bd5bed1ced"), 1000000m, 1, new Guid("44444444-4444-4444-4444-444444444442"), true, 0 }
                });
        }
    }
}
