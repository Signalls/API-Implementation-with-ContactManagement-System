using ContactModel;
using Microsoft.AspNetCore.Identity;

namespace ContactData.Repository
{
    public interface IUserRepository
    {
        Task<string> Login(LoginDto loginRequestDTO);
        Task<IdentityResult> RegisterAsync(RegisterDto registrationRequestDTO, string role);
    }
}
