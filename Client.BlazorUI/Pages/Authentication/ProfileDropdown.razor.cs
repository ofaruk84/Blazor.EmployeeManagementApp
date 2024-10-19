using Client.Lib.Utilities.Authentication;

using Shared.Lib.DTOs;

namespace Client.BlazorUI.Pages.Authentication
{
    public partial class ProfileDropdown
    {
        public string ProfileImage { get; set; }

        async Task HandleLogout()
        {
            var logoutDto = new UserSessionDto();
            var customAuthProvider = (CustomAuthenticationStateProvider)AuthStateProvider;
            await customAuthProvider.UpdateAuthenticationState(logoutDto);
            NavManager.NavigateTo("/login", true);

        }
    }
}
