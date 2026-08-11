using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Ecologistics.PickupManagement.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedRestOfDistricts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            ArgumentNullException.ThrowIfNull(migrationBuilder);

            migrationBuilder.InsertData(
                table: "Districts",
#pragma warning disable CA1861
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "Description", "IsDeleted", "Name", "Region", "UpdatedAt", "UpdatedBy" },
#pragma warning restore CA1861
                values: new object[,]
                {
                    { new Guid("006e415f-b2d2-4c0c-baa6-180741f4d2dd"), new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System", null, null, "", false, "East Gonja", "Northern", new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System" },
                    { new Guid("05595fe0-2420-40b8-ae08-5d2892c4ab59"), new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System", null, null, "", false, "Asutifi", "BrongAhafo", new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System" },
                    { new Guid("0ce742fd-e155-43d1-9c36-342ce2c15b76"), new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System", null, null, "", false, "Bia", "Western", new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System" },
                    { new Guid("140f223e-1cd5-4b6c-9830-dd66262588c3"), new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System", null, null, "", false, "East Akim Municipal", "Eastern", new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System" },
                    { new Guid("144d7559-82d6-4abd-a8df-e62f5bb473e0"), new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System", null, null, "", false, "Bole", "Northern", new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System" },
                    { new Guid("15ef1bd2-727e-4571-b170-4424cfdb60d4"), new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System", null, null, "", false, "Asunafo North Municipal", "BrongAhafo", new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System" },
                    { new Guid("242ad89b-3310-4639-87a6-48f4f3962b1b"), new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System", null, null, "", false, "Asante Akim North (New)", "Ashanti", new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System" },
                    { new Guid("2992905a-274b-4be7-8c09-3282b11c5af1"), new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System", null, null, "", false, "Bibiani/Anhwiaso/Bekwai", "Western", new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System" },
                    { new Guid("2a6965ad-58c5-43b7-8179-9d6fa1237fe9"), new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System", null, null, "", false, "Asutifi South (New)", "BrongAhafo", new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System" },
                    { new Guid("32337e7b-c92d-4664-a918-2b72ef9dd1cd"), new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System", null, null, "", false, "Bawku Municipal", "UpperEast", new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System" },
                    { new Guid("3413c566-98ba-470b-a8c5-802258b8c582"), new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System", null, null, "", false, "Asuogyaman", "Eastern", new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System" },
                    { new Guid("3644166e-3913-41d3-a2c7-6714fdd18eee"), new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System", null, null, "", false, "Builsa South (New)", "UpperEast", new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System" },
                    { new Guid("3786daa1-7cd0-46af-b340-66a85f0858b8"), new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System", null, null, "", false, "Amansie West", "Ashanti", new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System" },
                    { new Guid("39c14664-6994-4cb3-8ace-3a94752e4bed"), new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System", null, null, "", false, "Atebubu-Amantin Municipal", "BrongAhafo", new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System" },
                    { new Guid("3c50877d-0674-457e-bbd0-f83b64752a4d"), new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System", null, null, "", false, "Bia East (New)", "Western", new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System" },
                    { new Guid("3fbdbb37-b2d3-4b3c-8eb3-a5a70bfc9c13"), new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System", null, null, "", false, "Binduri (New)", "UpperEast", new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System" },
                    { new Guid("428ab56a-236c-4b55-9666-199a024020f9"), new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System", null, null, "", false, "Fanteakwa", "Eastern", new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System" },
                    { new Guid("443b507d-1915-4335-ab53-7f738bf6475b"), new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System", null, null, "", false, "Daffiama Bussie Issa (New)", "UpperWest", new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System" },
                    { new Guid("463cf0a5-523a-40b8-98ec-b225b5947018"), new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System", null, null, "", false, "Atwima Nwabiagya", "Ashanti", new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System" },
                    { new Guid("4d146b19-e199-438b-8263-29f949196abb"), new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System", null, null, "", false, "Asante Akim South", "Ashanti", new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System" },
                    { new Guid("51242457-2646-4768-85b1-d569a4f42d9f"), new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System", null, null, "", false, "Ejura/Sekyedumase", "Ashanti", new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System" },
                    { new Guid("51f3d516-bbc9-4b75-b752-b06237590d11"), new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System", null, null, "", false, "Biakoye", "Volta", new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System" },
                    { new Guid("5324b9be-cede-4b7d-a54d-021f019a21e2"), new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System", null, null, "", false, "Asikuma/Odoben/Brakwa", "Central", new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System" },
                    { new Guid("54538b6d-8014-49a8-aeaa-411da20696bc"), new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System", null, null, "", false, "Asokore Mampong Municipal (New)", "Ashanti", new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System" },
                    { new Guid("55d9f466-e8ea-4b57-8e85-64a19c2c6654"), new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System", null, null, "", false, "Atwima Mponua", "Ashanti", new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System" }
                });

            migrationBuilder.InsertData(
                table: "Districts",
#pragma warning disable CA1861
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "Description", "IsDeleted", "Name", "UpdatedAt", "UpdatedBy" },
#pragma warning restore CA1861
                values: new object[] { new Guid("57da89bb-1428-4bbd-9730-b4eaa3f4143e"), new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System", null, null, "", false, "Dangme West", new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System" });

            migrationBuilder.InsertData(
                table: "Districts",
#pragma warning disable CA1861
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "Description", "IsDeleted", "Name", "Region", "UpdatedAt", "UpdatedBy" },
#pragma warning restore CA1861
                values: new object[,]
                {
                    { new Guid("57fbe8c0-d4e7-4049-a06f-b93174f012df"), new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System", null, null, "", false, "Aowin", "Western", new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System" },
                    { new Guid("5ab85f0e-7524-4c47-961e-64fbccc187be"), new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System", null, null, "", false, "Effutu Municipal", "Central", new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System" },
                    { new Guid("5d726208-258c-4702-bcb8-80a62836f34f"), new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System", null, null, "", false, "Bekwai Municipal", "Ashanti", new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System" },
                    { new Guid("5fb54aca-7428-43eb-bdb7-ab9cdb17e416"), new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System", null, null, "", false, "Birim South", "Eastern", new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System" },
                    { new Guid("613669eb-6dd2-44a3-93a5-feaf3126227c"), new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System", null, null, "", false, "Assin South", "Central", new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System" },
                    { new Guid("6177dc24-5c19-46be-895d-5aac86eba920"), new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System", null, null, "", false, "Dormaa East", "BrongAhafo", new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System" },
                    { new Guid("623f9133-7bf1-4e53-91a9-deb98f7a725f"), new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System", null, null, "", false, "Ellembele", "Western", new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System" },
                    { new Guid("64c4102b-62b0-4022-bb0f-521c972b85e2"), new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System", null, null, "", false, "Ayensuano (New)", "Eastern", new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System" },
                    { new Guid("66733647-073c-4a15-b792-3fb8a1e7d237"), new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System", null, null, "", false, "Birim Central Municipal", "Eastern", new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System" },
                    { new Guid("68dacefa-24c7-4eb0-8be1-d8581b24de27"), new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System", null, null, "", false, "Bodi (New)", "Western", new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System" },
                    { new Guid("747b6c16-6e5b-494b-9d96-e7cf7de49612"), new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System", null, null, "", false, "Ekumfi (New)", "Central", new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System" },
                    { new Guid("754fea2f-8897-4a68-85b7-b7f7f79c08ae"), new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System", null, null, "", false, "Banda (New)", "BrongAhafo", new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System" }
                });

            migrationBuilder.InsertData(
                table: "Districts",
#pragma warning disable CA1861
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "Description", "IsDeleted", "Name", "UpdatedAt", "UpdatedBy" },
#pragma warning restore CA1861
                values: new object[] { new Guid("7ff9d3b2-d348-4f6b-82e6-3aed7110d377"), new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System", null, null, "", false, "Ga Central (New)", new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System" });

            migrationBuilder.InsertData(
                table: "Districts",
#pragma warning disable CA1861
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "Description", "IsDeleted", "Name", "Region", "UpdatedAt", "UpdatedBy" },
#pragma warning restore CA1861
                values: new object[,]
                {
                    { new Guid("81d29506-e56f-42d4-9639-6e7eb25e632a"), new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System", null, null, "", false, "Dormaa West (New)", "BrongAhafo", new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System" },
                    { new Guid("902d057b-e871-4382-aec4-1154baa97f40"), new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System", null, null, "", false, "Cape Coast Metropolitan", "Central", new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System" },
                    { new Guid("9e08ff26-610b-43b3-8314-e0ff7d7c4b47"), new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System", null, null, "", false, "Dormaa Municipal", "BrongAhafo", new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System" },
                    { new Guid("9e6e6e22-d2bf-4a7f-8c5c-c68c2e458916"), new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System", null, null, "", false, "Bolgatanga Municipal", "UpperEast", new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System" },
                    { new Guid("9f2c294c-1590-47fb-b048-861712c10e81"), new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System", null, null, "", false, "Ejisu-Juaben Municipal", "Ashanti", new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System" },
                    { new Guid("a3f9d78c-379f-4f78-8f39-532e91508338"), new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System", null, null, "", false, "Bongo", "UpperEast", new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System" },
                    { new Guid("a4a740b6-9c16-40e4-b40e-dd9ea2e4b93f"), new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System", null, null, "", false, "Awutu-Senya-East (New)", "Central", new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System" },
                    { new Guid("a4c7b800-315c-461f-8ae6-59b050e2b39d"), new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System", null, null, "", false, "Denkyembour (New)", "Eastern", new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System" },
                    { new Guid("a67954a3-c4dd-4367-97cd-4074486605fd"), new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System", null, null, "", false, "Awutu-Senya", "Central", new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System" },
                    { new Guid("aefdcca4-fab8-439a-ba2e-64c9a2e6a076"), new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System", null, null, "", false, "Asunafo South", "BrongAhafo", new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System" }
                });

            migrationBuilder.InsertData(
                table: "Districts",
#pragma warning disable CA1861
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "Description", "IsDeleted", "Name", "UpdatedAt", "UpdatedBy" },
#pragma warning restore CA1861
                values: new object[] { new Guid("af3184c3-41fe-4fe1-ad9f-2ce76a1ac496"), new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System", null, null, "", false, "Ashaiman Municipal", new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System" });

            migrationBuilder.InsertData(
                table: "Districts",
#pragma warning disable CA1861
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "Description", "IsDeleted", "Name", "Region", "UpdatedAt", "UpdatedBy" },
#pragma warning restore CA1861
                values: new object[,]
                {
                    { new Guid("afdd8184-13b7-4a66-8c7d-d2c25153605c"), new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System", null, null, "", false, "Chereponi", "Northern", new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System" },
                    { new Guid("b35e4afe-2659-4afe-a52e-8411f9d4ce56"), new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System", null, null, "", false, "Central Gonja", "Northern", new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System" },
                    { new Guid("b665a389-f026-4a58-9029-14261aba355f"), new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System", null, null, "", false, "Atiwa", "Eastern", new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System" },
                    { new Guid("bf0ba27b-44d6-4e87-a29e-b17dc1ce2177"), new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System", null, null, "", false, "Builsa", "UpperEast", new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System" },
                    { new Guid("c4c80a74-8896-4c1a-9347-486a79815cf4"), new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System", null, null, "", false, "Bosome Freho", "Ashanti", new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System" }
                });

            migrationBuilder.InsertData(
                table: "Districts",
#pragma warning disable CA1861
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "Description", "IsDeleted", "Name", "UpdatedAt", "UpdatedBy" },
#pragma warning restore CA1861
                values: new object[] { new Guid("cabea05a-99f1-4e95-9429-d520f6da028e"), new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System", null, null, "", false, "Ga East Municipal", new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System" });

            migrationBuilder.InsertData(
                table: "Districts",
#pragma warning disable CA1861
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "Description", "IsDeleted", "Name", "Region", "UpdatedAt", "UpdatedBy" },
#pragma warning restore CA1861
                values: new object[,]
                {
                    { new Guid("cecaa704-88cd-4323-aa40-d4b93d655441"), new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System", null, null, "", false, "Bunkpurugu-Yunyoo", "Northern", new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System" },
                    { new Guid("cffa99c0-1de4-4867-b9dd-5f5f468c064f"), new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System", null, null, "", false, "Berekum Municipal", "BrongAhafo", new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System" }
                });

            migrationBuilder.InsertData(
                table: "Districts",
#pragma warning disable CA1861
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "Description", "IsDeleted", "Name", "UpdatedAt", "UpdatedBy" },
#pragma warning restore CA1861
                values: new object[] { new Guid("d328b881-9cae-43f1-9185-e5a0b4bcf964"), new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System", null, null, "", false, "Dangme East", new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System" });

            migrationBuilder.InsertData(
                table: "Districts",
#pragma warning disable CA1861
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "Description", "IsDeleted", "Name", "Region", "UpdatedAt", "UpdatedBy" },
#pragma warning restore CA1861
                values: new object[,]
                {
                    { new Guid("d4a8f69a-4f24-4e89-a69b-416c0fe77e67"), new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System", null, null, "", false, "Bawku West", "UpperEast", new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System" },
                    { new Guid("d691e123-5b27-4798-8bb0-e7e65e5c0587"), new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System", null, null, "", false, "East Mamprusi", "Northern", new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System" },
                    { new Guid("db352e37-073e-42cc-b143-b7a00efbbc68"), new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System", null, null, "", false, "Bosomtwe", "Ashanti", new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System" },
                    { new Guid("de431fe6-514c-4d25-b9e4-7d6bcb5cd0ec"), new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System", null, null, "", false, "Atwima Kwanwoma", "Ashanti", new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System" },
                    { new Guid("e711560c-9725-430e-b00f-6af67223b9a8"), new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System", null, null, "", false, "Birim North", "Eastern", new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System" },
                    { new Guid("e7e2e24c-851e-42cb-b6f7-641a118fa31c"), new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System", null, null, "", false, "Assin North Municipal", "Central", new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System" },
                    { new Guid("ea663b18-8f1c-4e81-8bf1-d557ccf7a063"), new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System", null, null, "", false, "Central Tongu", "Volta", new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System" },
                    { new Guid("eac0c8a5-a1f5-426e-b168-588e690f0268"), new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System", null, null, "", false, "Asante Akim Central Municipal", "Ashanti", new DateTimeOffset(new DateTime(2026, 8, 6, 20, 22, 30, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "System" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            ArgumentNullException.ThrowIfNull(migrationBuilder);

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: new Guid("006e415f-b2d2-4c0c-baa6-180741f4d2dd"));

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: new Guid("05595fe0-2420-40b8-ae08-5d2892c4ab59"));

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: new Guid("0ce742fd-e155-43d1-9c36-342ce2c15b76"));

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: new Guid("140f223e-1cd5-4b6c-9830-dd66262588c3"));

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: new Guid("144d7559-82d6-4abd-a8df-e62f5bb473e0"));

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: new Guid("15ef1bd2-727e-4571-b170-4424cfdb60d4"));

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: new Guid("242ad89b-3310-4639-87a6-48f4f3962b1b"));

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: new Guid("2992905a-274b-4be7-8c09-3282b11c5af1"));

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: new Guid("2a6965ad-58c5-43b7-8179-9d6fa1237fe9"));

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: new Guid("32337e7b-c92d-4664-a918-2b72ef9dd1cd"));

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: new Guid("3413c566-98ba-470b-a8c5-802258b8c582"));

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: new Guid("3644166e-3913-41d3-a2c7-6714fdd18eee"));

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: new Guid("3786daa1-7cd0-46af-b340-66a85f0858b8"));

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: new Guid("39c14664-6994-4cb3-8ace-3a94752e4bed"));

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: new Guid("3c50877d-0674-457e-bbd0-f83b64752a4d"));

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: new Guid("3fbdbb37-b2d3-4b3c-8eb3-a5a70bfc9c13"));

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: new Guid("428ab56a-236c-4b55-9666-199a024020f9"));

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: new Guid("443b507d-1915-4335-ab53-7f738bf6475b"));

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: new Guid("463cf0a5-523a-40b8-98ec-b225b5947018"));

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: new Guid("4d146b19-e199-438b-8263-29f949196abb"));

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: new Guid("51242457-2646-4768-85b1-d569a4f42d9f"));

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: new Guid("51f3d516-bbc9-4b75-b752-b06237590d11"));

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: new Guid("5324b9be-cede-4b7d-a54d-021f019a21e2"));

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: new Guid("54538b6d-8014-49a8-aeaa-411da20696bc"));

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: new Guid("55d9f466-e8ea-4b57-8e85-64a19c2c6654"));

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: new Guid("57da89bb-1428-4bbd-9730-b4eaa3f4143e"));

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: new Guid("57fbe8c0-d4e7-4049-a06f-b93174f012df"));

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: new Guid("5ab85f0e-7524-4c47-961e-64fbccc187be"));

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: new Guid("5d726208-258c-4702-bcb8-80a62836f34f"));

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: new Guid("5fb54aca-7428-43eb-bdb7-ab9cdb17e416"));

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: new Guid("613669eb-6dd2-44a3-93a5-feaf3126227c"));

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: new Guid("6177dc24-5c19-46be-895d-5aac86eba920"));

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: new Guid("623f9133-7bf1-4e53-91a9-deb98f7a725f"));

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: new Guid("64c4102b-62b0-4022-bb0f-521c972b85e2"));

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: new Guid("66733647-073c-4a15-b792-3fb8a1e7d237"));

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: new Guid("68dacefa-24c7-4eb0-8be1-d8581b24de27"));

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: new Guid("747b6c16-6e5b-494b-9d96-e7cf7de49612"));

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: new Guid("754fea2f-8897-4a68-85b7-b7f7f79c08ae"));

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: new Guid("7ff9d3b2-d348-4f6b-82e6-3aed7110d377"));

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: new Guid("81d29506-e56f-42d4-9639-6e7eb25e632a"));

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: new Guid("902d057b-e871-4382-aec4-1154baa97f40"));

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: new Guid("9e08ff26-610b-43b3-8314-e0ff7d7c4b47"));

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: new Guid("9e6e6e22-d2bf-4a7f-8c5c-c68c2e458916"));

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: new Guid("9f2c294c-1590-47fb-b048-861712c10e81"));

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: new Guid("a3f9d78c-379f-4f78-8f39-532e91508338"));

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: new Guid("a4a740b6-9c16-40e4-b40e-dd9ea2e4b93f"));

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: new Guid("a4c7b800-315c-461f-8ae6-59b050e2b39d"));

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: new Guid("a67954a3-c4dd-4367-97cd-4074486605fd"));

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: new Guid("aefdcca4-fab8-439a-ba2e-64c9a2e6a076"));

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: new Guid("af3184c3-41fe-4fe1-ad9f-2ce76a1ac496"));

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: new Guid("afdd8184-13b7-4a66-8c7d-d2c25153605c"));

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: new Guid("b35e4afe-2659-4afe-a52e-8411f9d4ce56"));

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: new Guid("b665a389-f026-4a58-9029-14261aba355f"));

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: new Guid("bf0ba27b-44d6-4e87-a29e-b17dc1ce2177"));

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: new Guid("c4c80a74-8896-4c1a-9347-486a79815cf4"));

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: new Guid("cabea05a-99f1-4e95-9429-d520f6da028e"));

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: new Guid("cecaa704-88cd-4323-aa40-d4b93d655441"));

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: new Guid("cffa99c0-1de4-4867-b9dd-5f5f468c064f"));

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: new Guid("d328b881-9cae-43f1-9185-e5a0b4bcf964"));

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: new Guid("d4a8f69a-4f24-4e89-a69b-416c0fe77e67"));

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: new Guid("d691e123-5b27-4798-8bb0-e7e65e5c0587"));

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: new Guid("db352e37-073e-42cc-b143-b7a00efbbc68"));

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: new Guid("de431fe6-514c-4d25-b9e4-7d6bcb5cd0ec"));

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: new Guid("e711560c-9725-430e-b00f-6af67223b9a8"));

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: new Guid("e7e2e24c-851e-42cb-b6f7-641a118fa31c"));

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: new Guid("ea663b18-8f1c-4e81-8bf1-d557ccf7a063"));

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: new Guid("eac0c8a5-a1f5-426e-b168-588e690f0268"));
        }
    }
}
