using System.Security.Claims;

namespace webapitest.Middlewares.JwtDataExtraction;

public class UserInfo
{
    private readonly RequestDelegate _next;

    public UserInfo(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        // Grab userID from jwt
        var nameIdentifierClaim = context.User.FindFirst(ClaimTypes.NameIdentifier);

        string? userId = null;
        if (nameIdentifierClaim != null)
        {
            userId = nameIdentifierClaim.Value;
            context.Items.Add("userId", userId);
        }

        // Call the next middleware in the pipeline
        await _next(context);
    }
}