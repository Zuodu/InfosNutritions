using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            public static void RegisterServices(bool useMock)
            {
                #region Interface registration


                #endregion

                #region Class as singleton registration


                #endregion

            }

            public static Page InitNavigation()
            {
                #region Page Initialization
                var login = FreshPageModelResolver.ResolvePageModel<LoginPageModel>();
                login.Title = Lang.Auth_Title;
                var authContainer = new FreshNavigationContainer(login, NavigationContainerNames.AuthenticationContainer);
                var mainContainer = new FreshMasterDetailNavigationContainer(NavigationContainerNames.MainContainer);
                mainContainer.Init(Lang.Global_MenuTitle);
                mainContainer.AddPage<HomePageModel>(Lang.Home_Title);
                mainContainer.AddPage<HistoryPageModel>(Lang.Lang_HistoryTitle);
                mainContainer.AddPage<TestPageModel>("Test Page");

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
