using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Http;

namespace RegisterCard.WebApi.IntegrationTests;

public sealed class MockPolicyEvaluator : IPolicyEvaluator
{
    public async Task<AuthenticateResult> AuthenticateAsync(AuthorizationPolicy policy, HttpContext context)
    {
        const string testScheme = "MockScheme";

        ClaimsPrincipal principal = new();

        principal.AddIdentity(new ClaimsIdentity(
            new[]
            {
                new Claim("Permission", "CanViewPage"), new Claim("Manager", "yes"),
                new Claim(ClaimTypes.Role, "Administrator"), new Claim(ClaimTypes.NameIdentifier, "John")
            },
            testScheme));

        return await Task.FromResult(AuthenticateResult.Success(
            new AuthenticationTicket(principal, new AuthenticationProperties(), testScheme)));
    }

    public async Task<PolicyAuthorizationResult> AuthorizeAsync(AuthorizationPolicy policy,
        AuthenticateResult authenticationResult, HttpContext context, object? resource)
    {
        return await Task.FromResult(PolicyAuthorizationResult.Success());
    }
}