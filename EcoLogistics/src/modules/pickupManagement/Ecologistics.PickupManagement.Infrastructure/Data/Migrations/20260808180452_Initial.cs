using System;
using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

#nullable disable

namespace Ecologistics.PickupManagement.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            ArgumentNullException.ThrowIfNull(migrationBuilder);

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:postgis", ",,");

            migrationBuilder.CreateTable(
                name: "Districts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Region = table.Column<string>(type: "text", nullable: false, defaultValue: "GreaterAccra"),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
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
                    table.PrimaryKey("PK_Districts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pickups",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false, defaultValue: "OneTime"),
                    Coordinates = table.Column<Point>(type: "geography (point)", nullable: false),
                    FormattedAddress = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    GhanaPostGpsAddress = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: true),
                    Landmark = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    ContactName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    ContactPhone = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: true),
                    ExtraNotes = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    PickupFeeId = table.Column<Guid>(type: "uuid", nullable: false),
                    PickupStatus = table.Column<string>(type: "text", nullable: false, defaultValue: "Scheduled"),
                    CollectorMemberId = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    DownloadableInvoiceUrl = table.Column<string>(type: "text", nullable: true),
                    CancelledAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CancelledBy = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    Reason = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    JobCompletedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    JobStartedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    ScheduledForPickupAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
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
                    table.PrimaryKey("PK_Pickups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WasteMeasurements",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    UnitOfMeasurement = table.Column<string>(type: "character varying(4)", maxLength: 4, nullable: false),
                    WeightRangeMax = table.Column<decimal>(name: "WeightRangeMax ", type: "numeric(10,2)", nullable: false),
                    WeightRangeMin = table.Column<decimal>(name: "WeightRangeMin ", type: "numeric(10,2)", nullable: false),
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
                    table.PrimaryKey("PK_WasteMeasurements", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PickupFees",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DistrictId = table.Column<Guid>(type: "uuid", nullable: false),
                    WasteMeasurementId = table.Column<Guid>(type: "uuid", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
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
                    table.PrimaryKey("PK_PickupFees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PickupFees_Districts_DistrictId",
                        column: x => x.DistrictId,
                        principalTable: "Districts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PickupFees_WasteMeasurements_WasteMeasurementId",
                        column: x => x.WasteMeasurementId,
                        principalTable: "WasteMeasurements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Districts_IsDeleted",
                table: "Districts",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_PickupFees_DistrictId_WasteMeasurementId",
                table: "PickupFees",
#pragma warning disable CA1861
                columns: new[] { "DistrictId", "WasteMeasurementId" });
#pragma warning restore CA1861

            migrationBuilder.CreateIndex(
                name: "IX_PickupFees_IsDeleted",
                table: "PickupFees",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_PickupFees_WasteMeasurementId",
                table: "PickupFees",
                column: "WasteMeasurementId");

            migrationBuilder.CreateIndex(
                name: "IX_Pickups_CollectorMemberId",
                table: "Pickups",
                column: "CollectorMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_Pickups_Coordinates",
                table: "Pickups",
                column: "Coordinates")
                .Annotation("Npgsql:IndexMethod", "GIST");

            migrationBuilder.CreateIndex(
                name: "IX_Pickups_CreatedBy",
                table: "Pickups",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Pickups_IsDeleted",
                table: "Pickups",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Pickups_PickupStatus",
                table: "Pickups",
                column: "PickupStatus");

            migrationBuilder.CreateIndex(
                name: "IX_WasteMeasurements_IsDeleted",
                table: "WasteMeasurements",
                column: "IsDeleted");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            ArgumentNullException.ThrowIfNull(migrationBuilder);

            migrationBuilder.DropTable(
                name: "PickupFees");

            migrationBuilder.DropTable(
                name: "Pickups");

            migrationBuilder.DropTable(
                name: "Districts");

            migrationBuilder.DropTable(
                name: "WasteMeasurements");
        }
    }
}
