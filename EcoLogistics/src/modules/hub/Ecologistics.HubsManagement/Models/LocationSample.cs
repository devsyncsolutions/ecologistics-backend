// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.HubsManagement.Models;

public record LocationSample(
    double Lat,
    double Lon,
    DateTimeOffset Timestamp);
