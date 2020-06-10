using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Stage_API.AuthenticationModels;
using Stage_API.Business._Models;
using Stage_API.Data.IRepositories;
using Stage_API.Domain;
using Stage_API.Domain.Classes;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Stage_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IOptions<TokenSettings> _tokenSettings;
        private readonly IResetPasswordRequestRepository _resetRequestRepository;
        private readonly IUserRepository _userRepository;


        public AuthenticationController(RoleManager<Role> roleManager, SignInManager<User> signInManager, IOptions<TokenSettings> tokenSettings, IResetPasswordRequestRepository resetPasswordRequestRepository, IUserRepository userRepository)
        {
            _userManager = signInManager.UserManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _tokenSettings = tokenSettings;
            _userRepository = userRepository;
            _resetRequestRepository = resetPasswordRequestRepository;
        }

        #region register
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var isReviewer = model.Email.ToLower().EndsWith("@pxl.be");
            var isStudent = model.Email.ToLower().EndsWith("@student.pxl.be");
            var role = isReviewer ? Role.Reviewer : isStudent ? Role.Student : null;
            if (role == null) return StatusCode(403, "Registreren moet met een PXL mail.");

            User user = null;
            if (isReviewer)
            {
                user = new Reviewer()
                {
                    Voornaam = model.Voornaam,
                    Naam = model.Naam,
                    UserName = model.Email,
                    Email = model.Email
                };
            }
            else if (isStudent)
            {
                user = new Student()
                {
                    Voornaam = model.Voornaam,
                    Naam = model.Naam,
                    UserName = model.Email,
                    Email = model.Email
                };
            }

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                var newUser = await _userManager.FindByIdAsync(user.Id.ToString());
                newUser.EmailConfirmed = true;
                await _userManager.UpdateAsync(user);
                await EnsureRoleExists(role);
                await _userManager.AddToRoleAsync(user, role);
                return NoContent();
            }

            foreach (var error in result.Errors)
            {
                if (error.Code == "DuplicateEmail")
                {
                    ModelState.AddModelError(error.Code, "Er bestaat al een account met dit e-mailadres!");
                }
            }

            return BadRequest(ModelState);
        }
        [HttpPost("register/bedrijf")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterBedrijfModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (_userManager.Users.FirstOrDefault(b => b.Naam == model.Naam) != null)
            {
                return BadRequest("Your company is already registered!");
            }

            var user = new Bedrijf()
            {
                UserName = model.Contactpersoon.Email,
                Email = model.Contactpersoon.Email,
                Naam = model.Naam,
                AantalBegeleidendeMedewerkers = model.AantalBegeleidendeMedewerkers,
                AantalITMedewerkers = model.AantalITMedewerkers,
                AantalMedewerkers = model.AantalMedewerkers,
                Adres = model.Adres,
                Gemeente = model.Gemeente,
                Postcode = model.Postcode,
                IsBedrijf = true,
                Bedrijfspromotor = new Bedrijfspromotor()
                {
                    Id = model.Bedrijfspromotor.Id,
                    Voornaam = model.Bedrijfspromotor.Voornaam,
                    Naam = model.Bedrijfspromotor.Naam,
                    Email = model.Bedrijfspromotor.Email,
                    Telefoonnummer = model.Bedrijfspromotor.Telefoonnummer,
                    Titel = model.Bedrijfspromotor.Titel,
                },
                Contactpersoon = new Contactpersoon()
                {
                    Id = model.Contactpersoon.Id,
                    Titel = model.Contactpersoon.Titel,
                    Naam = model.Contactpersoon.Naam,
                    Voornaam = model.Contactpersoon.Voornaam,
                    Telefoonnummer = model.Contactpersoon.Telefoonnummer,
                    Email = model.Contactpersoon.Email
                }
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                var role = Role.Bedrijf;
                await EnsureRoleExists(role);
                await _userManager.AddToRoleAsync(user, role);
                return NoContent();
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
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var user = await _userManager.FindByNameAsync(model.Email);

            if (user == null) return StatusCode(403, "Het e-mailadres of wachtwoord is ongeldig!");

            if (!await _userManager.IsEmailConfirmedAsync(user))
            {
                return BadRequest("This account is deactivated!");
            }

            var result = await _signInManager.PasswordSignInAsync(user, model.Password, true, true);

            if (!result.Succeeded)
                return StatusCode(403,
                    !result.IsLockedOut
                        ? "Het e-mailadres of wachtwoord is ongeldig!"
                        : "Te veel incorrecte pogingen. Wacht 5 minuten en probeer later opniew.");

            var token = await CreateJwtToken(user);
            return Ok(token);
        }

        private async Task<object> CreateJwtToken(User user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var allClaims = new[]
            {
                new Claim("id", user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("voornaam", user.Voornaam ?? ""),
                new Claim("naam", user.Naam)
            }.Union(userClaims).ToList();

            var userRoles = await _userManager.GetRolesAsync(user);

            foreach (var role in userRoles)
            {
                if (role == "reviewer" && ((Reviewer)(user)).IsCoordinator.ToString() == "True")
                {
                    allClaims.Add(new Claim(ClaimTypes.Role, "coordinator"));
                }
                else
                {
                    allClaims.Add(new Claim(ClaimTypes.Role, role));
                }
            }

            var keyBytes = Encoding.UTF8.GetBytes(_tokenSettings.Value.Key);
            var symmetricSecurityKey = new SymmetricSecurityKey(keyBytes);
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _tokenSettings.Value.Issuer,
                audience: _tokenSettings.Value.Audience,
                claims: allClaims,
                expires: DateTime.UtcNow.AddMinutes(_tokenSettings.Value.ExpirationTimeInMinutes),
                signingCredentials: signingCredentials);

            var encryptedToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            return encryptedToken;
        }

        #endregion


        [HttpPost("ResetPasswordRequest")]
        public IActionResult PasswordResetRequest(ResetRequestModel resetRequest)
        {
            string email = resetRequest.Email;
            var user = _userRepository.Find(u=>u.Email==email).FirstOrDefault();

            if (user == null)
            {
                return Unauthorized();
            }

            _resetRequestRepository.Add(user);
            return Ok();
        }

        [HttpGet("CheckToken/{token}")]
        public IActionResult CheckToken(string token)
        {
            bool isValid = false;
            var request = _resetRequestRepository.Find(req => req.PasswordResetToken.ToString() == token).FirstOrDefault();
            if (request == null) return NotFound();
            if (!request.IsConsumed)
            {
                if (IsTokenValid(request))
                {
                    isValid = true;
                    return Ok(isValid);
                }
            }

            return Ok(isValid);

        }

        [HttpPatch("ResetPassword/{token}")]
        public IActionResult ResetPassword(string token, ResetPassWordModel resetPassWordModel)
        {
            var request = _resetRequestRepository.Find(req => req.PasswordResetToken.ToString() == token).FirstOrDefault();
            if (request == null) return NotFound();
            var password = resetPassWordModel.NewPassWord;
            var user = _userRepository.Find(u=>u.Email==request.Email).FirstOrDefault();
            user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, password);
            request.IsConsumed = true;
            _resetRequestRepository.Save();
            _userRepository.Save();
            return Ok();
        }
        private async Task EnsureRoleExists(string role)
        {
            if (await _roleManager.RoleExistsAsync(role)) return;
            await _roleManager.CreateAsync(new Role
            {
                Name = role,
                NormalizedName = role.ToUpper()
            });
        }

        private bool IsTokenValid(ResetPasswordRequest resetCheck)
        {
            bool valid = false;

            DateTime expiredDateTime = resetCheck.ResetRequestDateTime.AddMinutes(20);
            var compare = DateTime.Compare(DateTime.Now, expiredDateTime);
            if (compare < 0)
            {
                valid = true;
            }

            return valid;
        }
    }
}