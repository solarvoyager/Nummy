using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NummyApi.Migrations
{
    /// <inheritdoc />
    public partial class added_application_healthcheckurl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HealthCheckerUrl",
                table: "Applications",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsHealthy",
                table: "Applications",
                type: "boolean",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HealthCheckerUrl",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "IsHealthy",
                table: "Applications");
        }
    }
}
