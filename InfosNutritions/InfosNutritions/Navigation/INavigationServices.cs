using System;
using FreshMvvm;

namespace InfosNutritions.Navigation
{
    public interface INavigationServices
    {
        void GoBack(FreshBasePageModel pageModel, object data, bool animated = true, bool modal = false);
        void NavigateToRoot(FreshBasePageModel pageModel, string containerName);
        void PopToRoot(FreshBasePageModel pageModel);
        void NavigateToNextContainer(FreshBasePageModel pageModel, bool isAppStarted = true);
    }
}