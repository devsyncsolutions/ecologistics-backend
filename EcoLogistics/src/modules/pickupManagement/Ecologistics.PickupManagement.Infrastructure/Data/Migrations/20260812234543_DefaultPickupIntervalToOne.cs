using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecologistics.PickupManagement.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class DefaultPickupIntervalToOne : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            ArgumentNullException.ThrowIfNull(migrationBuilder);

            migrationBuilder.AlterColumn<int>(
                name: "RecurringProperties_DaysIntervalForPickupIfRecurring",
                table: "Pickups",
                type: "integer",
                nullable: true,
                defaultValue: 1,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            ArgumentNullException.ThrowIfNull(migrationBuilder);

            migrationBuilder.AlterColumn<int>(
                name: "RecurringProperties_DaysIntervalForPickupIfRecurring",
                table: "Pickups",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true,
                oldDefaultValue: 1);
        }
    }
}
