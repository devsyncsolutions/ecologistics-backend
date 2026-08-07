using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecologistics.TenantManagement.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedCompanyPurposes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            ArgumentNullException.ThrowIfNull(migrationBuilder);

            migrationBuilder.InsertData(
                table: "CompanyPurposes",
#pragma warning disable CA1861
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "IsDeleted", "Type", "UpdatedAt", "UpdatedBy" },
#pragma warning restore CA1861
                values: new object[]
                {
                    new Guid("133e4567-e89b-12d3-a456-426655440000"),
                    new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                    "System",
                    null,
                    null,
                    false,
                    "Collector",
                    new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                    "System"
                });

            migrationBuilder.InsertData(
                table: "CompanyPurposes",
#pragma warning disable CA1861
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "IsDeleted", "Type", "UpdatedAt", "UpdatedBy" },
#pragma warning restore CA1861
                values: new object[]
                {
                    new Guid("233e4567-e89b-12d3-a456-426655440000"),
                    new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                    "System",
                    null,
                    null,
                    false,
                    "Processor",
                    new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                    "System"
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            ArgumentNullException.ThrowIfNull(migrationBuilder);

            migrationBuilder.DeleteData(
                table: "CompanyPurposes",
                keyColumn: "Id",
                keyValue: new Guid("133e4567-e89b-12d3-a456-426655440000"));

            migrationBuilder.DeleteData(
                table: "CompanyPurposes",
                keyColumn: "Id",
                keyValue: new Guid("233e4567-e89b-12d3-a456-426655440000"));
        }
    }
}
