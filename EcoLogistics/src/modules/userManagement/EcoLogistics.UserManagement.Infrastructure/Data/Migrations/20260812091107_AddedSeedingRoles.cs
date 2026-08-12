using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EcoLogistics.UserManagement.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedSeedingRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            ArgumentNullException.ThrowIfNull(migrationBuilder);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
#pragma warning disable CA1861
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
#pragma warning restore CA1861
                values: new object[,]
                {
                    { "1b73f79d-059f-469f-a2d0-e220bd95a439", "795db7e2-1bfd-4013-ad67-761058c988dd", "Collector", null },
                    { "e221fd32-55c7-47b0-aa4c-121be12e256a", "eb629136-7eee-4195-85d2-7f7f3ed62f4f", "Generator", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            ArgumentNullException.ThrowIfNull(migrationBuilder);

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1b73f79d-059f-469f-a2d0-e220bd95a439");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e221fd32-55c7-47b0-aa4c-121be12e256a");
        }
    }
}
