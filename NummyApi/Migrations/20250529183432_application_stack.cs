using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NummyApi.Migrations
{
    /// <inheritdoc />
    public partial class application_stack : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "StackId",
                table: "Applications",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "ApplicationStacks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    IconSvg = table.Column<string>(type: "text", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationStacks", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "ApplicationStacks",
                columns: new[] { "Id", "CreatedAt", "IconSvg", "Title", "Type" },
                values: new object[,]
                {
                    { new Guid("60974335-6296-4596-9250-702824c3920f"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "<svg><!-- Flutter Icon --></svg>", "Flutter", 2 },
                    { new Guid("663e0065-ff2f-4dd6-9c9d-2e6c83595ada"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "<svg><!-- Javascript Icon --></svg>", "Javascript", 3 },
                    { new Guid("85ef383f-1a89-4882-b59e-2db52bfedfe0"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "<svg><!-- Java Icon --></svg>", "Java", 1 },
                    { new Guid("e089f7ee-7a8f-40f4-989e-316d00212d96"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "<svg><!-- CSharp Icon --></svg>", "C#", 0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Applications_StackId",
                table: "Applications",
                column: "StackId");

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_ApplicationStacks_StackId",
                table: "Applications",
                column: "StackId",
                principalTable: "ApplicationStacks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applications_ApplicationStacks_StackId",
                table: "Applications");

            migrationBuilder.DropTable(
                name: "ApplicationStacks");

            migrationBuilder.DropIndex(
                name: "IX_Applications_StackId",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "StackId",
                table: "Applications");
        }
    }
}
