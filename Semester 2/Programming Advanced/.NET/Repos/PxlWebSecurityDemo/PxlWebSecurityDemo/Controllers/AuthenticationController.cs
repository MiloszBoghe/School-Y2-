using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PxlWebSecurityDemo.Data;
using PxlWebSecurityDemo.Models;

namespace PxlWebSecurityDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IOptions<TokenSettings> _tokenSettings;

        public AuthenticationController(UserManager<User> userManager, RoleManager<Role> roleManager, IPasswordHasher<User> passwordHasher, IOptions<TokenSettings> tokenSettings)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _passwordHasher = passwordHasher;
            _tokenSettings = tokenSettings;
        }

        #region register
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var user = new User()
            {
                UserName = model.Email,
                Email = model.Email,
                DateOfBirth = model.DateOfBirth
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                var role = user.Email.ToLower().EndsWith("@pxl.be") ? Role.Lector : Role.Student;
                await EnsureRoleExists(role);
                await _userManager.AddToRoleAsync(user, role);
                return Ok();
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(error.Code, error.Description);
            }

            return BadRequest(ModelState);
        }
        #endregion


        #region CreateToken

        [HttpPost("token")]
        public async Task<IActionResult> CreateToken([FromBody] LoginModel model)
        {
            var user = await _userManager.FindByNameAsync(model.Email);
            if (user == null) return Unauthorized();
            if (_passwordHasher.VerifyHashedPassword(user, user.PasswordHash, model.Password) !=
                PasswordVerificationResult.Success) return Unauthorized();

            var token = await CreateJwtToken(user);
            return Ok(token);
        }

        private async Task<object> CreateJwtToken(User user)
        {
            var claims = await _userManager.GetClaimsAsync(user);
            claims.Add(new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.UserName));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
            
            var userRoles = await _userManager.GetRolesAsync(user);
            foreach (var role in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role,role));
            }

            var keyBytes = Encoding.UTF8.GetBytes(_tokenSettings.Value.Key);
            var symmetricSecurityKey = new SymmetricSecurityKey(keyBytes);
            var signingCredentials = new SigningCredentials(symmetricSecurityKey,SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _tokenSettings.Value.Issuer,
                _tokenSettings.Value.Audience,
                claims,
                expires: DateTime.UtcNow.AddMinutes(_tokenSettings.Value.ExpirationTimeInMinutes),
                signingCredentials: signingCredentials);
            
            var encryptedToken = new JwtSecurityTokenHandler().WriteToken(token);
            return encryptedToken;
        }

        #endregion

        private async Task EnsureRoleExists(string role)
        {
            if (await _roleManager.RoleExistsAsync(role)) return;
            await _roleManager.CreateAsync(new Role
            {
                Name = role,
                NormalizedName = role.ToUpper()
            });
        }
    }
}