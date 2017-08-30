using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using FreshMvvm;
using InfosNutritions.Droid.AndroidServices;
using InfosNutritions.Init.m2oScanner.Init;
using InfosNutritions.PlatformServices;

namespace InfosNutritions.Droid
{
    [Activity(Label = "Infos Nutritions", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true,ScreenOrientation = ScreenOrientation.Portrait,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);
            SetupIOC();
            global::Xamarin.Forms.Forms.Init(this, bundle);
            ZXing.Net.Mobile.Forms.Android.Platform.Init();

            LoadApplication(new App());
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            ZXing.Net.Mobile.Android.PermissionsHandler.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        private void SetupIOC()
        {
            FreshIOC.Container.Register<IDeviceInfo, DroidDeviceInfo>().AsSingleton();
            Setup.RegisterServices();
        }
    }
}

