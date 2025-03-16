using System.Security.Claims;

namespace Api.Endpoints;

public static class UserEndpoints
{
    public static IEndpointRouteBuilder MapUsers(this IEndpointRouteBuilder endpoints)
    {
        IEndpointRouteBuilder group = endpoints.MapGroup("users")
            .WithTags("Users")
            .RequireAuthorization(op =>
            {
                op.RequireRole("administrator");
            });;
        group.MapGet("me", (ClaimsPrincipal claimsPrincipal) =>
        {
            return claimsPrincipal.Claims.ToDictionary(c => c.Type, c => c.Value);
        });
        return endpoints;
    }
}