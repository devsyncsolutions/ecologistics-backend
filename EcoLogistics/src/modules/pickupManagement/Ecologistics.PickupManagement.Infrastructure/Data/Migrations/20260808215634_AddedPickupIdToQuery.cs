using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecologistics.PickupManagement.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedPickupIdToQuery : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            ArgumentNullException.ThrowIfNull(migrationBuilder);

            migrationBuilder.DropPrimaryKey(
                name: "PK_PickupsForQuerying",
                table: "PickupsForQuerying");

            migrationBuilder.AddColumn<Guid>(
                name: "PickupId",
                table: "PickupsForQuerying",
                type: "uuid",
                nullable: false,
#pragma warning disable S4581
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
#pragma warning restore S4581

            migrationBuilder.AddColumn<string>(
                name: "Priority",
                table: "PickupsForQuerying",
                type: "character varying(6)",
                maxLength: 6,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PickupsForQuerying",
                table: "PickupsForQuerying",
                column: "PickupId");

            migrationBuilder.CreateIndex(
                name: "IX_PickupsForQuerying_Coordinates",
                table: "PickupsForQuerying",
                column: "Coordinates")
                .Annotation("Npgsql:IndexMethod", "GIST");

            migrationBuilder.CreateIndex(
                name: "IX_PickupsForQuerying_Type_Priority",
                table: "PickupsForQuerying",
#pragma warning disable CA1861
                columns: new[] { "Type", "Priority" });
#pragma warning restore CA1861
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            ArgumentNullException.ThrowIfNull(migrationBuilder);

            migrationBuilder.DropPrimaryKey(
                name: "PK_PickupsForQuerying",
                table: "PickupsForQuerying");

            migrationBuilder.DropIndex(
                name: "IX_PickupsForQuerying_Coordinates",
                table: "PickupsForQuerying");

            migrationBuilder.DropIndex(
                name: "IX_PickupsForQuerying_Type_Priority",
                table: "PickupsForQuerying");

            migrationBuilder.DropColumn(
                name: "PickupId",
                table: "PickupsForQuerying");

            migrationBuilder.DropColumn(
                name: "Priority",
                table: "PickupsForQuerying");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PickupsForQuerying",
                table: "PickupsForQuerying",
                column: "Reference");
        }
    }
}
