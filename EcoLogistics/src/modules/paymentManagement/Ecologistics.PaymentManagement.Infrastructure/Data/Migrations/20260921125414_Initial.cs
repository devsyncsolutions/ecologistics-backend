using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecologistics.PaymentManagement.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            ArgumentNullException.ThrowIfNull(migrationBuilder);
            migrationBuilder.CreateTable(
                name: "BalanceThresholdsPerDistricts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DistrictId = table.Column<Guid>(type: "uuid", nullable: false),
                    BalanceThresholdAmount = table.Column<decimal>(type: "numeric", nullable: false),
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
                    table.PrimaryKey("PK_BalanceThresholdsPerDistricts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserPaymentMethods",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Method = table.Column<string>(type: "text", nullable: false, defaultValue: "Cash"),
                    Status = table.Column<string>(type: "text", nullable: false, defaultValue: "PendingVerification"),
                    PaystackAuthorizationCode = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    PaystackSignature = table.Column<string>(type: "text", nullable: true),
                    Last4 = table.Column<string>(type: "text", nullable: true),
                    CardType = table.Column<string>(type: "text", nullable: true),
                    CustomerEmail = table.Column<string>(type: "text", nullable: true),
                    IsDefault = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
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
                    table.PrimaryKey("PK_UserPaymentMethods", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserPaymentMethods_CreatedBy",
                table: "UserPaymentMethods",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_UserPaymentMethods_CreatedBy_PaystackSignature",
                table: "UserPaymentMethods",
#pragma warning disable CA1861
                columns: new[] { "CreatedBy", "PaystackSignature" },
#pragma warning restore CA1861
                unique: true,
                filter: "\"PaystackSignature\" IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UserPaymentMethods_IsDeleted",
                table: "UserPaymentMethods",
                column: "IsDeleted");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            ArgumentNullException.ThrowIfNull(migrationBuilder);
            migrationBuilder.DropTable(
                name: "BalanceThresholdsPerDistricts");

            migrationBuilder.DropTable(
                name: "UserPaymentMethods");
        }
    }
}
