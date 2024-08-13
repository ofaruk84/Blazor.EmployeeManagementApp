using Shared.Lib.DTOs;
using Shared.Lib.Entities;
using Shared.Lib.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Business.Abstract
{
    public interface IUserService
    {
        Task<GeneralResponse> CreateUser(RegisterDto registerDto);
        Task<LoginResponse> SignIn(LoginDto loginDto);
        Task<LoginResponse> RefreshToken(AppRefreshToken refreshToken);

    }
}
