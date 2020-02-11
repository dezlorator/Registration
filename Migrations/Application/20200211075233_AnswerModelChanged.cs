using Microsoft.EntityFrameworkCore.Migrations;

namespace Registration.Migrations.Application
{
    public partial class AnswerModelChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsRight",
                table: "Answers");

            migrationBuilder.AddColumn<double>(
                name: "TimeSpend",
                table: "Questions",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeSpend",
                table: "Questions");

            migrationBuilder.AddColumn<bool>(
                name: "IsRight",
                table: "Answers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
