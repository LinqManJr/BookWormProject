using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BookWorm.Core.Models;
using BookWorm.Dto;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BookWorm.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        public AccountController(UserManager<BookUser> userManager,
                                 SignInManager<BookUser> signManager,
                                 IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
            _signManager = signManager;
        }

        private readonly UserManager<BookUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly SignInManager<BookUser> _signManager;

        [HttpPost]
        public async Task<object> Login([FromBody] LoginDto model)
        {
            var result = _signManager.PasswordSignInAsync(model.Email, model.Password, false, false);
            if (result.Result.Succeeded)
            {
                var user = _userManager.Users.SingleOrDefault(x => x.Email == model.Email);
                return await GenerateJwtToken(user.Email, user);

            }
            throw new ApplicationException("Invalid_Login_Attempt");
        }

        private Task<object> GenerateJwtToken(string email, BookUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub,email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtKey"]));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(_configuration["JwtExpieDays"]));

            var token = new JwtSecurityToken(
                _configuration["JwtIssuer"],
                _configuration["JwtIssuer"],
                claims,
                expires: expires,
                signingCredentials: cred);

            return Task.FromResult((object)new JwtSecurityTokenHandler().WriteToken(token));
        }

        [HttpPost]
        public async Task<object> Register([FromBody] RegisterDto model)
        {
            var user = new BookUser
            {
                UserName = model.Email,
                Email = model.Email
            };

            var result = _userManager.CreateAsync(user, model.Password);
            if (result.Result.Succeeded)
            {
                await _signManager.SignInAsync(user, false);
                return await GenerateJwtToken(user.Email, user);
            }
            //TODO: create Middleware that handle error registration

            throw new ApplicationException("Register_Failed");
        }
    }
}