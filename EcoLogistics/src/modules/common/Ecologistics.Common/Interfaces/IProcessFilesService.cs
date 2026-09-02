// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Ecologistics.Common.Interfaces;

public interface IProcessFilesService
{
    Task<string?> ProcessImageAsync(IFormFile file, CancellationToken cancellationToken);
    Task<string?> ProcessPdfAsync(string fileName, byte[] file, CancellationToken cancellationToken);
}
