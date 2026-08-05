// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace EcoLogistics.UserManagement.Infrastructure.Data;

public class UserManagementDbContext : IdentityDbContext<ApplicationUser>, IUserManagementDbContext
{
    public UserManagementDbContext(DbContextOptions<UserManagementDbContext> options) : base(options)
    {
    }

    public DbSet<ApplicationUser> ApplicationUsers => Set<ApplicationUser>();

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        foreach (var entityEntry in ChangeTracker.Entries<IBaseEntity>())
        {
            switch (entityEntry.State)
            {
                case EntityState.Added:
                    entityEntry.Entity.CreatedAt = DateTimeOffset.UtcNow;
                    entityEntry.Entity.CreatedBy = string.Empty;
                    break;
                case EntityState.Modified:
                    entityEntry.Entity.UpdatedAt = DateTimeOffset.UtcNow;
                    entityEntry.Entity.UpdatedBy = string.Empty;
                    break;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        ApplyDeletedFilter(builder);

        base.OnModelCreating(builder);
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
