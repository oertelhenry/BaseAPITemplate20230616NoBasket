using Mobalyz.Data.Models.Dto.Authorization;

namespace Mobalyz.Odyssey.Resources.Interfaces
{
    public interface ITokenService
    {
        Task<string> CreateToken(AuthResult user);
    }
}