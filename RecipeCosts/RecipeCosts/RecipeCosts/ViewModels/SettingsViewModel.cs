using Newtonsoft.Json;
using RecipeCosts.Model;
using RecipeCosts.Models;
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

        private string userName;

        public string UserName
        {
            get { return userName; }
            set { 
                userName = value;
                OnPropertyChanged();
            }
        }

        public User AppUser { get; set; }

        public ICommand LoginLogoutCommand { get; }

        public SettingsViewModel()
        {
            LoginLogoutCommand = new Command(OnLoginLogoutClicked);

            Init();
        }

        public void Init()
        {
            if (Preferences.ContainsKey(PreferenceKeys.PREF_CURRENT_APP_USER))
            {
                var userAsJson = Preferences.Get(PreferenceKeys.PREF_CURRENT_APP_USER, "Not found");

                try
                {
                    AppUser = JsonConvert.DeserializeObject<User>(userAsJson);
                    UserName = AppUser.UserName;

                    LoginButtonText = "Logout";
                }
                catch (Exception)
                {
                    UserName = "";
                    LoginButtonText = "Login";
                    return;
                }

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
                await Shell.Current.GoToAsync($"{nameof(LoginPage)}");
            } else
            {
                Preferences.Remove(PreferenceKeys.PREF_CURRENT_APP_USER_ID);
                Preferences.Remove(PreferenceKeys.PREF_CURRENT_APP_USER);
                UserName = "";
                LoginButtonText = "Login";
            }
        }
    }
}
