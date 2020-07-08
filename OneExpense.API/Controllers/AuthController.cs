using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using OneExpense.API.Controllers;
using OneExpense.API.Extensions;
using OneExpense.API.Interfaces;
using OneExpense.API.ViewModel;
using OneExpense.Business.Events;
using OneExpense.Business.Interfaces;
using OneExpense.Business.Mediator;
using OneExpense.Business.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace OneExpenseAuth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : MainController
    {
        private readonly SignInManager<CompanyUser> _signInManager;
        private readonly UserManager<CompanyUser> _userManager;
        private readonly AppSettings _appSettings;
        private readonly IMediatorHandler _mediatorHandler;

        public AuthController(INotifier notifier,
                              SignInManager<CompanyUser> signInManager,
                              UserManager<CompanyUser> userManager,
                              IOptions<AppSettings> appSettings,
                              ICompanyUserService appUser,
                              IMediatorHandler mediatorHandler) : base(appUser, notifier)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _appSettings = appSettings.Value;
            _mediatorHandler = mediatorHandler;
        }

        [HttpPost("Create")]
        public async Task<ActionResult> Create(UserRecord userRecord)
        {
            var user = new CompanyUser()
            {
                UserName = userRecord.Email,
                Email = userRecord.Email,
                EmailConfirmed = true,
                CompanyId = userRecord.CompanyId
            };

            var result = await _userManager.CreateAsync(user, userRecord.Password);
            _mediatorHandler.PublishEvent(new CompanyUserRegisteredEvent(userRecord.CompanyId, userRecord.Email, userRecord.Password, userRecord.PasswordConfirmation));

            if (result.Succeeded)
            {
                return ApiResponse();
            }

            foreach (var error in result.Errors)
            {
                AddError("User", error.Description);
            }

            return ApiResponse();
        }

        [HttpPost("Login")]
        public async Task<ActionResult> Login(UserLogin userLogin)
        {
            var result = await _signInManager.PasswordSignInAsync(userLogin.Email, userLogin.Password, false, false);

            if (result.Succeeded)
            {
                return ApiResponse(await CreateJWT(userLogin.Email));
            }

            AddError("User", "User or password incorrect");

            return ApiResponse();
        }

        private async Task<UserLoginResponse> CreateJWT(string email)
        {
            var user = await _userManager.FindByNameAsync(email);
            var claims = await _userManager.GetClaimsAsync(user);

            var claimsIdentity = await GetUserClaims(claims, user);
            var encodedToken = EncodeToken(claimsIdentity);

            return GetUserTokenResponse(encodedToken, user, claims);
        }

        private async Task<ClaimsIdentity> GetUserClaims(ICollection<Claim> claims, CompanyUser user)
        {
            var userRoles = await _userManager.GetRolesAsync(user);

            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.UtcNow).ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64));

            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim("role", userRole));
            }

            var claimsIdentity = new ClaimsIdentity();
            claimsIdentity.AddClaims(claims);

            return claimsIdentity;
        }

        private string EncodeToken(ClaimsIdentity claimsIdentity)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _appSettings.Issuer,
                Audience = _appSettings.ValidIn,
                Subject = claimsIdentity,
                Expires = DateTime.UtcNow.AddHours(_appSettings.ExpirationHours),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            });

            var encodedToken = tokenHandler.WriteToken(token);

            return encodedToken;
        }

        private UserLoginResponse GetUserTokenResponse(string encodedToken, CompanyUser user, IEnumerable<Claim> claims)
        {
            return new UserLoginResponse
            {
                AccessToken = encodedToken,
                ExpiresIn = TimeSpan.FromHours(_appSettings.ExpirationHours).TotalSeconds,
                UserToken = new UserToken
                {
                    Id = user.Id,
                    Email = user.Email,
                    Claims = claims.Select(c => new UserClaim { Type = c.Type, Value = c.Value })
                }
            };
        }

        private static long ToUnixEpochDate(DateTime date)
            => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);
    }
}