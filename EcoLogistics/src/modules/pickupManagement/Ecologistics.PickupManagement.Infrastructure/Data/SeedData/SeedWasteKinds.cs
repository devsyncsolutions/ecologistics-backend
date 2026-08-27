// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.PickupManagement.Infrastructure.Data.SeedData;

internal sealed class SeedWasteKinds : IEntityTypeConfiguration<WasteKind>
{
    public void Configure(EntityTypeBuilder<WasteKind> builder)
    {
        builder.HasData(new List<WasteKind>
        {
            new()
            {
                Id = new WasteKindId(Guid.Parse("233e4567-e89b-12d3-a456-426655440000")),
                Name = "Plastic",
                CreatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                UpdatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                CreatedBy = "System",
                UpdatedBy = "System",
                IsDeleted = false
            },
            new()
            {
                Id = new WasteKindId(Guid.Parse("233e4567-e89b-12d3-a456-426655440001")),
                Name = "Glass",
                CreatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                UpdatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                CreatedBy = "System",
                UpdatedBy = "System",
                IsDeleted = false
            },
            new()
            {
                Id = new WasteKindId(Guid.Parse("233e4567-e89b-12d3-a456-426655440002")),
                Name = "Paper",
                CreatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                UpdatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                CreatedBy = "System",
                UpdatedBy = "System",
                IsDeleted = false
            },
            new()
            {
                Id = new WasteKindId(Guid.Parse("233e4567-e89b-12d3-a456-426655440003")),
                Name = "Metal",
                CreatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                UpdatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                CreatedBy = "System",
                UpdatedBy = "System",
                IsDeleted = false
            },
            new()
            {
                Id = new WasteKindId(Guid.Parse("233e4567-e89b-12d3-a456-426655440004")),
                Name = "Organic",
                CreatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                UpdatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                CreatedBy = "System",
                UpdatedBy = "System",
                IsDeleted = false
            },
            new()
            {
                Id = new WasteKindId(Guid.Parse("233e4567-e89b-12d3-a456-426655440005")),
                Name = "General",
                CreatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                UpdatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                CreatedBy = "System",
                UpdatedBy = "System",
                IsDeleted = false
            },
            new()
            {
                Id = new WasteKindId(Guid.Parse("233e4567-e89b-12d3-a456-426655440006")),
                Name = "Liquid",
                CreatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                UpdatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                CreatedBy = "System",
                UpdatedBy = "System",
                IsDeleted = false
            }
        });
    }
}
