using Microsoft.AspNetCore.Authorization;

namespace DmsApi.Authorization;

class AuthorizeRoleAttribute :
    AuthorizeAttribute, 
    IAuthorizationRequirement,
    IAuthorizationRequirementData
{
    public string Role { get; set; } = string.Empty;

    public AuthorizeRoleAttribute(string role)
    {
        this.Role = role;
    }

    public IEnumerable<IAuthorizationRequirement> GetRequirements()
    {
        yield return this;
    }
}
