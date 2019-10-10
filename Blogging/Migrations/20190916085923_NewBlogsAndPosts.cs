using Microsoft.EntityFrameworkCore.Migrations;

namespace Blogging.Migrations
{
    public partial class NewBlogsAndPosts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Blog",
                columns: new[] { "BlogId", "Url" },
                values: new object[,]
                {
                    { 4, "http://test1.dk" },
                    { 5, "http://test2.dk" },
                    { 6, "http://test3.dk" },
                    { 7, "http://test4.dk" }
                });

            migrationBuilder.InsertData(
                table: "Post",
                columns: new[] { "PostId", "Content", "FK_BlogId", "Title" },
                values: new object[] { 7, "Post Til Data Seeding Test", 4, "Test 1 Post" });

            migrationBuilder.InsertData(
                table: "Post",
                columns: new[] { "PostId", "Content", "FK_BlogId", "Title" },
                values: new object[] { 8, "Post Til Data Seeding Test", 5, "Test 2 Post" });

            migrationBuilder.InsertData(
                table: "Post",
                columns: new[] { "PostId", "Content", "FK_BlogId", "Title" },
                values: new object[] { 9, "Post Til Data Seeding Test", 6, "Test 3 Post" });

            migrationBuilder.InsertData(
                table: "Post",
                columns: new[] { "PostId", "Content", "FK_BlogId", "Title" },
                values: new object[] { 10, "Post Til Data Seeding Test", 7, "Test 4 Post" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Post",
                keyColumn: "PostId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Post",
                keyColumn: "PostId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Post",
                keyColumn: "PostId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Post",
                keyColumn: "PostId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Blog",
                keyColumn: "BlogId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Blog",
                keyColumn: "BlogId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Blog",
                keyColumn: "BlogId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Blog",
                keyColumn: "BlogId",
                keyValue: 7);
        }
    }
}
