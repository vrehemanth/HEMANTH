using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EGI_Backend.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Resubmission : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsBlocked",
                table: "CorporateClients",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "ReSubmissionCount",
                table: "CorporateClients",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsBlocked",
                table: "CorporateClients");

            migrationBuilder.DropColumn(
                name: "ReSubmissionCount",
                table: "CorporateClients");
        }
    }
}
