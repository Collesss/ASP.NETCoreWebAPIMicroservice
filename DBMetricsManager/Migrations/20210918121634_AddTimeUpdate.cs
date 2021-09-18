using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DBMetricsManager.Migrations
{
    public partial class AddTimeUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "AddressAgent",
                table: "MetricAgents",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdateTime",
                table: "MetricAgents",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddUniqueConstraint(
                name: "AK_MetricAgents_AddressAgent",
                table: "MetricAgents",
                column: "AddressAgent");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_MetricAgents_AddressAgent",
                table: "MetricAgents");

            migrationBuilder.DropColumn(
                name: "LastUpdateTime",
                table: "MetricAgents");

            migrationBuilder.AlterColumn<string>(
                name: "AddressAgent",
                table: "MetricAgents",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");
        }
    }
}
