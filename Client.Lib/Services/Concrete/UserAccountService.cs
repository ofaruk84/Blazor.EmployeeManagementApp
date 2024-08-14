using Client.Lib.Services.Abstract;
using Client.Lib.Utilities.Http;
using Shared.Lib.DTOs;
using Shared.Lib.Entities;
using Shared.Lib.Responses;

namespace Client.Lib.Services.Concrete
{
    public class UserAccountService : IUserAccountService
    {
        private readonly IHttpClientUtil _httpClientUtil;
        private readonly string authUtl = "api/authentication";

        public UserAccountService(IHttpClientUtil httpClientUtil)
        {
            _httpClientUtil = httpClientUtil;
        }

        public Task<GeneralResponse> CreateUserAccount(RegisterDto registerDto)
        {
            throw new NotImplementedException();
        }

        public Task<LoginResponse> RefreshToken(AppRefreshToken refreshToken)
        {
            throw new NotImplementedException();
        }

        public Task<LoginResponse> SignIn(LoginDto loginDto)
        {
            throw new NotImplementedException();
        }
    }
}
