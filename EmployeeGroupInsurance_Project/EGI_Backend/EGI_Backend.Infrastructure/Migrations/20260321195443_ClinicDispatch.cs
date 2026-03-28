using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EGI_Backend.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ClinicDispatch : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("05f827fe-b0b0-4b21-b972-30b8fb758928"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("15120f86-d95f-41fb-8d6b-eb0109a938da"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("2abe3a0b-35d3-4f63-a157-b9177c98f18a"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("39e6274c-3d7f-448a-88c2-e7ca6c467a30"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("42d88169-c231-4f4e-b377-83ec2d31238a"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("43c82cb7-38e0-4601-b10e-124aa811571f"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("44b90e13-2eb5-4171-a5ed-5708db54e6ed"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("4eefff3d-2a2c-483d-ac97-4bab92c12b3d"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("58ad9da2-3bb1-4041-a232-6469460b798d"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("646949c2-de42-4ea0-9e2c-e9d190142f62"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("6abf7dcd-faa7-4f47-be0b-69b6926a3a2c"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("6cfade22-1a0f-4a88-8a94-fbc7aed8e472"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("755c706a-1d3c-45a5-98ce-cb308d5c0133"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("7eb06be3-90a3-4c1b-a604-823924edcba9"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("8ae9c135-09cb-4abf-a425-7ed2aa11deac"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("9456eaa3-2883-4bcb-9931-ff1127dd6a2b"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("9cb0e19e-b32c-4190-858b-085d51a21aa8"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("9d19578e-a3e0-49be-a228-269b2297afa4"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("a1d2d06d-95c0-4a71-b44d-1167ac40db3b"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("a344e9c2-eb02-4f01-a969-50b84e1b03df"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("b6258b62-ef06-46c1-b021-90205cba8842"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("c38f3564-b1e6-4c1e-aa97-3813f09cc2ec"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("d30ba428-cf87-4b54-9bc6-4bb683ec1c8e"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("d5fcff21-038f-4038-9687-c99514dde3f9"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("e03e44e7-3abf-4a60-a780-56ec9c6ecabd"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("e3372303-1a97-4b31-9264-00b42ecd6034"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("e3d80a51-39a8-4d5c-916f-5aaa0c7c0c82"));

            migrationBuilder.DeleteData(
                table: "PlanCoverages",
                keyColumn: "Id",
                keyValue: new Guid("fa61b891-1a33-4cae-89ed-cd183c2649bb"));

            migrationBuilder.CreateTable(
                name: "ClinicalDispatches",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HospitalId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MemberId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DependentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PatientName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmployeeCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PolicyNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CoverageSummaryJson = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DispatchDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsClosed = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClinicalDispatches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClinicalDispatches_Dependents_DependentId",
                        column: x => x.DependentId,
                        principalTable: "Dependents",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ClinicalDispatches_Hospitals_HospitalId",
                        column: x => x.HospitalId,
                        principalTable: "Hospitals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClinicalDispatches_Members_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_ClinicalDispatches_DependentId",
                table: "ClinicalDispatches",
                column: "DependentId");

            migrationBuilder.CreateIndex(
                name: "IX_ClinicalDispatches_HospitalId",
                table: "ClinicalDispatches",
                column: "HospitalId");

            migrationBuilder.CreateIndex(
                name: "IX_ClinicalDispatches_MemberId",
                table: "ClinicalDispatches",
                column: "MemberId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClinicalDispatches");

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

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 20, 4, 33, 44, 649, DateTimeKind.Utc).AddTicks(3223));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111112"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 20, 4, 33, 44, 649, DateTimeKind.Utc).AddTicks(3257));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111113"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 20, 4, 33, 44, 649, DateTimeKind.Utc).AddTicks(3263));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222221"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 20, 4, 33, 44, 649, DateTimeKind.Utc).AddTicks(3267));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 20, 4, 33, 44, 649, DateTimeKind.Utc).AddTicks(3271));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222223"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 20, 4, 33, 44, 649, DateTimeKind.Utc).AddTicks(3287));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222224"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 20, 4, 33, 44, 649, DateTimeKind.Utc).AddTicks(3291));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333331"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 20, 4, 33, 44, 649, DateTimeKind.Utc).AddTicks(3295));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333332"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 20, 4, 33, 44, 649, DateTimeKind.Utc).AddTicks(3299));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 20, 4, 33, 44, 649, DateTimeKind.Utc).AddTicks(3303));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333334"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 20, 4, 33, 44, 649, DateTimeKind.Utc).AddTicks(3306));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333335"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 20, 4, 33, 44, 649, DateTimeKind.Utc).AddTicks(3309));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444441"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 20, 4, 33, 44, 649, DateTimeKind.Utc).AddTicks(3312));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444442"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 20, 4, 33, 44, 649, DateTimeKind.Utc).AddTicks(3318));

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444443"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 20, 4, 33, 44, 649, DateTimeKind.Utc).AddTicks(3321));

            migrationBuilder.InsertData(
                table: "PlanCoverages",
                columns: new[] { "Id", "CoverageAmount", "CoveredGroup", "InsurancePlanId", "IsActive", "Type" },
                values: new object[,]
                {
                    { new Guid("05f827fe-b0b0-4b21-b972-30b8fb758928"), 500000m, 1, new Guid("22222222-2222-2222-2222-222222222222"), true, 0 },
                    { new Guid("15120f86-d95f-41fb-8d6b-eb0109a938da"), 3000000m, 0, new Guid("33333333-3333-3333-3333-333333333334"), true, 1 },
                    { new Guid("2abe3a0b-35d3-4f63-a157-b9177c98f18a"), 500000m, 0, new Guid("33333333-3333-3333-3333-333333333335"), true, 3 },
                    { new Guid("39e6274c-3d7f-448a-88c2-e7ca6c467a30"), 200000m, 0, new Guid("11111111-1111-1111-1111-111111111111"), true, 0 },
                    { new Guid("42d88169-c231-4f4e-b377-83ec2d31238a"), 4000000m, 0, new Guid("44444444-4444-4444-4444-444444444443"), true, 2 },
                    { new Guid("43c82cb7-38e0-4601-b10e-124aa811571f"), 500000m, 1, new Guid("22222222-2222-2222-2222-222222222223"), true, 0 },
                    { new Guid("44b90e13-2eb5-4171-a5ed-5708db54e6ed"), 700000m, 1, new Guid("22222222-2222-2222-2222-222222222224"), true, 0 },
                    { new Guid("4eefff3d-2a2c-483d-ac97-4bab92c12b3d"), 2000000m, 0, new Guid("33333333-3333-3333-3333-333333333334"), true, 2 },
                    { new Guid("58ad9da2-3bb1-4041-a232-6469460b798d"), 1000000m, 0, new Guid("22222222-2222-2222-2222-222222222223"), true, 1 },
                    { new Guid("646949c2-de42-4ea0-9e2c-e9d190142f62"), 5000000m, 0, new Guid("44444444-4444-4444-4444-444444444443"), true, 1 },
                    { new Guid("6abf7dcd-faa7-4f47-be0b-69b6926a3a2c"), 4000000m, 0, new Guid("33333333-3333-3333-3333-333333333335"), true, 1 },
                    { new Guid("6cfade22-1a0f-4a88-8a94-fbc7aed8e472"), 500000m, 0, new Guid("11111111-1111-1111-1111-111111111113"), true, 1 },
                    { new Guid("755c706a-1d3c-45a5-98ce-cb308d5c0133"), 300000m, 1, new Guid("11111111-1111-1111-1111-111111111112"), true, 0 },
                    { new Guid("7eb06be3-90a3-4c1b-a604-823924edcba9"), 3000000m, 0, new Guid("33333333-3333-3333-3333-333333333335"), true, 2 },
                    { new Guid("8ae9c135-09cb-4abf-a425-7ed2aa11deac"), 300000m, 0, new Guid("22222222-2222-2222-2222-222222222221"), true, 0 },
                    { new Guid("9456eaa3-2883-4bcb-9931-ff1127dd6a2b"), 1500000m, 2, new Guid("33333333-3333-3333-3333-333333333335"), true, 0 },
                    { new Guid("9cb0e19e-b32c-4190-858b-085d51a21aa8"), 700000m, 1, new Guid("33333333-3333-3333-3333-333333333332"), true, 0 },
                    { new Guid("9d19578e-a3e0-49be-a228-269b2297afa4"), 300000m, 1, new Guid("11111111-1111-1111-1111-111111111113"), true, 0 },
                    { new Guid("a1d2d06d-95c0-4a71-b44d-1167ac40db3b"), 1000000m, 0, new Guid("22222222-2222-2222-2222-222222222224"), true, 2 },
                    { new Guid("a344e9c2-eb02-4f01-a969-50b84e1b03df"), 700000m, 1, new Guid("33333333-3333-3333-3333-333333333333"), true, 0 },
                    { new Guid("b6258b62-ef06-46c1-b021-90205cba8842"), 3000000m, 0, new Guid("44444444-4444-4444-4444-444444444442"), true, 1 },
                    { new Guid("c38f3564-b1e6-4c1e-aa97-3813f09cc2ec"), 2000000m, 2, new Guid("44444444-4444-4444-4444-444444444443"), true, 0 },
                    { new Guid("d30ba428-cf87-4b54-9bc6-4bb683ec1c8e"), 1500000m, 0, new Guid("22222222-2222-2222-2222-222222222224"), true, 1 },
                    { new Guid("d5fcff21-038f-4038-9687-c99514dde3f9"), 1000000m, 1, new Guid("33333333-3333-3333-3333-333333333334"), true, 0 },
                    { new Guid("e03e44e7-3abf-4a60-a780-56ec9c6ecabd"), 1000000m, 1, new Guid("44444444-4444-4444-4444-444444444442"), true, 0 },
                    { new Guid("e3372303-1a97-4b31-9264-00b42ecd6034"), 700000m, 1, new Guid("44444444-4444-4444-4444-444444444441"), true, 0 },
                    { new Guid("e3d80a51-39a8-4d5c-916f-5aaa0c7c0c82"), 500000m, 0, new Guid("33333333-3333-3333-3333-333333333331"), true, 0 },
                    { new Guid("fa61b891-1a33-4cae-89ed-cd183c2649bb"), 2000000m, 0, new Guid("33333333-3333-3333-3333-333333333333"), true, 1 }
                });
        }
    }
}
