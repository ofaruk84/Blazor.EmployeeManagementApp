using Client.Lib.Utilities.Authentication;
using Shared.Lib.DTOs;

namespace Client.BlazorUI.Pages.Account
{
    public partial class Register
    {
        private bool showLoadingSpinner = false;
        private RegisterDto user = new RegisterDto{
            IsAdmin = false
        };

        async Task HandleRegister()
        {
            showLoadingSpinner = true;
            var result = await UserAccountService.RegisterUser(user);

            if (result is not null && result.Flag)
            {
                showLoadingSpinner = false;
                NavManager.NavigateTo("identity/account/login", true);
            }

        }
    }
}
