// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.PickupManagement.UseCases.GetActivePickups;

public record GetActivePickupsQuery;

public record ActivePickupDto(string PickupId, string ContentName, string StartedAt);
