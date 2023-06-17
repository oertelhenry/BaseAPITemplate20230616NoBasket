using Mobalyz.Data.Models.Dto.Authorization;

namespace Mobalyz.Odyssey.Resources.Interfaces
{
    public interface IAccountProvider
    {
        Task<UserDto> LoginAsync(LoginDto request);
    }
}
