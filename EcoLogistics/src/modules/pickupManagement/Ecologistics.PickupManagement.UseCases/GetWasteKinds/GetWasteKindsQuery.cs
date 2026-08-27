// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.PickupManagement.UseCases.GetWasteKinds;

public record GetWasteKindsQuery(
    int Page,
    int PageSize,
    string? SearchTerm);

public record WasteKindDto(string Id, string Name);
