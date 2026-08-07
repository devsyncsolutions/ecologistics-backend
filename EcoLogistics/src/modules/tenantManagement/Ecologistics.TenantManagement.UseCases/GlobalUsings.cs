// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

global using Ecologistics.Common.Contracts;
global using Ecologistics.Common.Exceptions;
global using Ecologistics.Common.Interfaces;
global using Ecologistics.Common.Utilities;
global using Ecologistics.TenantManagement.Domain.Commons;
global using Ecologistics.TenantManagement.Domain.Commons.ValueObjects;
global using Ecologistics.TenantManagement.Domain.TenantAggregate;
global using Ecologistics.TenantManagement.Domain.TenantAggregate.Enums;
global using Ecologistics.TenantManagement.Domain.TenantAggregate.Events;
global using Ecologistics.TenantManagement.UseCases.Interfaces;
global using FluentValidation;
global using Microsoft.AspNetCore.Http;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.Extensions.Logging;
global using Wolverine;
global using ZiggyCreatures.Caching.Fusion;
