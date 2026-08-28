// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CloudinaryDotNet.Actions;

namespace EcoLogistics.Host.Services;

internal sealed class ProcessImagesService(Cloudinary cloudinary, ILogger<ProcessImagesService> logger)
    : IProcessImagesService
{
    public async Task<string?> ProcessImageAsync(IFormFile file, CancellationToken cancellationToken)
    {
        var stream = file.OpenReadStream();
        await using var streamDisposable = stream.ConfigureAwait(false);

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
            var uploadResult = await cloudinary.UploadAsync(uploadParams, cancellationToken).ConfigureAwait(false);
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
}
