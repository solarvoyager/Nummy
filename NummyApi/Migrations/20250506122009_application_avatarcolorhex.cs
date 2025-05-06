using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NummyApi.Migrations
{
    /// <inheritdoc />
    public partial class application_avatarcolorhex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AvatarColorHex",
                table: "Applications",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvatarColorHex",
                table: "Applications");
        }
    }
}
