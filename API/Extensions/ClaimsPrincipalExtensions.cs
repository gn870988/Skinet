using System.Linq;
using System.Security.Claims;

namespace API.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static string RetrieveClaimTypeFromPrincipal(this ClaimsPrincipal user, string claimType)
        {
            return user?.Claims?
                       .FirstOrDefault(u => u.Type == claimType)?.Value ?? string.Empty;
        }
    }
}