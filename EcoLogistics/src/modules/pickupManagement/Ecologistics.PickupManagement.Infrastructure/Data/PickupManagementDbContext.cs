// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.PickupManagement.Infrastructure.Data;

public class PickupManagementDbContext :DbContext, IPickupManagementDbContext
{
    public PickupManagementDbContext(DbContextOptions<PickupManagementDbContext>  options) : base(options)
    {
    }

    public DbSet<District> Districts => Set<District>();
    public DbSet<WasteMeasurement> WasteMeasurements => Set<WasteMeasurement>();
    public DbSet<PickupFee> PickupFees => Set<PickupFee>();
    public DbSet<Pickup> Pickups => Set<Pickup>();
    public DbSet<PickupForQuery> PickupsForQuerying => Set<PickupForQuery>();
    public DbSet<WasteKind> WasteKinds => Set<WasteKind>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ArgumentNullException.ThrowIfNull(modelBuilder);

        modelBuilder.HasPostgresExtension("postgis");
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PickupManagementDbContext).Assembly);
        ApplyDeletedFilter(modelBuilder);
        base.OnModelCreating(modelBuilder);
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
