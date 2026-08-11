// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.Common.Exceptions;

public class ForbiddenRequestException(string message) : Exception(message);
