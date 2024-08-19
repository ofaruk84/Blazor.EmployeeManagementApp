
using Client.Lib.Utilities.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Shared.Lib.DTOs;
using System.Security.Claims;

namespace Client.Lib.Utilities.Authentication
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly IAppLocalStorageService _localStorageService;
        private readonly ClaimsPrincipal _anonymus = new(new ClaimsIdentity());
        private readonly string key = Constants.authKey;
        public CustomAuthenticationStateProvider(IAppLocalStorageService localStorageService)
        {
            _localStorageService = localStorageService;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {

            var stringToken = await _localStorageService.GetItem(key);
            
            if (string.IsNullOrEmpty(stringToken)) return await Task.FromResult(new AuthenticationState(_anonymus));

            var authToken = SerializationsUtil.DeserializeObj<UserSessionDto>(stringToken);
            if(authToken is null) return await Task.FromResult(new AuthenticationState(_anonymus));

            var userClaims = JWTHelper.DecryptToken(authToken.Token!);
            if (userClaims is null) return await Task.FromResult(new AuthenticationState(_anonymus));

            var claimsPrincipals = JWTHelper.SetClaimsPrincipal(userClaims);
            if (userClaims is null) return await Task.FromResult(new AuthenticationState(_anonymus));


            return await Task.FromResult(new AuthenticationState(claimsPrincipals));

        }

        public async Task UpdateAuthenticationState(UserSessionDto userSession)
        {
            var claimsPrincipal = new ClaimsPrincipal();

            if (userSession.Token is not null || userSession.RefreshToken is not null)
            {
                try
                {
                    var authToken = SerializationsUtil.SerializeObj(userSession);
                    await _localStorageService.SetItem(key, authToken!);
                    var userClaims = JWTHelper.DecryptToken(userSession.Token!);
                    claimsPrincipal = JWTHelper.SetClaimsPrincipal(userClaims);
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine(ex);
                    return;
                }

            }
            else
            {
                await _localStorageService.RemoveItem(key);
            }

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
        }
    }
}
