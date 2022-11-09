using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace IdentityConfigurationSample.Interfaces;

public interface IUserService
{
    Task<IdentityUser> FindByPhoneNumberAsync(string phoneNumber);
    Task<IdentityResult> CreateUserAsync(IdentityUser user, string pin);
    Task<bool> CheckPasswordAsync(IdentityUser user, string pin);
}