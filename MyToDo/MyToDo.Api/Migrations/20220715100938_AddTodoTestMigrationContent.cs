using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyToDo.Api.Migrations
{
    public partial class AddTodoTestMigrationContent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TestMigrationContent",
                table: "ToDos",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TestMigrationContent",
                table: "ToDos");
        }
    }
}
