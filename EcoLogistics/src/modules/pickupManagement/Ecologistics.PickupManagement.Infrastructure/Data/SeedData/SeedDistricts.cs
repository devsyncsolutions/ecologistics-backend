// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.PickupManagement.Infrastructure.Data.SeedData;

internal sealed class SeedDistricts : IEntityTypeConfiguration<District>
{
    public void Configure(EntityTypeBuilder<District> builder)
    {
        builder.HasData(
            new List<District>
            {
                new ()
                {
                    Id = new DistrictId(Guid.Parse("233e4567-e89b-12d3-a456-426655440000")),
                    Name = "Akuapim South (New)",
                    Description = "",
                    Region = Region.Eastern,
                    CreatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    UpdatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    CreatedBy = "System",
                    UpdatedBy = "System",
                    IsDeleted = false
                },
                new ()
                {
                    Id = new DistrictId(Guid.Parse("333e4567-e89b-12d3-a456-426655440000")),
                    Name = "Abura/Asebu/Kwamankese",
                    Description = "",
                    Region = Region.Central,
                    CreatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    UpdatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    CreatedBy = "System",
                    UpdatedBy = "System",
                    IsDeleted = false
                },
                new ()
                {
                    Id = new DistrictId(Guid.Parse("133e4567-e89b-12d3-a456-426655440000")),
                    Name = "Accra Metropolitan",
                    Description = "",
                    Region = Region.GreaterAccra,
                    CreatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    UpdatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    CreatedBy = "System",
                    UpdatedBy = "System",
                    IsDeleted = false
                },
                new ()
                {
                    Id = new DistrictId(Guid.Parse("433e4567-e89b-12d3-a456-426655440000")),
                    Name = "Ada West (New)",
                    Description = "",
                    Region = Region.GreaterAccra,
                    CreatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    UpdatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    CreatedBy = "System",
                    UpdatedBy = "System",
                    IsDeleted = false
                },
                new ()
                {
                    Id = new DistrictId(Guid.Parse("533e4567-e89b-12d3-a456-426655440000")),
                    Name = "Adaklu (New)",
                    Description = "",
                    Region = Region.Volta,
                    CreatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    UpdatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    CreatedBy = "System",
                    UpdatedBy = "System",
                    IsDeleted = false
                },
                new ()
                {
                    Id = new DistrictId(Guid.Parse("633e4567-e89b-12d3-a456-426655440000")),
                    Name = "Adansi North",
                    Description = "",
                    Region = Region.Ashanti,
                    CreatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    UpdatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    CreatedBy = "System",
                    UpdatedBy = "System",
                    IsDeleted = false
                },
                new ()
                {
                    Id = new DistrictId(Guid.Parse("733e4567-e89b-12d3-a456-426655440000")),
                    Name = "Adansi South",
                    Description = "",
                    Region = Region.Ashanti,
                    CreatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    UpdatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    CreatedBy = "System",
                    UpdatedBy = "System",
                    IsDeleted = false
                },
                new ()
                {
                    Id = new DistrictId(Guid.Parse("833e4567-e89b-12d3-a456-426655440000")),
                    Name = "Adentan municipal",
                    Description = "",
                    Region = Region.GreaterAccra,
                    CreatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    UpdatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    CreatedBy = "System",
                    UpdatedBy = "System",
                    IsDeleted = false
                },
                new ()
                {
                    Id = new DistrictId(Guid.Parse("933e4567-e89b-12d3-a456-426655440000")),
                    Name = "Afadjato (New)",
                    Description = "",
                    Region = Region.Volta,
                    CreatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    UpdatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    CreatedBy = "System",
                    UpdatedBy = "System",
                    IsDeleted = false
                },
                new ()
                {
                    Id = new DistrictId(Guid.Parse("913e4567-e89b-12d3-a456-426655440000")),
                    Name = "Afigya-Kwabre",
                    Description = "",
                    Region = Region.Ashanti,
                    CreatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    UpdatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    CreatedBy = "System",
                    UpdatedBy = "System",
                    IsDeleted = false
                },
                new ()
                {
                    Id = new DistrictId(Guid.Parse("923e4567-e89b-12d3-a456-426655440000")),
                    Name = "Afram Plains South (New)",
                    Description = "",
                    Region = Region.Eastern,
                    CreatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    UpdatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    CreatedBy = "System",
                    UpdatedBy = "System",
                    IsDeleted = false
                },
                new ()
                {
                    Id = new DistrictId(Guid.Parse("943e4567-e89b-12d3-a456-426655440000")),
                    Name = "Agona East",
                    Description = "",
                    Region = Region.Central,
                    CreatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    UpdatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    CreatedBy = "System",
                    UpdatedBy = "System",
                    IsDeleted = false
                },
                new ()
                {
                    Id = new DistrictId(Guid.Parse("953e4567-e89b-12d3-a456-426655440000")),
                    Name = "Agona West Municipal",
                    Description = "",
                    Region = Region.Central,
                    CreatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    UpdatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    CreatedBy = "System",
                    UpdatedBy = "System",
                    IsDeleted = false
                },
                new ()
                {
                    Id = new DistrictId(Guid.Parse("963e4567-e89b-12d3-a456-426655440000")),
                    Name = "Agotime Ziope",
                    Description = "",
                    Region = Region.Volta,
                    CreatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    UpdatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    CreatedBy = "System",
                    UpdatedBy = "System",
                    IsDeleted = false
                },
                new ()
                {
                    Id = new DistrictId(Guid.Parse("973e4567-e89b-12d3-a456-426655440000")),
                    Name = "Ahafo Ano North",
                    Description = "",
                    Region = Region.Ashanti,
                    CreatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    UpdatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    CreatedBy = "System",
                    UpdatedBy = "System",
                    IsDeleted = false
                },
                new ()
                {
                    Id = new DistrictId(Guid.Parse("983e4567-e89b-12d3-a456-426655440000")),
                    Name = "Ahafo Ano South",
                    Description = "",
                    Region = Region.Ashanti,
                    CreatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    UpdatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    CreatedBy = "System",
                    UpdatedBy = "System",
                    IsDeleted = false
                },
                new ()
                {
                    Id = new DistrictId(Guid.Parse("993e4567-e89b-12d3-a456-426655440000")),
                    Name = "Ahanta West",
                    Description = "",
                    Region = Region.Western,
                    CreatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    UpdatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    CreatedBy = "System",
                    UpdatedBy = "System",
                    IsDeleted = false
                },
                new ()
                {
                    Id = new DistrictId(Guid.Parse("991e4567-e89b-12d3-a456-426655440000")),
                    Name = "Ajumako/Enyan/Essiam",
                    Description = "",
                    Region = Region.Central,
                    CreatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    UpdatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    CreatedBy = "System",
                    UpdatedBy = "System",
                    IsDeleted = false
                },
                new ()
                {
                    Id = new DistrictId(Guid.Parse("992e4567-e89b-12d3-a456-426655440000")),
                    Name = "Akatsi North (New)",
                    Description = "",
                    Region = Region.Volta,
                    CreatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    UpdatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    CreatedBy = "System",
                    UpdatedBy = "System",
                    IsDeleted = false
                },
                new ()
                {
                    Id = new DistrictId(Guid.Parse("994e4567-e89b-12d3-a456-426655440000")),
                    Name = "Akatsi South",
                    Description = "",
                    Region = Region.Volta,
                    CreatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    UpdatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    CreatedBy = "System",
                    UpdatedBy = "System",
                    IsDeleted = false
                },
                new ()
                {
                    Id = new DistrictId(Guid.Parse("995e4567-e89b-12d3-a456-426655440000")),
                    Name = "Akuapim North",
                    Description = "",
                    Region = Region.Eastern,
                    CreatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    UpdatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    CreatedBy = "System",
                    UpdatedBy = "System",
                    IsDeleted = false
                },
                new ()
                {
                    Id = new DistrictId(Guid.Parse("996e4567-e89b-12d3-a456-426655440000")),
                    Name = "Akyemansa",
                    Description = "",
                    Region = Region.Eastern,
                    CreatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    UpdatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    CreatedBy = "System",
                    UpdatedBy = "System",
                    IsDeleted = false
                },
                new ()
                {
                    Id = new DistrictId(Guid.Parse("997e4567-e89b-12d3-a456-426655440000")),
                    Name = "Amansie Central",
                    Description = "",
                    Region = Region.Ashanti,
                    CreatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    UpdatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    CreatedBy = "System",
                    UpdatedBy = "System",
                    IsDeleted = false
                },
                new ()
                {
                    Id = new DistrictId(Guid.Parse("3786daa1-7cd0-46af-b340-66a85f0858b8")),
                    Name = "Amansie West",
                    Description = "",
                    Region = Region.Ashanti,
                    CreatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    UpdatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    CreatedBy = "System",
                    UpdatedBy = "System",
                    IsDeleted = false
                },
                new ()
                {
                    Id = new DistrictId(Guid.Parse("57fbe8c0-d4e7-4049-a06f-b93174f012df")),
                    Name = "Aowin",
                    Description = "",
                    Region = Region.Western,
                    CreatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    UpdatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    CreatedBy = "System",
                    UpdatedBy = "System",
                    IsDeleted = false
                },
                new ()
                {
                    Id = new DistrictId(Guid.Parse("eac0c8a5-a1f5-426e-b168-588e690f0268")),
                    Name = "Asante Akim Central Municipal",
                    Description = "",
                    Region = Region.Ashanti,
                    CreatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    UpdatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    CreatedBy = "System",
                    UpdatedBy = "System",
                    IsDeleted = false
                },
                new ()
                {
                    Id = new DistrictId(Guid.Parse("242ad89b-3310-4639-87a6-48f4f3962b1b")),
                    Name = "Asante Akim North (New)",
                    Description = "",
                    Region = Region.Ashanti,
                    CreatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    UpdatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    CreatedBy = "System",
                    UpdatedBy = "System",
                    IsDeleted = false
                },
                new ()
                {
                    Id = new DistrictId(Guid.Parse("4d146b19-e199-438b-8263-29f949196abb")),
                    Name = "Asante Akim South",
                    Description = "",
                    Region = Region.Ashanti,
                    CreatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    UpdatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    CreatedBy = "System",
                    UpdatedBy = "System",
                    IsDeleted = false
                },
                new ()
                {
                    Id = new DistrictId(Guid.Parse("af3184c3-41fe-4fe1-ad9f-2ce76a1ac496")),
                    Name = "Ashaiman Municipal",
                    Description = "",
                    Region = Region.GreaterAccra,
                    CreatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    UpdatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    CreatedBy = "System",
                    UpdatedBy = "System",
                    IsDeleted = false
                },
                new ()
                {
                    Id = new DistrictId(Guid.Parse("5324b9be-cede-4b7d-a54d-021f019a21e2")),
                    Name = "Asikuma/Odoben/Brakwa",
                    Description = "",
                    Region = Region.Central,
                    CreatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    UpdatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    CreatedBy = "System",
                    UpdatedBy = "System",
                    IsDeleted = false
                },
                new ()
                {
                    Id = new DistrictId(Guid.Parse("54538b6d-8014-49a8-aeaa-411da20696bc")),
                    Name = "Asokore Mampong Municipal (New)",
                    Description = "",
                    Region = Region.Ashanti,
                    CreatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    UpdatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    CreatedBy = "System",
                    UpdatedBy = "System",
                    IsDeleted = false
                },
                new ()
                {
                    Id = new DistrictId(Guid.Parse("e7e2e24c-851e-42cb-b6f7-641a118fa31c")),
                    Name = "Assin North Municipal",
                    Description = "",
                    Region = Region.Central,
                    CreatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    UpdatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    CreatedBy = "System",
                    UpdatedBy = "System",
                    IsDeleted = false
                },
                new ()
                {
                    Id = new DistrictId(Guid.Parse("613669eb-6dd2-44a3-93a5-feaf3126227c")),
                    Name = "Assin South",
                    Description = "",
                    Region = Region.Central,
                    CreatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    UpdatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    CreatedBy = "System",
                    UpdatedBy = "System",
                    IsDeleted = false
                },
                new ()
                {
                    Id = new DistrictId(Guid.Parse("15ef1bd2-727e-4571-b170-4424cfdb60d4")),
                    Name = "Asunafo North Municipal",
                    Description = "",
                    Region = Region.BrongAhafo,
                    CreatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    UpdatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    CreatedBy = "System",
                    UpdatedBy = "System",
                    IsDeleted = false
                },
                new ()
                {
                    Id = new DistrictId(Guid.Parse("aefdcca4-fab8-439a-ba2e-64c9a2e6a076")),
                    Name = "Asunafo South",
                    Description = "",
                    Region = Region.BrongAhafo,
                    CreatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    UpdatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    CreatedBy = "System",
                    UpdatedBy = "System",
                    IsDeleted = false
                },
                new ()
                {
                    Id = new DistrictId(Guid.Parse("3413c566-98ba-470b-a8c5-802258b8c582")),
                    Name = "Asuogyaman",
                    Description = "",
                    Region = Region.Eastern,
                    CreatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    UpdatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    CreatedBy = "System",
                    UpdatedBy = "System",
                    IsDeleted = false
                },
                new ()
                {
                    Id = new DistrictId(Guid.Parse("05595fe0-2420-40b8-ae08-5d2892c4ab59")),
                    Name = "Asutifi",
                    Description = "",
                    Region = Region.BrongAhafo,
                    CreatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    UpdatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    CreatedBy = "System",
                    UpdatedBy = "System",
                    IsDeleted = false
                },
                new ()
                {
                    Id = new DistrictId(Guid.Parse("2a6965ad-58c5-43b7-8179-9d6fa1237fe9")),
                    Name = "Asutifi South (New)",
                    Description = "",
                    Region = Region.BrongAhafo,
                    CreatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    UpdatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    CreatedBy = "System",
                    UpdatedBy = "System",
                    IsDeleted = false
                },
                new ()
                {
                    Id = new DistrictId(Guid.Parse("39c14664-6994-4cb3-8ace-3a94752e4bed")),
                    Name = "Atebubu-Amantin Municipal",
                    Description = "",
                    Region = Region.BrongAhafo,
                    CreatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    UpdatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    CreatedBy = "System",
                    UpdatedBy = "System",
                    IsDeleted = false
                },
                new ()
                {
                    Id = new DistrictId(Guid.Parse("b665a389-f026-4a58-9029-14261aba355f")),
                    Name = "Atiwa",
                    Description = "",
                    Region = Region.Eastern,
                    CreatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    UpdatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    CreatedBy = "System",
                    UpdatedBy = "System",
                    IsDeleted = false
                },
                new ()
                {
                    Id = new DistrictId(Guid.Parse("de431fe6-514c-4d25-b9e4-7d6bcb5cd0ec")),
                    Name = "Atwima Kwanwoma",
                    Description = "",
                    Region = Region.Ashanti,
                    CreatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    UpdatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    CreatedBy = "System",
                    UpdatedBy = "System",
                    IsDeleted = false
                },
                new ()
                {
                    Id = new DistrictId(Guid.Parse("55d9f466-e8ea-4b57-8e85-64a19c2c6654")),
                    Name = "Atwima Mponua",
                    Description = "",
                    Region = Region.Ashanti,
                    CreatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    UpdatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    CreatedBy = "System",
                    UpdatedBy = "System",
                    IsDeleted = false
                },
                new ()
                {
                    Id = new DistrictId(Guid.Parse("463cf0a5-523a-40b8-98ec-b225b5947018")),
                    Name = "Atwima Nwabiagya",
                    Description = "",
                    Region = Region.Ashanti,
                    CreatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    UpdatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    CreatedBy = "System",
                    UpdatedBy = "System",
                    IsDeleted = false
                },
                new ()
                {
                    Id = new DistrictId(Guid.Parse("a67954a3-c4dd-4367-97cd-4074486605fd")),
                    Name = "Awutu-Senya",
                    Description = "",
                    Region = Region.Central,
                    CreatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    UpdatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    CreatedBy = "System",
                    UpdatedBy = "System",
                    IsDeleted = false
                },
                new ()
                {
                    Id = new DistrictId(Guid.Parse("a4a740b6-9c16-40e4-b40e-dd9ea2e4b93f")),
                    Name = "Awutu-Senya-East (New)",
                    Description = "",
                    Region = Region.Central,
                    CreatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    UpdatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    CreatedBy = "System",
                    UpdatedBy = "System",
                    IsDeleted = false
                },
                new ()
                {
                    Id = new DistrictId(Guid.Parse("64c4102b-62b0-4022-bb0f-521c972b85e2")),
                    Name = "Ayensuano (New)",
                    Description = "",
                    Region = Region.Eastern,
                    CreatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    UpdatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    CreatedBy = "System",
                    UpdatedBy = "System",
                    IsDeleted = false
                },
                new ()
                {
                    Id = new DistrictId(Guid.Parse("754fea2f-8897-4a68-85b7-b7f7f79c08ae")),
                    Name = "Banda (New)",
                    Description = "",
                    Region = Region.BrongAhafo,
                    CreatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    UpdatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    CreatedBy = "System",
                    UpdatedBy = "System",
                    IsDeleted = false
                },
                new ()
                {
                    Id = new DistrictId(Guid.Parse("32337e7b-c92d-4664-a918-2b72ef9dd1cd")),
                    Name = "Bawku Municipal",
                    Description = "",
                    Region = Region.UpperEast,
                    CreatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    UpdatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    CreatedBy = "System",
                    UpdatedBy = "System",
                    IsDeleted = false
                },
                new ()
                {
                    Id = new DistrictId(Guid.Parse("d4a8f69a-4f24-4e89-a69b-416c0fe77e67")),
                    Name = "Bawku West",
                    Description = "",
                    Region = Region.UpperEast,
                    CreatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    UpdatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    CreatedBy = "System",
                    UpdatedBy = "System",
                    IsDeleted = false
                },
                new ()
                {
                    Id = new DistrictId(Guid.Parse("5d726208-258c-4702-bcb8-80a62836f34f")),
                    Name = "Bekwai Municipal",
                    Description = "",
                    Region = Region.Ashanti,
                    CreatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    UpdatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    CreatedBy = "System",
                    UpdatedBy = "System",
                    IsDeleted = false
                },
                new ()
                {
                    Id = new DistrictId(Guid.Parse("cffa99c0-1de4-4867-b9dd-5f5f468c064f")),
                    Name = "Berekum Municipal",
                    Description = "",
                    Region = Region.BrongAhafo,
                    CreatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    UpdatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    CreatedBy = "System",
                    UpdatedBy = "System",
                    IsDeleted = false
                },
                new ()
                {
                    Id = new DistrictId(Guid.Parse("0ce742fd-e155-43d1-9c36-342ce2c15b76")),
                    Name = "Bia",
                    Description = "",
                    Region = Region.Western,
                    CreatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    UpdatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    CreatedBy = "System",
                    UpdatedBy = "System",
                    IsDeleted = false
                },
                new ()
                {
                    Id = new DistrictId(Guid.Parse("3c50877d-0674-457e-bbd0-f83b64752a4d")),
                    Name = "Bia East (New)",
                    Description = "",
                    Region = Region.Western,
                    CreatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    UpdatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    CreatedBy = "System",
                    UpdatedBy = "System",
                    IsDeleted = false
                },
                new ()
                {
                    Id = new DistrictId(Guid.Parse("51f3d516-bbc9-4b75-b752-b06237590d11")),
                    Name = "Biakoye",
                    Description = "",
                    Region = Region.Volta,
                    CreatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    UpdatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    CreatedBy = "System",
                    UpdatedBy = "System",
                    IsDeleted = false
                },
                new ()
                {
                    Id = new DistrictId(Guid.Parse("2992905a-274b-4be7-8c09-3282b11c5af1")),
                    Name = "Bibiani/Anhwiaso/Bekwai",
                    Description = "",
                    Region = Region.Western,
                    CreatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    UpdatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    CreatedBy = "System",
                    UpdatedBy = "System",
                    IsDeleted = false
                },
                new ()
                {
                    Id = new DistrictId(Guid.Parse("3fbdbb37-b2d3-4b3c-8eb3-a5a70bfc9c13")),
                    Name = "Binduri (New)",
                    Description = "",
                    Region = Region.UpperEast,
                    CreatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    UpdatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    CreatedBy = "System",
                    UpdatedBy = "System",
                    IsDeleted = false
                },
                new ()
                {
                    Id = new DistrictId(Guid.Parse("66733647-073c-4a15-b792-3fb8a1e7d237")),
                    Name = "Birim Central Municipal",
                    Description = "",
                    Region = Region.Eastern,
                    CreatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    UpdatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    CreatedBy = "System",
                    UpdatedBy = "System",
                    IsDeleted = false
                },
                new ()
                {
                    Id = new DistrictId(Guid.Parse("e711560c-9725-430e-b00f-6af67223b9a8")),
                    Name = "Birim North",
                    Description = "",
                    Region = Region.Eastern,
                    CreatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    UpdatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    CreatedBy = "System",
                    UpdatedBy = "System",
                    IsDeleted = false
                },
                new ()
                {
                    Id = new DistrictId(Guid.Parse("5fb54aca-7428-43eb-bdb7-ab9cdb17e416")),
                    Name = "Birim South",
                    Description = "",
                    Region = Region.Eastern,
                    CreatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    UpdatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    CreatedBy = "System",
                    UpdatedBy = "System",
                    IsDeleted = false
                },
                new ()
                {
                    Id = new DistrictId(Guid.Parse("68dacefa-24c7-4eb0-8be1-d8581b24de27")),
                    Name = "Bodi (New)",
                    Description = "",
                    Region = Region.Western,
                    CreatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    UpdatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    CreatedBy = "System",
                    UpdatedBy = "System",
                    IsDeleted = false
                },
                new ()
                {
                    Id = new DistrictId(Guid.Parse("144d7559-82d6-4abd-a8df-e62f5bb473e0")),
                    Name = "Bole",
                    Description = "",
                    Region = Region.Northern,
                    CreatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    UpdatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    CreatedBy = "System",
                    UpdatedBy = "System",
                    IsDeleted = false
                },
                new ()
                {
                    Id = new DistrictId(Guid.Parse("9e6e6e22-d2bf-4a7f-8c5c-c68c2e458916")),
                    Name = "Bolgatanga Municipal",
                    Description = "",
                    Region = Region.UpperEast,
                    CreatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    UpdatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    CreatedBy = "System",
                    UpdatedBy = "System",
                    IsDeleted = false
                },
                new ()
                {
                    Id = new DistrictId(Guid.Parse("a3f9d78c-379f-4f78-8f39-532e91508338")),
                    Name = "Bongo",
                    Description = "",
                    Region = Region.UpperEast,
                    CreatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    UpdatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    CreatedBy = "System",
                    UpdatedBy = "System",
                    IsDeleted = false
                },
                new ()
                {
                    Id = new DistrictId(Guid.Parse("c4c80a74-8896-4c1a-9347-486a79815cf4")),
                    Name = "Bosome Freho",
                    Description = "",
                    Region = Region.Ashanti,
                    CreatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    UpdatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    CreatedBy = "System",
                    UpdatedBy = "System",
                    IsDeleted = false
                },
                new ()
                {
                    Id = new DistrictId(Guid.Parse("db352e37-073e-42cc-b143-b7a00efbbc68")),
                    Name = "Bosomtwe",
                    Description = "",
                    Region = Region.Ashanti,
                    CreatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    UpdatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    CreatedBy = "System",
                    UpdatedBy = "System",
                    IsDeleted = false
                },
                new ()
                {
                    Id = new DistrictId(Guid.Parse("bf0ba27b-44d6-4e87-a29e-b17dc1ce2177")),
                    Name = "Builsa",
                    Description = "",
                    Region = Region.UpperEast,
                    CreatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    UpdatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    CreatedBy = "System",
                    UpdatedBy = "System",
                    IsDeleted = false
                },
                new ()
                {
                    Id = new DistrictId(Guid.Parse("3644166e-3913-41d3-a2c7-6714fdd18eee")),
                    Name = "Builsa South (New)",
                    Description = "",
                    Region = Region.UpperEast,
                    CreatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    UpdatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    CreatedBy = "System",
                    UpdatedBy = "System",
                    IsDeleted = false
                },
                new ()
                {
                    Id = new DistrictId(Guid.Parse("cecaa704-88cd-4323-aa40-d4b93d655441")),
                    Name = "Bunkpurugu-Yunyoo",
                    Description = "",
                    Region = Region.Northern,
                    CreatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    UpdatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    CreatedBy = "System",
                    UpdatedBy = "System",
                    IsDeleted = false
                },
                new ()
                {
                    Id = new DistrictId(Guid.Parse("902d057b-e871-4382-aec4-1154baa97f40")),
                    Name = "Cape Coast Metropolitan",
                    Description = "",
                    Region = Region.Central,
                    CreatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    UpdatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    CreatedBy = "System",
                    UpdatedBy = "System",
                    IsDeleted = false
                },
                new ()
                {
                    Id = new DistrictId(Guid.Parse("b35e4afe-2659-4afe-a52e-8411f9d4ce56")),
                    Name = "Central Gonja",
                    Description = "",
                    Region = Region.Northern,
                    CreatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    UpdatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    CreatedBy = "System",
                    UpdatedBy = "System",
                    IsDeleted = false
                },
                new ()
                {
                    Id = new DistrictId(Guid.Parse("ea663b18-8f1c-4e81-8bf1-d557ccf7a063")),
                    Name = "Central Tongu",
                    Description = "",
                    Region = Region.Volta,
                    CreatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    UpdatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    CreatedBy = "System",
                    UpdatedBy = "System",
                    IsDeleted = false
                },
                new ()
                {
                    Id = new DistrictId(Guid.Parse("afdd8184-13b7-4a66-8c7d-d2c25153605c")),
                    Name = "Chereponi",
                    Description = "",
                    Region = Region.Northern,
                    CreatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    UpdatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    CreatedBy = "System",
                    UpdatedBy = "System",
                    IsDeleted = false
                },
                new ()
                {
                    Id = new DistrictId(Guid.Parse("443b507d-1915-4335-ab53-7f738bf6475b")),
                    Name = "Daffiama Bussie Issa (New)",
                    Description = "",
                    Region = Region.UpperWest,
                    CreatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    UpdatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    CreatedBy = "System",
                    UpdatedBy = "System",
                    IsDeleted = false
                },
                new ()
                {
                    Id = new DistrictId(Guid.Parse("d328b881-9cae-43f1-9185-e5a0b4bcf964")),
                    Name = "Dangme East",
                    Description = "",
                    Region = Region.GreaterAccra,
                    CreatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    UpdatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    CreatedBy = "System",
                    UpdatedBy = "System",
                    IsDeleted = false
                },
                new ()
                {
                    Id = new DistrictId(Guid.Parse("57da89bb-1428-4bbd-9730-b4eaa3f4143e")),
                    Name = "Dangme West",
                    Description = "",
                    Region = Region.GreaterAccra,
                    CreatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    UpdatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    CreatedBy = "System",
                    UpdatedBy = "System",
                    IsDeleted = false
                },
                new ()
                {
                    Id = new DistrictId(Guid.Parse("a4c7b800-315c-461f-8ae6-59b050e2b39d")),
                    Name = "Denkyembour (New)",
                    Description = "",
                    Region = Region.Eastern,
                    CreatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    UpdatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    CreatedBy = "System",
                    UpdatedBy = "System",
                    IsDeleted = false
                },
                new ()
                {
                    Id = new DistrictId(Guid.Parse("6177dc24-5c19-46be-895d-5aac86eba920")),
                    Name = "Dormaa East",
                    Description = "",
                    Region = Region.BrongAhafo,
                    CreatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    UpdatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    CreatedBy = "System",
                    UpdatedBy = "System",
                    IsDeleted = false
                },
                new ()
                {
                    Id = new DistrictId(Guid.Parse("9e08ff26-610b-43b3-8314-e0ff7d7c4b47")),
                    Name = "Dormaa Municipal",
                    Description = "",
                    Region = Region.BrongAhafo,
                    CreatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    UpdatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    CreatedBy = "System",
                    UpdatedBy = "System",
                    IsDeleted = false
                },
                new ()
                {
                    Id = new DistrictId(Guid.Parse("81d29506-e56f-42d4-9639-6e7eb25e632a")),
                    Name = "Dormaa West (New)",
                    Description = "",
                    Region = Region.BrongAhafo,
                    CreatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    UpdatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    CreatedBy = "System",
                    UpdatedBy = "System",
                    IsDeleted = false
                },
                new ()
                {
                    Id = new DistrictId(Guid.Parse("140f223e-1cd5-4b6c-9830-dd66262588c3")),
                    Name = "East Akim Municipal",
                    Description = "",
                    Region = Region.Eastern,
                    CreatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    UpdatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    CreatedBy = "System",
                    UpdatedBy = "System",
                    IsDeleted = false
                },
                new ()
                {
                    Id = new DistrictId(Guid.Parse("006e415f-b2d2-4c0c-baa6-180741f4d2dd")),
                    Name = "East Gonja",
                    Description = "",
                    Region = Region.Northern,
                    CreatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    UpdatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    CreatedBy = "System",
                    UpdatedBy = "System",
                    IsDeleted = false
                },
                new ()
                {
                    Id = new DistrictId(Guid.Parse("d691e123-5b27-4798-8bb0-e7e65e5c0587")),
                    Name = "East Mamprusi",
                    Description = "",
                    Region = Region.Northern,
                    CreatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    UpdatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    CreatedBy = "System",
                    UpdatedBy = "System",
                    IsDeleted = false
                },
                new ()
                {
                    Id = new DistrictId(Guid.Parse("5ab85f0e-7524-4c47-961e-64fbccc187be")),
                    Name = "Effutu Municipal",
                    Description = "",
                    Region = Region.Central,
                    CreatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    UpdatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    CreatedBy = "System",
                    UpdatedBy = "System",
                    IsDeleted = false
                },
                new ()
                {
                    Id = new DistrictId(Guid.Parse("9f2c294c-1590-47fb-b048-861712c10e81")),
                    Name = "Ejisu-Juaben Municipal",
                    Description = "",
                    Region = Region.Ashanti,
                    CreatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    UpdatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    CreatedBy = "System",
                    UpdatedBy = "System",
                    IsDeleted = false
                },
                new ()
                {
                    Id = new DistrictId(Guid.Parse("51242457-2646-4768-85b1-d569a4f42d9f")),
                    Name = "Ejura/Sekyedumase",
                    Description = "",
                    Region = Region.Ashanti,
                    CreatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    UpdatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    CreatedBy = "System",
                    UpdatedBy = "System",
                    IsDeleted = false
                },
                new ()
                {
                    Id = new DistrictId(Guid.Parse("747b6c16-6e5b-494b-9d96-e7cf7de49612")),
                    Name = "Ekumfi (New)",
                    Description = "",
                    Region = Region.Central,
                    CreatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    UpdatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    CreatedBy = "System",
                    UpdatedBy = "System",
                    IsDeleted = false
                },
                new ()
                {
                    Id = new DistrictId(Guid.Parse("623f9133-7bf1-4e53-91a9-deb98f7a725f")),
                    Name = "Ellembele",
                    Description = "",
                    Region = Region.Western,
                    CreatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    UpdatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    CreatedBy = "System",
                    UpdatedBy = "System",
                    IsDeleted = false
                },
                new ()
                {
                    Id = new DistrictId(Guid.Parse("428ab56a-236c-4b55-9666-199a024020f9")),
                    Name = "Fanteakwa",
                    Description = "",
                    Region = Region.Eastern,
                    CreatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    UpdatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    CreatedBy = "System",
                    UpdatedBy = "System",
                    IsDeleted = false
                },
                new ()
                {
                    Id = new DistrictId(Guid.Parse("7ff9d3b2-d348-4f6b-82e6-3aed7110d377")),
                    Name = "Ga Central (New)",
                    Description = "",
                    Region = Region.GreaterAccra,
                    CreatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    UpdatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    CreatedBy = "System",
                    UpdatedBy = "System",
                    IsDeleted = false
                },
                new ()
                {
                    Id = new DistrictId(Guid.Parse("cabea05a-99f1-4e95-9429-d520f6da028e")),
                    Name = "Ga East Municipal",
                    Description = "",
                    Region = Region.GreaterAccra,
                    CreatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    UpdatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                    CreatedBy = "System",
                    UpdatedBy = "System",
                    IsDeleted = false
                }
            }
        );
    }
}
