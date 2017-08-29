using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using FreshMvvm;
using InfosNutritions.Navigation;
using InfosNutritions.Services;
using InfosNutritions.UI.Globalization;
using Plugin.Settings;
using Xamarin.Forms;

namespace InfosNutritions.UI.Screens.PageModels
{
    public class LoginPageModel : FreshBasePageModel
    {
        #region Fields
        private readonly INavigationServices _navigationService;
        private const string _trueUsername = "nepas";
        private const string _truePassword = "faireca";
        #endregion

        #region Constructor

        public LoginPageModel(INavigationServices navigationService)
        {
            _navigationService = navigationService;
        }

        #endregion

        #region properties

        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsValidateButtonActive => !string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password);
        public double ValidateButtonOpacity => IsValidateButtonActive ? 1 : 0.3;

        #endregion

        #region Commands

        public ICommand ValidateCommand => new Command(ValidateCommandExecute);

        private void ValidateCommandExecute()
        {
            if (IsValidateButtonActive)
            {
                if (Username == _trueUsername && Password == _truePassword)
                    _navigationService.NavigateToNextContainer(this);
                else
                    CoreMethods.DisplayAlert(Lang.Login_Error_Title,Lang.Login_Error_Body,Lang.Ok);
            }
        }

        #endregion
    }
}
