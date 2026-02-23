using ContosoMedicalClinic.Infrastructure.DataApi;

namespace ContosoMedicalClinic.Tests.Helpers;

/// <summary>
/// Factory methods for creating DabHttpClient instances with mock HTTP handlers.
/// </summary>
public static class DabTestHelpers
{
    public static (DabHttpClient Client, MockHttpMessageHandler Handler) CreateDabClient()
    {
        var handler = new MockHttpMessageHandler();
        var httpClient = handler.CreateMockHttpClient();
        var dabClient = new DabHttpClient(httpClient);
        return (dabClient, handler);
    }
}
