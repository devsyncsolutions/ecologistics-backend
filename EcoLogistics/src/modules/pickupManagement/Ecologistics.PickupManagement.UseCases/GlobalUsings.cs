// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

global using System.Globalization;
global using Carter;
global using Ecologistics.Common.Contracts.Responses;
global using Ecologistics.Common.Utilities;
global using Ecologistics.PickupManagement.Domain.Commons.Utility;
global using Ecologistics.PickupManagement.Domain.Interfaces;
global using Ecologistics.PickupManagement.Infrastructure.Data;
global using Ecologistics.PickupManagement.Infrastructure.Services;
global using FluentValidation;
global using Microsoft.AspNetCore.Builder;
global using Microsoft.AspNetCore.Http;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.AspNetCore.Routing;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using Wolverine;
global using ZiggyCreatures.Caching.Fusion;
