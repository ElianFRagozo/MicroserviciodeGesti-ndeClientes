using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class JwtAuthorizationFilter : IAuthorizationFilter
{
    private readonly IConfiguration _configuration;

    public JwtAuthorizationFilter(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var token = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

        if (string.IsNullOrEmpty(token))
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);

        try
        {
            // Si el token no comienza con "Bearer", lo ignoramos
            if (!token.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            // Removemos "Bearer " del inicio del token
            token = token.Substring("Bearer ".Length).Trim();

            var jwtToken = tokenHandler.ReadJwtToken(token);
            var claims = jwtToken.Claims.ToList();

            if (!claims.Any(x => x.Type == ClaimTypes.NameIdentifier))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var identity = new ClaimsIdentity(claims, "JWT");
            var principal = new ClaimsPrincipal(identity);

            context.HttpContext.User = principal;
        }
        catch
        {
            context.Result = new UnauthorizedResult();
        }
    }
}
