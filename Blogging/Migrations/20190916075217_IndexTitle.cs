using Microsoft.EntityFrameworkCore.Migrations;

namespace Blogging.Migrations
{
    public partial class IndexTitle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Post",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "Index_Title",
                table: "Post",
                column: "Title");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "Index_Title",
                table: "Post");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Post",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
