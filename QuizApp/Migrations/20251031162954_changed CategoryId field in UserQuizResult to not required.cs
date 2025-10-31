using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuizApp.Migrations
{
    /// <inheritdoc />
    public partial class changedCategoryIdfieldinUserQuizResulttonotrequired : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserQuizResult_Categories_CategoryId",
                table: "UserQuizResult");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "UserQuizResult",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_UserQuizResult_Categories_CategoryId",
                table: "UserQuizResult",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserQuizResult_Categories_CategoryId",
                table: "UserQuizResult");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "UserQuizResult",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UserQuizResult_Categories_CategoryId",
                table: "UserQuizResult",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
