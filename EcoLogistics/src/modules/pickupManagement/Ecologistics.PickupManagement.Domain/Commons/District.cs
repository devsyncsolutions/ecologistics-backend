// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.PickupManagement.Domain.Commons;

public class District : BaseEntity<DistrictId>
{
    public Region Region { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
}
