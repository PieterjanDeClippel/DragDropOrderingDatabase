using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReorderDatabase.Data.Migrations
{
    public partial class AddNotesOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Text",
                table: "Notes",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Text",
                table: "Notes");
        }
    }
}
