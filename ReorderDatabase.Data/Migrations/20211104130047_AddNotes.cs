using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReorderDatabase.Data.Migrations
{
    public partial class AddNotes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Notes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Numerator = table.Column<int>(type: "int", nullable: false),
                    Denominator = table.Column<int>(type: "int", nullable: false),
                    Order = table.Column<double>(type: "float", nullable: false, computedColumnSql: "[Numerator] / [Denominator]", stored: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notes", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Notes");
        }
    }
}
