// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.TenantManagement.Infrastructure.Data;

public class TenantManagementDbContext : DbContext, ITenantManagementDbContext
{
    public TenantManagementDbContext(DbContextOptions<TenantManagementDbContext> options) : base(options)
    {
    }

    public DbSet<Tenant> Tenants => Set<Tenant>();
    public DbSet<Member> Members => Set<Member>();
    public DbSet<TenantCompanyPurpose> TenantCompanyPurposes => Set<TenantCompanyPurpose>();
    public DbSet<CompanyPurpose> CompanyPurposes => Set<CompanyPurpose>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ArgumentNullException.ThrowIfNull(modelBuilder);

        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TenantManagementDbContext).Assembly);
        ApplyDeletedFilter(modelBuilder);
    }

    private static void ApplyDeletedFilter(ModelBuilder modelBuilder)
    {
        modelBuilder.Model.GetEntityTypes()
            .Select(entityType => entityType.ClrType)
            .Where(entityType => typeof(IBaseEntity).IsAssignableFrom(entityType))
            .ToList()
            .ForEach(entityType => ApplyDeletedFilterInternal(modelBuilder, entityType));
    }

    private static void ApplyDeletedFilterInternal(ModelBuilder modelBuilder, Type entityType)
    {
        var parameter = Expression.Parameter(entityType, "e");
        var property = Expression.Property(parameter, nameof(IBaseEntity.IsDeleted));
        var isNotDeleted = Expression.Equal(property, Expression.Constant(false));
        var lambda = Expression.Lambda(isNotDeleted, parameter);

        modelBuilder.Entity(entityType).HasQueryFilter(lambda);
    }
}
