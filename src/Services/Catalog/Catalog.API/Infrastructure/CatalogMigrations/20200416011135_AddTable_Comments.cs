using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Catalog.API.Infrastructure.CatalogMigrations
{
    public partial class AddTable_Comments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence(
                name: "comment_hilo",
                incrementBy: 10);

            migrationBuilder.CreateTable(
                name: "Comment",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    CatalogItemId = table.Column<int>(nullable: false),
                    Text = table.Column<string>(maxLength: 2000, nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    UserId = table.Column<string>(maxLength: 400, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comment_Catalog_CatalogItemId",
                        column: x => x.CatalogItemId,
                        principalTable: "Catalog",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comment_CatalogItemId",
                table: "Comment",
                column: "CatalogItemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comment");
        }
    }
}
