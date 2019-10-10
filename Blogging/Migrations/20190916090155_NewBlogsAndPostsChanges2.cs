using Microsoft.EntityFrameworkCore.Migrations;

namespace Blogging.Migrations
{
    public partial class NewBlogsAndPostsChanges2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Blog",
                keyColumn: "BlogId",
                keyValue: 5);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Blog",
                columns: new[] { "BlogId", "Url" },
                values: new object[] { 5, "http://test2ændringer.dk" });
        }
    }
}
