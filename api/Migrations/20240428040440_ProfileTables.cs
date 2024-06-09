using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class ProfileTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1a648a58-af22-4c1f-8c90-5fa739b975ab");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6e4ada05-7de6-461d-8de9-8c07fc7e7c5b");

            migrationBuilder.CreateTable(
                name: "ProjectProfiles",
                columns: table => new
                {
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    AppUserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectProfiles", x => new { x.AppUserId, x.ProjectId });
                    table.ForeignKey(
                        name: "FK_ProjectProfiles_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectProfiles_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TodoProfiles",
                columns: table => new
                {
                    TodoId = table.Column<int>(type: "int", nullable: false),
                    AppUserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TodoProfiles", x => new { x.AppUserId, x.TodoId });
                    table.ForeignKey(
                        name: "FK_TodoProfiles_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TodoProfiles_Todos_TodoId",
                        column: x => x.TodoId,
                        principalTable: "Todos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0dc13f31-e291-4faf-907c-1b0d7dab826d", null, "Admin", "ADMIN" },
                    { "7c8ab32a-4aba-4fde-8372-6d632c6aa1e2", null, "User", "USER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectProfiles_ProjectId",
                table: "ProjectProfiles",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_TodoProfiles_TodoId",
                table: "TodoProfiles",
                column: "TodoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectProfiles");

            migrationBuilder.DropTable(
                name: "TodoProfiles");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0dc13f31-e291-4faf-907c-1b0d7dab826d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7c8ab32a-4aba-4fde-8372-6d632c6aa1e2");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1a648a58-af22-4c1f-8c90-5fa739b975ab", null, "Admin", "ADMIN" },
                    { "6e4ada05-7de6-461d-8de9-8c07fc7e7c5b", null, "User", "USER" }
                });
        }
    }
}
