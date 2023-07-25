﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using TastingClubBLL.DTOs.ApplicationUserDTOs;
using TastingClubBLL.Exceptions;
using TastingClubBLL.Interfaces.IServices;
using TastingClubDAL.Models;

namespace TastingClubBLL.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _config;

        public AuthService(UserManager<ApplicationUser> userManager,
            IConfiguration config)
        {
            _userManager = userManager;
            _config = config;
        }

        public string GenerateTokenString(ApplicationUserDtoForLogin user, IEnumerable<string> roles)
        {

            IEnumerable<Claim> claims = new List<Claim>
            {
                //new Claim(ClaimTypes.Email, user.Email),
                //roles.Contains(RoleConstants.AdminRole)?
                //new Claim(ClaimTypes.Role,  RoleConstants.AdminRole) : new Claim("isAdmin","No"),
                //new Claim(ClaimTypes.Role,  RoleConstants.UserRole)
            };
            //claims.Append(new Claim(ClaimTypes.Role, RoleConstants.UserRole));
            SecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("Jwt:Key").Value));
            SigningCredentials signingCred = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);
            SecurityToken securityToken = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(60),
                issuer: _config.GetSection("Jwt:Issuer").Value,
                audience: _config.GetSection("Jwt:Audience").Value,
                signingCredentials: signingCred);
            string tokenString = new JwtSecurityTokenHandler().WriteToken(securityToken);
            return tokenString;
        }

        public async Task<string> LoginAsync(ApplicationUserDtoForLogin user)
        {
            var appUser = await _userManager.FindByEmailAsync(user.Email);
            if (appUser == null)
            {
                throw new HttpStatusException(HttpStatusCode.BadRequest,"Wrong username or password");
            }

            if (!await _userManager.CheckPasswordAsync(appUser, user.Password))
            {
                throw new HttpStatusException(HttpStatusCode.BadRequest,"Wrong username or password");

            }

            var roles = await _userManager.GetRolesAsync(appUser);
            return GenerateTokenString(user, roles);
        }



        public async Task<bool> RegisterUserAsync(ApplicationUserDtoForLogin user)
        {
            var existingUser = _userManager.Users.FirstOrDefault(u => u.Email == user.Email);

            if (existingUser != null)
            {
                throw new DbUpdateException("User with the same email alredy exists");
            }

            var identityUser = new ApplicationUser
            {
                UserName = user.Email,
                Email = user.Email
            };
            var result = await _userManager.CreateAsync(identityUser, user.Password);
            return result.Succeeded;
        }

    }
}

