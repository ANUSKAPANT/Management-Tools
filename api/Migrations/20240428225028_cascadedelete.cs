using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class cascadedelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectProfiles_Projects_ProjectId",
                table: "ProjectProfiles");

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
                    { "066d266f-ac2a-4a19-8fe6-80b566823f2f", null, "User", "USER" },
                    { "25231a0b-a092-4167-862d-907d61ae7284", null, "Admin", "ADMIN" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectProfiles_Projects_ProjectId",
                table: "ProjectProfiles",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectProfiles_Projects_ProjectId",
                table: "ProjectProfiles");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "066d266f-ac2a-4a19-8fe6-80b566823f2f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "25231a0b-a092-4167-862d-907d61ae7284");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0dc13f31-e291-4faf-907c-1b0d7dab826d", null, "Admin", "ADMIN" },
                    { "7c8ab32a-4aba-4fde-8372-6d632c6aa1e2", null, "User", "USER" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectProfiles_Projects_ProjectId",
                table: "ProjectProfiles",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
