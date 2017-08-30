using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.Content;
using Android.Views;
using Android.Widget;
using InfosNutritions.PlatformServices;

namespace InfosNutritions.Droid.AndroidServices
{
    public class DroidDeviceInfo : IDeviceInfo
    {
        public bool CheckFlashlightPermission()
        {
            Permission permissionResult =
                ContextCompat.CheckSelfPermission(Application.Context, Manifest.Permission.Flashlight);
            return permissionResult == Permission.Granted;
        }
    }
}