using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Ecologistics.PickupManagement.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedWasteKinds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            ArgumentNullException.ThrowIfNull(migrationBuilder);
            migrationBuilder.CreateTable(
                name: "WasteKinds",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<string>(type: "text", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    DeletedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WasteKinds", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "WasteKinds",
#pragma warning disable CA1861
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "IsDeleted", "Name", "UpdatedAt", "UpdatedBy" },
#pragma warning restore CA1861
                values: new object[,]
                {
                    { new Guid("233e4567-e89b-12d3-a456-426655440000"), new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System", null, null, false, "Plastic", new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System" },
                    { new Guid("233e4567-e89b-12d3-a456-426655440001"), new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System", null, null, false, "Glass", new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System" },
                    { new Guid("233e4567-e89b-12d3-a456-426655440002"), new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System", null, null, false, "Paper", new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System" },
                    { new Guid("233e4567-e89b-12d3-a456-426655440003"), new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System", null, null, false, "Metal", new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System" },
                    { new Guid("233e4567-e89b-12d3-a456-426655440004"), new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System", null, null, false, "Organic", new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System" },
                    { new Guid("233e4567-e89b-12d3-a456-426655440005"), new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System", null, null, false, "General", new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System" },
                    { new Guid("233e4567-e89b-12d3-a456-426655440006"), new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System", null, null, false, "Liquid", new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_WasteKinds_IsDeleted",
                table: "WasteKinds",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_WasteKinds_Name_CreatedAt",
                table: "WasteKinds",
#pragma warning disable CA1861
                columns: new[] { "Name", "CreatedAt" },
#pragma warning restore CA1861
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            ArgumentNullException.ThrowIfNull(migrationBuilder);
            migrationBuilder.DropTable(
                name: "WasteKinds");
        }
    }
}
