// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

global using System.Text;
global using Carter;
global using Ecologistics.Common.Contracts;
global using Ecologistics.Common.Contracts.Commands;
global using Ecologistics.Common.Enums;
global using Ecologistics.Common.Interfaces;
global using EcoLogistics.UserManagement.Core.Enums;
global using EcoLogistics.UserManagement.Core.Features.GetUserDetails;
global using EcoLogistics.UserManagement.Core.Features.RegisterUserAccount;
global using EcoLogistics.UserManagement.Core.Features.UpdateUserDetails;
global using EcoLogistics.UserManagement.Core.Interfaces;
global using EcoLogistics.UserManagement.Core.Models;
global using EcoLogistics.UserManagement.Core.Utils;
global using EcoLogistics.UserManagement.Infrastructure.Data;
global using EcoLogistics.UserManagement.Infrastructure.Services;
global using Microsoft.AspNetCore.Authentication.JwtBearer;
global using Microsoft.AspNetCore.Builder;
global using Microsoft.AspNetCore.Http;
global using Microsoft.AspNetCore.Identity;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.AspNetCore.Routing;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Options;
global using Microsoft.IdentityModel.Tokens;
global using Wolverine;
