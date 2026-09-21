// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

global using Ecologistics.Common.Abstractions;
global using Ecologistics.Common.Contracts.Commands;
global using Ecologistics.Common.Contracts.Queries;
global using Ecologistics.Common.Enums;
global using Ecologistics.Common.Exceptions;
global using Ecologistics.Common.Interfaces;
global using Ecologistics.Common.Utilities;
global using EcoLogistics.UserManagement.Core.Enums;
global using EcoLogistics.UserManagement.Core.Interfaces;
global using EcoLogistics.UserManagement.Core.Models;
global using FluentValidation;
global using FluentValidation.Results;
global using Microsoft.AspNetCore.Http;
global using Microsoft.AspNetCore.Identity;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.Extensions.Logging;
global using Wolverine;
global using ZiggyCreatures.Caching.Fusion;
