// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.PickupManagement.Domain.Commons;

public class WasteKind : BaseEntity<WasteKindId>
{
    public string Name { get; set; } = string.Empty;
}
