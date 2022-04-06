using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MyTasks.WebApi.Extensions
{
    public static class ClaimPrincipalExtensions
    {
        public static string GetUserId(this ClaimsPrincipal claims)
        {
            return claims.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
