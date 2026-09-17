// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

global using System.Diagnostics;
global using System.Linq.Expressions;
global using Ecologistics.Common.Abstractions;
global using Ecologistics.Common.Audits.SaveUpdateAudit;
global using Ecologistics.Common.Exceptions;
global using Ecologistics.Common.Interfaces;
global using Ecologistics.PaymentManagement.Core.Entities;
global using Ecologistics.PaymentManagement.Core.Interfaces;
global using Ecologistics.PaymentManagement.Core.ValueObjects;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore.ChangeTracking;
global using Microsoft.EntityFrameworkCore.Design;
global using Microsoft.EntityFrameworkCore.Diagnostics;
global using Microsoft.EntityFrameworkCore.Metadata.Builders;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.Logging;
global using Wolverine;
