using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EGI_Backend.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddClaimsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("06b9bc4a-272d-47c5-8e17-ba57fa726d43"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("0de0267c-d44d-4930-b3cd-488aed61dbdb"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("1074e971-47b1-46f9-b179-975a42a6b548"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("2165eeba-d9cd-42f6-b5a8-6ce3f15fc608"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("25340623-6b9c-4e26-b255-30a7c15a9b77"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("27b36f44-40b5-4b0d-ae5e-ca52fecbd9bb"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("34519cac-aafe-4a13-acc6-aab49149f42e"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("3cffaf10-b54f-4167-96d7-b89eb2c97574"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("435287f1-20ed-49ce-8308-2534a7118075"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("4d8b05ad-5a82-46a2-bc6f-beaf467855ef"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("51d05842-a9fd-43eb-8c32-e6a065f51669"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("587c5763-dba8-4723-bd50-68191986ad98"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("58dde2d5-8ee8-4c99-b650-e8ca45097b5a"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("6d9a1038-e6cd-49fb-b691-a4c2f46690e5"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("7a1eca14-9757-4b55-bb58-7c54bebaeac9"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("807850a3-d457-4874-a8b9-1c56739c28cb"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("891d0990-1ec3-4208-93ae-0f0cdf04b8fc"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("8be8d0c7-2028-40fd-b889-92d6553e84a2"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("8c432b22-3f86-47c6-a4fb-73615843679a"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("8d9efe0f-2e66-4327-9838-36bf87545c00"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("ae632154-bc38-4330-888e-7e18601e3f45"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("b8257e75-5796-40a4-aab8-466a8f4aec47"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("b9002967-39e4-4c0a-8a59-78d888518bfd"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("ba235442-32d4-4675-a576-1e4c2e5a284a"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("be195344-e7f8-4324-8681-5b2afc44d4fe"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("c020f7cb-ecf4-4a48-8ff0-b7e596bff228"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("c4bdc3a7-1ee4-400a-aff7-313b7c14815c"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("ce4ae364-fbcf-4989-ba93-2cbb3068c39c"));

            migrationBuilder.CreateTable(
                name: "Claims",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PolicyAssignmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MemberId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DependentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ClaimType = table.Column<int>(type: "int", nullable: false),
                    ClaimAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ClaimReason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClaimDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    ReviewedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ReviewedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RejectionReason = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Claims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Claims_Dependents_DependentId",
                        column: x => x.DependentId,
                        principalTable: "Dependents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Claims_Members_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Claims_PolicyAssignments_PolicyAssignmentId",
                        column: x => x.PolicyAssignmentId,
                        principalTable: "PolicyAssignments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Claims_Users_ReviewedBy",
                        column: x => x.ReviewedBy,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 28, 11, 6, 29, 226, DateTimeKind.Utc).AddTicks(7553));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111112"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 28, 11, 6, 29, 226, DateTimeKind.Utc).AddTicks(7573));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111113"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 28, 11, 6, 29, 226, DateTimeKind.Utc).AddTicks(7578));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222221"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 28, 11, 6, 29, 226, DateTimeKind.Utc).AddTicks(7589));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 28, 11, 6, 29, 226, DateTimeKind.Utc).AddTicks(7594));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222223"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 28, 11, 6, 29, 226, DateTimeKind.Utc).AddTicks(7597));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222224"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 28, 11, 6, 29, 226, DateTimeKind.Utc).AddTicks(7601));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333331"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 28, 11, 6, 29, 226, DateTimeKind.Utc).AddTicks(7605));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333332"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 28, 11, 6, 29, 226, DateTimeKind.Utc).AddTicks(7610));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 28, 11, 6, 29, 226, DateTimeKind.Utc).AddTicks(7613));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333334"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 28, 11, 6, 29, 226, DateTimeKind.Utc).AddTicks(7616));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333335"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 28, 11, 6, 29, 226, DateTimeKind.Utc).AddTicks(7622));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444441"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 28, 11, 6, 29, 226, DateTimeKind.Utc).AddTicks(7626));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444442"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 28, 11, 6, 29, 226, DateTimeKind.Utc).AddTicks(7629));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444443"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 28, 11, 6, 29, 226, DateTimeKind.Utc).AddTicks(7632));

            migrationBuilder.InsertData(
                table: "PlanCoverages",
                columns: new[] { "Id", "CoverageAmount", "CoveredGroup", "InsurancePlanId", "IsActive", "Type" },
                values: new object[,]
                {
                    { new Guid("02070f6d-0835-4883-9d5c-47af2ddafd4c"), 700000m, 1, new Guid("44444444-4444-4444-4444-444444444441"), true, 0 },
                    { new Guid("073529a3-1d61-4bbc-a767-6b954868242e"), 2000000m, 2, new Guid("44444444-4444-4444-4444-444444444443"), true, 0 },
                    { new Guid("08adf795-e8e0-485d-8b9f-6f6fcbeee525"), 300000m, 1, new Guid("11111111-1111-1111-1111-111111111112"), true, 0 },
                    { new Guid("0ac88bf7-f5fc-488d-849e-cdc7c3f69529"), 500000m, 0, new Guid("33333333-3333-3333-3333-333333333335"), true, 3 },
                    { new Guid("0fa6d84d-6416-40a6-a2b3-27549d26c8d4"), 300000m, 1, new Guid("11111111-1111-1111-1111-111111111113"), true, 0 },
                    { new Guid("1093d006-dfd1-419e-a34b-fe18f3dbe91d"), 4000000m, 0, new Guid("44444444-4444-4444-4444-444444444443"), true, 2 },
                    { new Guid("11c7cde2-eda1-42dc-bfb7-f1cc9913b080"), 2000000m, 0, new Guid("33333333-3333-3333-3333-333333333333"), true, 1 },
                    { new Guid("27c0e8fa-a29f-424e-9839-b9dd1702adec"), 200000m, 0, new Guid("11111111-1111-1111-1111-111111111111"), true, 0 },
                    { new Guid("2f11c6ac-3404-44ed-8003-be4dd13c4027"), 1500000m, 2, new Guid("33333333-3333-3333-3333-333333333335"), true, 0 },
                    { new Guid("37797882-badd-42ca-ae90-0301b6e14ec2"), 5000000m, 0, new Guid("44444444-4444-4444-4444-444444444443"), true, 1 },
                    { new Guid("436caf01-d7c1-4dd0-97c9-8f8e0272683e"), 1000000m, 0, new Guid("22222222-2222-2222-2222-222222222224"), true, 2 },
                    { new Guid("49bf1796-f2c5-47af-a5fd-d15d6f86859d"), 700000m, 1, new Guid("33333333-3333-3333-3333-333333333333"), true, 0 },
                    { new Guid("531cedbe-1e54-4b75-96aa-9deb63e20ca7"), 1000000m, 0, new Guid("22222222-2222-2222-2222-222222222223"), true, 1 },
                    { new Guid("589bef96-2783-4559-a7da-808c71d141ab"), 3000000m, 0, new Guid("33333333-3333-3333-3333-333333333335"), true, 2 },
                    { new Guid("6430c515-6752-464c-bc22-7f2eb93486b1"), 3000000m, 0, new Guid("44444444-4444-4444-4444-444444444442"), true, 1 },
                    { new Guid("7b55b1b4-f17a-40f8-8988-586d7fcf600e"), 1500000m, 0, new Guid("22222222-2222-2222-2222-222222222224"), true, 1 },
                    { new Guid("7d4bb819-e9bd-436d-8f81-8eb921fc3b92"), 3000000m, 0, new Guid("33333333-3333-3333-3333-333333333334"), true, 1 },
                    { new Guid("80383b7e-8661-4636-9666-ed5b0b8d5932"), 2000000m, 0, new Guid("33333333-3333-3333-3333-333333333334"), true, 2 },
                    { new Guid("84f312dd-7e20-47a2-9bfd-c4f18b4b2f41"), 4000000m, 0, new Guid("33333333-3333-3333-3333-333333333335"), true, 1 },
                    { new Guid("8a092db9-e8fd-4615-845a-91586ee8ae98"), 500000m, 0, new Guid("33333333-3333-3333-3333-333333333331"), true, 0 },
                    { new Guid("b95e0dd6-1f06-4c46-b4b2-5b96e72648d2"), 500000m, 1, new Guid("22222222-2222-2222-2222-222222222222"), true, 0 },
                    { new Guid("baa56247-29e9-4be8-899c-9fc621430522"), 300000m, 0, new Guid("22222222-2222-2222-2222-222222222221"), true, 0 },
                    { new Guid("bb9daaff-44e0-4d17-8330-d4cd6a7b1e5d"), 1000000m, 1, new Guid("44444444-4444-4444-4444-444444444442"), true, 0 },
                    { new Guid("bc283713-9b48-46bb-b83b-b88632034af7"), 500000m, 1, new Guid("22222222-2222-2222-2222-222222222223"), true, 0 },
                    { new Guid("ca6fcd14-54b5-4f6c-af1f-4a3756d1bd34"), 1000000m, 1, new Guid("33333333-3333-3333-3333-333333333334"), true, 0 },
                    { new Guid("dda814b5-cb3a-44f1-a580-ed3ac665ed0b"), 700000m, 1, new Guid("33333333-3333-3333-3333-333333333332"), true, 0 },
                    { new Guid("f06dc1cc-6e9f-4038-a217-fc42342a4979"), 500000m, 0, new Guid("11111111-1111-1111-1111-111111111113"), true, 1 },
                    { new Guid("fa7a4762-4a38-4885-b280-2618d538f43f"), 700000m, 1, new Guid("22222222-2222-2222-2222-222222222224"), true, 0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Claims_DependentId",
                table: "Claims",
                column: "DependentId");

            migrationBuilder.CreateIndex(
                name: "IX_Claims_MemberId",
                table: "Claims",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_Claims_PolicyAssignmentId",
                table: "Claims",
                column: "PolicyAssignmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Claims_ReviewedBy",
                table: "Claims",
                column: "ReviewedBy");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Claims");

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("02070f6d-0835-4883-9d5c-47af2ddafd4c"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("073529a3-1d61-4bbc-a767-6b954868242e"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("08adf795-e8e0-485d-8b9f-6f6fcbeee525"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("0ac88bf7-f5fc-488d-849e-cdc7c3f69529"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("0fa6d84d-6416-40a6-a2b3-27549d26c8d4"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("1093d006-dfd1-419e-a34b-fe18f3dbe91d"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("11c7cde2-eda1-42dc-bfb7-f1cc9913b080"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("27c0e8fa-a29f-424e-9839-b9dd1702adec"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("2f11c6ac-3404-44ed-8003-be4dd13c4027"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("37797882-badd-42ca-ae90-0301b6e14ec2"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("436caf01-d7c1-4dd0-97c9-8f8e0272683e"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("49bf1796-f2c5-47af-a5fd-d15d6f86859d"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("531cedbe-1e54-4b75-96aa-9deb63e20ca7"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("589bef96-2783-4559-a7da-808c71d141ab"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("6430c515-6752-464c-bc22-7f2eb93486b1"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("7b55b1b4-f17a-40f8-8988-586d7fcf600e"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("7d4bb819-e9bd-436d-8f81-8eb921fc3b92"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("80383b7e-8661-4636-9666-ed5b0b8d5932"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("84f312dd-7e20-47a2-9bfd-c4f18b4b2f41"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("8a092db9-e8fd-4615-845a-91586ee8ae98"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("b95e0dd6-1f06-4c46-b4b2-5b96e72648d2"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("baa56247-29e9-4be8-899c-9fc621430522"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("bb9daaff-44e0-4d17-8330-d4cd6a7b1e5d"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("bc283713-9b48-46bb-b83b-b88632034af7"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("ca6fcd14-54b5-4f6c-af1f-4a3756d1bd34"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("dda814b5-cb3a-44f1-a580-ed3ac665ed0b"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("f06dc1cc-6e9f-4038-a217-fc42342a4979"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("fa7a4762-4a38-4885-b280-2618d538f43f"));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 28, 9, 57, 0, 33, DateTimeKind.Utc).AddTicks(4060));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111112"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 28, 9, 57, 0, 33, DateTimeKind.Utc).AddTicks(4077));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111113"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 28, 9, 57, 0, 33, DateTimeKind.Utc).AddTicks(4081));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222221"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 28, 9, 57, 0, 33, DateTimeKind.Utc).AddTicks(4084));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 28, 9, 57, 0, 33, DateTimeKind.Utc).AddTicks(4087));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222223"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 28, 9, 57, 0, 33, DateTimeKind.Utc).AddTicks(4090));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222224"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 28, 9, 57, 0, 33, DateTimeKind.Utc).AddTicks(4092));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333331"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 28, 9, 57, 0, 33, DateTimeKind.Utc).AddTicks(4095));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333332"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 28, 9, 57, 0, 33, DateTimeKind.Utc).AddTicks(4100));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 28, 9, 57, 0, 33, DateTimeKind.Utc).AddTicks(4102));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333334"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 28, 9, 57, 0, 33, DateTimeKind.Utc).AddTicks(4105));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333335"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 28, 9, 57, 0, 33, DateTimeKind.Utc).AddTicks(4108));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444441"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 28, 9, 57, 0, 33, DateTimeKind.Utc).AddTicks(4110));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444442"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 28, 9, 57, 0, 33, DateTimeKind.Utc).AddTicks(4113));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444443"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 28, 9, 57, 0, 33, DateTimeKind.Utc).AddTicks(4116));

            migrationBuilder.InsertData(
                table: "PlanCoverages",
                columns: new[] { "Id", "CoverageAmount", "CoveredGroup", "InsurancePlanId", "IsActive", "Type" },
                values: new object[,]
                {
                    { new Guid("06b9bc4a-272d-47c5-8e17-ba57fa726d43"), 500000m, 1, new Guid("22222222-2222-2222-2222-222222222222"), true, 0 },
                    { new Guid("0de0267c-d44d-4930-b3cd-488aed61dbdb"), 700000m, 1, new Guid("44444444-4444-4444-4444-444444444441"), true, 0 },
                    { new Guid("1074e971-47b1-46f9-b179-975a42a6b548"), 4000000m, 0, new Guid("33333333-3333-3333-3333-333333333335"), true, 1 },
                    { new Guid("2165eeba-d9cd-42f6-b5a8-6ce3f15fc608"), 300000m, 1, new Guid("11111111-1111-1111-1111-111111111112"), true, 0 },
                    { new Guid("25340623-6b9c-4e26-b255-30a7c15a9b77"), 2000000m, 0, new Guid("33333333-3333-3333-3333-333333333334"), true, 2 },
                    { new Guid("27b36f44-40b5-4b0d-ae5e-ca52fecbd9bb"), 1500000m, 2, new Guid("33333333-3333-3333-3333-333333333335"), true, 0 },
                    { new Guid("34519cac-aafe-4a13-acc6-aab49149f42e"), 4000000m, 0, new Guid("44444444-4444-4444-4444-444444444443"), true, 2 },
                    { new Guid("3cffaf10-b54f-4167-96d7-b89eb2c97574"), 200000m, 0, new Guid("11111111-1111-1111-1111-111111111111"), true, 0 },
                    { new Guid("435287f1-20ed-49ce-8308-2534a7118075"), 3000000m, 0, new Guid("33333333-3333-3333-3333-333333333334"), true, 1 },
                    { new Guid("4d8b05ad-5a82-46a2-bc6f-beaf467855ef"), 700000m, 1, new Guid("22222222-2222-2222-2222-222222222224"), true, 0 },
                    { new Guid("51d05842-a9fd-43eb-8c32-e6a065f51669"), 500000m, 0, new Guid("33333333-3333-3333-3333-333333333331"), true, 0 },
                    { new Guid("587c5763-dba8-4723-bd50-68191986ad98"), 500000m, 0, new Guid("33333333-3333-3333-3333-333333333335"), true, 3 },
                    { new Guid("58dde2d5-8ee8-4c99-b650-e8ca45097b5a"), 1000000m, 0, new Guid("22222222-2222-2222-2222-222222222223"), true, 1 },
                    { new Guid("6d9a1038-e6cd-49fb-b691-a4c2f46690e5"), 700000m, 1, new Guid("33333333-3333-3333-3333-333333333333"), true, 0 },
                    { new Guid("7a1eca14-9757-4b55-bb58-7c54bebaeac9"), 2000000m, 0, new Guid("33333333-3333-3333-3333-333333333333"), true, 1 },
                    { new Guid("807850a3-d457-4874-a8b9-1c56739c28cb"), 500000m, 0, new Guid("11111111-1111-1111-1111-111111111113"), true, 1 },
                    { new Guid("891d0990-1ec3-4208-93ae-0f0cdf04b8fc"), 1000000m, 1, new Guid("44444444-4444-4444-4444-444444444442"), true, 0 },
                    { new Guid("8be8d0c7-2028-40fd-b889-92d6553e84a2"), 1000000m, 1, new Guid("33333333-3333-3333-3333-333333333334"), true, 0 },
                    { new Guid("8c432b22-3f86-47c6-a4fb-73615843679a"), 1500000m, 0, new Guid("22222222-2222-2222-2222-222222222224"), true, 1 },
                    { new Guid("8d9efe0f-2e66-4327-9838-36bf87545c00"), 3000000m, 0, new Guid("33333333-3333-3333-3333-333333333335"), true, 2 },
                    { new Guid("ae632154-bc38-4330-888e-7e18601e3f45"), 1000000m, 0, new Guid("22222222-2222-2222-2222-222222222224"), true, 2 },
                    { new Guid("b8257e75-5796-40a4-aab8-466a8f4aec47"), 3000000m, 0, new Guid("44444444-4444-4444-4444-444444444442"), true, 1 },
                    { new Guid("b9002967-39e4-4c0a-8a59-78d888518bfd"), 300000m, 0, new Guid("22222222-2222-2222-2222-222222222221"), true, 0 },
                    { new Guid("ba235442-32d4-4675-a576-1e4c2e5a284a"), 2000000m, 2, new Guid("44444444-4444-4444-4444-444444444443"), true, 0 },
                    { new Guid("be195344-e7f8-4324-8681-5b2afc44d4fe"), 300000m, 1, new Guid("11111111-1111-1111-1111-111111111113"), true, 0 },
                    { new Guid("c020f7cb-ecf4-4a48-8ff0-b7e596bff228"), 5000000m, 0, new Guid("44444444-4444-4444-4444-444444444443"), true, 1 },
                    { new Guid("c4bdc3a7-1ee4-400a-aff7-313b7c14815c"), 500000m, 1, new Guid("22222222-2222-2222-2222-222222222223"), true, 0 },
                    { new Guid("ce4ae364-fbcf-4989-ba93-2cbb3068c39c"), 700000m, 1, new Guid("33333333-3333-3333-3333-333333333332"), true, 0 }
                });
        }
    }
}
