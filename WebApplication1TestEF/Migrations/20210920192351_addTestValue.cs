using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication1TestEF.Migrations
{
    public partial class addTestValue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TestValue",
                table: "MetricAgents",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TestValue",
                table: "MetricAgents");
        }
    }
}
