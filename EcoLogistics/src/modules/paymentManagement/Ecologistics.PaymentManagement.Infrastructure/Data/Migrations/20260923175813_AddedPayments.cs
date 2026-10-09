using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecologistics.PaymentManagement.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedPayments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            ArgumentNullException.ThrowIfNull(migrationBuilder);

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AssociatedPickupId = table.Column<Guid>(type: "uuid", nullable: false),
                    GeneratorId = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    CollectorMemberId = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    PaymentMethodId = table.Column<Guid>(type: "uuid", nullable: true),
                    PendingPaymentMethodId = table.Column<Guid>(type: "uuid", nullable: true),
                    Status = table.Column<string>(type: "text", nullable: false),
                    PaystackReference = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    PaystackTransactionId = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: true),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    Currency = table.Column<string>(type: "text", nullable: false),
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
                    table.PrimaryKey("PK_Payments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PickupJobInvoices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ReferenceNumber = table.Column<string>(type: "character varying(13)", maxLength: 13, nullable: false),
                    AmountPayable = table.Column<decimal>(type: "numeric", nullable: false),
                    Commission = table.Column<decimal>(type: "numeric", nullable: false),
                    PaymentCompletedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    InvoiceUrl = table.Column<string>(type: "text", nullable: true),
                    ContentName = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    AssociatedPickupId = table.Column<Guid>(type: "uuid", nullable: false),
                    CollectorMemberId = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false, defaultValue: 1),
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
                    table.PrimaryKey("PK_PickupJobInvoices", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Payments_AssociatedPickupId",
                table: "Payments",
                column: "AssociatedPickupId");

            migrationBuilder.CreateIndex(
                name: "IX_PickupJobInvoices_CollectorMemberId",
                table: "PickupJobInvoices",
                column: "CollectorMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_PickupJobInvoices_CreatedBy",
                table: "PickupJobInvoices",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_PickupJobInvoices_ReferenceNumber",
                table: "PickupJobInvoices",
                column: "ReferenceNumber");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            ArgumentNullException.ThrowIfNull(migrationBuilder);

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "PickupJobInvoices");
        }
    }
}
