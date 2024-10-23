using System.Text.Json;
using System.Text;

namespace BFFService;

public class JsonContentHandler : DelegatingHandler
{
    private readonly JsonSerializerOptions _jsonSerializerOptions;

    public JsonContentHandler(JsonSerializerOptions jsonSerializerOptions)
    {
        _jsonSerializerOptions = jsonSerializerOptions;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        if (request.Content is StringContent stringContent)
        {
            var json = await stringContent.ReadAsStringAsync();
            var obj = JsonSerializer.Deserialize<object>(json, _jsonSerializerOptions);
            var newJson = JsonSerializer.Serialize(obj, _jsonSerializerOptions);
            request.Content = new StringContent(newJson, Encoding.UTF8, "application/json");
        }

        return await base.SendAsync(request, cancellationToken);
    }
}