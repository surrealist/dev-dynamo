using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DevDynamo.Web.Data.AppDbMigrations
{
    public partial class Update003 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkflowSteps_Projects_ProjectId",
                table: "WorkflowSteps");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProjectId",
                table: "WorkflowSteps",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkflowSteps_Projects_ProjectId",
                table: "WorkflowSteps",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkflowSteps_Projects_ProjectId",
                table: "WorkflowSteps");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProjectId",
                table: "WorkflowSteps",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkflowSteps_Projects_ProjectId",
                table: "WorkflowSteps",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id");
        }
    }
}
