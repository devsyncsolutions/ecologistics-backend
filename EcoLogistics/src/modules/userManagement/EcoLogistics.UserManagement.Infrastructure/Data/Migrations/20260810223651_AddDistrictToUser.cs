using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcoLogistics.UserManagement.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddDistrictToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            ArgumentNullException.ThrowIfNull(migrationBuilder);

            migrationBuilder.AddColumn<Guid>(
                name: "DistrictId",
                table: "AspNetUsers",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("133e4567-e89b-12d3-a456-426655440000"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            ArgumentNullException.ThrowIfNull(migrationBuilder);

            migrationBuilder.DropColumn(
                name: "DistrictId",
                table: "AspNetUsers");
        }
    }
}
