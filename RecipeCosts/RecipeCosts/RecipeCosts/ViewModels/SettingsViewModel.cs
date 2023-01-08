using RecipeCosts.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace RecipeCosts.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {
        private const string KEY_USERID = "UserId";
        private const string KEY_USERNAME = "UserName";

        public ICommand LoginLogoutCommand { get; }

        public SettingsViewModel()
        {
            LoginLogoutCommand = new Command(LoginLogout);
        }

        private async void LoginLogout()
        {
            await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
        }
    }
}
