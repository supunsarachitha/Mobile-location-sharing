using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using findU.Services;
using findU.Views;
using Xamarin.Essentials;

namespace findU
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();



            bool RES = Preferences.Get("isRegistered", false);
            if (RES)
            {
                MainPage = new AppShell();
            }
            else
            {
                MainPage = new Register();
            }

            
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
