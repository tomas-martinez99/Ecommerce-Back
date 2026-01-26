using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface IJwtService
    {
        string GenerateToken(List<Claim> claimsForToken, DateTime ExpirationTime);

        ClaimsPrincipal? ValidateToken(string token);
    }
}
