// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.TenantManagement.UseCases.Tenants.CreateATenantCompany;

public sealed class CreateATenantCompanyCommandValidator: AbstractValidator<CreateATenantCompanyCommand>
{
    public CreateATenantCompanyCommandValidator()
    {
        RuleFor(x => x.CompanyName)
            .NotEmpty().WithMessage("Company name is required.");
    }
}
