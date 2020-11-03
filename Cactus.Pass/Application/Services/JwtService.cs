using Application.Extensions;
using Application.Interfaces;
using Application.ViewModel.Account;
using Application.ViewModel.UserJwtToken;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class JwtService : IJwtService
    {
        protected readonly IUserJwtTokenRepository UserJwtTokenRepository;
        protected readonly IUserService UserService;

        protected readonly string Issuer;
        protected readonly string Audience;
        protected readonly string Key;
        protected readonly double ExpirationInMinutes;

        public JwtService(IConfiguration config, IUserJwtTokenRepository userJwtTokenRepository, IUserService userService)
        {
            UserJwtTokenRepository = userJwtTokenRepository;
            UserService = userService;

            Issuer = config["Jwt:Issuer"];
            Audience = config["Jwt:Issuer"];
            Key = config["Jwt:Key"];
            ExpirationInMinutes = double.Parse(config["Jwt:ExpirationInMinutes"]);
        }

        public virtual UserJwtTokenDto GenerateJwtToken(LoginDto userDto)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, userDto.Username),
                new Claim(JwtRegisteredClaimNames.AuthTime, DateTime.Now.ToString("MM/dd/yyyy"))
            };

            var token = new JwtSecurityToken(
                Issuer,
                Audience,
                claims,
                DateTime.Now.AddSeconds(10),
                DateTime.Now.AddMinutes(ExpirationInMinutes),
                credentials
            );

            var encodeToken = new JwtSecurityTokenHandler().WriteToken(token);

            var userJwtTokenDto = new UserJwtTokenDto
            {
                Username = userDto.Username,
                AccessToken = encodeToken,
                AccessTokenExpireDateTime = DateTime.Now.AddMinutes(ExpirationInMinutes),
                TokenPlatform = userDto.Platform
            };

            return userJwtTokenDto;
        }

        public virtual async Task<UserJwtToken> SaveJwtTokenAsync(UserJwtTokenDto userJwtTokenDto)
        {
            var accessToken = await HashToken(userJwtTokenDto.AccessToken);

            var userJwtToken = new UserJwtToken
            {
                AccessToken = accessToken,
                AccessTokenExpireDateTime = userJwtTokenDto.AccessTokenExpireDateTime,
                Platform = userJwtTokenDto.TokenPlatform,
                UserId = await UserService.GetUserIdAsync(userJwtTokenDto.Username)
            };

            return await UserJwtTokenRepository.AddAsync(userJwtToken);
        }

        public virtual async Task<UserJwtTokenDto> GenerateAndSaveJwtTokenAsync(LoginDto userDto)
        {
            var userJwtTokenDto = GenerateJwtToken(userDto);

            await SaveJwtTokenAsync(userJwtTokenDto);

            return userJwtTokenDto;
        }

        public virtual Task<string> HashToken(string token) => Task.FromResult(token.EncryptMd5());

        public virtual async Task<bool> IsExistTokenAsync(string token)
        {
            var accessToken = await HashToken(token);

            return await UserJwtTokenRepository.ExistsAsync(u => u.AccessToken == accessToken);
        }

        public virtual async Task<UserJwtToken> GetJwtTokenAsync(string token)
        {
            var accessToken = await HashToken(token);

            var userJwtToken = await UserJwtTokenRepository.FirstOrDefaultAsync(u => u.AccessToken == accessToken);

            return userJwtToken;
        }

        public virtual async Task DeleteJwtTokenAsync(UserJwtToken userJwtToken)
            => await UserJwtTokenRepository.DeleteAsync(userJwtToken);

        public virtual async Task DeleteJwtTokenAsync(string token)
        {
            var userJwtToken = await GetJwtTokenAsync(token);
            await DeleteJwtTokenAsync(userJwtToken);
        }
    }
}
