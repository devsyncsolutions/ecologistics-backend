// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace EcoLogistics.Host.Services;

internal sealed class ProcessFilesService(Cloudinary cloudinary, ILogger<ProcessFilesService> logger)
    : IProcessFilesService
{
    public async Task<string?> ProcessImageAsync(IFormFile file, CancellationToken cancellationToken)
    {
        Stream stream = file.OpenReadStream();
        await using ConfiguredAsyncDisposable streamDisposable = stream.ConfigureAwait(false);

        // upload image to a specified folder in Cloudinary
        var uploadParams = new ImageUploadParams
        {
            Folder = "EcologisticsProfileImages",
            File = new FileDescription(file.FileName, stream),
            UniqueFilename = true,
            Overwrite = true,
            Transformation = new Transformation().Width(500).Height(500).Crop("scale").Quality("auto")
        };

        try
        {
            ImageUploadResult uploadResult = await cloudinary.UploadAsync(uploadParams, cancellationToken).ConfigureAwait(false);
            return uploadResult.StatusCode == HttpStatusCode.OK
                ? uploadResult.SecureUrl.ToString()
                : null;
        }
        catch (Exception e)
        {
            logger.LogErrorMessage(
                $"Error uploading image. Error {e.StackTrace}",
                null,
                null,
                null);

            return null;
        }
    }

    public async Task<string?> ProcessPdfAsync(string fileName, byte[] file, CancellationToken cancellationToken)
    {
        using var stream = new MemoryStream(file);
        await using ConfiguredAsyncDisposable streamDisposable = stream.ConfigureAwait(false);

        var uploadParams = new ImageUploadParams
        {
            Folder = "EcologisticsInvoices",
            File = new FileDescription(fileName, stream),
            Overwrite = true
        };

        try
        {
            ImageUploadResult uploadResult = await cloudinary.UploadAsync(uploadParams, cancellationToken).ConfigureAwait(false);
            return uploadResult.StatusCode == HttpStatusCode.OK ?  uploadResult.SecureUrl.ToString() : null;
        }
        catch (Exception e)
        {
            logger.LogErrorMessage(
                $"Error uploading pdf. Error {e.StackTrace}",
                null,
                null,
                null);
            return null;
        }
    }
}
