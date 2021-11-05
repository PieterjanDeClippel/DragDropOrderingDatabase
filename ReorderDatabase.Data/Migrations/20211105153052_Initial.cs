using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReorderDatabase.Data.Migrations
{
    public partial class Initial : Migration
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
                    Order = table.Column<decimal>(type: "decimal(18,2)", nullable: false, computedColumnSql: "CAST([Numerator] AS DECIMAL) / CAST([Denominator] AS DECIMAL)", stored: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notes", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Notes",
                columns: new[] { "Id", "Denominator", "Numerator", "Text" },
                values: new object[,]
                {
                    { -10, 1m, 1m, "Ronaldo" },
                    { -9, 1m, 2m, "Roberto Baggio" },
                    { -8, 1m, 3m, "Ferenc Puskás" },
                    { -7, 1m, 4m, "Marco van Basten" },
                    { -6, 1m, 5m, "Stanley Matthews" },
                    { -5, 1m, 6m, "Lev Yashin" },
                    { -4, 1m, 7m, "Alfredo Di Stéfano" },
                    { -3, 1m, 8m, "Diego Maradonna" },
                    { -2, 1m, 9m, "Bobby Moore" },
                    { -1, 1m, 10m, "Pelé" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Notes");
        }
    }
}
