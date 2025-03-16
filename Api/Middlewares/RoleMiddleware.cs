using System.Security.Claims;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Api.Middlewares;

public class RoleMiddleware(RequestDelegate @delegate)
{
    public async Task InvokeAsync(HttpContext httpContext)
    {
        string realmAccess = httpContext.User.Claims.ToDictionary(x => x.Type, y => y.Value)["realm_access"];
        Access access = JsonSerializer.Deserialize<Access>(realmAccess);
        httpContext.User.AddIdentity(new ClaimsIdentity(access.Roles.Select(role => new Claim(ClaimTypes.Role, role))));
        @delegate.Invoke(httpContext);
    }
}

public record Access
{
    [JsonPropertyName("roles")]
    public string[] Roles { get; set; } = Array.Empty<string>();
}