// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.PaymentManagement.Infrastructure.Data;

public sealed class PaymentManagementDbContext : DbContext, IPaymentManagementDbContext
{
    public PaymentManagementDbContext(DbContextOptions<PaymentManagementDbContext> options) : base(options)
    {
    }

    public DbSet<BalanceThresholdPerDistrict> BalanceThresholdPerDistricts => Set<BalanceThresholdPerDistrict>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ArgumentNullException.ThrowIfNull(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PaymentManagementDbContext).Assembly);
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
        ParameterExpression parameter = Expression.Parameter(entityType, "e");
        MemberExpression property = Expression.Property(parameter, nameof(IBaseEntity.IsDeleted));
        BinaryExpression isNotDeleted = Expression.Equal(property, Expression.Constant(false));
        LambdaExpression lambda = Expression.Lambda(isNotDeleted, parameter);

        modelBuilder.Entity(entityType).HasQueryFilter(lambda);
    }
}
