using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace API.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5f506e91-44fd-464d-b73e-9721694d8b41");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d42fae18-9df3-4b80-a4c9-1f612929d707");

            migrationBuilder.DeleteData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "28ae989c-f0b3-4d67-bb1d-1cf3a5241cbe", null, "Administrator", "ADMINISTRATOR" },
                    { "79d4768b-813a-4cb8-bfe5-9c346a87a3dc", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "28ae989c-f0b3-4d67-bb1d-1cf3a5241cbe");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "79d4768b-813a-4cb8-bfe5-9c346a87a3dc");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "5f506e91-44fd-464d-b73e-9721694d8b41", null, "Administrator", "ADMINISTRATOR" },
                    { "d42fae18-9df3-4b80-a4c9-1f612929d707", null, "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "Hotels",
                columns: new[] { "Id", "Address", "CountryId", "Name", "Rating" },
                values: new object[] { 4, "100 Reforma Ave, Mexico City", 3, "Hotel Mexico City", 4.2999999999999998 });
        }
    }
}
