using findU.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;
using Map = Xamarin.Forms.Maps.Map;

namespace findU.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class locationPage : ContentPage
    {
        FirebaseHelper firebaseHelper = new FirebaseHelper();
        Location location = null;

        bool mapVisible = false;
        public locationPage()
        {
            InitializeComponent();
            map.IsShowingUser = true;
        }

        private async Task  getFriendLocation(int UserId)
        {
            try
            {
                Person FriendDetails = await firebaseHelper.GetPerson(UserId);

                if(FriendDetails!=null && FriendDetails.Longitude!=null && FriendDetails.Latitude != null)
                {
                    Position position = new Position(Convert.ToDouble(FriendDetails.Latitude), Convert.ToDouble(FriendDetails.Longitude));

                    MapSpan mapSpan = MapSpan.FromCenterAndRadius(position, Distance.FromKilometers(0.444));
                    map.MoveToRegion(mapSpan);

                    Pin pin = new Pin
                    {
                        Label = "User 10",
                        Address = "The city with a boardwalk",
                        Type = PinType.Place,
                        Position = position
                    };
                    map.Pins.Add(pin);


                    txtLastUpdatedTime.Text =  !string.IsNullOrEmpty(FriendDetails.LastUpdatedTime) ? FriendDetails.LastUpdatedTime.ToString() : "";

                    txtLastUpdatedTime.Text = "Last updated time" + txtLastUpdatedTime.Text;
                }
            }
            catch (Exception)
            {

                return;
            }
            
        }

        private async void GetSendLocation(int RemoteUserID)
        {
            while (1 == 1 && mapVisible)
            {
                try
                {
                    await getFriendLocation(RemoteUserID);

                    await Task.Delay(20000);
                }
                
                catch (Exception ex)
                {
                    // Unable to get location
                }
            }
        }

        protected async override void OnAppearing()
        {
            mapVisible = true;
            GetSendLocation(10);
            base.OnAppearing();
            //var allPersons = await firebaseHelper.GetAllPersons();
            //lstPersons.ItemsSource = allPersons;
        }

        protected override void OnDisappearing()
        {
            mapVisible = false;
            base.OnDisappearing();
        }





    }
}