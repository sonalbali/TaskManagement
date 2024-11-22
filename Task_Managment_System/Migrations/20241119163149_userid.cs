using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Task_Managment_System.Migrations
{
    /// <inheritdoc />
    public partial class userid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TaskItemTask_Id",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_TaskItemTask_Id",
                table: "Users",
                column: "TaskItemTask_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_TaskItems_TaskItemTask_Id",
                table: "Users",
                column: "TaskItemTask_Id",
                principalTable: "TaskItems",
                principalColumn: "Task_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_TaskItems_TaskItemTask_Id",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_TaskItemTask_Id",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "TaskItemTask_Id",
                table: "Users");
        }
    }
}
