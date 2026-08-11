using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Ecologistics.PickupManagement.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedPickupRecurringStrategies : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            ArgumentNullException.ThrowIfNull(migrationBuilder);

            migrationBuilder.AddColumn<int>(
                name: "RecurringProperties_DaysIntervalForPickupIfRecurring",
                table: "Pickups",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "RecurringProperties_IsActive",
                table: "Pickups",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "RecurringProperties_NextScheduledDate",
                table: "Pickups",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "RecurringProperties_ParentPickupId_Value",
                table: "Pickups",
                type: "uuid",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Districts",
#pragma warning disable CA1861
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "Description", "IsDeleted", "Name", "UpdatedAt", "UpdatedBy" },
#pragma warning restore CA1861
                values: new object[] { new Guid("133e4567-e89b-12d3-a456-426655440000"), new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System", null, null, "", false, "Accra Metropolitan", new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System" });

            migrationBuilder.InsertData(
                table: "Districts",
#pragma warning disable CA1861
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "Description", "IsDeleted", "Name", "Region", "UpdatedAt", "UpdatedBy" },
#pragma warning restore CA1861
                values: new object[,]
                {
                    { new Guid("233e4567-e89b-12d3-a456-426655440000"), new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System", null, null, "", false, "Akuapim South (New)", "Eastern", new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System" },
                    { new Guid("333e4567-e89b-12d3-a456-426655440000"), new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System", null, null, "", false, "Abura/Asebu/Kwamankese", "Central", new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System" }
                });

            migrationBuilder.InsertData(
                table: "Districts",
#pragma warning disable CA1861
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "Description", "IsDeleted", "Name", "UpdatedAt", "UpdatedBy" },
#pragma warning restore CA1861
                values: new object[] { new Guid("433e4567-e89b-12d3-a456-426655440000"), new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System", null, null, "", false, "Ada West (New)", new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System" });

            migrationBuilder.InsertData(
                table: "Districts",
#pragma warning disable CA1861
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "Description", "IsDeleted", "Name", "Region", "UpdatedAt", "UpdatedBy" },
#pragma warning restore CA1861
                values: new object[,]
                {
                    { new Guid("533e4567-e89b-12d3-a456-426655440000"), new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System", null, null, "", false, "Adaklu (New)", "Volta", new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System" },
                    { new Guid("633e4567-e89b-12d3-a456-426655440000"), new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System", null, null, "", false, "Adansi North", "Ashanti", new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System" },
                    { new Guid("733e4567-e89b-12d3-a456-426655440000"), new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System", null, null, "", false, "Adansi South", "Ashanti", new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System" }
                });

            migrationBuilder.InsertData(
                table: "Districts",
#pragma warning disable CA1861
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "Description", "IsDeleted", "Name", "UpdatedAt", "UpdatedBy" },
#pragma warning restore CA1861
                values: new object[] { new Guid("833e4567-e89b-12d3-a456-426655440000"), new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System", null, null, "", false, "Adentan municipal", new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System" });

            migrationBuilder.InsertData(
                table: "Districts",
#pragma warning disable CA1861
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "Description", "IsDeleted", "Name", "Region", "UpdatedAt", "UpdatedBy" },
#pragma warning restore CA1861
                values: new object[,]
                {
                    { new Guid("913e4567-e89b-12d3-a456-426655440000"), new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System", null, null, "", false, "Afigya-Kwabre", "Ashanti", new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System" },
                    { new Guid("923e4567-e89b-12d3-a456-426655440000"), new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System", null, null, "", false, "Afram Plains South (New)", "Eastern", new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System" },
                    { new Guid("933e4567-e89b-12d3-a456-426655440000"), new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System", null, null, "", false, "Afadjato (New)", "Volta", new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System" },
                    { new Guid("943e4567-e89b-12d3-a456-426655440000"), new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System", null, null, "", false, "Agona East", "Central", new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System" },
                    { new Guid("953e4567-e89b-12d3-a456-426655440000"), new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System", null, null, "", false, "Agona West Municipal", "Central", new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System" },
                    { new Guid("963e4567-e89b-12d3-a456-426655440000"), new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System", null, null, "", false, "Agotime Ziope", "Volta", new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System" },
                    { new Guid("973e4567-e89b-12d3-a456-426655440000"), new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System", null, null, "", false, "Ahafo Ano North", "Ashanti", new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System" },
                    { new Guid("983e4567-e89b-12d3-a456-426655440000"), new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System", null, null, "", false, "Ahafo Ano South", "Ashanti", new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System" },
                    { new Guid("991e4567-e89b-12d3-a456-426655440000"), new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System", null, null, "", false, "Ajumako/Enyan/Essiam", "Central", new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System" },
                    { new Guid("992e4567-e89b-12d3-a456-426655440000"), new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System", null, null, "", false, "Akatsi North (New)", "Volta", new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System" },
                    { new Guid("993e4567-e89b-12d3-a456-426655440000"), new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System", null, null, "", false, "Ahanta West", "Western", new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System" },
                    { new Guid("994e4567-e89b-12d3-a456-426655440000"), new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System", null, null, "", false, "Akatsi South", "Volta", new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System" },
                    { new Guid("995e4567-e89b-12d3-a456-426655440000"), new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System", null, null, "", false, "Akuapim North", "Eastern", new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System" },
                    { new Guid("996e4567-e89b-12d3-a456-426655440000"), new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System", null, null, "", false, "Akyemansa", "Eastern", new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System" },
                    { new Guid("997e4567-e89b-12d3-a456-426655440000"), new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System", null, null, "", false, "Amansie Central", "Ashanti", new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            ArgumentNullException.ThrowIfNull(migrationBuilder);
            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: new Guid("133e4567-e89b-12d3-a456-426655440000"));

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: new Guid("233e4567-e89b-12d3-a456-426655440000"));

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: new Guid("333e4567-e89b-12d3-a456-426655440000"));

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: new Guid("433e4567-e89b-12d3-a456-426655440000"));

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: new Guid("533e4567-e89b-12d3-a456-426655440000"));

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: new Guid("633e4567-e89b-12d3-a456-426655440000"));

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: new Guid("733e4567-e89b-12d3-a456-426655440000"));

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: new Guid("833e4567-e89b-12d3-a456-426655440000"));

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: new Guid("913e4567-e89b-12d3-a456-426655440000"));

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: new Guid("923e4567-e89b-12d3-a456-426655440000"));

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: new Guid("933e4567-e89b-12d3-a456-426655440000"));

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: new Guid("943e4567-e89b-12d3-a456-426655440000"));

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: new Guid("953e4567-e89b-12d3-a456-426655440000"));

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: new Guid("963e4567-e89b-12d3-a456-426655440000"));

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: new Guid("973e4567-e89b-12d3-a456-426655440000"));

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: new Guid("983e4567-e89b-12d3-a456-426655440000"));

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: new Guid("991e4567-e89b-12d3-a456-426655440000"));

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: new Guid("992e4567-e89b-12d3-a456-426655440000"));

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: new Guid("993e4567-e89b-12d3-a456-426655440000"));

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: new Guid("994e4567-e89b-12d3-a456-426655440000"));

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: new Guid("995e4567-e89b-12d3-a456-426655440000"));

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: new Guid("996e4567-e89b-12d3-a456-426655440000"));

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: new Guid("997e4567-e89b-12d3-a456-426655440000"));

            migrationBuilder.DropColumn(
                name: "RecurringProperties_DaysIntervalForPickupIfRecurring",
                table: "Pickups");

            migrationBuilder.DropColumn(
                name: "RecurringProperties_IsActive",
                table: "Pickups");

            migrationBuilder.DropColumn(
                name: "RecurringProperties_NextScheduledDate",
                table: "Pickups");

            migrationBuilder.DropColumn(
                name: "RecurringProperties_ParentPickupId_Value",
                table: "Pickups");
        }
    }
}
