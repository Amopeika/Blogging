using Microsoft.EntityFrameworkCore.Migrations;

namespace Blogging.Migrations
{
    public partial class NameChangeFK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Post_Blog_BlogId",
                table: "Post");

            migrationBuilder.RenameColumn(
                name: "BlogId",
                table: "Post",
                newName: "FK_BlogId");

            migrationBuilder.RenameIndex(
                name: "IX_Post_BlogId",
                table: "Post",
                newName: "IX_Post_FK_BlogId");

            migrationBuilder.AddForeignKey(
                name: "ForeignKey_Post_Blog",
                table: "Post",
                column: "FK_BlogId",
                principalTable: "Blog",
                principalColumn: "BlogId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "ForeignKey_Post_Blog",
                table: "Post");

            migrationBuilder.RenameColumn(
                name: "FK_BlogId",
                table: "Post",
                newName: "BlogId");

            migrationBuilder.RenameIndex(
                name: "IX_Post_FK_BlogId",
                table: "Post",
                newName: "IX_Post_BlogId");

            migrationBuilder.AddForeignKey(
                name: "FK_Post_Blog_BlogId",
                table: "Post",
                column: "BlogId",
                principalTable: "Blog",
                principalColumn: "BlogId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
