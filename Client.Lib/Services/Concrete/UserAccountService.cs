using Client.Lib.Services.Abstract;
using Client.Lib.Utilities;
using Client.Lib.Utilities.Http;
using Shared.Lib.DTOs;
using Shared.Lib.Entities;
using Shared.Lib.Responses;
using System.Net.Http.Json;

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

        public async Task<GeneralResponse?> CreateUserAccount(RegisterDto registerDto)
        {
            var httpClient =  _httpClientUtil.CreatePublicHttpClient();
            var result = await httpClient.PostAsJsonAsync($"{authUtl}/register",registerDto);
            
            if (!result.IsSuccessStatusCode) return new GeneralResponse(false, Message: Messages.ErrorOcured);

            return await result.Content.ReadFromJsonAsync<GeneralResponse>();

       
        }

        public async Task<LoginResponse?> RefreshToken(AppRefreshToken refreshToken)
        {
                throw new NotImplementedException();
        }

        public async Task<LoginResponse?> SignIn(LoginDto loginDto)
        {
            var httpClient = _httpClientUtil.CreatePublicHttpClient();
            var result = await httpClient.PostAsJsonAsync($"{authUtl}/register", loginDto);

            if (!result.IsSuccessStatusCode) return new LoginResponse(false, Message: Messages.ErrorOcured);

            return await result.Content.ReadFromJsonAsync<LoginResponse>();
        }
    }
}
