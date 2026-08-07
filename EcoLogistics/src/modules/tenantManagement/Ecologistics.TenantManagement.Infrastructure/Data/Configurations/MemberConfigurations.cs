// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.TenantManagement.Infrastructure.Data.Configurations;

internal sealed class MemberConfigurations : IEntityTypeConfiguration<Member>
{
    public void Configure(EntityTypeBuilder<Member> builder)
    {
        builder.ToTable("Members");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasConversion(memberId => memberId.Value, dbId => new MemberId(dbId));

        builder.Property(x => x.IsOwner)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(x => x.TenantId)
            .IsRequired();
        builder.HasIndex(x => x.TenantId);

        builder.Property(x => x.UserId)
            .IsRequired();
        builder.HasIndex(x => x.UserId).IsUnique();

        builder.HasIndex(x => x.IsDeleted);
    }
}
