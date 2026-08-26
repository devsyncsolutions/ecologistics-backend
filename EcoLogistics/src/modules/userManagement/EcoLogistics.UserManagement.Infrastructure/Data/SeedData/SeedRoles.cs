// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Ecologistics.Common.Enums;
using Microsoft.AspNetCore.Identity;

namespace EcoLogistics.UserManagement.Infrastructure.Data.SeedData;

public sealed class SeedRoles : IEntityTypeConfiguration<IdentityRole>
{
    public void Configure(EntityTypeBuilder<IdentityRole> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.HasData(new List<IdentityRole>
        {
            new()
            {
                Name = nameof(AccountType.Collector),
                NormalizedName = nameof(AccountType.Collector).ToUpperInvariant()
            }, new()
            {
                Name = nameof(AccountType.Generator),
                NormalizedName = nameof(AccountType.Generator).ToUpperInvariant()
            }
        });
    }
}
