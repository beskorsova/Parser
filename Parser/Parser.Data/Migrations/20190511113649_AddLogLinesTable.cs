using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Parser.Data.Migrations
{
    public partial class AddLogLinesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LogLineId",
                table: "QueryParameters",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "LogLines",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Host = table.Column<string>(nullable: true),
                    Route = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    StatusResult = table.Column<int>(nullable: false),
                    BytesSent = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogLines", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_QueryParameters_LogLineId",
                table: "QueryParameters",
                column: "LogLineId");

            migrationBuilder.AddForeignKey(
                name: "FK_QueryParameters_LogLines_LogLineId",
                table: "QueryParameters",
                column: "LogLineId",
                principalTable: "LogLines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QueryParameters_LogLines_LogLineId",
                table: "QueryParameters");

            migrationBuilder.DropTable(
                name: "LogLines");

            migrationBuilder.DropIndex(
                name: "IX_QueryParameters_LogLineId",
                table: "QueryParameters");

            migrationBuilder.DropColumn(
                name: "LogLineId",
                table: "QueryParameters");
        }
    }
}
