// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.Common.Abstractions;

public abstract record BaseRequest
{
    public string RequestId { get; set; } = string.Empty;
}
