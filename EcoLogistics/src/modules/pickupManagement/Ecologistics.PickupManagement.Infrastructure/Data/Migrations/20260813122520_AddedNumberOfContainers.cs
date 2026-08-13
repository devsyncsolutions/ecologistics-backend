using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecologistics.PickupManagement.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedNumberOfContainers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            ArgumentNullException.ThrowIfNull(migrationBuilder);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfContainers",
                table: "PickupsForQuerying",
                type: "integer",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfContainers",
                table: "Pickups",
                type: "integer",
                nullable: false,
                defaultValue: 1);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            ArgumentNullException.ThrowIfNull(migrationBuilder);

            migrationBuilder.DropColumn(
                name: "NumberOfContainers",
                table: "PickupsForQuerying");

            migrationBuilder.DropColumn(
                name: "NumberOfContainers",
                table: "Pickups");
        }
    }
}
