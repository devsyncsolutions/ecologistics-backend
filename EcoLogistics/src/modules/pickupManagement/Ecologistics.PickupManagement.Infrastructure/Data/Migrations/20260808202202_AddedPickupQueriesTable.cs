using System;
using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

#nullable disable

namespace Ecologistics.PickupManagement.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedPickupQueriesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            ArgumentNullException.ThrowIfNull(migrationBuilder);

            migrationBuilder.AlterColumn<string>(
                name: "FormattedAddress",
                table: "Pickups",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Priority",
                table: "Pickups",
                type: "text",
                nullable: false,
                defaultValue: "High");

            migrationBuilder.AddColumn<string>(
                name: "Reference",
                table: "Pickups",
                type: "character varying(7)",
                maxLength: 7,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "WasteContentName",
                table: "Pickups",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "Commission",
                table: "PickupFees",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "PickupsForQuerying",
                columns: table => new
                {
                    Reference = table.Column<string>(type: "character varying(7)", maxLength: 7, nullable: false),
                    ContentName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    FormattedAddress = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Type = table.Column<string>(type: "character varying(9)", maxLength: 9, nullable: false),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    Commission = table.Column<decimal>(type: "numeric", nullable: false),
                    AverageWeight = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    Coordinates = table.Column<Point>(type: "geography (point)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PickupsForQuerying", x => x.Reference);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pickups_Reference",
                table: "Pickups",
                column: "Reference",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            ArgumentNullException.ThrowIfNull(migrationBuilder);

            migrationBuilder.DropTable(
                name: "PickupsForQuerying");

            migrationBuilder.DropIndex(
                name: "IX_Pickups_Reference",
                table: "Pickups");

            migrationBuilder.DropColumn(
                name: "Priority",
                table: "Pickups");

            migrationBuilder.DropColumn(
                name: "Reference",
                table: "Pickups");

            migrationBuilder.DropColumn(
                name: "WasteContentName",
                table: "Pickups");

            migrationBuilder.DropColumn(
                name: "Commission",
                table: "PickupFees");

            migrationBuilder.AlterColumn<string>(
                name: "FormattedAddress",
                table: "Pickups",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255);
        }
    }
}
