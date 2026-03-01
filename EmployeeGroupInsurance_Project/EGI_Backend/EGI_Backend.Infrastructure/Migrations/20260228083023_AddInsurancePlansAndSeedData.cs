using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EGI_Backend.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddInsurancePlansAndSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InsurancePlans",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlanCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PlanName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BasePremium = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InsurancePlans", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "InsurancePlans",
                columns: new[] { "Id", "BasePremium", "CreatedAt", "Description", "PlanCode", "PlanName", "Status" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), 2000m, new DateTime(2026, 2, 28, 8, 30, 21, 676, DateTimeKind.Utc).AddTicks(4596), "Health: ₹2L | Covers: Employee Only | No Life | No Accident", "S-Level 1", "Health Basic", true },
                    { new Guid("11111111-1111-1111-1111-111111111112"), 4000m, new DateTime(2026, 2, 28, 8, 30, 21, 676, DateTimeKind.Utc).AddTicks(4616), "Health: ₹3L | Covers: Employee + Spouse + 2 Children | No Life | No Accident", "S-Level 2", "Health Family", true },
                    { new Guid("11111111-1111-1111-1111-111111111113"), 6000m, new DateTime(2026, 2, 28, 8, 30, 21, 676, DateTimeKind.Utc).AddTicks(4620), "Health: ₹3L (Family) | Life: ₹5L | No Accident | Parents Not Included", "S-Level 3", "Health + Life", true },
                    { new Guid("22222222-2222-2222-2222-222222222221"), 2500m, new DateTime(2026, 2, 28, 8, 30, 21, 676, DateTimeKind.Utc).AddTicks(4745), "Health: ₹3L | Covers: Employee Only", "M-Level 1", "Health Basic", true },
                    { new Guid("22222222-2222-2222-2222-222222222222"), 5000m, new DateTime(2026, 2, 28, 8, 30, 21, 676, DateTimeKind.Utc).AddTicks(4749), "Health: ₹5L | Covers: Employee + Spouse + 2 Children", "M-Level 2", "Health Family", true },
                    { new Guid("22222222-2222-2222-2222-222222222223"), 8000m, new DateTime(2026, 2, 28, 8, 30, 21, 676, DateTimeKind.Utc).AddTicks(4753), "Health: ₹5L (Family) | Life: ₹10L | Parents Not Included", "M-Level 3", "Health + Life", true },
                    { new Guid("22222222-2222-2222-2222-222222222224"), 10000m, new DateTime(2026, 2, 28, 8, 30, 21, 676, DateTimeKind.Utc).AddTicks(4768), "Health: ₹7L (Family) | Life: ₹15L | Accident: ₹10L | Parents Optional Add-on", "M-Level 4", "Health + Life + Accident", true },
                    { new Guid("33333333-3333-3333-3333-333333333331"), 4000m, new DateTime(2026, 2, 28, 8, 30, 21, 676, DateTimeKind.Utc).AddTicks(4771), "Health: ₹5L | Covers: Employee Only", "L-Level 1", "Health Basic", true },
                    { new Guid("33333333-3333-3333-3333-333333333332"), 7000m, new DateTime(2026, 2, 28, 8, 30, 21, 676, DateTimeKind.Utc).AddTicks(4776), "Health: ₹7L | Covers: Employee + Spouse + 2 Children", "L-Level 2", "Health Family", true },
                    { new Guid("33333333-3333-3333-3333-333333333333"), 12000m, new DateTime(2026, 2, 28, 8, 30, 21, 676, DateTimeKind.Utc).AddTicks(4779), "Health: ₹7L (Family) | Life: ₹20L", "L-Level 3", "Health + Life", true },
                    { new Guid("33333333-3333-3333-3333-333333333334"), 16000m, new DateTime(2026, 2, 28, 8, 30, 21, 676, DateTimeKind.Utc).AddTicks(4783), "Health: ₹10L (Family) | Life: ₹30L | Accident: ₹20L", "L-Level 4", "Health + Life + Accident", true },
                    { new Guid("33333333-3333-3333-3333-333333333335"), 25000m, new DateTime(2026, 2, 28, 8, 30, 21, 676, DateTimeKind.Utc).AddTicks(4786), "Health: ₹15L (Employee + Spouse + 2 Children + Parents) | Life: ₹40L | Accident: ₹30L | Critical Illness Included", "L-Level 5", "Comprehensive Plus", true },
                    { new Guid("44444444-4444-4444-4444-444444444441"), 6000m, new DateTime(2026, 2, 28, 8, 30, 21, 676, DateTimeKind.Utc).AddTicks(4790), "Health: ₹7L | Covers: Employee + Spouse + 2 Children", "E-Level 1", "Health Family", true },
                    { new Guid("44444444-4444-4444-4444-444444444442"), 14000m, new DateTime(2026, 2, 28, 8, 30, 21, 676, DateTimeKind.Utc).AddTicks(4793), "Health: ₹10L (Employee + Spouse + 2 Children) | Life: ₹30L", "E-Level 2", "Health + Life", true },
                    { new Guid("44444444-4444-4444-4444-444444444443"), 30000m, new DateTime(2026, 2, 28, 8, 30, 21, 676, DateTimeKind.Utc).AddTicks(4802), "Health: ₹20L (Employee + Spouse + 2 Children + Parents) | Life: ₹50L | Accident: ₹40L | Global Coverage Option | Dedicated Claim Officer", "E-Level 3", "Full Corporate Shield", true }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InsurancePlans");
        }
    }
}
