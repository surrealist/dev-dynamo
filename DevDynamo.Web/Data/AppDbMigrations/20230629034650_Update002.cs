using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DevDynamo.Web.Data.AppDbMigrations
{
    public partial class Update002 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TemplateName",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TemplateName",
                table: "Projects");
        }
    }
}
