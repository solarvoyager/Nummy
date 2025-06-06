using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NummyApi.Migrations
{
    /// <inheritdoc />
    public partial class codelog_requestlog_applicationid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ApplicationId",
                table: "RequestLogs",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ApplicationId",
                table: "CodeLogs",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_RequestLogs_ApplicationId",
                table: "RequestLogs",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_CodeLogs_ApplicationId",
                table: "CodeLogs",
                column: "ApplicationId");

            migrationBuilder.AddForeignKey(
                name: "FK_CodeLogs_Applications_ApplicationId",
                table: "CodeLogs",
                column: "ApplicationId",
                principalTable: "Applications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RequestLogs_Applications_ApplicationId",
                table: "RequestLogs",
                column: "ApplicationId",
                principalTable: "Applications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CodeLogs_Applications_ApplicationId",
                table: "CodeLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_RequestLogs_Applications_ApplicationId",
                table: "RequestLogs");

            migrationBuilder.DropIndex(
                name: "IX_RequestLogs_ApplicationId",
                table: "RequestLogs");

            migrationBuilder.DropIndex(
                name: "IX_CodeLogs_ApplicationId",
                table: "CodeLogs");

            migrationBuilder.DropColumn(
                name: "ApplicationId",
                table: "RequestLogs");

            migrationBuilder.DropColumn(
                name: "ApplicationId",
                table: "CodeLogs");
        }
    }
}
