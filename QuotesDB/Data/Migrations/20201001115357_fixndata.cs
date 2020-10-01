using Microsoft.EntityFrameworkCore.Migrations;

namespace QuotesDB.Data.Migrations
{
    public partial class fixndata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TagQuotes_Tags_TagName",
                table: "TagQuotes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tags",
                table: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_TagQuotes_TagName",
                table: "TagQuotes");

            migrationBuilder.DropColumn(
                name: "TagName",
                table: "TagQuotes");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Tags",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Tags",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tags",
                table: "Tags",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_TagQuotes_TagId",
                table: "TagQuotes",
                column: "TagId");

            migrationBuilder.AddForeignKey(
                name: "FK_TagQuotes_Tags_TagId",
                table: "TagQuotes",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TagQuotes_Tags_TagId",
                table: "TagQuotes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tags",
                table: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_TagQuotes_TagId",
                table: "TagQuotes");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Tags");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Tags",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TagName",
                table: "TagQuotes",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tags",
                table: "Tags",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_TagQuotes_TagName",
                table: "TagQuotes",
                column: "TagName");

            migrationBuilder.AddForeignKey(
                name: "FK_TagQuotes_Tags_TagName",
                table: "TagQuotes",
                column: "TagName",
                principalTable: "Tags",
                principalColumn: "Name",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
