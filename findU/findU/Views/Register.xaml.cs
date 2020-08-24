using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace findU.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Register : ContentPage
    {

        FirebaseHelper firebaseHelper = new FirebaseHelper();
        public Register()
        {
            InitializeComponent();
        }

        private async void btnRegister_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtUseName.Text))
            {
                await DisplayAlert("", "Invalid name", "Ok");
                return;
            }

            bool res = await firebaseHelper.checkExistingUser(txtUseName.Text.ToString());
            if (res)
            {
                await DisplayAlert("", "User Already registered", "Ok");
            }
            else
            {
                int userId = await firebaseHelper.GetMaxUserID();

                await firebaseHelper.AddPerson((userId+1), txtUseName.Text.ToString(), true, "", "", "");
                
                Preferences.Set("isRegistered", true);
                Application.Current.MainPage = new AppShell();
                
                
            }
        }


    }
}