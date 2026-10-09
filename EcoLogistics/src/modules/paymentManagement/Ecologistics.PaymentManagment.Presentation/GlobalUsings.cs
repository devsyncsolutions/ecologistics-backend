// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

global using System.Net;
global using System.Net.Http.Headers;
global using Carter;
global using Ecologistics.Common.Contracts.Responses;
global using Ecologistics.Common.Enums;
global using Ecologistics.PaymentManagement.Core.Features.ConfirmCashPayment;
global using Ecologistics.PaymentManagement.Core.Features.GetInvoiceByPickupId;
global using Ecologistics.PaymentManagement.Core.Features.GetInvoices;
global using Ecologistics.PaymentManagement.Core.Features.GetPaymentDetails;
global using Ecologistics.PaymentManagement.Core.Features.GetPaymentMethods;
global using Ecologistics.PaymentManagement.Core.Interfaces;
global using Ecologistics.PaymentManagement.Core.Utilities;
global using Ecologistics.PaymentManagement.Infrastructure.Data;
global using Ecologistics.PaymentManagement.Infrastructure.Services;
global using Microsoft.AspNetCore.Builder;
global using Microsoft.AspNetCore.Http;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.AspNetCore.Routing;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using Polly;
global using Polly.Timeout;
global using Wolverine;
