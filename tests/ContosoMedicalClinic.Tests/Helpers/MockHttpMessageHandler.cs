using System.Net;
using System.Text;
using System.Text.Json;

namespace ContosoMedicalClinic.Tests.Helpers;

/// <summary>
/// A configurable mock HttpMessageHandler for testing DabHttpClient and services.
/// Queues responses that are returned in order, or returns a default 404 if none remain.
/// </summary>
public class MockHttpMessageHandler : HttpMessageHandler
{
    private readonly Queue<MockResponse> _responses = new();
    private readonly List<HttpRequestMessage> _requests = [];

    public IReadOnlyList<HttpRequestMessage> Requests => _requests;
    public HttpRequestMessage LastRequest => _requests[^1];

    public void QueueResponse(HttpStatusCode statusCode, object? body = null)
    {
        var json = body is not null ? JsonSerializer.Serialize(body) : "{}";
        _responses.Enqueue(new MockResponse(statusCode, json));
    }

    public void QueueJsonResponse<T>(T body) where T : class =>
        QueueResponse(HttpStatusCode.OK, body);

    public void QueueDabList<T>(List<T> items) =>
        QueueResponse(HttpStatusCode.OK, new { value = items });

    public void QueueEmptyList() =>
        QueueResponse(HttpStatusCode.OK, new { value = Array.Empty<object>() });

    public void QueueNotFound() =>
        QueueResponse(HttpStatusCode.NotFound);

    public void QueueError(HttpStatusCode code = HttpStatusCode.BadRequest, string errorBody = "Bad Request") =>
        _responses.Enqueue(new MockResponse(code, errorBody));

    protected override Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request, CancellationToken cancellationToken)
    {
        _requests.Add(request);

        var mock = _responses.Count > 0
            ? _responses.Dequeue()
            : new MockResponse(HttpStatusCode.NotFound, "No more queued responses");

        var response = new HttpResponseMessage(mock.StatusCode)
        {
            Content = new StringContent(mock.Body, Encoding.UTF8, "application/json"),
            RequestMessage = request
        };

        return Task.FromResult(response);
    }

    private record MockResponse(HttpStatusCode StatusCode, string Body);
}

public static class MockHttpExtensions
{
    public static HttpClient CreateMockHttpClient(this MockHttpMessageHandler handler, string baseAddress = "http://localhost")
    {
        return new HttpClient(handler) { BaseAddress = new Uri(baseAddress) };
    }
}
