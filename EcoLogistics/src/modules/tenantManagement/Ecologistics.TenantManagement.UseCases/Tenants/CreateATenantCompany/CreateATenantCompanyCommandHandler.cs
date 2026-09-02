// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.TenantManagement.UseCases.Tenants.CreateATenantCompany;

public class CreateATenantCompanyCommandHandler(
    ITenantManagementDbContext dbContext,
    ILogger<CreateATenantCompanyCommandHandler> logger,
    IFusionCache cache)
{
    public async Task<CreateATenantCompanyCommandResponse> Handle(CreateATenantCompanyCommand command,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(command);

        var validation = new CreateATenantCompanyCommandValidator();
        var validationResult = await validation.ValidateAsync(command, cancellationToken).ConfigureAwait(false);
        if (!validationResult.IsValid) throw new ValidationException(validationResult.Errors);

        var requestResponse =  await cache.GetOrSetAsync(
            CacheKeys.GetRequestKey<CreateATenantCompanyCommand>(command.RequestId),
            async _ => await CreateTenantAsync(dbContext, logger, command, cancellationToken).ConfigureAwait(false),
            TimeSpan.FromMinutes(30),
            cancellationToken).ConfigureAwait(false);

        return requestResponse;
    }

    private static async Task<CreateATenantCompanyCommandResponse> CreateTenantAsync(
        ITenantManagementDbContext dbContext,
        ILogger<CreateATenantCompanyCommandHandler> logger, CreateATenantCompanyCommand command,
        CancellationToken cancellationToken)
    {
        List<CompanyPurposeId> companyPurposeIds = [];

        if (command.CompanyPurposes is not null)
        {
            companyPurposeIds = command.CompanyPurposes.Select(p => new CompanyPurposeId(Guid.Parse(p))).ToList();
        }
        else
        {
            var collectorPurposeId = await dbContext
                .CompanyPurposes
                .Where(p => p.Type == CompanyPurposeType.Collector)
                .Select(p => p.Id)
                .FirstOrDefaultAsync(cancellationToken)
                .ConfigureAwait(false);

            if (collectorPurposeId is null)
            {
                logger.LogErrorMessage(
                    "Collector company purpose not loaded in database.",
                    null,
                    null,
                    null);

                throw new BadRequestException("Collector company purpose not found.");
            }

            companyPurposeIds.Add(collectorPurposeId);
        }

        var newTenant = Tenant.Create(
            companyName: command.CompanyName,
            logoUrl: null,
            createdBy: command.UserId,
            companyPurposeIds: companyPurposeIds,
            CacheKeys.GenerateARegistrationNumber(8));

        await dbContext.Tenants.AddAsync(newTenant, cancellationToken).ConfigureAwait(false);
        await dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        return new CreateATenantCompanyCommandResponse(newTenant.Id.Value.ToString());
    }
}
