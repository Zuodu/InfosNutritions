using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InfosNutritions.Init.m2oScanner.Init;
using Xamarin.Forms;

namespace InfosNutritions
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = Setup.InitNavigation();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
