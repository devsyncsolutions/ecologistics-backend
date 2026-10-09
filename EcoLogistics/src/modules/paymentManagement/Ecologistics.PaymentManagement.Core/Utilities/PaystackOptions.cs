// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.PaymentManagement.Core.Utilities;

public class PaystackOptions
{
    public string BaseUrl { get; set; } = string.Empty;
    public string SecretKey { get; set; } = string.Empty;
    public string SectionName { get; set; } = string.Empty;
}
