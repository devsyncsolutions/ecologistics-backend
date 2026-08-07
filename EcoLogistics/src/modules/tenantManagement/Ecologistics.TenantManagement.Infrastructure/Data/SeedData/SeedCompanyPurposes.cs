// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Globalization;

namespace Ecologistics.TenantManagement.Infrastructure.Data.SeedData;

internal sealed class SeedCompanyPurposes : IEntityTypeConfiguration<CompanyPurpose>
{
    public void Configure(EntityTypeBuilder<CompanyPurpose> builder)
    {
        builder.HasData(new List<CompanyPurpose>
        {
            new()
            {
                Id = new CompanyPurposeId(Guid.Parse("133e4567-e89b-12d3-a456-426655440000")),
                Type = CompanyPurposeType.Collector,
                CreatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                UpdatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                CreatedBy = "System",
                UpdatedBy = "System",
                IsDeleted = false,
            },

            new()
            {
                Id = new CompanyPurposeId(Guid.Parse("233e4567-e89b-12d3-a456-426655440000")),
                Type = CompanyPurposeType.Processor,
                CreatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                UpdatedAt = DateTimeOffset.Parse("Thu, 06 Aug 2026 20:22:30 GMT", CultureInfo.InvariantCulture),
                CreatedBy = "System",
                UpdatedBy = "System",
                IsDeleted = false,
            }
        });
    }
}
