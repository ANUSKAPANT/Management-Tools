using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class cascadedelete1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TodoProfiles_Todos_TodoId",
                table: "TodoProfiles");

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
                    { "5a1add3c-0cc6-444a-bb01-977a403bfbd1", null, "Admin", "ADMIN" },
                    { "e2ec77c0-e2e4-4954-ba79-facd433997f1", null, "User", "USER" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_TodoProfiles_Todos_TodoId",
                table: "TodoProfiles",
                column: "TodoId",
                principalTable: "Todos",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TodoProfiles_Todos_TodoId",
                table: "TodoProfiles");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5a1add3c-0cc6-444a-bb01-977a403bfbd1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e2ec77c0-e2e4-4954-ba79-facd433997f1");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "066d266f-ac2a-4a19-8fe6-80b566823f2f", null, "User", "USER" },
                    { "25231a0b-a092-4167-862d-907d61ae7284", null, "Admin", "ADMIN" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_TodoProfiles_Todos_TodoId",
                table: "TodoProfiles",
                column: "TodoId",
                principalTable: "Todos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
