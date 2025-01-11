using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace RegisterCard.WebApi.IntegrationTests;

public abstract class BaseIntegrationTests
{
    protected HttpClient? ApplicationClient { get; private set; }

    [OneTimeSetUp]
    public void Setup()
    {
        ApplicationClient = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
        {
            builder.ConfigureTestServices(services =>
            {
                services.AddSingleton<IPolicyEvaluator, MockPolicyEvaluator>();
            });
        }).CreateClient();
    }
}