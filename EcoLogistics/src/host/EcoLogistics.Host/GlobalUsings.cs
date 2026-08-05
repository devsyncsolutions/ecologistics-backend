// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

global using System.Globalization;
global using System.Net;
global using System.Threading.RateLimiting;
global using Carter;
global using EcoLogistics.Host.DI;
global using EcoLogistics.Host.Services;
global using EcoLogistics.Host.Utilities;
global using EcoLogistics.UserManagement.Core.Exceptions;
global using EcoLogistics.UserManagement.Core.Features.StartLoginProcess;
global using EcoLogistics.UserManagement.Core.Interfaces;
global using EcoLogistics.UserManagement.Core.Models;
global using EcoLogistics.UserManagement.Presentation.DI;
global using Microsoft.AspNetCore.HttpOverrides;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.Extensions.Diagnostics.HealthChecks;
global using Microsoft.Extensions.Options;
global using Polly;
global using Scalar.AspNetCore;
global using Serilog;
global using Serilog.Events;
global using Wolverine;
global using Wolverine.Postgresql;
global using Wolverine.RabbitMQ;
global using ZiggyCreatures.Caching.Fusion;
global using ZiggyCreatures.Caching.Fusion.Backplane.StackExchangeRedis;
global using ZiggyCreatures.Caching.Fusion.Serialization.SystemTextJson;
