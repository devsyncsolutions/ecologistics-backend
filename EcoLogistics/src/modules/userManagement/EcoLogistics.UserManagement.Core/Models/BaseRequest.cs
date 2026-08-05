// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace EcoLogistics.UserManagement.Core.Models;

public record BaseRequest
{
    public string RequestId { get; set; } = string.Empty;
}
