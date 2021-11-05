using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReorderDatabase.Data.Migrations
{
    public partial class SwitchToUlong : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Notes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Numerator = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    Denominator = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    Order = table.Column<double>(type: "float", nullable: false, computedColumnSql: "CAST([Numerator] AS DOUBLE PRECISION) / CAST([Denominator] AS DOUBLE PRECISION)", stored: true)
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
