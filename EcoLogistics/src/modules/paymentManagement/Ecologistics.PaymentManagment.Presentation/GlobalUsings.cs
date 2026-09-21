// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

global using System.Net;
global using Ecologistics.PaymentManagement.Core.Interfaces;
global using Ecologistics.PaymentManagement.Infrastructure.Data;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using Polly;
global using Polly.Timeout;
