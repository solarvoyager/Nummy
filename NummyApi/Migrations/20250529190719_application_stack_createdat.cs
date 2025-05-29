using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NummyApi.Migrations
{
    /// <inheritdoc />
    public partial class application_stack_createdat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ApplicationStacks",
                keyColumn: "Id",
                keyValue: new Guid("60974335-6296-4596-9250-702824c3920f"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2025, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 4, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "ApplicationStacks",
                keyColumn: "Id",
                keyValue: new Guid("663e0065-ff2f-4dd6-9c9d-2e6c83595ada"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2025, 1, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 4, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "ApplicationStacks",
                keyColumn: "Id",
                keyValue: new Guid("85ef383f-1a89-4882-b59e-2db52bfedfe0"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2025, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 4, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "ApplicationStacks",
                keyColumn: "Id",
                keyValue: new Guid("e089f7ee-7a8f-40f4-989e-316d00212d96"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 4, 0, 0, 0)));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ApplicationStacks",
                keyColumn: "Id",
                keyValue: new Guid("60974335-6296-4596-9250-702824c3920f"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "ApplicationStacks",
                keyColumn: "Id",
                keyValue: new Guid("663e0065-ff2f-4dd6-9c9d-2e6c83595ada"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "ApplicationStacks",
                keyColumn: "Id",
                keyValue: new Guid("85ef383f-1a89-4882-b59e-2db52bfedfe0"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "ApplicationStacks",
                keyColumn: "Id",
                keyValue: new Guid("e089f7ee-7a8f-40f4-989e-316d00212d96"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));
        }
    }
}
