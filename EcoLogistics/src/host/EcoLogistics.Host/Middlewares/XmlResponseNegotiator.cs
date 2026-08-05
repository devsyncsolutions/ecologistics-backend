// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Text;
using System.Xml.Serialization;
using Microsoft.Net.Http.Headers;

namespace EcoLogistics.Host.Middlewares;

internal sealed class XmlResponseNegotiator : IResponseNegotiator
{
    public bool CanHandle(MediaTypeHeaderValue accept) =>
        accept.MediaType.Value?.Equals("application/xml", StringComparison.OrdinalIgnoreCase) == true;

    public Task Handle<T>(HttpRequest req, HttpResponse res, T model, CancellationToken cancellationToken)
    {
        res.ContentType = "application/xml; charset=utf-8";

        if (model is null) return res.WriteAsync(string.Empty, cancellationToken);

        var serializer = new XmlSerializer(typeof(T));
        using var writer = new Utf8StringWriter();
        serializer.Serialize(writer, model);

        return res.WriteAsync(writer.ToString(), cancellationToken);
    }
}

public sealed class Utf8StringWriter : StringWriter
{
    public override Encoding Encoding => Encoding.UTF8;
}
