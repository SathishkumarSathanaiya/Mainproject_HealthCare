using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mainproject_HealthCare.Migrations
{
    /// <inheritdoc />
    public partial class pres : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Injection",
                table: "Prescription",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Injection",
                table: "Prescription");
        }
    }
}
