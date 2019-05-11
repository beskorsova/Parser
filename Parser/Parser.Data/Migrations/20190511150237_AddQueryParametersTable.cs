using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Parser.Data.Migrations
{
    public partial class AddQueryParametersTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "QueryParameters",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true),
                    LogLineId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QueryParameters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QueryParameters_LogLines_LogLineId",
                        column: x => x.LogLineId,
                        principalTable: "LogLines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_QueryParameters_LogLineId",
                table: "QueryParameters",
                column: "LogLineId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QueryParameters");
        }
    }
}
