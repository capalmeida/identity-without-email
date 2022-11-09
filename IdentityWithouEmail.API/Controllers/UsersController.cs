using System.Threading.Tasks;
using IdentityConfigurationSample.Interfaces;
using IdentityConfigurationSample.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace IdentityConfigurationSample.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UsersController> _logger;

        public UsersController(ILogger<UsersController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        /// <summary>
        /// SignIn
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>IdentityUser</returns>
        [HttpGet]
        public async Task<ActionResult<string>> SignIn(SignInDto dto)
        {
            var user = await _userService.FindByPhoneNumberAsync(dto.PhoneNumber);

            if (user == null)
            {
                return NotFound();
            }
            
            var matches = await _userService.CheckPasswordAsync(user, dto.Pin);

            return matches
                ? Ok(new
                {
                    user
                })
                : BadRequest("Wrong password.");
        }
        
        /// <summary>
        /// CreateUser
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>IdentityResult</returns>
        [HttpPost]
        public async Task<ActionResult<string>> CreateUserAsync(SignInDto dto)
        {
            var result = await _userService.CreateUserAsync(new IdentityUser
            {
                UserName = dto.PhoneNumber,
                Email = null,
                PhoneNumber = dto.PhoneNumber,
            }, dto.Pin);

            return Ok(new
            {
                result
            });
        }
    }
}
