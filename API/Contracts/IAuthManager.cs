using System;
using API.Data.ModelsData;
using API.Models.Users;
using Microsoft.AspNetCore.Identity;

namespace API.Contracts;

public interface IAuthManager
{
    Task<IEnumerable<IdentityError>> Register(UserDto userDto);
    Task<AuthResponseDto> Login(LoginDto loginDto);
    Task<string> CreateRefreshToken();
    Task<AuthResponseDto> VerifyRefreshToken(AuthResponseDto request);
}
