using Client.Lib.Utilities.Authentication;
using Shared.Lib.DTOs;

namespace Client.BlazorUI.Pages.Account
{
    public partial class Register
    {
        private RegisterDto user = new RegisterDto{
            IsAdmin = false
        };

        async Task HandleRegister()
        {
            var result = await UserAccountService.RegisterUser(user);

            if (result is not null && result.Flag)
            {
                NavManager.NavigateTo("identity/account/login", true);
            }

        }
    }
}
