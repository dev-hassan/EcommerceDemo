using Microsoft.EntityFrameworkCore.Migrations;

namespace Uplift.DataAccess.Migrations
{
    public partial class AddCategoryValidationMigration2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DisplayedOrder",
                table: "Category");

            migrationBuilder.AddColumn<int>(
                name: "DisplayOrder",
                table: "Category",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DisplayOrder",
                table: "Category");

            migrationBuilder.AddColumn<int>(
                name: "DisplayedOrder",
                table: "Category",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
