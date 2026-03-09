using EGI_Backend.Application.Contracts.Auth;
using EGI_Backend.Application.Interfaces;
using EGI_Backend.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using JwtClaim = System.Security.Claims.Claim;
using ClaimTypes = System.Security.Claims.ClaimTypes;

namespace EGI_Backend.Infrastructure.Services
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly IConfiguration _config;

        public JwtTokenGenerator(IConfiguration config)
        {
            _config = config;
        }

        public AuthResponse GenerateToken(User user)
        {
            var claims = new List<JwtClaim>
            {
                new JwtClaim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new JwtClaim(ClaimTypes.Email, user.Email),
                new JwtClaim(ClaimTypes.Role, user.Role.ToString()),
                new JwtClaim(ClaimTypes.Name, user.Name ?? "")
            };

            var expiry = DateTime.UtcNow.AddMinutes(
                Convert.ToDouble(_config["JwtSettings:DurationInMinutes"]));

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_config["JwtSettings:Key"]!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["JwtSettings:Issuer"],
                audience: _config["JwtSettings:Audience"],
                claims: claims,
                expires: expiry,
                signingCredentials: creds
            );

            return new AuthResponse
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiry,
                RequirePasswordChange = user.MustChangePassword
            };
        }
    }
}
