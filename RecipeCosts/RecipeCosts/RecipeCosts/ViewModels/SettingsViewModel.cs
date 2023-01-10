using RecipeCosts.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace RecipeCosts.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {
        private const string KEY_USERID = "UserId";
        private const string KEY_USERNAME = "UserName";

        private string loginButtonText;

        public string LoginButtonText
        {
            get { return loginButtonText; }
            set { 
                loginButtonText = value; 
                OnPropertyChanged();
            }
        }


        private string userId;

        public string UserId
        {
            get { return userId; }
            set { 
                userId = value;
                OnPropertyChanged();
            }
        }


        public ICommand LoginLogoutCommand { get; }

        public SettingsViewModel()
        {
            LoginLogoutCommand = new Command(OnLoginLogoutClicked);

            Init();
        }

        public void Init()
        {
            if (Preferences.ContainsKey("UserId"))
            {
                UserId = Preferences.Get("UserId", "Not found");
                LoginButtonText = "Logout";
            }
            else
            {
                LoginButtonText = "Login";
            }
        }

        private async void OnLoginLogoutClicked()
        {
            if (LoginButtonText.Equals("Login"))
            {
                await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
            } else
            {
                Preferences.Remove("UserId");
                UserId = "";
                LoginButtonText = "Login";
            }
        }
    }
}
