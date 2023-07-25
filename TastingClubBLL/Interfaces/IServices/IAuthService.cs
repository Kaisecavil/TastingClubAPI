using TastingClubBLL.DTOs.ApplicationUserDTOs;

namespace TastingClubBLL.Interfaces.IServices
{
    public interface IAuthService
    {
        string GenerateTokenString(ApplicationUserDtoForLogin user, IEnumerable<string> roles);
        Task<string> LoginAsync(ApplicationUserDtoForLogin user);
        Task<bool> RegisterUserAsync(ApplicationUserDtoForLogin user);
    }
}