using System.Security.Claims;
using TourmalineCore.AspNetCore.JwtAuthentication.Core.Contract;

namespace Api;

public class UserClaimsProvider : IUserClaimsProvider
{
    public const string PermissionClaimType = "permissions";

    public Task<List<Claim>> GetUserClaimsAsync(string login)
    {
        throw new NotImplementedException();
    }
}