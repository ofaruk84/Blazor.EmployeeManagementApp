using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Lib.Utilities.Authentication
{
    public static class AuthStateHelper
    {
        public static async void CheckUserAuthentication(AuthenticationState state, NavigationManager navigationManager)
        {
            if (state is null || navigationManager is null)
            {
                return;
            }

            var user = state.User;

            if (user.Identity!.IsAuthenticated)
            {
                navigationManager.NavigateTo(AppRoutes.Home);
            }
            else
            {
                navigationManager.NavigateTo(AppRoutes.Login);
            }

        }
    }
}
