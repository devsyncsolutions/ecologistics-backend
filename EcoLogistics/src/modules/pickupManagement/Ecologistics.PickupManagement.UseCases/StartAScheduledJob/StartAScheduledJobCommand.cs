// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.PickupManagement.UseCases.StartAScheduledJob;

public record StartAScheduledJobCommand(Guid PickupId) : BaseRequest;
