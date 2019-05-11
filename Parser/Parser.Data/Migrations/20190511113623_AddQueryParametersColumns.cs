using Microsoft.EntityFrameworkCore.Migrations;

namespace Parser.Data.Migrations
{
    public partial class AddQueryParametersColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "QueryParameters",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Value",
                table: "QueryParameters",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "QueryParameters");

            migrationBuilder.DropColumn(
                name: "Value",
                table: "QueryParameters");
        }
    }
}
