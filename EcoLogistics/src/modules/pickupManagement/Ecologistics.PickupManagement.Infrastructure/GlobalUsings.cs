// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

global using System.Diagnostics;
global using System.Globalization;
global using System.Linq.Expressions;
global using Ecologistics.Common.Abstractions;
global using Ecologistics.Common.Audits.SaveUpdateAudit;
global using Ecologistics.Common.Contracts.Queries;
global using Ecologistics.Common.Contracts.Responses;
global using Ecologistics.Common.Exceptions;
global using Ecologistics.Common.Interfaces;
global using Ecologistics.Common.Utilities;
global using Ecologistics.PickupManagement.Domain.Commons;
global using Ecologistics.PickupManagement.Domain.Commons.Enums;
global using Ecologistics.PickupManagement.Domain.Commons.ValueObjects;
global using Ecologistics.PickupManagement.Domain.Dtos;
global using Ecologistics.PickupManagement.Domain.Interfaces;
global using Ecologistics.PickupManagement.Domain.PickupAggregate;
global using Ecologistics.PickupManagement.Domain.PickupAggregate.Enums;
global using Ecologistics.PickupManagement.Domain.PickupAggregate.ValueObjects;
global using Ecologistics.PickupManagement.Infrastructure.Data;
global using Ecologistics.PickupManagement.Infrastructure.Services;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore.ChangeTracking;
global using Microsoft.EntityFrameworkCore.Design;
global using Microsoft.EntityFrameworkCore.Diagnostics;
global using Microsoft.EntityFrameworkCore.Metadata.Builders;
global using Microsoft.Extensions.Logging;
global using Wolverine;
