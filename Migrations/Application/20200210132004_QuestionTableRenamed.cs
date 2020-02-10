using Microsoft.EntityFrameworkCore.Migrations;

namespace Registration.Migrations.Application
{
    public partial class QuestionTableRenamed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answers_GuessWhatGoogleGame_QuestionId",
                table: "Answers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GuessWhatGoogleGame",
                table: "GuessWhatGoogleGame");

            migrationBuilder.RenameTable(
                name: "GuessWhatGoogleGame",
                newName: "Questions");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Questions",
                table: "Questions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_Questions_QuestionId",
                table: "Answers",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answers_Questions_QuestionId",
                table: "Answers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Questions",
                table: "Questions");

            migrationBuilder.RenameTable(
                name: "Questions",
                newName: "GuessWhatGoogleGame");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GuessWhatGoogleGame",
                table: "GuessWhatGoogleGame",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_GuessWhatGoogleGame_QuestionId",
                table: "Answers",
                column: "QuestionId",
                principalTable: "GuessWhatGoogleGame",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
