using RecipeCosts.Model;
using RecipeCosts.Model.Helpers;
using RecipeCosts.Model.Security;
using RecipeCosts.Models.Keys;
using RecipeCosts.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace RecipeCosts.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private const string KEY_USERID = "UserId";
        private const string KEY_USERNAME = "UserName";

        private bool registerLayoutVisible;

        public bool RegisterLayoutVisible
        {
            get { return registerLayoutVisible; }
            set {
                registerLayoutVisible = value;
                OnPropertyChanged();
            }
        }

        private string loginButtonText;

        public string LoginButtonText
        {
            get { return loginButtonText; }
            set {
                loginButtonText = value;
                OnPropertyChanged();
            }
        }

        private string switchLayoutButtonText;

        public string SwitchLayoutButtonText
        {
            get { return switchLayoutButtonText; }
            set { 
                switchLayoutButtonText = value;
                OnPropertyChanged();
            }
        }

        public User MyUser{ get; set; }

        private string userId;

        public string UserId
        {
            get { return userId; }
            set { userId = value; }
        }

        private string email;

        public string Email
        {
            get { return email; }
            set { 
                email = value;
                MyUser.Email = email;
                LoginCommand.ChangeCanExecute();
            }
        }

        private string userName;

        public string UserName
        {
            get { return userName; }
            set { 
                userName = value;
                MyUser.UserName = userName;
            }
        }

        private string password;

        public string Password
        {
            get { return password; }
            set { 
                password = value; 
                MyUser.Password = password;
                LoginCommand.ChangeCanExecute();
            }
        }

        private string confirmPassword;

        public string ConfirmPassword
        {
            get { return confirmPassword; }
            set { 
                confirmPassword = value;
                MyUser.ConfirmPassword = confirmPassword;
                LoginCommand.ChangeCanExecute();
            }
        }


        public Command LoginCommand { get; }
        public Command RegisterCommand { get; }
        public Command SwitchLoginModeCommand { get; set; }

        public LoginViewModel()
        {
            LoginCommand = new Command( async () => { await OnLoginClicked(); }, () => { return OnLoginClickedEnabled(); });
            SwitchLoginModeCommand = new Command(OnSwitchLoginModeClicked);

            MyUser = new User();

            RegisterLayoutVisible = false;
            LoginButtonText = "Login";
            SwitchLayoutButtonText = "Switch to Register";

            //if (Application.Current.Properties.TryGetValue(KEY_USERID, out object userIdProperty))
            //{
            //    UserId = userIdProperty.ToString();
            //}
        }

        private async Task OnLoginClicked()
        {
            var apiKeyFirebase = StringCipher.DecryptStringAes(ApiKeys.Firebase.ApiKeyEncrypted, ApiKeys.Firebase.AesKey, ApiKeys.Firebase.AesIV);

            if (RegisterLayoutVisible)
            {
                var id = await FirebaseAuthHelper.Register(MyUser, apiKeyFirebase);

            } else
            {

            }
            // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
            await Shell.Current.GoToAsync($"//{nameof(AboutPage)}");
        }

        private bool OnLoginClickedEnabled()
        {
            if (RegisterLayoutVisible)
            {
                if (MyUser == null)
                {
                    return false;
                }
                if (String.IsNullOrEmpty(MyUser.Email))
                {
                    return false;
                }
                if (String.IsNullOrEmpty(MyUser.Password))
                {
                    return false;
                }
                if (String.IsNullOrEmpty(MyUser.ConfirmPassword))
                {
                    return false;
                }
                if (!MyUser.Password.Equals(MyUser.ConfirmPassword))
                {
                    return false;
                }

                return true;
            } else
            {
                if (MyUser == null)
                {
                    return false;
                }
                if (String.IsNullOrEmpty(MyUser.Email))
                {
                    return false;
                }
                if (String.IsNullOrEmpty(MyUser.Password))
                {
                    return false;
                }

                return true;
            }
        }

        private void OnSwitchLoginModeClicked()
        {
            if (RegisterLayoutVisible)
            {
                RegisterLayoutVisible = false;
                LoginButtonText = "Login";
                SwitchLayoutButtonText = "Switch to Register";
            } else
            {
                RegisterLayoutVisible = true;
                LoginButtonText = "Register";
                SwitchLayoutButtonText = "Switch to Login";
            }

            LoginCommand.ChangeCanExecute();
        }
    }
}
