using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.Contracts;
using API.Data.ModelsData;
using API.Models.Users;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace API.Repository;

public class AuthManager(IMapper mapper, UserManager<User> userManager, IConfiguration configuration) : IAuthManager
{
    private User user;
    private const string _loginProvider = "HotelListingApi";
    private const string _refreshTokenName = "RefreshToken";


    public async Task<string> CreateRefreshToken()
    {
        await userManager.RemoveAuthenticationTokenAsync(user, _loginProvider, 
            _refreshTokenName);
        
        var newRefreshToken = await userManager.GenerateUserTokenAsync(user,
            _loginProvider, _refreshTokenName);

        var result = await userManager.SetAuthenticationTokenAsync(user,
            _loginProvider, _refreshTokenName, newRefreshToken);

        return newRefreshToken; 
    }

    public async Task<AuthResponseDto> Login(LoginDto loginDto)
    {
        user = await userManager.FindByEmailAsync(loginDto.Email);
        var isValidUser = await userManager.CheckPasswordAsync(user, loginDto.Password);
        
        if(user == null || isValidUser == false)
        {
            return null;
        }

        var token = await GenerateToken();

        return new AuthResponseDto
        {
            Token = token,
            UserId = user.Id
        };
    }

    public async Task<IEnumerable<IdentityError>> Register(UserDto userDto)
    {
        user = mapper.Map<User>(userDto);
        user.UserName = userDto.Email;

        var result = await userManager.CreateAsync(user, userDto.Password);

        if(result.Succeeded)
        {
            await userManager.AddToRoleAsync(user, "User");
        }

        return result.Errors;
    }

    public async Task<AuthResponseDto> VerifyRefreshToken(AuthResponseDto request)
    {
        var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        var tokenContent = jwtSecurityTokenHandler.ReadJwtToken(request.Token);
        var username = tokenContent.Claims.ToList().FirstOrDefault(q => 
            q.Type == JwtRegisteredClaimNames.Email)?.Value;
        
        user = await userManager.FindByNameAsync(username);

        if(user == null || user.Id != request.UserId) return null;

        var isValidRefreshToken = await userManager.VerifyUserTokenAsync(user, 
            _loginProvider, _refreshTokenName, request.RefreshToken);

        if(isValidRefreshToken)
        {
            var token = await GenerateToken();

            return new AuthResponseDto
            {
                Token = token,
                UserId = user.Id,
                RefreshToken = await CreateRefreshToken()
            };
        }
        await userManager.UpdateSecurityStampAsync(user);

        return null;
    }

    private async Task<string> GenerateToken()
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"]));

        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var roles = await userManager.GetRolesAsync(user);
        var roleClaims = roles.Select(x => new Claim(ClaimTypes.Role, x)).ToList();
        var userClaims = await userManager.GetClaimsAsync(user);

        var claims = new List<Claim>
        {
            new (JwtRegisteredClaimNames.Sub, user.Email),
            new (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new (JwtRegisteredClaimNames.Email, user.Email),
            new ("uid", user.Id)
        }
        .Union(userClaims).Union(roleClaims);

        var token = new JwtSecurityToken(
            issuer: configuration["JwtSettings:Issuer"],
            audience: configuration["JwtSettings:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(Convert.ToInt32(
                configuration["JwtSettings:DurationInMinutes"]
            )),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
