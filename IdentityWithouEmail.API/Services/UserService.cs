using System.Threading.Tasks;
using IdentityConfigurationSample.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace IdentityConfigurationSample.Services;

public class UserService : IUserService
{
    private readonly UserManager<IdentityUser> _userManager;

    public UserService(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
    }

    /// <summary>
    /// FindByPhoneNumberAsync
    /// </summary>
    /// <param name="phoneNumber"></param>
    /// <returns></returns>
    public async Task<IdentityUser> FindByPhoneNumberAsync(string phoneNumber)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(u => u.PhoneNumber == phoneNumber);

        return user;
    }
    
    /// <summary>
    /// CreateUserAsync
    /// </summary>
    /// <param name="user"></param>
    /// <param name="pin"></param>
    /// <returns></returns>
    public async Task<IdentityResult> CreateUserAsync(IdentityUser user, string pin)
    {
        var checkIfExists = await FindByPhoneNumberAsync(user.PhoneNumber);

        return checkIfExists is null ? await _userManager.CreateAsync(user, pin) : null;
    }
    
    /// <summary>
    /// CheckPasswordAsync
    /// </summary>
    /// <param name="user"></param>
    /// <param name="pin"></param>
    /// <returns></returns>
    public async Task<bool> CheckPasswordAsync(IdentityUser user, string pin)
    {
        return await _userManager.CheckPasswordAsync(user, pin);
    }
}