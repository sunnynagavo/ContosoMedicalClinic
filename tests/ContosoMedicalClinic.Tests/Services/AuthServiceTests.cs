using ContosoMedicalClinic.Application.DTOs;
using ContosoMedicalClinic.Infrastructure.DataApi;
using ContosoMedicalClinic.Tests.Helpers;
using Microsoft.Extensions.Configuration;
using NSubstitute;

namespace ContosoMedicalClinic.Tests.Services;

public class AuthServiceTests
{
    private static AuthService CreateService(MockHttpMessageHandler handler, IConfiguration? config = null)
    {
        var httpClient = handler.CreateMockHttpClient();
        var dabClient = new DabHttpClient(httpClient);
        config ??= Substitute.For<IConfiguration>();
        return new AuthService(dabClient, config);
    }

    [Fact]
    public async Task GetUserByEmailAsync_ReturnsUser()
    {
        var handler = new MockHttpMessageHandler();
        var user = new UserAccountDto(1, "test@email.com", "hash", "Test User", "Patient", 1, null, true);
        handler.QueueDabList(new List<UserAccountDto> { user });
        var svc = CreateService(handler);

        var result = await svc.GetUserByEmailAsync("test@email.com");

        Assert.NotNull(result);
        Assert.Equal("test@email.com", result.Email);
    }

    [Fact]
    public async Task GetUserByEmailAsync_NotFound_ReturnsNull()
    {
        var handler = new MockHttpMessageHandler();
        handler.QueueEmptyList();
        var svc = CreateService(handler);

        var result = await svc.GetUserByEmailAsync("nobody@test.com");

        Assert.Null(result);
    }

    [Fact]
    public async Task GetUserByEmailAsync_SanitizesSingleQuotes()
    {
        var handler = new MockHttpMessageHandler();
        handler.QueueEmptyList();
        var svc = CreateService(handler);

        await svc.GetUserByEmailAsync("test'injection@evil.com");

        var url = handler.LastRequest.RequestUri!.ToString();
        // Single quote should be doubled for OData escaping
        Assert.DoesNotContain("test'injection", url);
    }

    [Fact]
    public async Task ValidatePasswordAsync_DemoHash_AcceptsPassword1()
    {
        var handler = new MockHttpMessageHandler();
        var svc = CreateService(handler);

        var result = await svc.ValidatePasswordAsync("Password1!", "DEMO_HASH");

        Assert.True(result);
    }

    [Fact]
    public async Task ValidatePasswordAsync_DemoHash_RejectsWrongPassword()
    {
        var handler = new MockHttpMessageHandler();
        var svc = CreateService(handler);

        var result = await svc.ValidatePasswordAsync("WrongPassword", "DEMO_HASH");

        Assert.False(result);
    }

    [Fact]
    public async Task ValidatePasswordAsync_RealHash_MatchesHashedPassword()
    {
        var handler = new MockHttpMessageHandler();
        var svc = CreateService(handler);
        var hash = svc.HashPassword("MySecurePassword");

        var result = await svc.ValidatePasswordAsync("MySecurePassword", hash);

        Assert.True(result);
    }

    [Fact]
    public async Task ValidatePasswordAsync_RealHash_RejectsWrongPassword()
    {
        var handler = new MockHttpMessageHandler();
        var svc = CreateService(handler);
        var hash = svc.HashPassword("MySecurePassword");

        var result = await svc.ValidatePasswordAsync("WrongPassword", hash);

        Assert.False(result);
    }

    [Fact]
    public void HashPassword_UsesFallbackKey_WhenConfigMissing()
    {
        var handler = new MockHttpMessageHandler();
        var config = Substitute.For<IConfiguration>();
        config["Security:HmacKey"].Returns((string?)null);
        var svc = CreateService(handler, config);

        var hash1 = svc.HashPassword("test");
        var hash2 = svc.HashPassword("test");

        Assert.NotEmpty(hash1);
        Assert.Equal(hash1, hash2); // Deterministic
    }

    [Fact]
    public void HashPassword_UsesConfigKey_WhenProvided()
    {
        var handler = new MockHttpMessageHandler();
        var configDefault = Substitute.For<IConfiguration>();
        configDefault["Security:HmacKey"].Returns((string?)null);

        var configCustom = Substitute.For<IConfiguration>();
        configCustom["Security:HmacKey"].Returns("CustomKey123");

        var svcDefault = CreateService(handler, configDefault);
        handler.QueueEmptyList(); // Won't be used but needed for handler
        var svcCustom = CreateService(handler, configCustom);

        var hashDefault = svcDefault.HashPassword("test");
        var hashCustom = svcCustom.HashPassword("test");

        // Different keys produce different hashes
        Assert.NotEqual(hashDefault, hashCustom);
    }

    [Fact]
    public void HashPassword_ProducesBase64String()
    {
        var handler = new MockHttpMessageHandler();
        var svc = CreateService(handler);

        var hash = svc.HashPassword("test");

        // Should be valid base64
        var bytes = Convert.FromBase64String(hash);
        Assert.Equal(32, bytes.Length); // HMACSHA256 produces 32 bytes
    }
}
