using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EGI_Backend.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AdminApproval : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RejectionReason",
                table: "CorporateClients",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RejectionReason",
                table: "CorporateClients");
        }
    }
}
