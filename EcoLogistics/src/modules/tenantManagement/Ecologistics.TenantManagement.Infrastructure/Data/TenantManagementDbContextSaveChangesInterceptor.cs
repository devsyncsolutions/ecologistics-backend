// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Diagnostics;
using Ecologistics.Common.Exceptions;
using Ecologistics.Common.Interfaces;
using Ecologistics.TenantManagement.UseCases.Audits.SaveUpdateAudit;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using Wolverine;

namespace Ecologistics.TenantManagement.Infrastructure.Data;

public sealed class TenantManagementDbContextSaveChangesInterceptor : SaveChangesInterceptor
{
    private readonly ILoggedInUserService _loggedInUserService;
    private readonly ILogger<TenantManagementDbContextSaveChangesInterceptor> _logger;
    private readonly IMessageBus _bus;

    public TenantManagementDbContextSaveChangesInterceptor(
        ILoggedInUserService loggedInUserService,
        ILogger<TenantManagementDbContextSaveChangesInterceptor> logger,
        IMessageBus bus)
    {
        _loggedInUserService = loggedInUserService;
        _logger = logger;
        _bus = bus;
    }

    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = new CancellationToken())
    {
        ArgumentNullException.ThrowIfNull(eventData);

        DbContext? context = await ModifyAuditableProperties(eventData).ConfigureAwait(false);
        await DispatchDomainEvents(context).ConfigureAwait(false);

        return await base.SavingChangesAsync(eventData, result, cancellationToken).ConfigureAwait(false);
    }

    private async ValueTask<DbContext?> ModifyAuditableProperties(DbContextEventData eventData)
    {
        var context = eventData.Context;

        if (context is null)
            return context;

        var entries = context.ChangeTracker.Entries<IBaseEntity>()
            .Where(e => e.State is EntityState.Added or EntityState.Deleted or EntityState.Modified)
            .ToList();

        foreach (var entry in entries)
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedAt = DateTimeOffset.UtcNow;
                    entry.Entity.CreatedBy = _loggedInUserService.GetUserId();
                    break;
                case EntityState.Deleted:
                    entry.Entity.DeletedAt = DateTimeOffset.UtcNow;
                    entry.Entity.IsDeleted = true;
                    entry.State = EntityState.Modified;
                    entry.Entity.DeletedBy = _loggedInUserService.GetUserId();
                    break;
                case EntityState.Modified:
                    entry.Entity.UpdatedAt = DateTimeOffset.UtcNow;
                    entry.Entity.UpdatedBy = _loggedInUserService.GetUserId();
                    await PublishForModificationAuditing(entry).ConfigureAwait(false);
                    break;
            }
        }

        return context;
    }

    private async Task DispatchDomainEvents(DbContext? context)
    {
        if (context is null) return;

        var aggregates = context.ChangeTracker
            .Entries<IAggregate>()
            .Where(e => e.Entity.DomainEvents.Count > 0)
            .Select(e => e.Entity)
            .ToArray();

        var domainEvents = aggregates.SelectMany(a => a.DomainEvents).ToArray();
        await _bus.PublishAsync(domainEvents).ConfigureAwait(false);

        foreach (var aggregate in aggregates)
        {
            aggregate.ClearDomainEvents();
        }
    }

    private async Task PublishForModificationAuditing(EntityEntry<IBaseEntity> entry)
    {
        var @event = new SaveUpdateAuditingCommand
        {
            UserId = _loggedInUserService.GetUserId(),
            Action = nameof(entry.State),
            EntityName = entry.Entity.GetType().Name,
            Before = entry.OriginalValues.ToObject().ToString() ?? string.Empty,
            After = entry.CurrentValues.ToObject().ToString() ?? string.Empty,
            TraceId = Activity.Current?.TraceId.ToString() ?? string.Empty
        };

        try
        {
            await _bus.PublishAsync(@event).ConfigureAwait(false);
        }
        catch (Exception e)
        {
            _logger.LogErrorMessage(
                $"Error publishing SaveUpdateAuditingCommand. Stack: {e.StackTrace} and Message: {e.Message}",
                null,
                @event.UserId,
                @event.TraceId);
        }
    }
}
