using Shared.Lib.DTOs;
using Shared.Lib.Entities;
using Shared.Lib.Responses;
using System;

namespace Client.Lib.Services.Abstract
{
    public  interface IUserAccountService
    {
        Task<GeneralResponse?> CreateUserAccount(RegisterDto registerDto);
        Task<LoginResponse?> SignIn(LoginDto loginDto);
        Task<LoginResponse?> RefreshToken(AppRefreshToken refreshToken);

    }
}
