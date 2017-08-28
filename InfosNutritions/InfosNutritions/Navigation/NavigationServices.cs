using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InfosNutritions.Init.m2oScanner.Init;

namespace InfosNutritions.Navigation
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using FreshMvvm;
    namespace m2oScanner.Navigation
    {
        public class NavigationServices : INavigationServices
        {
            public void GoBack(FreshBasePageModel pageModel, object data, bool animated = true, bool modal = false)
            {
                Xamarin.Forms.Device.BeginInvokeOnMainThread(
                    () => pageModel.CoreMethods.PopPageModel(data, modal, animated));
            }
            public void NavigateToRoot(FreshBasePageModel pageModel, string containerName)
            {
                if (pageModel.CoreMethods == null)
                    pageModel.CoreMethods = new PageModelCoreMethods(pageModel.CurrentPage, pageModel);
                if (pageModel.CurrentNavigationServiceName == containerName)
                    PopToRoot(pageModel);
                else
                    Xamarin.Forms.Device.BeginInvokeOnMainThread(
                        () => pageModel.CoreMethods.SwitchOutRootNavigation(containerName));
            }

            public void PopToRoot(FreshBasePageModel pageModel)
            {
                if (pageModel.CoreMethods == null)
                    pageModel.CoreMethods = new PageModelCoreMethods(pageModel.CurrentPage, pageModel);
                Xamarin.Forms.Device.BeginInvokeOnMainThread(
                    () => pageModel.CoreMethods.PopToRoot(true));
            }

            public void NavigateToNextContainer(FreshBasePageModel pageModel, bool isAppStarted = true)
            {
                if (pageModel.CoreMethods == null)
                    pageModel.CoreMethods = new PageModelCoreMethods(pageModel.CurrentPage, pageModel);
                Xamarin.Forms.Device.BeginInvokeOnMainThread(
                    async () => NavigateToRoot(pageModel, await Setup.GetNextNavigation(isAppStarted)));
            }

            public void OnLoadDataError(Exception exception)
            {
                Debugger.Break();
            }
        }
    }

}
