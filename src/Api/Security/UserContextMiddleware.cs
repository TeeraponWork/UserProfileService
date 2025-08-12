using Application.Abstractions;
using System.Security.Claims;

public class UserContextMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IHostEnvironment _env;

    public UserContextMiddleware(RequestDelegate next, IHostEnvironment env)
    {
        _next = next;
        _env = env;
    }

    public async Task Invoke(HttpContext ctx, IUserContext userContext)
    {
        var userIdHeader = ctx.Request.Headers["X-User-Id"].FirstOrDefault();
        var emailHeader = ctx.Request.Headers["X-User-Email"].FirstOrDefault();
        var rolesHeader = ctx.Request.Headers["X-User-Roles"].FirstOrDefault();

        // 1) มี header จาก Gateway → ใช้ตามปกติ
        if (Guid.TryParse(userIdHeader, out var uidFromHeader))
        {
            var claims = new List<Claim> { new Claim(ClaimTypes.NameIdentifier, uidFromHeader.ToString()) };
            if (!string.IsNullOrWhiteSpace(emailHeader)) claims.Add(new Claim(ClaimTypes.Email, emailHeader));
            if (!string.IsNullOrWhiteSpace(rolesHeader))
                foreach (var r in rolesHeader.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries))
                    claims.Add(new Claim(ClaimTypes.Role, r));

            ctx.User = new ClaimsPrincipal(new ClaimsIdentity(claims, "Gateway"));
            await _next(ctx);
            return;
        }

        // 2) ไม่มี header และเป็น Development → mock จาก IUserContext (เช่น MockUserContext)
        if (_env.IsDevelopment())
        {
            var claims = new List<Claim> { new Claim(ClaimTypes.NameIdentifier, userContext.UserId.ToString()) };
            if (!string.IsNullOrWhiteSpace(userContext.Email))
                claims.Add(new Claim(ClaimTypes.Email, userContext.Email!));
            foreach (var r in userContext.Roles)
                claims.Add(new Claim(ClaimTypes.Role, r));

            ctx.User = new ClaimsPrincipal(new ClaimsIdentity(claims, "DevMock"));
            await _next(ctx);
            return;
        }

        // 3) ไม่มี header และไม่ใช่ Dev → 401
        ctx.Response.StatusCode = StatusCodes.Status401Unauthorized;
        await ctx.Response.WriteAsync("Missing or invalid X-User-Id header");
    }
}
