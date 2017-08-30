using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InfosNutritions.Helpers;
using InfosNutritions.Navigation;
using InfosNutritions.Navigation.m2oScanner.Navigation;
using InfosNutritions.Providers;
using InfosNutritions.Services;
using InfosNutritions.UI.Globalization;
using InfosNutritions.UI.Screens.PageModels;

namespace InfosNutritions.Init
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using FreshMvvm;
    using Xamarin.Forms;

    namespace m2oScanner.Init
    {
        public static class Setup
        {
            public static void RegisterServices()
            {
                #region Interface registration

                FreshIOC.Container.Register<IDataProvider, DataProvider>();
                FreshIOC.Container.Register<INavigationServices, NavigationServices>();
                FreshIOC.Container.Register<IDataAccessServices, DataAccessServices>();
                FreshIOC.Container.Register<ISettingsServices, SettingsServices>();

                #endregion

                #region Class as singleton registration

                FreshIOC.Container.Register<MyHttpClient, MyHttpClient>();
                
                #endregion

            }

            public static Page InitNavigation()
            {
                #region Page Initialization
                var login = FreshPageModelResolver.ResolvePageModel<LoginPageModel>();
                login.Title = Lang.App_Name;
                var authContainer = new FreshNavigationContainer(login, NavigationContainerNames.AuthenticationContainer);
                var mainContainer = new FreshMasterDetailNavigationContainer(NavigationContainerNames.MainContainer);
                mainContainer.Init(Lang.Global_MenuTitle);
                mainContainer.AddPage<HomePageModel>(Lang.Home_Title);
                mainContainer.AddPage<HistoryPageModel>(Lang.History_Title);

                #endregion

                #region Landing page computing

                return authContainer;

                #endregion
            }

            public static async Task<string> GetNextNavigation(bool isAppStarted = true)
            {
                await Task.Delay(20);//todo a changer
                return NavigationContainerNames.MainContainer;
            }

        }
    }

}
