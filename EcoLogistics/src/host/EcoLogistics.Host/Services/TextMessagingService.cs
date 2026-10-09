// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace EcoLogistics.Host.Services;

public class TextMessagingService(
    IHttpClientFactory httpClient,
    ILogger<TextMessagingService> logger,
    Arkessel arkessel,
    IFusionCache cache)
    : ITextMessagingService
{
    public async Task<bool> SendAsync(string phoneNumber, string message, string? requestId = null, CancellationToken cancellationToken = default)
    {
        HttpClient http = httpClient.CreateClient("arkessel");

        var body = new
        {
            recipients = new [] { phoneNumber },
            sender = arkessel.SenderId,
            message
        };

        try
        {
            HttpResponseMessage response = await http.PostAsJsonAsync("/api/v2/sms/send", body, cancellationToken)
                .ConfigureAwait(false);

            if (response.IsSuccessStatusCode) return response.IsSuccessStatusCode;

            if(!string.IsNullOrWhiteSpace(requestId))
                await cache.RemoveAsync(CacheKeys.GetRequestKey<LoginCommand>(requestId), token: cancellationToken).ConfigureAwait(false);

            logger.LogErrorMessage(
                $"Error sending text message to {phoneNumber}: {response.ReasonPhrase}",
                null,
                phoneNumber,
                null);

            return response.IsSuccessStatusCode;
        }
        catch (Exception e)
        {
            logger.LogErrorMessage(
                $"Error sending text message to {phoneNumber}: \nStack Trace: {e.StackTrace}",
                null,
                phoneNumber,
                null);

            if (!string.IsNullOrWhiteSpace(requestId))
                await cache.RemoveAsync(CacheKeys.GetRequestKey<LoginCommand>(requestId), token: cancellationToken).ConfigureAwait(false);

            return false;
        }
    }
}
