using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReorderDatabase.Data.Migrations
{
    public partial class OrderAsPrecisionDouble : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Order",
                table: "Notes",
                type: "float",
                nullable: false,
                computedColumnSql: "CAST([Numerator] AS DOUBLE PRECISION) / CAST([Denominator] AS DOUBLE PRECISION)",
                stored: true,
                oldClrType: typeof(double),
                oldType: "float",
                oldComputedColumnSql: "[Numerator] / [Denominator]",
                oldStored: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Order",
                table: "Notes",
                type: "float",
                nullable: false,
                computedColumnSql: "[Numerator] / [Denominator]",
                stored: true,
                oldClrType: typeof(double),
                oldType: "float",
                oldComputedColumnSql: "CAST([Numerator] AS DOUBLE PRECISION) / CAST([Denominator] AS DOUBLE PRECISION)",
                oldStored: true);
        }
    }
}
