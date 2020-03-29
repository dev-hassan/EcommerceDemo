using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Uplift.DataAccess.Migrations
{
    public partial class FixPictureProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Pricture",
                table: "WebImages");

            migrationBuilder.AddColumn<byte[]>(
                name: "Picture",
                table: "WebImages",
                nullable: false,
                defaultValue: new byte[] {  });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Picture",
                table: "WebImages");

            migrationBuilder.AddColumn<byte[]>(
                name: "Pricture",
                table: "WebImages",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[] {  });
        }
    }
}
