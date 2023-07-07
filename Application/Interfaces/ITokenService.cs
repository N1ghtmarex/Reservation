using System.Security.Claims;

namespace Application.Interfaces
{
    public interface ITokenService
    {
        string Create(IEnumerable<Claim> claims);
    }
}
