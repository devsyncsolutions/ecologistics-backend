// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.Common.Contracts.Commands;

public record CreateATenantCompanyCommand(
    string CompanyName,
    string UserId,
    IEnumerable<string>? CompanyPurposes = null) : BaseRequest;

public record CreateATenantCompanyCommandResponse(string CompanyId);
